using System;
using System.Collections.Generic;

namespace TrainingWEBAPI.Models;

public partial class Question
{
    public int QnId { get; set; }

    public int? QuizId { get; set; }

    public string? Question1 { get; set; }

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public string? CorrectAnswer { get; set; }

    public virtual ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();

    public virtual Quiz? Quiz { get; set; }
}
