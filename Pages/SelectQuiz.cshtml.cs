using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;
using Projekt_Quizy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class SelectQuizModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public string nazwadzialu;
    public SelectQuizModel(ApplicationDbContext context)
    {
        _context = context;
    }


    [BindProperty(SupportsGet = true)]
    public int DzialId { get; set; }
    [BindProperty]
    public int KategoriaId { get; set; }
    
    [BindProperty]
    public int LiczbaPytan { get; set; }

    [BindProperty]
    public string PoziomTrudnosci { get; set; }

    public SelectList Kategorie { get; set; }

    public void OnGet()
    {
        nazwadzialu = _context.Dzialy.Where(d => d.DzialId == DzialId).Select(d => d.Nazwa).FirstOrDefault();

        var kategorieDzialu = _context.Kategorie
            .Where(k => k.DzialId == DzialId)
            .ToList();
        
       
        
        Kategorie = new SelectList(kategorieDzialu, "KategoriaId", "Nazwa");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            
            return Page();
        }

        var dzial = _context.Dzialy.FirstOrDefault(e => e.DzialId == DzialId);

        if (dzial != null)
        {
            
            dzial.licznik++;

            
            await _context.SaveChangesAsync();
        }
        var pytania = _context.Pytania
            .Where(p => p.PoziomTrudnosci == PoziomTrudnosci)
            .ToList();

       
        LiczbaPytan = Math.Min(LiczbaPytan, pytania.Count);

       
        var losowePytania = LosowePytania(pytania);

        return RedirectToPage("/DisplayQuestions", new
        {
            KategoriaId,
            LiczbaPytan,
            PoziomTrudnosci
        });
    }

    private List<Pytanie> LosowePytania(List<Pytanie> pytania)
    {
        var losoweIndeksy = new List<int>();
        var losowePytania = new List<Pytanie>();
        var rand = new Random();

        
        while (losoweIndeksy.Count < LiczbaPytan)
        {
            var losowyIndeks = rand.Next(0, pytania.Count);
            if (!losoweIndeksy.Contains(losowyIndeks))
            {
                losoweIndeksy.Add(losowyIndeks);
                losowePytania.Add(pytania[losowyIndeks]);
            }
        }

        return losowePytania;
    }
}
