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
    public class UserGetAllHandler : IRequestHandler<UserGetAllQuery, List<UserDto>>
{
        private readonly IUserRepository _repository;
        public UserGetAllHandler(IUserRepository repository)
        {
            _repository = repository;   
        }
        public async Task<List<UserDto>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            return users.Select(a => new UserDto()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhoneNumber = a.PhoneNumber,
                TelegramUsername = a.TelegramUsername,
            }).ToList();
        }
    }
}
