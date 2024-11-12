using LogisticBot.Services.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Commands.UserCommad
{
    public class UserGetByIdQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
    }
}
