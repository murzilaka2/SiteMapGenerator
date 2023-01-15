using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SiteMapGenerator.Interfaces;
using SiteMapGenerator.Models;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;

namespace SiteMapGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _products;
        private readonly MapGenerator _generator;

        public HomeController(IProduct products, MapGenerator generator)
        {
            _products = products;
            _generator = generator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("product")]
        public ActionResult Product(string productId)
        {
            var product = _products.GetProduct(productId);
            if (product is not null)
            {
                return Content($"{product.Name}");
            }
            return NotFound();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [Route("/sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var result = _generator.GetSitemapNodes(this.Url);
            string host = Request.Scheme + "://" + Request.Host;

            var sitemapNodes = _generator.GetSitemapNodes(this.Url);
            string xml = _generator.GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}