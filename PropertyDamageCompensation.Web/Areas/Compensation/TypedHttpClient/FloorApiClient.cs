using PropertyDamageCompensation.Contracts.Dtos;
using PropertyDamageCompensation.Web.Areas.Compensation.Interfaces;
using System.Text;
using System.Text.Json;

namespace PropertyDamageCompensation.Web.Areas.Compensation.TypedHttpClient
{
    public class FloorApiClient : IFloorApiClient
    {
        private readonly HttpClient _httpClient;

        public FloorApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async  Task<FloorDto> IFloorApiClient.GetById(int id)
        {
           var response=await _httpClient.GetAsync($"/api/Floor/{id}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FloorDto>();
        }

        async Task<IEnumerable<FloorDto>> IFloorApiClient.GetAll()
        {
            var response = await _httpClient.GetAsync($"/api/Floor");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<FloorDto>>();
        }
        async Task<FloorDto> IFloorApiClient.Create(FloorDto floor)
        {
            var json = JsonSerializer.Serialize(floor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Floor", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FloorDto>();
        }

        async Task IFloorApiClient.Update(int id, FloorDto floor)
        {
            var json = JsonSerializer.Serialize(floor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Floor/{id}", content);
            response.EnsureSuccessStatusCode();
            //return await response.Content.ReadFromJsonAsync<FloorDto>();
        }

        async Task<bool> IFloorApiClient.Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Floor/{id}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
