using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Ano : EntidadeBase
    {
        public int AnoLetivo { get; set; }
        public long UeId { get; set; }
        public Modalidade Modalidade { get; set; }
        public string Nome { get; set; }
    }
}
