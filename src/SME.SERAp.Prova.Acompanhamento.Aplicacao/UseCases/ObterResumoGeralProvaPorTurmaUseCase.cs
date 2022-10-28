using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorTurma;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterTurmasPorUe;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterResumoGeralProvaPorTurmaUseCase : AbstractUseCase, IObterResumoGeralProvaPorTurmaUseCase
    {
        public ObterResumoGeralProvaPorTurmaUseCase(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<ResumoGeralUnidadeDto>> Executar(FiltroDto filtro, long ueId, long provaId)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            var turmasPorUe = await mediator.Send(new ObterTurmasPorUeQuery(filtro.AnoLetivo, ueId));

            var listaTurmasResumo = new List<Turma>();
            //foreach (var turmaId in turmasId)
            //{
            //    var turmaExistente = turmasPorUe.Where(x => x.Id == turmaId.ToString()).FirstOrDefault();
            //    if (turmaExistente != null)
            //        listaTurmasResumo.Add(turmaExistente);
            //}


            var listaResumoGeralTurma = new List<ResumoGeralUnidadeDto>();


            foreach (var turma in turmasPorUe)
            {

                var retornoResumoGeralProva = await mediator.Send(new ObterResumoGeralProvaPorTurmaQuery(filtro, long.Parse(turma.Id), provaId, dresId, uesId, turmasId));
                if (retornoResumoGeralProva != null && retornoResumoGeralProva.TotalAlunos > 0)
                {
                    var resumoGeralTurma = new ResumoGeralUnidadeDto();
                    resumoGeralTurma.Id = long.Parse(turma.Id);
                    resumoGeralTurma.Nome = turma.Nome;
                    resumoGeralTurma.Item = retornoResumoGeralProva;
                    listaResumoGeralTurma.Add(resumoGeralTurma);
                }
            }

            return listaResumoGeralTurma;
        }
    }
}