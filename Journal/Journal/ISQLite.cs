using System;
using System.Collections.Generic;
using System.Text;

namespace Journal
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
