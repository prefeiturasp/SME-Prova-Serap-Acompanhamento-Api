using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
  public  class ObterAbrangenciaUsuarioLogadoPorClaimsQuery : IRequest<IEnumerable<ParametroDto>>
    {
        public ObterAbrangenciaUsuarioLogadoPorClaimsQuery(params string[] claims)
        {
            Claims = claims;
        }

        public string[] Claims { get; set; }
    }
}