namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalDto
    {
        public TotalDto(string titulo, string cor, string valor)
        {
            Titulo = titulo;
            Cor = cor;
            Valor = valor;
        }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public string Valor { get; set; }
        public string Tooltip { get; set; }

        public GraficosDto Graficos { get; set; }
    }
}
