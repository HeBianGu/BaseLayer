using HeBianGu.Product.MovieServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeBianGu.Product.MovieServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 首页 </summary>
        public ActionResult HomePage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 动作片 </summary>
        public ActionResult Action()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 15; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "343434";
                items.Add(mm);

            }

            return View(items);
        }


        /// <summary> 喜剧片 </summary>
        public ActionResult Comedy()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 爱情片 </summary>
        public ActionResult AiQing()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 科幻片 </summary>
        public ActionResult KeHuan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 剧情片 </summary>
        public ActionResult JuQing()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary> 悬疑片 </summary>
        public ActionResult XuanYi()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary> 战争片 </summary>
        public ActionResult ZhanZheng()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary> 恐怖片 </summary>
        public ActionResult KongBu()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary> 灾难片 </summary>
        public ActionResult Zhainan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary> 连续剧 </summary>
        public ActionResult LianXuJu()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary> 动漫 </summary>
        public ActionResult DongMan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}