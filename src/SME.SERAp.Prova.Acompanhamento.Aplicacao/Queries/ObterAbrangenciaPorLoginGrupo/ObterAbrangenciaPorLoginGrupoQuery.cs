using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorLoginGrupoQuery : IRequest<IEnumerable<Abrangencia>>
    {
        public ObterAbrangenciaPorLoginGrupoQuery(string login, string perfil)
        {
            Login = login;
            Perfil = perfil;
        }

        public string Login { get; set; }
        public string Perfil { get; set; }
    }
}
