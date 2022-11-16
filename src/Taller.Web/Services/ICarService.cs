using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Taller.Api.Entities;
using Taller.Core.Communications;
using Taller.Core.Extensions;
using Taller.Core.Interfaces;
using Taller.Web.Commands;
using Taller.Web.Extensions;

namespace Taller.Web.Services
{
    public interface ICarService
    {
        Task<PagedResult<Car>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null);
        Task<ResponseResult> Add(AddCarCommand command);
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

        public async Task<ResponseResult> Add(AddCarCommand command)
        {
            var content = GetContent(command);
            var response = await _httpClient.PostAsync("/cliente/endereco/", content);
            if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);
            return ReturnOk();
        }

        public async Task<PagedResult<Car>> GetAllCarsPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");
            HandleErrorsResponse(response);
            return await DeserializeResponseObject<PagedResult<Car>>(response);
        }

    }
}
