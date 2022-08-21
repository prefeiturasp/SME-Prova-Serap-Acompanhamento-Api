using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery : IRequest<IEnumerable<Abrangencia>>
    {
        public ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
