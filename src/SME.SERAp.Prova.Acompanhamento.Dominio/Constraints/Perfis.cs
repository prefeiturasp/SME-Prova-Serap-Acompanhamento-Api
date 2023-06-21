using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Constraints
{
    public static class Perfis
    {
        public readonly static Guid PERFIL_ADMINISTRADOR = Guid.Parse("AAD9D772-41A3-E411-922D-782BCB3D218E");
        public readonly static Guid PERFIL_ADMINISTRADOR_NTA = Guid.Parse("22366A3E-9E4C-E711-9541-782BCB3D218E");

        public static bool PerfilEhAdministrador(string perfil)
        {
            var ehGuid = Guid.TryParse(perfil, out var guidPerfil);
            return ehGuid &&
                (guidPerfil == Perfis.PERFIL_ADMINISTRADOR ||
                guidPerfil == Perfis.PERFIL_ADMINISTRADOR_NTA);
        }
    }
}
