using System;
using System.Collections.Generic;

namespace WebFinal.Models;

public partial class Lecturer
{
    public long Id { get; set; }

    public string? LecturerId { get; set; }

    public string? LecturerName { get; set; }

    public string? LecturerPhone { get; set; }

    public string? LecturerEmail { get; set; }
}
