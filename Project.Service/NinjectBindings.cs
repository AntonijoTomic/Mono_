using Ninject.Modules;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class NinjectBindings  : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>();  
            Bind<IPaginationService<VehicleMake>>().To<PaginationService<VehicleMake>>();
            Bind<IPaginationService<VehicleModel>>().To<PaginationService<VehicleModel>>();
        }
    }
}
