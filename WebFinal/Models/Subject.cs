using System;
using System.Collections.Generic;

namespace WebFinal.Models;

public partial class Subject
{
    public long Id { get; set; }

    public string? SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public byte? Credits { get; set; }
}
