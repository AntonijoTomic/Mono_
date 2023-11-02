using Project.Service.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    public class VMVehicle
    {
        
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMake makes { get; set; }

    }
}

