using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ProvaSituacao? ProvaSituacao { get; set; }

    }
}
