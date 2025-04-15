using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecordAbstract
{
    public int Id { get; set; }

    public int? Trainingrecordid { get; set; }

    public string Buid { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Authors { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Conference { get; set; }

    public string? City { get; set; }

    public string? Date { get; set; }

    public string? PosterOral { get; set; }

    public string? Presenter { get; set; }
}
