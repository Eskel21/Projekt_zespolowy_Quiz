using Microsoft.AspNetCore.Mvc;  // Importuje przestrzeń nazw dla obsługi MVC w ASP.NET Core.
using Microsoft.AspNetCore.Mvc.RazorPages;  // Importuje przestrzeń nazw dla obsługi stron Razor Pages w ASP.NET Core.

namespace Projekt_zespołowy_Quiz.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;  // Prywatne pole przechowujące instancję klasy ApiService.

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;  // Konstruktor klasy IndexModel, przyjmuje instancję ApiService i inicjalizuje pole _apiService.
        }

        public QuestionModel[] Questions { get; set; }  // Właściwość przechowująca pytania.

        public async Task OnGet()
        {
            Questions = await _apiService.GetQuestionsAsync();  // Metoda OnGet, asynchronicznie pobiera pytania z ApiService i przypisuje je do właściwości Questions.
        }
    }
}