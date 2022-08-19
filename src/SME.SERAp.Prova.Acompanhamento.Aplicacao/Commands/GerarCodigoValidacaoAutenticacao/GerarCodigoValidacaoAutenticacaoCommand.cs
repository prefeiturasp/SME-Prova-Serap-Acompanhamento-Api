using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class GerarCodigoValidacaoAutenticacaoCommand : IRequest<AutenticacaoValidarDto>
    {
        public GerarCodigoValidacaoAutenticacaoCommand(IEnumerable<Abrangencia> abrangencias)
        {
            Abrangencias = abrangencias;
        }

        public IEnumerable<Abrangencia> Abrangencias { get; set; }
    }
}
