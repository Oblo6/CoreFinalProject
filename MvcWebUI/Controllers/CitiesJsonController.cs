using AppCore.Business.Models.Results;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MvcWebUI.Controllers
{
    [Route("[controller]")]
    public class CitiesJsonController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesJsonController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("CitiesGet/{countryId?}")] 
        public IActionResult GetCitiesByCountryIdWithGet(int? countryId)
        {
            if (countryId == null)
                return View("NotFound");
            var result = _cityService.GetCities(countryId.Value);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            return Json(result.Data);
        }

        [HttpPost]
        [Route("CitiesPost/{countryId?}")]
        public IActionResult GetCitiesByCountryIdWithPost(int? countryId)
        {
            if (countryId == null)
                return View("NotFound");
            var result = _cityService.GetCities(countryId.Value);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            return Json(result.Data);
        }
    }
}
