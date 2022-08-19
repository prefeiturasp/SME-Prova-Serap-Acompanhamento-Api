namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Teste : EntidadeBase
    {
        public Teste(string descricao)
        {
            Descricao = descricao;
        }
        public string Descricao { get; set; }
    }
}
