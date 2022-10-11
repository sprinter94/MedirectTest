using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Settings
{
    public class GenSettings
    {
        public APISettings APISettings { get; set; }
        public int AbsoluteExpirationInMinutes { get; set; }
    }

    public class APISettings
    {
        public string FixerKey { get; set; }
    }
}