using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecordPatent
{
    public int Id { get; set; }

    public int? Personid { get; set; }

    public string? Trainingrecordid { get; set; }

    public string? Buid { get; set; }

    public string Inventors { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateOnly? Dateissued { get; set; }
}
