using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.Domain.Entities;
using LogisticBot.Services.MediatR.Commands.UserCommad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Handlers.UserHandler
{
    public class UserCreateHandler : IRequestHandler<UserCreateCommand, long>
    {
        private readonly IUserRepository _repository;
        public UserCreateHandler(IUserRepository repository)
        {
            _repository = repository;   
        }
        public async Task<long> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            User user = new User()
            {
                FirstName=request.UserDto.FirstName,
                LastName=request.UserDto.LastName,
                PhoneNumber=request.UserDto.PhoneNumber,
                TelegramUsername=request.UserDto.TelegramUsername,  
            };

          var id= await _repository.AddAsync(user);

            return id;
        }
    }
}
