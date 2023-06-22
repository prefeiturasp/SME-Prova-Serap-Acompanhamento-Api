namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public static class UtilResumoGeral
    {
        public static long ObterTempoMedio(long tempoTotal, long totalTurmas)
        {
            if (tempoTotal == 0) return 0;
            return (int)(tempoTotal / totalTurmas);
        }
    }
}
