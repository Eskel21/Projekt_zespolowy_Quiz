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
        private readonly ApiService _apiService; 
        public QuestionModel[] Pytania { get; set; }

        public QuestionModel[] Questions { get; set; }  

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedDifficulty { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTags { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedLimit { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nazwaDzialu { get; set; }

        public async Task OnPost()
        {
            
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
            Questions = await _apiService.GetQuestionsAsync();  
        }
       
    }
}