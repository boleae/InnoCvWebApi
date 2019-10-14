using DataAccessRepository;
using DataServicesLayer;
using DataServicesLayer.Models;
using DataServicesLayer.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WebApiInnoCV.Models;

namespace WebApiInnoCV
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new UnityContainer();
            container.RegisterType<DbContext, UsersContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenericRepository<User>, EfRepository<User>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsersService, UsersService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
