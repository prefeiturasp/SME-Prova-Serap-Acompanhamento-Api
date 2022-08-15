
namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class ResumoGeralProvaDto
    {
        public ResumoGeralProvaDto()
        {
            
        }

        public string TituloProva { get; set; }
        public int TotalAlunos { get; set; }
        public int ProvasIniciadas { get; set; }
        public int ProvasNaoFinalizadas { get; set; }
        public int ProvasFinalizadas { get; set; }
        public int TempoMedio { get; set; }
        public decimal PencentualRealizado { get; set; }
        public DetalheProvaDto DetalheProva { get; set; }

    }
}