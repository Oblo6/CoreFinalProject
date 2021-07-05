using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Utils.Bases;
using Microsoft.Extensions.Configuration;

namespace AppCore.Utils
{
    public class AppSettingsUtil : AppSettingsUtilBase
    {
        public AppSettingsUtil(IConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
