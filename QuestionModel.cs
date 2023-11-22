using System;
using System.Collections.Generic;
public class QuestionModel
{


    public int Id { get; set; }
    public string Question { get; set; }
    public string Description { get; set; }
    public AnswersModel Answers { get; set; }
    public bool MultipleCorrectAnswers { get; set; }
    public CorrectAnswersModel Correct_Answers { get; set; }
    public string Correct_Answer { get; set; }
    public string Explanation { get; set; }
    public string Tip { get; set; }
    public List<TagModel> Tags { get; set; }
    public string Category { get; set; }
    public string Difficulty { get; set; }
    public override string ToString()
    {
        return "Treść: " + Question + "\n" + "Odpowiedź A: " + Answers.Answer_A + "\n" + "Odpowiedź B: " + Answers.Answer_B + "\n" + "Odpowiedź C: " + Answers.Answer_C + "\n" + "Odpowiedź D: " + Answers.Answer_D + "\n" + "Odpowiedź E: " + Answers.Answer_E + "\n" + "Odpowiedź F: " + Answers.Answer_F + "\n";
    }
}

public class AnswersModel
{
    public string Answer_A { get; set; }
    public string Answer_B { get; set; }
    public string Answer_C { get; set; }
    public string Answer_D { get; set; }
    public string Answer_E { get; set; }
    public string Answer_F { get; set; }

}

public class CorrectAnswersModel
{
    public bool Answer_A_Correct { get; set; }
    public bool Answer_B_Correct { get; set; }
    public bool Answer_C_Correct { get; set; }
    public bool Answer_D_Correct { get; set; }
    public bool Answer_E_Correct { get; set; }
    public bool Answer_F_Correct { get; set; }
}

public class TagModel
{
    public string Name { get; set; }


}







