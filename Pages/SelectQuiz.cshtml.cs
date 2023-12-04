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
        // Filtruj kategorie tylko dla dzia³u o id=1
        var kategorieDzialu = _context.Kategorie
            .Where(k => k.DzialId == DzialId)
            .ToList();

        Kategorie = new SelectList(kategorieDzialu, "KategoriaId", "Nazwa");
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            // Handle validation errors
            return Page();
        }

        // Pobierz wszystkie pytania dla wybranej kategorii i poziomu trudnoœci
        var pytania = _context.Pytania
            .Where(p => p.KategoriaId == KategoriaId && p.PoziomTrudnosci == PoziomTrudnosci)
            .ToList();

        // Jeœli liczba pytañ do wylosowania jest wiêksza ni¿ dostêpna liczba pytañ, ustaw maksymaln¹ mo¿liw¹ liczbê pytañ
        LiczbaPytan = Math.Min(LiczbaPytan, pytania.Count);

        // Losowo wybierz pytania
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

        // Losowo generuj indeksy pytañ
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
