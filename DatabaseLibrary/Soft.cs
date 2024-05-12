using System;
using System.Collections.Generic;

namespace DatabaseLibrary;

public partial class Soft
{
    public Guid RowId { get; set; }

    public string Value { get; set; } = null!;
}
