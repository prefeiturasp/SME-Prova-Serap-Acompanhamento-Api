using FluentValidation;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTokenJwtQueryValidator : AbstractValidator<ObterTokenJwtQuery>
    {
        public ObterTokenJwtQueryValidator()
        {
            RuleFor(x => x.Abrangencias)
                .NotNull()
                .NotEmpty()
                .WithMessage("Abrangências é obrigatório");
        }
    }
}
