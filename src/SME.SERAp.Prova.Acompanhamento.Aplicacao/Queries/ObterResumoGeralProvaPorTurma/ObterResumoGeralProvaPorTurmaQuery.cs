using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorTurma
{
  public  class ObterResumoGeralProvaPorTurmaQuery : IRequest<ResumoGeralProvaDto>
    {
        public ObterResumoGeralProvaPorTurmaQuery(FiltroDto filtro, long turmaId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId)
        {
            Filtro = filtro;
            DresAbrangenciaId = dresAbrangenciaId;
            UesAbrangenciaId = uesAbrangenciaId;
            TurmasAbrangenciaId = turmasAbrangenciaId;
            TurmaId = turmaId;
            ProvaId = provaId;

        }

        public FiltroDto Filtro { get; set; }
        public long[] DresAbrangenciaId { get; set; }
        public long[] UesAbrangenciaId { get; set; }
        public long[] TurmasAbrangenciaId { get; set; }
        public long TurmaId { get; set; }
        public long ProvaId { get; set; }
    }
}