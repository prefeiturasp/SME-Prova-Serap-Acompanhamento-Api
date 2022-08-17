using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{

    public class ObterTotaisProvasUseCase : AbstractUseCase, IObterTotaisProvasUseCase
    {

        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;
        public ObterTotaisProvasUseCase(IMediator mediator, IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado) : base(mediator)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<List<TotalDto>> Executar(FiltroDto filtro)
        {
            var listaTotais = new List<TotalDto>();

            var totalProvas = await repositorioProvaAlunoResultado.ObterTotalProvasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Total de Provas", "#1B80D4", totalProvas.ToString()));

            var totalIniciadas = await repositorioProvaAlunoResultado.ObterTotalProvasIniciadasHojePorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Iniciadas hoje", "#198459", totalIniciadas.ToString()));

            var totalNaoFinalizadas = await repositorioProvaAlunoResultado.ObterTotalProvasNaoFinalizadasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Não Finalizadas", "#B40C02", totalNaoFinalizadas.ToString()));

            var totalFinalizadas = await repositorioProvaAlunoResultado.ObterTotalProvasFinalizadasPorFiltroAsync(filtro);
            listaTotais.Add(new TotalDto("Provas Finalizadas", "#198459", totalFinalizadas.ToString()));

            var percentualRealizado = (totalFinalizadas * 100) / totalProvas;
            listaTotais.Add(new TotalDto("Percentual Realizado", "#1B80D4", $"{percentualRealizado}%"));

            return listaTotais;
        }
    }
}
