using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class FiltroDto
    {
        [Required]
        public int AnoLetivo { get; set; }
        public  Modalidade? Modalidade { get; set; }
        public int? DreId { get; set; }
        public long? UeId { get; set; }
        public int? AnoEscolar { get; set; }
        public long? TurmaId { get; set; }
        public int[] ProvasId { get; set; }

        [Required]
        public ProvaSituacao ProvaSituacao { get; set; }
        
        public int NumeroPagina { get; set; }
        public int NumeroRegistros { get; set; }
    }
}
