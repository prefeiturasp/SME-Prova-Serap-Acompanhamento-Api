using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{

    public class ObterTotaisProvasUseCase : AbstractUseCase, IObterTotaisProvasUseCase
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterTotaisProvasUseCase(IMediator mediator, IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado) : base(mediator)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<List<TotalDto>> Executar(FiltroDto filtro)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());

            var listaTotais = new List<TotalDto>();
            var totalProvas = await repositorioProvaTurmaResultado.ObterTotalProvasPorFiltroAsync(filtro, dresId, uesId, turmasId);
            listaTotais.Add(new TotalDto("Total de Provas", "#1B80D4", totalProvas.ToString()));

            var totalIniciadas = await repositorioProvaTurmaResultado.ObterTotalProvasIniciadasHojePorFiltroAsync(filtro, dresId, uesId, turmasId);
            listaTotais.Add(new TotalDto("Provas Iniciadas hoje", "#198459", totalIniciadas.ToString()));

            var totalNaoFinalizadas = await repositorioProvaTurmaResultado.ObterTotalProvasNaoFinalizadasPorFiltroAsync(filtro, dresId, uesId, turmasId);
            var provasNaoFinalizadas = new TotalDto("Provas Não Finalizadas", "#B40C02", totalNaoFinalizadas.ToString());
            provasNaoFinalizadas.Tooltip = "Provas iniciadas em dias anteriores e não finalizadas.";
            listaTotais.Add(provasNaoFinalizadas);

            var totalFinalizadas = await repositorioProvaTurmaResultado.ObterTotalProvasFinalizadasPorFiltroAsync(filtro, dresId, uesId, turmasId);
            listaTotais.Add(new TotalDto("Provas Finalizadas", "#198459", totalFinalizadas.ToString()));

            var percentualRealizado = totalFinalizadas > 0 ? (totalFinalizadas * 100) / totalProvas : 0;
            listaTotais.Add(new TotalDto("Percentual Realizado", "#1B80D4", $"{percentualRealizado:N2}%"));

            return listaTotais;
        }
    }
}
