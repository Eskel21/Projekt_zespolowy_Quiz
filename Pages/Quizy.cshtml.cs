using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;
using Projekt_Quizy.Data.Models;

namespace Projekt_Quizy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IndexModel(ILogger<IndexModel> logger, ApiService apiService,ApplicationDbContext context)
        {
            _logger = logger;
            _apiService = apiService;
            _context = context;
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
            var dzial = _context.Dzialy.FirstOrDefault(e => e.DzialId == 1002);

            if (dzial != null)
            {

                dzial.licznik++;


                await _context.SaveChangesAsync();
            }
            _apiService.limit_number = SelectedLimit;
            Questions = await _apiService.GetQuestionsAsync();  
        }
       
    }
}