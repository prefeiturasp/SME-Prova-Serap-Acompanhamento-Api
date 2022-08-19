using FluentValidation;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirTesteCommandValidator : AbstractValidator<InserirTesteCommand>
    {
        public InserirTesteCommandValidator()
        {
            RuleFor(query => query.Descricao)
                .NotEmpty()
                .WithMessage("A Descrição do teste não pode ser vazia");
        }
    }
}
