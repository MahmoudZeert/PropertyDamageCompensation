using PropertyDamageCompensation.Web.Areas.Compensation.Interfaces;
using PropertyDamageCompensation.Web.Areas.Compensation.TypedHttpClient;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Services
{
    public static class RegisterAllHttpClients
    {
        public static void AddAllHttpClients(this IServiceCollection services) {

            services.AddHttpClient<IFloorApiClient, FloorApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7092");

            });
        }   
    }
}
