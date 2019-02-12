using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace CoreApp.ApiCache
{
    public class CacheKeyProvider : ICacheKeyProvider
    {
        private HttpContext _context;
        public CacheKeyProvider(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.HttpContext;
        }
        public string CreateKey()
        {
            StringBuilder cacheKey = new StringBuilder();

            var methodType = _context.Request.Method;

            //key From Uri
            cacheKey.Append($"{_context.Request.Host.ToUriComponent()}{_context.Request.Path}");

            //Keys from QueryString
            var allQuerydata = _context.Request?.Query.Select(q => $"{q.Key}={q.Value}").ToList();
            cacheKey.Append(allQuerydata.Count > 0 ? "?" : "" + $"{string.Join("&", allQuerydata)}");


            if (methodType == "POST"
                || methodType == "PUT"
                || methodType == "DELETE")
            {

                //var allPostedData = context.ModelState.ToDictionary(k => k.Key, v => v.Value).Select(d => $"{d.Key}={d.Value}");
                var stream = _context.Request.Body;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    var byteArray = memoryStream.ToArray();
                    var calculatedhash = ComputeSha256Hash(byteArray);
                    cacheKey.Append((allQuerydata.Count > 0 ? "&" : "?") + $"HashCalculated={calculatedhash}");
                }
            }



            // keys From Claims
            int userId, companyId = 0;
            var claims = _context.User?.Claims.ToList();
            if (claims.Count > 0)
            {
                int.TryParse(claims.Where(c => c.Type == "UserID").Single().Value, out userId);
                int.TryParse(claims.Where(c => c.Type == "CompanyID").Single().Value, out companyId);
                cacheKey.Append($"userId={userId}&companyId={companyId}");
            }

            return cacheKey.ToString(); ;
        }

        private static string ComputeSha256Hash(byte[] rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(rawData);

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

    }
}
