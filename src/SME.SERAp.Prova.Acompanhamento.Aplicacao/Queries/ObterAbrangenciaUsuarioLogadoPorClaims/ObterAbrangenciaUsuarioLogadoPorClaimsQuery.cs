using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
    public class ObterAbrangenciaUsuarioLogadoPorClaimsQuery : IRequest<IEnumerable<ParametroDto>>
    {
        public ObterAbrangenciaUsuarioLogadoPorClaimsQuery(params string[] claims)
        {
            Claims = claims;
        }

        public string[] Claims { get; set; }
    }
}