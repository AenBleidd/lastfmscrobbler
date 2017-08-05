using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;

namespace liblastfmscrobbler
{
  public class DbReader
  {
    DbProviderFactory factory = null;
    SQLiteConnection connection = null;
    public void Connect(string PathToLibrary)
    {
      if (factory == null)
        factory = DbProviderFactories.GetFactory("System.Data.SQLite");
      if (connection != null)
        CloseConnection();
      connection = factory.CreateConnection() as SQLiteConnection;
      connection.ConnectionString = "Data Source = " + PathToLibrary;
      connection.Open();
      return;
    }

    public void CloseConnection()
    {
      if (connection != null)
        connection.Close();
      connection = null;
    }

    public IList<DbDataRecord> ExecuteCommand(string query, Dictionary<string, SQLiteParameter> parameters = null)
    {
      if (query == null)
        return null;

      var command = new SQLiteCommand(query, connection);
      if (parameters != null)
      {
        foreach (var p in parameters)
        {
          command.Parameters.Add(p.Value);
        }
      }
      var reader = command.ExecuteReader();
      if (reader.HasRows)
        return reader.Cast<DbDataRecord>().ToList();

      return null;
    }

    public DataTable ExecuteQuery(string querySelect = null,
                                  Dictionary<string, DbParameter> parametersSelect = null,
                                  string queryInsert = null,
                                  Dictionary<string, DbParameter> parametersInsert = null,
                                  string queryUpdate = null,
                                  Dictionary<string, DbParameter> parametersUpdate = null,
                                  string queryDelete = null,
                                  Dictionary<string, DbParameter> parametersDelete = null)
    {
      if (querySelect == null && queryInsert == null && queryUpdate == null && queryDelete == null)
        return null;

      var adapter = new SQLiteDataAdapter();

      if (querySelect != null)
      {
        var select = new SQLiteCommand(querySelect, connection);
        if (parametersSelect != null)
        {
          foreach (var p in parametersSelect)
          {
            select.Parameters.Add(p.Value);
          }
        }
        adapter.SelectCommand = select;
      }

      if (queryInsert != null)
      {
        var insert = new SQLiteCommand(queryInsert, connection);
        if (parametersInsert != null)
        {
          foreach (var p in parametersInsert)
          {
            insert.Parameters.Add(p.Value);
          }
        }
        adapter.InsertCommand = insert;
      }

      if (queryUpdate != null)
      {
        var update = new SQLiteCommand(queryUpdate, connection);
        if (parametersUpdate != null)
        {
          foreach (var p in parametersUpdate)
          {
            update.Parameters.Add(p.Value);
          }
        }
        adapter.UpdateCommand = update;
      }

      if (queryDelete != null)
      {
        var delete = new SQLiteCommand(queryDelete, connection);
        if (parametersDelete != null)
        {
          foreach (var p in parametersDelete)
          {
            delete.Parameters.Add(p.Value);
          }
        }
        adapter.DeleteCommand = delete;
      }

      var table = new DataTable();
      adapter.Fill(table);
      return table;
    }
  }
}
