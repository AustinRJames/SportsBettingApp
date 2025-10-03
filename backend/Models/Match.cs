using System;
using System.Collections.Generic;

namespace MyBackend.Models;

public partial class Match
{
    public int Id { get; set; }

    public string TeamA { get; set; } = null!;

    public string TeamB { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? ScoreA { get; set; }

    public int? ScoreB { get; set; }

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}
