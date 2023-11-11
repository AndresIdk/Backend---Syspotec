using Microsoft.Extensions.Configuration;

namespace Syspotec.Infrastructure.Data
{
    public class DB
    {
        private readonly string? masterdb = string.Empty;
        public DB() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            masterdb = builder.GetSection("ConnectionStrings:masterdb").Value;
        }

        public string DBConnection()
        {
            return masterdb;
        }


    }
}

