using Medirect.Application.Contracts;
using Medirect.Application.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<GenSettings> _options;

        public RedisCacheService(IDistributedCache cache, IOptions<GenSettings> options)
        {
            _cache = cache;
            _options = options;
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey.ToUpper());
        }

        public async Task<T> Set<T>(string cacheKey, T value)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = DateTime.Now.AddMinutes(_options.Value.AbsoluteExpirationInMinutes).TimeOfDay;
            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(cacheKey.ToUpper(), jsonData, options);
            return value;
        }

        public async Task<T> TryGet<T>(string cacheKey)
        {
            var jsonData = await _cache.GetStringAsync(cacheKey.ToUpper());
            if (jsonData is null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}