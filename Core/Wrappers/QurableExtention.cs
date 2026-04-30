using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Core.Wrappers
{
    public static  class QurableExtention
    {
        public static  async Task<Pagination<T>>ToPaginationListAsync<T> 
            (this IQueryable<T> query,int PageNumber,int PageSize)
            where T : class
        {
            if (query == null)
                throw new Exception("No Data ");
            PageNumber=PageNumber==0? 1 :PageNumber;
            PageSize=PageSize==0? 10 :PageSize;

            int count = await  query.AsNoTracking().CountAsync();
            if (count == 0)
                return Pagination<T>.PaginationSuccess(new List<T>(),count,PageNumber,PageSize);
            var paginated = await  query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

            return Pagination<T>
                .PaginationSuccess(paginated, count, PageNumber, PageSize);
        }
    }
}
