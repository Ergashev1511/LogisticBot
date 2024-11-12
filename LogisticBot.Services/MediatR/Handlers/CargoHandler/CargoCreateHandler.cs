using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.Domain.Entities;
using LogisticBot.Services.MediatR.Commands.CargoCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Handlers.CargoHandler
{
    public class CargoCreateHandler : IRequestHandler<CargoCreateCommand, long>
    {
        private readonly ICargoRepository _repository;
        public CargoCreateHandler(ICargoRepository repository)
        {
            _repository = repository;
        }
        public async Task<long> Handle(CargoCreateCommand request, CancellationToken cancellationToken)
        {
            Cargo cargo=new Cargo()
            {
               Type=request.CargoDto.Type,
               Weight=request.CargoDto.Weight,
               Volume=request.CargoDto.Volume,
               Price=request.CargoDto.Price,    
               StartLocation=request.CargoDto.StartLocation,    
               DestinationLocation=request.CargoDto.DestinationLocation,
               NumberOfTruck=request.CargoDto.NumberOfTruck,
               TypeOfTruck=request.CargoDto.TypeOfTruck,
               AvailableForm=request.CargoDto.AvailableForm,
               AvailableUntil=request.CargoDto.AvailableUntil,
               OwnerId=request.CargoDto.OwnerId,    
            };

            var Id=await _repository.AddAsync(cargo);
            return Id;
        }
    }
}
