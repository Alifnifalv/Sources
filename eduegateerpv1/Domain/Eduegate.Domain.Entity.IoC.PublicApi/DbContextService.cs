using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eduegate.Domain.Entity.IoC.PublicApi
{
    public class DbContextService
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddDbContextPool<dbEduegateERPContext>(options =>
            {
                options.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                options.EnableSensitiveDataLogging();
            });

            services.AddDbContextPool<dbEduegateSchoolContext>(options =>
            {
                options.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                options.EnableSensitiveDataLogging();
            });

            //services.AddScoped(p =>
            //    p.GetRequiredService<IDbContextFactory<dbEduegateERPContext>>().CreateDbContext());
        }
    }
}