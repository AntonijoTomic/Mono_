using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IPaginationService<T>
    {
        IEnumerable<T> GetPage(IEnumerable<T> list, int page, int pageSize);
        int GetTotalPages(int totalItems, int pageSize);
    }
}
