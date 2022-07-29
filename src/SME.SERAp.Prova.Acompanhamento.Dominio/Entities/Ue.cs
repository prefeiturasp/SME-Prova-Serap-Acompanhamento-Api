namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Ue : EntidadeBase
    {
        public Ue(long id, long dreId, long codigo, string nome)
        {
            Id = id.ToString();
            DreId = dreId;
            Codigo = codigo;
            Nome = nome;
        }

        public long DreId { get; set; }
        public long Codigo { get; set; }
        public string Nome { get; set; }
    }
}
