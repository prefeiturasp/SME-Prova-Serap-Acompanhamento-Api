using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoFiltroQuery : IRequest<IEnumerable<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoFiltroQuery(FiltroDto filtro)
        {
            Filtro = filtro;
        }
        public FiltroDto Filtro { get; set; }
    }
}