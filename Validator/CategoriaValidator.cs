using dotnet_test.Model;
using FluentValidation;

namespace dotnet_test.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(100);
        }
    }
}