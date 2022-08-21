using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasQuery : IRequest<IEnumerable<Dominio.Entities.Prova>>
    {
        public ObterProvasQuery(int anoLetivo, ProvaSituacao provaSituacao)
        {
            AnoLetivo = anoLetivo;
            ProvaSituacao = provaSituacao;
        }

        public int AnoLetivo { get; set; }
        public ProvaSituacao ProvaSituacao { get; set; }
    }
}
