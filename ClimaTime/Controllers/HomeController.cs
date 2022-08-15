using ClimaTime.Models;
using ClimaTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace ClimaTime.Controllers
{
    public class HomeController : Controller
    {
        #region nativas
        private readonly ILogger<HomeController> _logger;
 
        private readonly IConfiguration _configuration;
        private readonly ServicesAPI _ServicesAPI;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ServicesAPI servicesAPI)
        {
            _logger = logger;

            _configuration = configuration;

            _ServicesAPI = servicesAPI;
        }

       

        public IActionResult Index()
        {
            return View();
        }
      
          public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


        #region operations
        /// <summary>
        ///  Objetivo de pesquisar cidades de um determinado dia onde as temparaturas são maiores
        /// </summary>
        /// <param name="modelRequest">Objeto da Model Request com Objetivo de pesquisar cidades de um determinado dia onde as temparaturas são maiores</param>
        /// <returns> Lista de Dados - Response Model </returns>
        [HttpGet]
        public async Task<List<ResponseModel>> GetHotCity()
        {
            List<ResponseModel> response = new List<ResponseModel>();

            response = await _ServicesAPI.HotCitys();
            return response;
        }

        /// <summary>
        ///  Objetivo de pesquisar cidades de um determinado dia onde as temparaturas são mais baixas
        /// </summary>
        /// <param name="modelRequest">Objeto da Model Request com Objetivo de pesquisar cidades de um determinado dia onde as temparaturas são mais baixas</param>
        /// <returns> Lista de Dados - Response Model </returns>
        [HttpGet]
        public async Task<List<ResponseModel>> GetColdCity()
        {
            List<ResponseModel> response = new List<ResponseModel>();

            response = await _ServicesAPI.ColdCitys();
            return response;
         
        }

        /// <summary>
        ///  Objetivo de pesquisar temperaturas para os próximos dias de uma determinada cidade
        /// </summary>
        /// <param name="modelRequest">Objeto da Model Request com Objetivo de pesquisar temperaturas para o próximo dia</param>
        /// <returns> Lista de Dados - Response Model </returns>
        [HttpGet]
        public async Task<List<ResponseModel>> GetNextTemperatures(int id)
        {
            List<ResponseModel> response = new List<ResponseModel>();

            response = await _ServicesAPI.NextTemperatures(id);
            return response;
        }
        /// <summary>
        /// Usado para capturar uma lista de estados disponiveis
        /// </summary>
        /// <returns>Retorna Objeto de lista</returns>
        public async Task<List<UFResponseModel>> GetUF()
        {
            List<UFResponseModel> response = new List<UFResponseModel>();

            response = await _ServicesAPI.GetUf();
            return response;
        }

        /// <summary>
        /// Usado para capturar uma lista de cidades disponiveis
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna Obejeto de Lista</returns>
        public async Task<List<CityResponseModel>> GetCitys(int id)
        {
            List<CityResponseModel> response = new List<CityResponseModel>();

            response = await _ServicesAPI.GetCity(id);
            return response;
        }

        #endregion
      
    }
}