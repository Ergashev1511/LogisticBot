using LogisticBot.Services.DTOs;
using LogisticBot.Services.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Commands.CargoCommand
{
    public class CargoGetAllQuery : IRequest<List<CargoViewModel>>
    {
    }
}
