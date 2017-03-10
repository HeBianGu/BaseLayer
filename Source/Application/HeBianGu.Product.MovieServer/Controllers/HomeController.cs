using HeBianGu.Product.MovieServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var items = new List<MovieModel>();

            string dataFolder = Path.Combine(@"D:\WorkArea\DevTest\BaseLayer\Source\Application\HeBianGu.Product.MovieServer", "Data", "DZP");

            DirectoryInfo dir = Directory.CreateDirectory(dataFolder);

            foreach (var item in dir.GetDirectories())
            {
                MovieModel mm = new MovieModel();

                mm.Title = item.Name;

                string detial = Path.Combine(item.FullName, "Detail.txt");

                mm.Detial = System.IO.File.ReadAllText(detial).Substring(0,20);

                var imageFile = item.GetFiles().ToList().Find(l => l.Extension == ".jpg");

                mm.ImagePath =Path.Combine("../Data/DZP",item.Name,imageFile.Name);

                items.Add(mm);

            }


            return View(items);
        }


        /// <summary> 喜剧片 </summary>
        public ActionResult Comedy()
        {
            ViewBag.Message = "Your contact page.";


            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }


        /// <summary> 爱情片 </summary>
        public ActionResult AiQing()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }


        /// <summary> 科幻片 </summary>
        public ActionResult KeHuan()
        {
            var items = new List<MovieModel>();

            string dataFolder = Path.Combine(@"D:\WorkArea\DevTest\BaseLayer\Source\Application\HeBianGu.Product.MovieServer", "Data", "KHP");

            DirectoryInfo dir = Directory.CreateDirectory(dataFolder);

            foreach (var item in dir.GetDirectories())
            {
                MovieModel mm = new MovieModel();

                mm.Title = item.Name;

                string detial = Path.Combine(item.FullName, "Detail.txt");

                mm.Detial = System.IO.File.ReadAllText(detial).Substring(0, 20);

                var imageFile = item.GetFiles().ToList().Find(l => l.Extension == ".jpg");

                mm.ImagePath = Path.Combine("../Data/KHP", item.Name, imageFile.Name);

                items.Add(mm);

            }


            return View("Action",items);
        }


        /// <summary> 剧情片 </summary>
        public ActionResult JuQing()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 悬疑片 </summary>
        public ActionResult XuanYi()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 战争片 </summary>
        public ActionResult ZhanZheng()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 恐怖片 </summary>
        public ActionResult KongBu()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }


        /// <summary> 灾难片 </summary>
        public ActionResult Zhainan()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 连续剧 </summary>
        public ActionResult LianXuJu()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 动漫 </summary>
        public ActionResult DongMan()
        {
            ViewBag.Message = "Your contact page.";

            var items = new List<MovieModel>();

            for (int i = 0; i < 8; i++)
            {
                MovieModel mm = new MovieModel();
                mm.Title = i.ToString();
                mm.Detial = "88";
                items.Add(mm);

            }

            return View("Action", items);
        }

        /// <summary> 详细信息 </summary>
        public ActionResult MovieDetail(MovieModel m)
        {

            //var items = new List<MovieModel>();

            //string dataFolder = Path.Combine(@"D:\WorkArea\DevTest\BaseLayer\Source\Application\HeBianGu.Product.MovieServer", "Data", "DZP");

            //DirectoryInfo dir = Directory.CreateDirectory(dataFolder);

            //foreach (var item in dir.GetDirectories())
            //{
            //    MovieModel mm = new MovieModel();

            //    mm.Title = item.Name;

            //    string detial = Path.Combine(item.FullName, "Detail.txt");

            //    mm.Detial = System.IO.File.ReadAllText(detial).Substring(0, 20);

            //    var imageFile = item.GetFiles().ToList().Find(l => l.Extension == ".jpg");

            //    mm.ImagePath = Path.Combine("../Data/DZP", item.Name, imageFile.Name);

            //    items.Add(mm);

            //}


            return View(m);

            //return View(items.First());
        }



    }
}