using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenHandler : IRequestHandler<CreateUserTokenCommand, Response<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserTokenHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UserDTO>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserDTO>();
            var user = await _unitOfWork.Users.Authenticate(request.UserName, request.Password);

            if (user == null)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
                return response;
            }
            response.Data = _mapper.Map<UserDTO>(user);
            response.IsSuccess = true;
            response.Message = "Autenticación exitosa";

            return response;
        }
    }
}
