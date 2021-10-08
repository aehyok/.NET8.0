using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.Data.Entity.GuideLine;
using aehyok.Core.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{

    /// <summary>
    /// 指标定义器
    /// </summary>
    public class GuideLineController : Controller
    {
        private readonly IGuideLineRepository _guideLineRepository;

        public GuideLineController(IGuideLineRepository guideLineRepository)
        {
            this._guideLineRepository = guideLineRepository;
        }
        public IActionResult Index()
        {
            var list=this._guideLineRepository.GetGuideLineList();
            return View();
        }

        public void ExecuteList(List<MD_GuideLine> list)
        {
            var model = list.Where(item => item.FatherId == "-1");

            foreach(var item in model)
            {
                item.Children = new List<MD_GuideLine>();
                item.Children = list.Where(item => item.FatherId == item.Id).ToList();
            }
        }
    }
}