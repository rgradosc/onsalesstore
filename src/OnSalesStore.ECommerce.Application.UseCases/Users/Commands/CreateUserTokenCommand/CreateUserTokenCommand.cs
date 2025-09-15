using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;

namespace OnSalesStore.ECommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public sealed record CreateUserTokenCommand : IRequest<Response<UserDTO>>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
