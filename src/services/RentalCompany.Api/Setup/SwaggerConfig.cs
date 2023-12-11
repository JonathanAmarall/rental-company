using Microsoft.OpenApi.Models;

namespace RentalCompany.Api.Setup
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RentalCompany API",
                    Description = "Api developed to control certain items that can be loaned to a contact.",
                    Contact = new OpenApiContact() { Name = "Jonathan Amaral", Email = "jhouamaral95@gmail.com" },
                });
            });
        }
    }
}
