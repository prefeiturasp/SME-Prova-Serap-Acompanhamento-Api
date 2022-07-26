﻿namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Abrangencia : EntidadeBase
    {
        public Abrangencia() { }

        public Abrangencia(string usuarioId, string login, string usuario, string grupoId, string grupo, bool permiteConsultar, bool permiteAlterar, long dreId, long ueId, long turmaId)
        {
            UsuarioId = usuarioId;
            Login = login;
            Usuario = usuario;
            GrupoId = grupoId;
            Grupo = grupo;
            PermiteConsultar = permiteConsultar;
            PermiteAlterar = permiteAlterar;
            DreId = dreId;
            UeId = ueId;
            TurmaId = turmaId;

            Id = $"{GrupoId}-{UsuarioId}-{DreId}-{UeId}-{TurmaId}";
        }

        public string UsuarioId { get; set; }
        public string Login { get; set; }
        public string Usuario { get; set; }
        public string GrupoId { get; set; }
        public string Grupo { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteAlterar { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
    }
}
