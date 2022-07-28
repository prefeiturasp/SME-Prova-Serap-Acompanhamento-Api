namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class SelecioneDto
    {
        public SelecioneDto(object valor, string descricao)
        {
            Valor = valor;
            Descricao = descricao;
        }

        public object Valor { get; set; }
        public string Descricao { get; set; }
    }
}
