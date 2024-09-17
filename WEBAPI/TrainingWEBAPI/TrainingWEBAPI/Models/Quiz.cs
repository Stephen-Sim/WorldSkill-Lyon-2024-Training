using System;
using System.Collections.Generic;

namespace TrainingWEBAPI.Models;

public partial class Quiz
{
    public int QuizId { get; set; }

    public string? Title { get; set; }

    public string? ForExpert { get; set; }

    public string? ForCompetitor { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
