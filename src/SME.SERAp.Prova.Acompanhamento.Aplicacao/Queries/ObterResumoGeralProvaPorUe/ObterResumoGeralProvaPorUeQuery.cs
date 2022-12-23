using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorUe
{
    public class ObterResumoGeralProvaPorUeQuery : IRequest<ResumoGeralProvaDto>
    {
        public ObterResumoGeralProvaPorUeQuery(FiltroDto filtro, long ueId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId)
        {
            Filtro = filtro;
            DresAbrangenciaId = dresAbrangenciaId;
            UesAbrangenciaId = uesAbrangenciaId;
            TurmasAbrangenciaId = turmasAbrangenciaId;
            UeId = ueId;
            ProvaId = provaId;

        }

        public FiltroDto Filtro { get; set; }
        public long[] DresAbrangenciaId { get; set; }
        public long[] UesAbrangenciaId { get; set; }
        public long[] TurmasAbrangenciaId { get; set; }
        public long UeId { get; set; }
        public long ProvaId { get; set; }
    }
}