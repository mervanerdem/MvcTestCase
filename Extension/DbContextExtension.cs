using Microsoft.EntityFrameworkCore;
using MvcTestCase.Models;

namespace MvcProject_Case.Extension
{
    public static class DbContextExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
        {

           var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
           services.AddDbContext<TestContext>(opts => opts.UseSqlServer(dbConfig));

        }
    }
}
