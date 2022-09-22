using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
  public  class ObterNomeUsuarioPorIdCoressoQuery : IRequest<Abrangencia>
    {
        public ObterNomeUsuarioPorIdCoressoQuery(string usuarioCoressoId)
        {
            UsuarioCoressoId = usuarioCoressoId;
        }

        public string UsuarioCoressoId { get; set; }
    }
}