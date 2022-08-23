namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Abrangencia : EntidadeBase
    {
        public Abrangencia() { }
        public Abrangencia(long id, string login, string usuario, string coressoId, string grupo, long dreId, long ueId, long turmaId)
        {
            Id = id.ToString();
            Login = login;
            Usuario = usuario;
            CoressoId = coressoId;
            Grupo = grupo;
            DreId = dreId;
            UeId = ueId;
            TurmaId = turmaId;
        }

        public string Login { get; set; }
        public string Usuario { get; set; }
        public string CoressoId { get; set; }
        public string Grupo { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
    }
}
