namespace SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables
{
    public class ElasticOptions
    {
        public static string Secao => "Elastic";
        public string Url { get; set; }
        public string DefaultIndex { get; set; }
        public string CertificateFingerprint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
