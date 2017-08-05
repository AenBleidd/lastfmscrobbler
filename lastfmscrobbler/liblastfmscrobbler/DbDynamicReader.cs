using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace liblastfmscrobbler
{
  public class DbDynamicReader : DbReader
  {
    const string DBLibraryName = "Dynamic.itdb";
    public DbDynamicReader(string path)
    {
      Connect(Path.Combine(path, DBLibraryName));
    }
    public IList<DbDataRecord> GetItemStatsList()
    {
      const string query = @"select * from item_stats;";
      return ExecuteCommand(query);
    }
  }
}
