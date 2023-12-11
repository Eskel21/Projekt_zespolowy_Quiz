using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy;
using Projekt_Quizy.Data;
using Projekt_Quizy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class DisplayQuestionsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public DisplayQuestionsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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
        var pytania = _context.Pytania
                               .Where(p => p.PoziomTrudnosci == PoziomTrudnosci)
                               .ToList();

        if (KategoriaId == 0)
        {
            LiczbaPytan = Math.Min(LiczbaPytan, pytania.Count);
            var losowePytania = LosowePytania(pytania);
            Questions = ConvertToQuestionModels(losowePytania);
        }
        else
        {
            var questions = pytania.Where(p => p.KategoriaId == KategoriaId).ToList();
            LiczbaPytan = Math.Min(LiczbaPytan, questions.Count);
            var randomPytania = LosowePytania(questions);
            Questions = ConvertToQuestionModels(randomPytania);
        }
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
