using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ProjectWEB.Models
{
    public class LanguageService
    {
        private readonly IStringLocalizer localizer;

        public LanguageService(IStringLocalizerFactory factory) 
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            localizer = factory.Create("SharedResource",assemblyName.Name);
        }
        public LocalizedString Getkey(string key) 
        {
            return localizer[key];
        }
    }
}
