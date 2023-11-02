using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleService
    {

        Task<IEnumerable<VehicleMake>> GetAllVehiclesMakes();
        Task <IEnumerable<VehicleModel>> GetAllModels();
        Task< VehicleMake> getMake(int makeID);
        Task<int> CreateVehicleMake(VehicleMake vehicle);
        Task<int> CreateVehicleModel(VehicleModel vehicle);
        Task<int> UpdateVehicleMake(VehicleMake vehicle);
        Task<int> DeleteVehicleMakes(int id);

        Task<int> UpdateVehicleModel(VehicleModel vehicle);
        Task<int> DeleteVehicleModel(int id);
        // Task<int> GetTotalMakesCount(string filter);

        Task<IEnumerable<VehicleMake>> SortMakes(string sort, string sortOrder);
        Task<IEnumerable<VehicleModel>> SortModels(string sort, string sortOrder, List<VehicleMake> makes);
        Task<IEnumerable<VehicleMake>> FilterBySearch(List<VehicleMake> makes, string search);

        Task<IEnumerable<VehicleModel>> FilterByMake(List<VehicleModel> models, int filterId);
        Task<IEnumerable<VehicleMake>> GetAllMakesFromModels(List<VehicleModel> models);
       // Task<IEnumerable<VehicleMake>> GetPagedAndMappedModels(string sortby, string sortorder, int filter, int page, int pageSize);
    }
}
