using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderViewAsync(string viewName, object model);
    }
}
