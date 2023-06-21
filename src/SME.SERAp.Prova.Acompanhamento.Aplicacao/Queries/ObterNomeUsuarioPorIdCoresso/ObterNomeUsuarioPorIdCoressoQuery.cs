using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterNomeUsuarioPorIdCoressoQuery : IRequest<Abrangencia>
    {
        public ObterNomeUsuarioPorIdCoressoQuery(string usuarioCoressoId)
        {
            UsuarioCoressoId = usuarioCoressoId;
        }

        public string UsuarioCoressoId { get; set; }
    }
}