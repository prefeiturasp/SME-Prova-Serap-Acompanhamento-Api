using FluentValidation;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryValidator : AbstractValidator<ObterAbrangenciaPorCodigoValidacaoAutenticacaoQuery>
    {
        public ObterAbrangenciaPorCodigoValidacaoAutenticacaoQueryValidator()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage("É necessário informar o código de validação.");
        }
    }
}
