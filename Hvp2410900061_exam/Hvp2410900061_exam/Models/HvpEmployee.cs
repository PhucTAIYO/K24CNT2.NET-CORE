using System;
using System.Collections.Generic;

namespace Hvp2410900061_exam.Models;

public partial class HvpEmployee
{
    public long Id { get; set; }

    public string? HvpName { get; set; }

    public bool? HvpGender { get; set; }

    public DateOnly? HvpBirthDay { get; set; }

    public string? HvpEmail { get; set; }

    public int? HvpPhone { get; set; }

    public string? HvpActive { get; set; }
}
