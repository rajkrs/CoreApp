using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.ApiCache
{
    interface ICacheKeyProvider
    {
        string CreateKey();
        byte[] ToByteArray<T>(T obj);
        T FromByteArray<T>(byte[] data);
    }
}
