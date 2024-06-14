using System;
using System.Collections.Generic;

namespace TestKomgrip.Models_DataSQLServer;

public partial class TbLogin
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }
}
