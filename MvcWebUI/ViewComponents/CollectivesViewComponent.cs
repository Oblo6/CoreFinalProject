using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcWebUI.ViewComponents
{
    public class CollectivesViewComponent : ViewComponent
    {
        private readonly ICollectiveService _collectiveServis;

        public CollectivesViewComponent(ICollectiveService collectiveService)
        {
            _collectiveServis = collectiveService;
        }

        public ViewViewComponentResult Invoke(int? collectiveId)
        {
            List<CollectiveModel> collectives;
            Task<Result<List<CollectiveModel>>> task = _collectiveServis.GetCollectivesAsync();
            Result<List<CollectiveModel>> result = task.Result;
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            collectives = result.Data;
            ViewBag.CollectiveId = collectiveId;
            return View(collectives);
        }
    }
}
