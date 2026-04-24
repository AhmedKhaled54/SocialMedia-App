using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> Complete();
        public IGenericRepository<T> Repository<T>()where T : class;
    }
}
