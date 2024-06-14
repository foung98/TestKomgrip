using System;
using System.Collections.Generic;

namespace TestKomgrip.Models_DataSQLServer;

public partial class TbTimeLog
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public DateTime? LastLogin { get; set; }
}
