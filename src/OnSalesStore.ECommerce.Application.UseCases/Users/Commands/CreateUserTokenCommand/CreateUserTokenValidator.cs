using FluentValidation;

namespace OnSalesStore.ECommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenCommand>
    {
        public CreateUserTokenValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
