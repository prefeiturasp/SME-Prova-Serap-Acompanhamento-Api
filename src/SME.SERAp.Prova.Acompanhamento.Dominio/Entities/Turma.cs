using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Turma : EntidadeBase
    {
        public Turma(long id, long ueId, long codigo, int anoLetivo, string ano, string nome, Modalidade modalidade, Turno turno, int etapaEja, string serieEnsino)
        {
            Id = id.ToString();
            UeId = ueId;
            Codigo = codigo;
            AnoLetivo = anoLetivo;
            Ano = ano;
            Nome = nome;
            Modalidade = modalidade;
            Turno = turno;
            EtapaEja = etapaEja;
            SerieEnsino = serieEnsino;
        }

        public long UeId { get; set; }
        public long Codigo { get; set; }
        public int AnoLetivo { get; set; }
        public string Ano { get; set; }
        public string Nome { get; set; }
        public Modalidade Modalidade { get; set; }
        public Turno Turno { get; set; }
        public int EtapaEja { get; set; }
        public string SerieEnsino { get; set; }
    }
}
