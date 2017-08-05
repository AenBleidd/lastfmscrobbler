using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace liblastfmscrobbler
{
  public class DbLibraryReader : DbReader
  {
    const string DBLibraryName = "Library.itdb";
    public DbLibraryReader(string path)
    {
      Connect(Path.Combine(path, DBLibraryName));
    }
    public IList<DbDataRecord> GetItemList()
    {
      const string query = @"select * from item;";
      return ExecuteCommand(query);
    }
  }
}
