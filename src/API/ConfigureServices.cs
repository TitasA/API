using System.Text.Json.Serialization;

namespace API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }
    }
}


//TODO: Add all endpoint with logic
//TODO: Add tests
//TODO: Refactor