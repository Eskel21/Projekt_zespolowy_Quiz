using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data; // Dodaj namespace, w którym znajduje się DbContext
using Projekt_Quizy.Data.Models;

public class LocalDbService
{
    private readonly ApplicationDbContext _dbContext;

    public LocalDbService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QuestionModel[]> GetQuestionsFromLocalDbAsync(int dzialId, int kategoriaId)
    {
        // Pobierz pytania z lokalnej bazy danych na podstawie działu i kategorii
        var pytania = await _dbContext.Pytania
            .Include(p => p.Kategoria)
            .Where(p => p.Kategoria.DzialId == dzialId && p.KategoriaId == kategoriaId)
            .ToArrayAsync();

        // Konwertuj pytania do formatu QuestionModel
        var questionModels = pytania.Select(ConvertToQuestionModel).ToArray();

        return questionModels;
    }

    private QuestionModel ConvertToQuestionModel(Pytanie pytanie)
    {
        return new QuestionModel
        {
            Id = pytanie.PytanieId,
            Question = pytanie.Tresc,
            Answers = new AnswersModel
            {
                Answer_A = pytanie.Answer_A,
                Answer_B = pytanie.Answer_B,
                Answer_C = pytanie.Answer_C,
                Answer_D = pytanie.Answer_D,
                Answer_E = pytanie.Answer_E,
                Answer_F = pytanie.Answer_F
            },
            MultipleCorrectAnswers = false, // Ustaw, jak jest zdefiniowane w lokalnej bazie danych
            Correct_Answers = new CorrectAnswersModel
            {
                Answer_A_Correct = pytanie.Answer_A_Correct.GetValueOrDefault(),
                Answer_B_Correct = pytanie.Answer_B_Correct.GetValueOrDefault(),
                Answer_C_Correct = pytanie.Answer_C_Correct.GetValueOrDefault(),
                Answer_D_Correct = pytanie.Answer_D_Correct.GetValueOrDefault(),
                Answer_E_Correct = pytanie.Answer_E_Correct.GetValueOrDefault(),
                Answer_F_Correct = pytanie.Answer_F_Correct.GetValueOrDefault()
            },
            Explanation = string.Empty, // Ustaw, jak jest zdefiniowane w lokalnej bazie danych
            Tip = string.Empty, // Ustaw, jak jest zdefiniowane w lokalnej bazie danych
            Tags = null,
            Category = pytanie.Kategoria.Nazwa,
            Difficulty = pytanie.PoziomTrudnosci
        };
    }
}
