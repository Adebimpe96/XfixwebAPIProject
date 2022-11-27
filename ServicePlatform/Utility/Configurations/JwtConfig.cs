using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.Utility.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpiryTime { get; set; }
    }
}
