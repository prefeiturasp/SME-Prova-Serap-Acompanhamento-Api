
namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class ResumoGeralProvaDto
    {
        public ResumoGeralProvaDto()
        {
            
        }

        public long ProvaId { get; set; }
        public string TituloProva { get; set; }
        public long TotalAlunos { get; set; }
        public long ProvasIniciadas { get; set; }
        public long ProvasNaoFinalizadas { get; set; }
        public long ProvasFinalizadas { get; set; }
        public int TempoMedio { get; set; }
        public decimal PencentualRealizado { get; set; }
        public DetalheProvaDto DetalheProva { get; set; }

    }
}