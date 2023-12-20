using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;

namespace Projekt_Quizy.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
   

        [BindProperty]
        public IFormFile Picture { get; set; }
        public ApplicationUser user { get; set; }
        private readonly ApplicationDbContext _dbContext;
        public List<ApplicationUser> UsersRank { get; set; }
        public int CurrentUserRank { get; set; }
        public ProfileModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
           
        }

        public async Task<IActionResult> OnGet()
        {
            
            user = await _userManager.GetUserAsync(User);
            UsersRank = _dbContext.Users.OrderByDescending(u => u.Points).ToList();
            CurrentUserRank = UsersRank.FindIndex(u => u.Id == user.Id) + 1;
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            
            if (Picture != null && Picture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                   
                    user.Picture = memoryStream.ToArray();
                    await _userManager.UpdateAsync(user);
                }
            }

            return RedirectToPage("/Profile");
        }
    }
}
