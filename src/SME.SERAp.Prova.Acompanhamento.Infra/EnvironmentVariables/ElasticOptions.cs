namespace SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables
{
    public class ElasticOptions
    {
        public static string Secao => "Elastic";
        public string Url { get; set; }
        public string DefaultIndex { get; set; }
    }
}
