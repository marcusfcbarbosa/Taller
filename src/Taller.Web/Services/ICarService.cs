using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Taller.Core.Communications;
using Taller.Core.Extensions;
using Taller.Web.Extensions;
using Taller.Web.Models;

namespace Taller.Web.Services
{
    public interface ICarService
    {
        Task<PagedResult<CarModel>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null);
        Task<ResponseResult> Add(AddCarModel command);
        Task<ResponseResult> Delete(Guid Id);
    }

    public class CarService : Service, ICarService
    {
        private readonly HttpClient _httpClient;
        public CarService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.TallerUrl);
        }

        public async Task<ResponseResult> Add(AddCarModel command)
        {
            var content = GetContent(command);
            var response = await _httpClient.PostAsync("/api/cars/create", content);
            if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<ResponseResult> Delete(Guid Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/cars/delete/{Id}");
            if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<PagedResult<CarModel>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/cars/all?ps={pageSize}&page={pageIndex}&q={query}");
            HandleErrorsResponse(response);
            return await DeserializeResponseObject<PagedResult<CarModel>>(response);
        }

    }
}
