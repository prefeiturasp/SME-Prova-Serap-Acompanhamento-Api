namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class AutenticacaoValidarDto
    {
        public AutenticacaoValidarDto(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
