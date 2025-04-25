using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class AdminReportsManuscript
{
    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Expr2 { get; set; }

    public string? Expr1 { get; set; }

    public string? Orcidid { get; set; }

    public string Citizenship { get; set; } = null!;

    public string? Position { get; set; }

    public string? Positionother { get; set; }

    public string? Acceptedpredoc { get; set; }

    public string? Leaveofabsence { get; set; }

    public string? Minority { get; set; }

    public string? Disability { get; set; }

    public string? Woman { get; set; }

    public string? Facultymentor { get; set; }

    public string? Yearsposition { get; set; }

    public DateOnly? Dateadded { get; set; }

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
