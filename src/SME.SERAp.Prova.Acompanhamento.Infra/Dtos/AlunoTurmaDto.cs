using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class AlunoTurmaDto
    {
        public AlunoTurmaDto()
        {

        }

        public AlunoTurmaDto(string nomeEstudante, bool fezDownload, long ra, DateTime? inicioProva, DateTime? fimProva, long? tempoMedio, int? questoesRespondidas, SituacaoProvaAluno? situacaoProvaAluno, string usurioCoressoUltimaReabertura, DateTime? dataUltimaReabertura)
        {
            NomeEstudante = nomeEstudante;
            FezDownload = fezDownload;
            InicioProva = inicioProva;
            FimProva = fimProva;
            TempoMedio = CalcularTempoMedioEmMinutos(tempoMedio);
            QuestoesRespondidas = questoesRespondidas;
            Ra = ra;
            SituacaoProvaAluno = situacaoProvaAluno;
            UsurioCoressoUltimaReabertura = usurioCoressoUltimaReabertura;
            DataUltimaReabertura = dataUltimaReabertura;


        }

        public long Ra { get; set; }
        public string NomeEstudante { get; set; }
        public bool FezDownload { get; set; }
        public DateTime? InicioProva { get; set; }
        public DateTime? FimProva { get; set; }
        public long? TempoMedio { get; set; }
        public int? QuestoesRespondidas { get; set; }
        public string UltimaReabertura { get; set; }
        public bool PodeReabrirProva { get; set; }
        public SituacaoProvaAluno? SituacaoProvaAluno { get; set; }
        public string UsurioCoressoUltimaReabertura { get; set; }

        public DateTime? DataUltimaReabertura { get; set; }



        private long? CalcularTempoMedioEmMinutos(long? tempoMedio)
        {
            if (tempoMedio == null || tempoMedio == 0)
                return tempoMedio;
            if (tempoMedio < 60)
                return 0;
            return tempoMedio / 60;
        }

    }
}