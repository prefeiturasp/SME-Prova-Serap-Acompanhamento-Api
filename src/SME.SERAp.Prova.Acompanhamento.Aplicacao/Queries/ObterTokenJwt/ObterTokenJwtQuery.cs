using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTokenJwtQuery : IRequest<AutenticacaoRetornoDto>
    {
        public ObterTokenJwtQuery(IEnumerable<Abrangencia> abrangencias)
        {
            Abrangencias = abrangencias;
        }

        public IEnumerable<Abrangencia> Abrangencias { get; set; }
    }
}
