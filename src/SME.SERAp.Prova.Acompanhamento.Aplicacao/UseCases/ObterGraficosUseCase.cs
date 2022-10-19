using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterGraficosUseCase : AbstractUseCase, IObterGraficosUseCase
    {
        public ObterGraficosUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<GraficosDto> Executar(FiltroDto filtro)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());

            var resumoGeral = await mediator.Send(new ObterResumoGeralProvaQuery(filtro, dresId, uesId, turmasId, filtro.NumeroPagina, filtro.NumeroRegistros));

            var graficos = new GraficosDto();
            graficos.TotalProvasVsIniciadas = new List<GraficoItemDto>();
            graficos.TotalProvasVsFinalizadas = new List<GraficoItemDto>();
            graficos.QuestoesPrevistasVsQuestoesRespondidas = new List<GraficoItemDto>();
            graficos.ProvaVSTempoMedio = new List<GraficoItemDto>();

            foreach (var provaResumo in resumoGeral.Items)
            {
                var totalProvas = new GraficoItemDto
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Total de Provas",
                    Valor = provaResumo.TotalAlunos
                };

                graficos.TotalProvasVsIniciadas.Add(totalProvas);
                graficos.TotalProvasVsIniciadas.Add(new GraficoItemDto
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Total Iniciadas",
                    Valor = provaResumo.ProvasIniciadas + provaResumo.ProvasNaoFinalizadas + provaResumo.ProvasFinalizadas
                });

                graficos.TotalProvasVsFinalizadas.Add(totalProvas);
                graficos.TotalProvasVsFinalizadas.Add(new GraficoItemDto()
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Total Finalizadas",
                    Valor = provaResumo.ProvasFinalizadas
                });

                graficos.QuestoesPrevistasVsQuestoesRespondidas.Add(new GraficoItemDto()
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Questões Previstas",
                    Valor = Convert.ToInt64(provaResumo.DetalheProva.TotalQuestoes),
                });
                graficos.QuestoesPrevistasVsQuestoesRespondidas.Add(new GraficoItemDto()
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Questões Respondidas",
                    Valor = Convert.ToInt64(provaResumo.DetalheProva.Respondidas)
                });

                graficos.ProvaVSTempoMedio.Add(new GraficoItemDto()
                {
                    Descricao = provaResumo.TituloProva,
                    Tipo = "Tempo Médio",
                    Valor = provaResumo.TempoMedio
                });
            }

            return graficos;
        }
    }
}
