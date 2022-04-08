using DynamicCssPocApp.Server.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;


#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace DynamicCssPocApp.Server.Views
{
    public class IndexModel : PageModel
    {
        private readonly ThemeRepository themeRepository;

        public IndexModel(ThemeRepository themeRepository)
        {
            this.themeRepository = themeRepository;
        }
        public string Version => Assembly.GetEntryAssembly().GetName().Version.ToString();

        public string CssIdentifier => themeRepository.NewestId();

        public void OnGet()
        {
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.