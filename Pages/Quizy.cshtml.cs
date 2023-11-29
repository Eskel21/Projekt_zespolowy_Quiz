using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_Quizy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        private readonly ApiService _apiService;  // Prywatne pole przechowujące instancję klasy ApiService.
        public QuestionModel[] Pytania { get; set; }

        public QuestionModel[] Questions { get; set; }  // Właściwość przechowująca pytania.

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedDifficulty { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTags { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedLimit { get; set; }

        public async Task OnPost()
        {
            // Przekaż parametry filtrujące do serwisu API
            _apiService.category_name = SelectedCategory;
            _apiService.difficulty_name = SelectedDifficulty;
            if (SelectedTags != null)
            {
                _apiService.Tags = SelectedTags.Split(',');
            }
            else
            {
                _apiService.Tags = new string[0];
            }

            _apiService.limit_number = SelectedLimit;
            Questions = await _apiService.GetQuestionsAsync();  // Metoda OnGet, asynchronicznie pobiera pytania z ApiService i przypisuje je do właściwości Questions.
        }
       
    }
}