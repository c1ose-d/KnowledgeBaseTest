using System;
using System.Collections.Generic;

namespace DatabaseLibrary;

public partial class Tag
{
    public Guid RowId { get; set; }

    public string Value { get; set; } = null!;
}
