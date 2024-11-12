using LogisticBot.Services.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Commands.CargoCommand
{
    public class CargoGetByIdQuery : IRequest<CargoDto>
    {
        public long Id { get; set; }
    }
}
