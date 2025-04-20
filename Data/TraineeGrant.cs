using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TraineeGrant
{
    public int Id { get; set; }

    public int? Personid { get; set; }

    public string Buid { get; set; } = null!;

    public string Researchfaculty { get; set; } = null!;

    public DateOnly? Trainingstartmm { get; set; }

    public DateOnly? Trainingstartyy { get; set; }

    public DateOnly? Trainingendmm { get; set; }

    public DateOnly? Trainingendyy { get; set; }

    public string? Stilltrainee { get; set; }

    public string? Status { get; set; }

    public string? Presentations { get; set; }

    public int? Trainingrantid { get; set; }

    public string? Statusother { get; set; }

    public string? Title { get; set; }
}
