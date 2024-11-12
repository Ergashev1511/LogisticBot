using LogisticBot.Services.DTOs;
using LogisticBot.Services.MediatR.Commands.CargoCommand;
using LogisticBot.Services.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot
{
    
    public class CargoMediatRService
    {
        private readonly IMediator _mediator;
        public CargoMediatRService(IMediator mediator)
        {
            _mediator = mediator;   
        }

        public async Task  CreateCargoService(CargoCreateCommand command)
        {
            await _mediator.Send(command);

        }

        public async Task<List<CargoViewModel>>  GetAllCargoService()
        {
            return await _mediator.Send(new CargoGetAllQuery());
        }
    }
}
