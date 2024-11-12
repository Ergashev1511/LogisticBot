using LogisticBot.Services.DTOs;
using LogisticBot.Services.MediatR.Commands.UserCommad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot
{
    public class UserMediatRService
    {
        private readonly IMediator _mediator;
        public UserMediatRService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<long>  CreateUserService(UserCreateCommand command)
        {
            var Id = await _mediator.Send(command);
            return Id;
        }

        public async Task<List<UserDto>> GetAllUserService()
        {
            return await _mediator.Send(new UserGetAllQuery());
        }
        
    }
}
