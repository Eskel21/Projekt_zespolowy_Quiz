using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_Quizy.Pages
{
    public class ScoreModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public ScoreModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task OnGetAsync()
        {
            CorrectAnswers = int.Parse(HttpContext.Request.Query["correctAnswers"]);
            TotalQuestions = int.Parse(HttpContext.Request.Query["totalQuestions"]);
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser != null)
            {
                
                loggedInUser.Points += CorrectAnswers;
                loggedInUser.PointsOverall += TotalQuestions;

               
                await _userManager.UpdateAsync(loggedInUser);
            }
        }
    }
}
