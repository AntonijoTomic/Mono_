using Project.Service.Classes;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeView
    {
        public string Filter { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public IEnumerable<VehicleMake> Makes { get; set; }
    }
}
