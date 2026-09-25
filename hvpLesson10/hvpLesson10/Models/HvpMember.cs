using System;
using System.Collections.Generic;

namespace hvpLesson10.Models;

public partial class HvpMember
{
    public long Id { get; set; }

    public string? HvpUserName { get; set; }

    public string? HvpPassword { get; set; }

    public string? HvpFullname { get; set; }

    public string? HvpEmail { get; set; }

    public string? HvpPhone { get; set; }

    public bool? HvpStatus { get; set; }
}
