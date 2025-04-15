using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class TrainingRecordVideo
{
    public int Id { get; set; }

    public int? Personid { get; set; }

    public string? Buid { get; set; }

    public int? Trainingrecordid { get; set; }

    public string Authors { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Website { get; set; }

    public DateOnly? Dateuploaded { get; set; }

    public string? Url { get; set; }
}
