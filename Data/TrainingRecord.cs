using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecord
{
    public int Id { get; set; }

    public int? Personid { get; set; }

    public string Buid { get; set; } = null!;

    public string Researchfaculty { get; set; } = null!;

    public string? Trainingstartmm { get; set; }

    public string? Trainingstartyy { get; set; }

    public string? Trainingendmm { get; set; }

    public string? Trainingendyy { get; set; }

    public string? Stilltrainee { get; set; }

    public string? Status { get; set; }

    public string? Statusother { get; set; }

    public string? Presentations { get; set; }
}
