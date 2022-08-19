using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class AlunoTurmaDto
    {
        public AlunoTurmaDto()
        {

        }

        public AlunoTurmaDto(string nomeEstudante, bool fezDownload, DateTime? inicioProva, DateTime? fimProva, long? tempoMedio, int? questoesRespondidas)
        {
            NomeEstudante = nomeEstudante;
            FezDownload = fezDownload;
            InicioProva = inicioProva;
            FimProva = fimProva;
            TempoMedio = tempoMedio;
            QuestoesRespondidas = questoesRespondidas;
        }

        public string NomeEstudante { get; set; }
        public bool FezDownload { get; set; }
        public DateTime? InicioProva { get; set; }
        public DateTime? FimProva { get; set; }
        public long? TempoMedio { get; set; }
        public int? QuestoesRespondidas { get; set; }

    }
}