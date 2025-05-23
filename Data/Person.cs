﻿using System;
using System.Collections.Generic;

namespace T32_TraineeGrant;

public partial class Person
{
    public int Id { get; set; }

    public string Buid { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Email { get; set; } = null!;

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
}
