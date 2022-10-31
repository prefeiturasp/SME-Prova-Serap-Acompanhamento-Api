using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaPorDreQuery : IRequest<ResumoGeralProvaDto>
    {
        public ObterResumoGeralProvaPorDreQuery(FiltroDto filtro, long dreId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId, int numeroPagina, int numeroRegistros)
        {
            Filtro = filtro;
            DresAbrangenciaId = dresAbrangenciaId;
            UesAbrangenciaId = uesAbrangenciaId;
            TurmasAbrangenciaId = turmasAbrangenciaId;
            DreId = dreId;
            ProvaId = provaId;
            NumeroPagina = numeroPagina > 0 ? numeroPagina : 1;
            NumeroRegistros = numeroRegistros > 0 ? numeroRegistros : 10;
        }

        public FiltroDto Filtro { get; set; }
        public long[] DresAbrangenciaId { get; set; }
        public long[] UesAbrangenciaId { get; set; }
        public long[] TurmasAbrangenciaId { get; set; }
        public long DreId { get; set; }
        public long ProvaId { get; set; }
        public int NumeroPagina { get; set; }
        public int NumeroRegistros { get; set; }
    }
}