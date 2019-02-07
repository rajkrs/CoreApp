using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace CoreApp.IpWhitelist
{
    public class IpWhitelistMiddleware
    {
        #region Private Fields

        private readonly List<string> _adminSafeIpList;
        private readonly ILogger<IpWhitelistMiddleware> _logger;
        private readonly RequestDelegate _next;

        #endregion Private Fields

        #region Public Constructors

        public IpWhitelistMiddleware(
            RequestDelegate next,
            ILogger<IpWhitelistMiddleware> logger,
            string ipSafeList)
        {
            _adminSafeIpList = ipSafeList.Split(';').ToList();
            _next = next;
            _logger = logger;
        }

        public IpWhitelistMiddleware(
            RequestDelegate next,
            ILogger<IpWhitelistMiddleware> logger,
            List<string> ipSafeList)
        {
            _adminSafeIpList = ipSafeList;
            _next = next;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Properties

        public int MyProperty { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task Invoke(HttpContext context)
        {
            //if (context.Request.Method != "GET")
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug($"Request from Remote IP address: {remoteIp}");

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in _adminSafeIpList)
                {
                    var testIp = IPAddress.Parse(address);
                    if (testIp.GetAddressBytes().SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogInformation($"Forbidden Request from Remote IP address: {remoteIp}");
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }

        #endregion Public Methods
    }
}