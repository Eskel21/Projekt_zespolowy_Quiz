using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt_Quizy.Data;
using Projekt_Quizy.Data.Models;
using System.Collections.Generic;
using System.Linq;

public class DepartmentsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DepartmentsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Dzial> Departments { get; set; }
   
    public void OnGet()
    {
        Departments = _context.Dzialy.ToList();
    }
    
}
