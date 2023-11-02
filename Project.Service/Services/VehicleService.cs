using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _dbContext;
  

        public VehicleService(ApplicationDbContext vehicleRepository)
        {
            _dbContext = vehicleRepository;         
    
        }
        public async Task<int> CreateVehicleMake(VehicleMake vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }
            try
            {
                _dbContext.VehicleMake.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return vehicle.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


 
        public async Task<int> DeleteVehicleMakes(int id)
        {           
            try
            {
                var result = await _dbContext.VehicleMake.FirstOrDefaultAsync(e => e.Id == id);
                if (result != null)
                {
                    _dbContext.VehicleMake.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        public async Task<IEnumerable<VehicleMake>> GetAllVehiclesMakes()
        {
            return await _dbContext.VehicleMake.ToListAsync();
        }

       

        public async Task<int> UpdateVehicleMake(VehicleMake vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }

            try
            {
                var existingMake = await _dbContext.VehicleMake.FindAsync(vehicle.Id);

                if (existingMake == null)
                {
                    return 0;
                }

                existingMake.Name = vehicle.Name;
                existingMake.Abrv = vehicle.Abrv;

                _dbContext.VehicleMake.Update(existingMake);
                await _dbContext.SaveChangesAsync();

                return 1;    
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }


        public async Task<IEnumerable<VehicleMake>> SortMakes(string sort, string sortOrder)
        {
            var makes = await _dbContext.VehicleMake.ToListAsync();
            switch (sort)
            {
                case "Name":
                    makes = sortOrder == "asc" ? makes.OrderBy(m => m.Name).ToList() : makes.OrderByDescending(m => m.Name).ToList();
                    break;
                case "Abrv":
                    makes = sortOrder == "asc" ? makes.OrderBy(m => m.Abrv).ToList() : makes.OrderByDescending(m => m.Abrv).ToList();
                    break;
            }
            return makes;
        }

        public async Task<IEnumerable<VehicleModel>> SortModels(string sort, string sortOrder, List<VehicleMake> makes)
        {
            var models = await _dbContext.VehicleModel.ToListAsync();
            switch (sort)
            {
                case "Name":
                    models = sortOrder == "asc" ? models.OrderBy(m => m.Name).ToList() :  models.OrderByDescending(m => m.Name).ToList();
                    break;
                case "Abrv":
                    models = sortOrder == "asc" ? models.OrderBy(m => m.Abrv).ToList() : models.OrderByDescending(m => m.Abrv).ToList();
                    break;
                case "Makes":
                    models = sortOrder == "asc" ? models.OrderBy(m => makes.FirstOrDefault(make => make.Id == m.MakeId)?.Name).ToList() : models.OrderByDescending(m => makes.FirstOrDefault(make => make.Id == m.MakeId)?.Name).ToList();                   
                    break;
            }
            return models;
        }

        public async Task<IEnumerable<VehicleMake>> FilterBySearch(List<VehicleMake> makes,string search)
        {
            return makes.Where(m => m.Name.ToUpper().Contains(search.ToUpper()));
        }

     

        public async Task<int> CreateVehicleModel(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }
            try
            {
                _dbContext.VehicleModel.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return vehicle.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<VehicleModel>> GetAllModels()
        {
            return await _dbContext.VehicleModel.ToListAsync();
        }
        public async Task<VehicleMake> getMake(int makeId)
        {   
            return await _dbContext.VehicleMake.Where(m => m.Id == makeId).FirstOrDefaultAsync();   
        }

        public async Task<IEnumerable<VehicleModel>> FilterByMake(List<VehicleModel> models, int filterId)
        {
            if (models.Any(m => m.MakeId == filterId))
            {
                var filteredModels = models.Where(m => m.MakeId == filterId).ToList();
                return filteredModels;
            }
            return new List<VehicleModel>();

        }

        public async Task<IEnumerable<VehicleMake>> GetAllMakesFromModels(List<VehicleModel> models)
        {
            var makeIds = models.Select(m => m.MakeId).Distinct().ToList();
            var matchingMakes = await _dbContext.VehicleMake.Where(make => makeIds.Contains(make.Id)).ToListAsync();

            return matchingMakes;
        }

        public async Task<int> UpdateVehicleModel(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }

            try
            {
                var existingModel = await _dbContext.VehicleModel.FindAsync(vehicle.Id);

                if (existingModel == null)
                {
                    return 0;
                }

                existingModel.Name = vehicle.Name;
                existingModel.Abrv = vehicle.Abrv;
                existingModel.MakeId = vehicle.MakeId;
                _dbContext.VehicleModel.Update(existingModel);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> DeleteVehicleModel(int id)
        {
            try
            {
                var result = await _dbContext.VehicleModel.FirstOrDefaultAsync(e => e.Id == id);
                if (result != null)
                {
                    _dbContext.VehicleModel.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

     
        /*
public async Task<IEnumerable<VehicleMake>> GetPagedAndMappedModels(string sortby, string sortorder, int filter, int page, int pageSize)
{
var models = await GetAllModels();
var makes = await GetAllVehiclesMakes();

if (sortby == "Makes" || !string.IsNullOrEmpty(sortby))
{
models = await SortModels(sortby, sortorder, (List<VehicleMake>)makes);
}
if (filter > 0)
{
models = await FilterByMake((List<VehicleModel>)models, filter);
}

var totalItems = models.Count();
var totalPages = _pagination.GetTotalPages(totalItems, pageSize);

var pagedModels = _pagination.GetPage(models, page, pageSize);
var modelViews = _mapper.Map<List<VehicleModelView>>(pagedModels);

var paginationInfo = new PaginationInfo
{
TotalItems = totalItems,
ItemsPerPage = pageSize,
CurrentPage = page,
TotalPages = totalPages
};

foreach (var modelView in modelViews)
{
modelView.SortBy = sortby;
modelView.filter = filter;
modelView.SortOrder = sortorder;
modelView.MakeName = await GetMakeName(modelView.MakeId);
modelView.PaginationInfo = paginationInfo;
modelView.Makes = await GetAllMakesFromModels(models.ToList());
}

return modelViews;
}*/
    }
}
