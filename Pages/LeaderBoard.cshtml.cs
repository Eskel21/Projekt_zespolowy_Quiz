using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;
using System.Diagnostics.Contracts;
using System.Drawing.Text;

namespace Projekt_Quizy.Pages
{
    
    public class LeaderBoardModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public LeaderBoardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> BestUsers { get; set; }
        
        public void OnGet()
        {
            BestUsers = _context.Users
                .OrderByDescending(n => n.Points)
                .Take(5)
                .ToList();
        }
    }
}
