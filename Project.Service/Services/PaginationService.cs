using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public IEnumerable<T> GetPage(IEnumerable<T> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int GetTotalPages(int totalItems, int pageSize)
        {
            return (int)Math.Ceiling((double)totalItems / pageSize);
        }
    }
}

