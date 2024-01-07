using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iridium.Infrastructure.Utilities
{
    using Microsoft.Extensions.Configuration;

    public class ConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetConfiguration<T>(string section) where T : new()
        {
            var settings = new T();
            _configuration.GetSection(section).Bind(settings);
            return settings;
        }
    }

}
