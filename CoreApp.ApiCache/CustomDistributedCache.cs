using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApp.ApiCache
{
    public class CustomDistributedCache : IDistributedCache
    {

        public byte[] Get(string key)
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public void Remove(string key)
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException("CustomDistributedCache custom throw");
        }
    }
}
