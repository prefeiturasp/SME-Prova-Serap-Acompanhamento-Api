﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterAlunosProvaTurmaUseCase : AbstractUseCase, IObterAlunosProvaTurmaUseCase
    {
        public ObterAlunosProvaTurmaUseCase(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<AlunoTurmaDto>> Executar(long provaId, long turmaId)
        {
            bool podeReabrir = await VerificaSeProvaPodeSerReaberta(provaId);
            var listaAlunosProva = await mediator.Send(new ObterAlunosProvaTurmaQuery(provaId, turmaId));

            if (listaAlunosProva == null || !listaAlunosProva.Any()) return default;

            listaAlunosProva = listaAlunosProva.ToList();

            foreach (var alunoProva in listaAlunosProva)
            {
                alunoProva.UltimaReabertura = await VerificaInformacaoUltimaReabertura(alunoProva);
                alunoProva.PodeReabrirProva = alunoProva.FimProva != null && podeReabrir ? true : false;

            }
            return listaAlunosProva.OrderBy(t => t.NomeEstudante);
        }

        private async Task<string> VerificaInformacaoUltimaReabertura(AlunoTurmaDto alunoProva)
        {
            if (alunoProva.UsurioCoressoUltimaReabertura != null)
            {
                var abrangencia = await mediator.Send(new ObterNomeUsuarioPorIdCoressoQuery(alunoProva.UsurioCoressoUltimaReabertura));
                return $"{abrangencia.Usuario} -  {alunoProva.DataUltimaReabertura}";
            }

            return string.Empty;
        }

        private async Task<bool> VerificaSeProvaPodeSerReaberta(long provaId)
        {
            var claims = await mediator.Send(new ObterAbrangenciaUsuarioLogadoPorClaimsQuery("PERMITEALTERAR"));
            var permiteAlterar = claims.FirstOrDefault(a => a.Chave == "PERMITEALTERAR")?.Valor;
            var prova = await mediator.Send(new ObterProvaPorIdQuery(provaId)); //tratar se prova null 
            var periodoProva = prova.Inicio <= DateTime.Now.Date && prova.Fim >= DateTime.Now.Date;
            var podeReabrir = periodoProva && permiteAlterar != null;
            return podeReabrir;
        }
    }
}
