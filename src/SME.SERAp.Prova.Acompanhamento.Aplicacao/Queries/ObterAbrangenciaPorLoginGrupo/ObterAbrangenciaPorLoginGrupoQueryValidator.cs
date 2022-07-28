using FluentValidation;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorLoginGrupoQueryValidator : AbstractValidator<ObterAbrangenciaPorLoginGrupoQuery>
    {
        public ObterAbrangenciaPorLoginGrupoQueryValidator()
        {
            RuleFor(c => c.Login)
               .NotEmpty()
               .WithMessage("O login ou código Rf é obrigatório.");

            RuleFor(c => c.Perfil)
                .NotEmpty()
                .WithMessage("O Perfil é obrigatório.");
        }
    }
}
