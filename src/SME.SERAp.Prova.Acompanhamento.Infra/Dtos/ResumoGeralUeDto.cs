
namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralUeDto
    {
        public long UeId { get; set; }
        public string UeNome { get; set; }

        public ResumoGeralProvaDto Item { get; set; }

    }
}
