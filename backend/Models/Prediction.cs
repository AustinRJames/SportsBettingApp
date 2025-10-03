using System;
using System.Collections.Generic;

namespace MyBackend.Models;

public partial class Prediction
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? MatchId { get; set; }

    public int? PointsBet { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ScoreA { get; set; }

    public int? ScoreB { get; set; }

    public bool? IsSettled { get; set; }

    public bool? IsWin { get; set; }

    public virtual Match? Match { get; set; }

    public virtual User? User { get; set; }
}
