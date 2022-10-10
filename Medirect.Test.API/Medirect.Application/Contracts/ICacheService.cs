using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Contracts
{
    public interface ICacheService
    {
        Task<T> TryGet<T>(string cacheKey);

        Task<T> Set<T>(string cacheKey, T value);

        void Remove(string cacheKey);
    }
}