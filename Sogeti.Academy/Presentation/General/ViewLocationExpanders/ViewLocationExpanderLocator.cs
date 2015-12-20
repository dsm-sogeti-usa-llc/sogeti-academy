using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc.Razor;

namespace Sogeti.Academy.Presentation.General.ViewLocationExpanders
{
    public interface IViewLocationExpanderLocator
    {
        IEnumerable<IViewLocationExpander> GetAll(Assembly assembly);
    }
    
    public class ViewLocationExpanderLocator : IViewLocationExpanderLocator
    {
        public IEnumerable<IViewLocationExpander> GetAll(Assembly assembly)
        {
            return assembly.GetTypes()
				.Where(t => t.GetInterface(typeof(IViewLocationExpander).FullName) != null)
                .Select(Activator.CreateInstance)
                .Cast<IViewLocationExpander>();
        }        
    }
}