using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.Services.DTOs;
using LogisticBot.Services.MediatR.Commands.UserCommad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Handlers.UserHandler
{
    public class UserGetByIdHandler : IRequestHandler<UserGetByIdQuery, UserDto>
    {
        private readonly IUserRepository _repository;
        public UserGetByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var user=await _repository.GetByIdAsync(request.Id);
            
            var userdto=new UserDto()
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                PhoneNumber=user.PhoneNumber,
                TelegramUsername=user.TelegramUsername,
            };
            return userdto;
        }
    }
}
