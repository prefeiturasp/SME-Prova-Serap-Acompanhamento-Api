﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvaQuery : IRequest<ResumoGeralDto>
    {
        public ObterResumoGeralProvaQuery(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId, int numeroPagina, int numeroRegistros)
        {
            Filtro = filtro;
            DresId = dresId;
            UesId = uesId;
            TurmasId = turmasId;
            NumeroPagina = numeroPagina > 0 ? numeroPagina : 1;
            NumeroRegistros = numeroRegistros > 0 ? numeroRegistros : 10;
        }

        public FiltroDto Filtro { get; set; }
        public long[] DresId { get; set; }
        public long[] UesId { get; set; }
        public long[] TurmasId { get; set; }
        public int NumeroPagina { get; set; }
        public int NumeroRegistros { get; set; }
    }
}
