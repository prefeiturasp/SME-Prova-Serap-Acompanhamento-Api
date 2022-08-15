using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{

    public class ObterTotaisProvasUseCase : AbstractUseCase, IObterTotaisProvasUseCase
    {
        public ObterTotaisProvasUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<List<TotalDto>> Executar(FiltroDto filtro)
        {
            var listaTotais = new List<TotalDto>();
            var provasAluno  = await mediator.Send(new ObterProvaAlunoResultadoFiltroQuery(filtro));
            
            if (filtro.ProvasId.Any())
                provasAluno = provasAluno.Where(x => filtro.ProvasId.Any(p => p == x.ProvaId)).ToList();

            if (provasAluno == null || !provasAluno.Any())
                return listaTotais;
         
            var totalProvas = provasAluno.ToList().Count;
            var retornoTotalAluno = new TotalDto("Total de Provas", "#1B80D4", totalProvas.ToString());

           var totalIniciadas = provasAluno.Where(x => x.AlunoInicio != null).Where(x => x.AlunoInicio.Value.Day == DateTime.Now.Day && x.AlunoFim == null).ToList().Count; 

            var retornoTotalIniciadas = new TotalDto("Provas Iniciadas", "#198459", totalIniciadas.ToString());
            
            var totalNaoFinalizadas = provasAluno.Where(x =>  x.AlunoFim ==  null).ToList().Count;
            var retornoTotalNaoFinalizadas = new TotalDto("Provas Não Finalizadas", "#B40C02", totalNaoFinalizadas.ToString());
           
            var totalFinalizadas = provasAluno.Where(x => x.AlunoFim != null).ToList().Count;
            var retornoTotalFinalizadas = new TotalDto("Provas Finalizadas", "#198459", totalFinalizadas.ToString());
       
            var percentualRealizado = (totalFinalizadas * 100) / totalProvas;
            var retornoPercentualRealizado = new TotalDto("Percentual Realizado", "#1B80D4", $"{percentualRealizado}%");

          
            listaTotais.Add(retornoTotalAluno);
            listaTotais.Add(retornoTotalIniciadas);
            listaTotais.Add(retornoTotalNaoFinalizadas);
            listaTotais.Add(retornoTotalFinalizadas);
            listaTotais.Add(retornoPercentualRealizado);

            return listaTotais;

        }
    }
}
