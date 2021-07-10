using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using qlsv.Models.Interfaces;
using RouteData = Microsoft.AspNetCore.Components.RouteData;
using Microsoft.AspNetCore.Mvc.Abstractions;
using qlsv.ViewModels.Marks;

namespace qlsv.Models.Services
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _env;
        private readonly HttpContext _http;

        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IHostingEnvironment env, IHttpContextAccessor ctx)
        {
            _razorViewEngine = razorViewEngine; _tempDataProvider = tempDataProvider; _serviceProvider = serviceProvider; _env = env; _http = ctx.HttpContext;
        }

        public async Task<string> RenderViewAsync(string viewName, MarkGPAView model)
        {
            //var actionContext = new ActionContext();
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext();
            actionContext.HttpContext = httpContext;
            actionContext.ActionDescriptor = new ActionDescriptor() ;
            actionContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(executingFilePath: viewName, viewPath: viewName, true);
                //var viewResult = _razorViewEngine.GetView(_env.WebRootPath, viewName, false); // For views outside the usual Views folder
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(_http, _tempDataProvider), sw, new HtmlHelperOptions());
                viewContext.RouteData = _http.GetRouteData();
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }

        }
    }
}
