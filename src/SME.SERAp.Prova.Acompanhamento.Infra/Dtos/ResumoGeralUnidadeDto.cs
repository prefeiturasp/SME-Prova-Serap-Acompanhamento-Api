namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ResumoGeralUnidadeDto
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Turno { get; set; }

        public ResumoGeralProvaDto Item { get; set; }

    }

}

