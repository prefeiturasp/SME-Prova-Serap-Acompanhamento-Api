using System;


namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ProvaAlunoReaberturaDto
    {
        public long ProvaId { get; set; }
        public long AlunoRA { get; set; }
        public string LoginCoresso { get; set; }
        public Guid UsuarioCoresso { get; set; }
        public Guid GrupoCoresso { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
    }
}
