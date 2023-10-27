using dotnet_test.Model;
using FluentValidation;

namespace dotnet_test.Validator
{
        public class ProdutoValidator : AbstractValidator<Produto>
        {
            public ProdutoValidator()
            {
                RuleFor(p => p.Nome)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(50);

            }
        }
}
