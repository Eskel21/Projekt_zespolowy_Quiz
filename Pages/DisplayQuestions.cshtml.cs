using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;
using Projekt_Quizy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class DisplayQuestionsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DisplayQuestionsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public int KategoriaId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int LiczbaPytan { get; set; }

    [BindProperty(SupportsGet = true)]
    public string PoziomTrudnosci { get; set; }

    public List<QuestionModel> Questions { get; set; }

    public void OnGet()
    {
        // Pobierz wszystkie pytania dla wybranej kategorii i poziomu trudnoœci
        var pytania = _context.Pytania
            .Include(p => p.Kategoria)
            .Where(p => p.KategoriaId == KategoriaId && p.PoziomTrudnosci == PoziomTrudnosci)
            .ToList();

        // Jeœli liczba pytañ do wylosowania jest wiêksza ni¿ dostêpna liczba pytañ, ustaw maksymaln¹ mo¿liw¹ liczbê pytañ
        LiczbaPytan = Math.Min(LiczbaPytan, pytania.Count);

        // Losowo wybierz pytania
        var losowePytania = LosowePytania(pytania);

        // Konwertuj pytania na modele QuestionModel
        Questions = ConvertToQuestionModels(losowePytania);
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

    private List<QuestionModel> ConvertToQuestionModels(List<Pytanie> pytania)
    {
        return pytania.Select(p => new QuestionModel
        {
            Id = p.PytanieId,
            Question = p.Tresc,
            Description = "", // brak odpowiednika w Pytanie
            Answers = new AnswersModel
            {
                Answer_A = p.Answer_A,
                Answer_B = p.Answer_B,
                Answer_C = p.Answer_C,
                Answer_D = p.Answer_D,
                Answer_E = p.Answer_E,
                Answer_F = p.Answer_F
            },
            MultipleCorrectAnswers = false, // brak odpowiednika w Pytanie
            Correct_Answers = new CorrectAnswersModel
            {
                Answer_A_Correct = p.Answer_A_Correct ?? false,
                Answer_B_Correct = p.Answer_B_Correct ?? false,
                Answer_C_Correct = p.Answer_C_Correct ?? false,
                Answer_D_Correct = p.Answer_D_Correct ?? false,
                Answer_E_Correct = p.Answer_E_Correct ?? false,
                Answer_F_Correct = p.Answer_F_Correct ?? false
            },
            Correct_Answer = "", // brak odpowiednika w Pytanie
            Explanation = "", // brak odpowiednika w Pytanie
            Tip = "", // brak odpowiednika w Pytanie
            Tags = new List<TagModel>(), // brak odpowiednika w Pytanie
            Category = p.Kategoria?.Nazwa ?? "", // Jeœli Kategoria jest null, ustaw pust¹ nazwê kategorii
            Difficulty = p.PoziomTrudnosci
        }).ToList();
    }
}
