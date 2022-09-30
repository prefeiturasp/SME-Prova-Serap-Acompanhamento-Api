using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterTurmasUseCase : AbstractUseCase, IObterTurmasUseCase
    {
        public ObterTurmasUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, long ueId, Modalidade modalidade, string ano)
        {
            var turmaIds = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            var turmas = await mediator.Send(new ObterTurmasQuery(anoLetivo, ueId, modalidade, ano, turmaIds));
            if (turmas == null && !turmas.Any()) return default;

            return turmas.Select(s => TratarTurma(s)).OrderBy(o => o.Descricao);
        }

        private static SelecioneDto TratarTurma(Turma turma)
        {
            string valor = turma.Id;
            string descricao = $"{turma.Modalidade} - {turma.Nome} - {turma.Turno.Descricao()}";

            if (turma.Modalidade == Modalidade.EJA)
                descricao += $" - {turma.SerieEnsino} - Semestre {turma.Semestre}";

            return new SelecioneDto(valor, descricao);
        }
    }
}
