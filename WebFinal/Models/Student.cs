using System;
using System.Collections.Generic;

namespace WebFinal.Models;

public partial class Student
{
    public long Id { get; set; }

    public string? StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? StudentPhone { get; set; }

    public string? StudentTown { get; set; }

    public string? StudentGender { get; set; }

    public string? StudentEmail { get; set; }
}
