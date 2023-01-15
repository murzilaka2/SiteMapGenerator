using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SiteMapGenerator.Interfaces;
using SiteMapGenerator.Repository;
using System;
using System.Xml.Linq;

namespace SiteMapGenerator.Models
{
    public static class UrlHelperExtensions
    {
        public static string AbsoluteRouteUrl(this IUrlHelper urlHelper,
            string routeName, object routeValues = null)
        {
            string scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }
    }
    public class MapGenerator
    {
        private readonly IProduct _products;

        public MapGenerator(IProduct products)
        {
            _products = products;
        }

        public IReadOnlyCollection<string> GetSitemapNodes(IUrlHelper urlHelper)
        {
            string scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
            List<string> nodes = new List<string>();

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Index" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "About" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Contact" }));

            foreach (string productId in _products.GetProductsId())
            {
                nodes.Add(urlHelper.AbsoluteRouteUrl("myroute", new { route = "product", id = productId }));
            }

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<string> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (string sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
}
