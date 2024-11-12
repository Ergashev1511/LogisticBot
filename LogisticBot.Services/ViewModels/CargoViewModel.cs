using LogisticBot.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Services.ViewModels
{
    public class CargoViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Volume { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty; // Dollar $ da
        public string StartLocation { get; set; } = string.Empty;
        public string DestinationLocation { get; set; } = string.Empty;
        public int NumberOfTruck { get; set; }
        public string TypeOfTruck { get; set; } = string.Empty;
        public string AvailableForm { get; set; } = string.Empty;
        public string AvailableUntil { get; set; } = string.Empty;

        public UserDto   UserDto { get; set; }
    }
}
