using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecordOther
{
    public int Id { get; set; }

    public int? Personid { get; set; }

    public int? Trainingrecordid { get; set; }

    public string Buid { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? Description { get; set; }
}
