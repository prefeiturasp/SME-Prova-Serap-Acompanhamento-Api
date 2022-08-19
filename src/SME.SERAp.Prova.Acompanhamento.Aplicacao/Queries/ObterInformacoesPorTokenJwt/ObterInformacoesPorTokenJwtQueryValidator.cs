using FluentValidation;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterInformacoesPorTokenJwtQueryValidator : AbstractValidator<ObterInformacoesPorTokenJwtQuery>
    {
        public ObterInformacoesPorTokenJwtQueryValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("O token é obrigatório.");
        }
    }
}
