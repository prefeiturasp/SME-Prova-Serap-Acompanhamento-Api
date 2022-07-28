using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterInformacoesPorTokenJwtQuery : IRequest<IEnumerable<Abrangencia>>
    {
        public ObterInformacoesPorTokenJwtQuery(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
