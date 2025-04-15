using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecordManuscript
{
    public int Id { get; set; }

    public string Buid { get; set; } = null!;

    public int Trainingrecordid { get; set; }

    public string Authors { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Journal { get; set; } = null!;

    public string? Year { get; set; }

    public string? Volume { get; set; }

    public string? Pages { get; set; }

    public string? DoiPmidPmcid { get; set; }

    public string? Status { get; set; }

    public string? Statusother { get; set; }
}
