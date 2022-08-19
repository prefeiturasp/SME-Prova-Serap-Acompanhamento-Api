using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dados;
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
            var listaTotais = new List<TotalDto>();

            var totalProvas = await repositorioProvaTurmaResultado.ObterTotalProvasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Total de Provas", "#1B80D4", totalProvas.ToString()));

            var totalIniciadas = await repositorioProvaTurmaResultado.ObterTotalProvasIniciadasHojePorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Iniciadas hoje", "#198459", totalIniciadas.ToString()));

            var totalNaoFinalizadas = await repositorioProvaTurmaResultado.ObterTotalProvasNaoFinalizadasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Não Finalizadas", "#B40C02", totalNaoFinalizadas.ToString()));

            var totalFinalizadas = await repositorioProvaTurmaResultado.ObterTotalProvasFinalizadasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Finalizadas", "#198459", totalFinalizadas.ToString()));

            var percentualRealizado = totalFinalizadas > 0 ? (totalFinalizadas * 100) / totalProvas : 0;
            listaTotais.Add(new TotalDto("Percentual Realizado", "#1B80D4", $"{percentualRealizado:N2}%"));

            return listaTotais;
        }
    }
}
