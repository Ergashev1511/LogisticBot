using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.Domain.Entities;
using LogisticBot.Services.DTOs;
using LogisticBot.Services.MediatR.Commands.CargoCommand;
using LogisticBot.Services.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.MediatR.Handlers.CargoHandler
{
    public class CargoGetAllHandler : IRequestHandler<CargoGetAllQuery, List<CargoViewModel>>
    {
        private readonly ICargoRepository _repository;
        private readonly IUserRepository _userRepository;
        public CargoGetAllHandler(ICargoRepository repository,IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public async Task<List<CargoViewModel>> Handle(CargoGetAllQuery request, CancellationToken cancellationToken)
        {


            List<CargoViewModel> cargoViewModels = new List<CargoViewModel>();
            var cargos = await _repository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();

            // Create a dictionary for faster lookup of users by Id
            var userDictionary = users.ToDictionary(u => u.Id);

            foreach (var cargo in cargos)
            {
                var user = userDictionary.GetValueOrDefault(cargo.OwnerId);

                if (user != null)
                {
                    cargoViewModels.Add(new CargoViewModel
                    {
                        Type = cargo.Type,
                        Weight = cargo.Weight,
                        Volume = cargo.Volume,
                        Price = cargo.Price,
                        StartLocation = cargo.StartLocation,
                        DestinationLocation = cargo.DestinationLocation,
                        NumberOfTruck = cargo.NumberOfTruck,
                        TypeOfTruck = cargo.TypeOfTruck,
                        AvailableForm = cargo.AvailableForm,
                        AvailableUntil = cargo.AvailableUntil,
                        UserDto = new UserDto()  // Initialize UserDto
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            TelegramUsername = user.TelegramUsername
                        }
                    });
                }
            }

            return cargoViewModels;


        }
    }
}
