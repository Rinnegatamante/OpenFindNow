// Decompiled with JetBrains decompiler
// Type: MainExe.DataBase
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading;

namespace MainExe
{
  public static class DataBase
  {
    public static int counterTrans = 0;
    public static string DB_PATH = "";
    public static string DB_PATH_LOG = "";
    private static object command_Lock_ = new object();
    public static SQLiteConnection myConnection;
    public static SQLiteTransaction myTransaction;
    public static SQLiteCommand command;

    public static void InitDB()
    {
      DataBase.myConnection = new SQLiteConnection("Data Source=" + DataBase.DB_PATH);
      DataBase.command = new SQLiteCommand(DataBase.myConnection);
      try
      {
        Monitor.Enter((object) DataBase.myConnection);
        DataBase.myConnection.Open();
      }
      catch (Exception ex)
      {
        throw new Exception("Error reading DB:" + ex.Message);
      }
    }

    public static void DeInitDB()
    {
      try
      {
        DataBase.myConnection.Close();
        DataBase.myConnection.Dispose();
        Monitor.Exit((object) DataBase.myConnection);
      }
      catch (Exception ex)
      {
        throw new Exception("Error reading DB:" + ex.Message);
      }
    }

    public static int getNumberOfLaunches()
    {
      return int.Parse(DataBase.getParameter("application", "nLaunches"));
    }

    public static void increaseNumberOfLaunches()
    {
      DataBase.setParameter("application", "nLaunches", (DataBase.getNumberOfLaunches() + 1).ToString());
    }

    public static string getParameter(string table, string name)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          DataBase.command.CommandText = "SELECT value FROM " + table + " WHERE name = '" + DataBase.correctSingleQuotation(name) + "'";
          SQLiteDataReader local_0 = DataBase.command.ExecuteReader();
          local_0.Read();
          string local_1 = local_0[0] != DBNull.Value ? local_0.GetString(0) : "";
          local_0.Close();
          return local_1;
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void delParameter(string table, string name)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          DataBase.command.CommandText = "DELETE FROM " + table + " WHERE name = '" + DataBase.correctSingleQuotation(name) + "'";
          DataBase.command.ExecuteNonQuery();
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void BeginTrans()
    {
      ++DataBase.counterTrans;
      if (DataBase.counterTrans >= 2)
        return;
      bool flag = true;
      while (flag)
      {
        try
        {
          DataBase.myTransaction = DataBase.myConnection.BeginTransaction(IsolationLevel.Unspecified);
          DataBase.command.Transaction = DataBase.myTransaction;
          flag = false;
        }
        catch (Exception ex)
        {
          flag = true;
          Thread.Sleep(100);
        }
      }
    }

    public static void EndTrans()
    {
      --DataBase.counterTrans;
      if (DataBase.counterTrans >= 1)
        return;
      bool flag = true;
      while (flag)
      {
        try
        {
          DataBase.myTransaction.Commit();
          flag = false;
        }
        catch (Exception ex)
        {
          flag = true;
          Thread.Sleep(100);
        }
      }
    }

    public static void setParameter(string table, string name, string value)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          DataBase.command.CommandText = "UPDATE " + table + " SET value = '" + DataBase.correctSingleQuotation(value) + "' WHERE name = '" + DataBase.correctSingleQuotation(name) + "'";
          if (DataBase.command.ExecuteNonQuery() >= 1)
            return;
          DataBase.command.CommandText = "INSERT INTO " + table + " VALUES ('" + DataBase.correctSingleQuotation(name) + "', '" + DataBase.correctSingleQuotation(value) + "')";
          DataBase.command.ExecuteNonQuery();
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void delAllEntriesWhere(string table, string Where)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_1 = "DELETE FROM " + table + " WHERE " + Where;
          DataBase.command.CommandText = local_1;
          DataBase.command.ExecuteNonQuery();
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    private static string correctSingleQuotation(string valueString)
    {
      char[] charArray = valueString.ToCharArray();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < charArray.Length; ++index)
      {
        stringBuilder.Append(charArray[index]);
        if ((int) charArray[index] == 39)
          stringBuilder.Append(charArray[index]);
      }
      return stringBuilder.ToString();
    }

    public static string extractPassword(string passwordAndKey)
    {
      return passwordAndKey.Substring(0, passwordAndKey.Length - passwordAndKey.Length / 2);
    }

    public static string extractKey(string passwordAndKey)
    {
      return passwordAndKey.Substring(passwordAndKey.Length - passwordAndKey.Length / 2, passwordAndKey.Length / 2);
    }

    public static void CreateTable(string NameTable, string[] fields, string[] typefields)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_0 = "CREATE TABLE " + NameTable + " (";
          for (int local_1 = 0; local_1 < fields.Length - 1; ++local_1)
            local_0 = local_0 + fields[local_1] + " " + typefields[local_1] + ",";
          string local_0_1 = local_0 + fields[fields.Length - 1] + " " + typefields[fields.Length - 1] + ")";
          DataBase.command.CommandText = local_0_1;
          DataBase.command.ExecuteNonQuery();
        }
        catch (SQLiteException exception_0)
        {
        }
      }
    }

    public static ArrayList[] GetAllRecords(string NameTable, string OrderBy, bool from_LOG)
    {
      return DataBase.GetAllRecords(NameTable, OrderBy, "", from_LOG);
    }

    public static ArrayList[] GetAllRecords(string NameTable, string OrderBy, string Where, bool from_LOG)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_0 = "SELECT * FROM " + NameTable;
          if (!Where.Equals(""))
            local_0 = local_0 + " WHERE " + Where;
          if (!OrderBy.Equals(""))
            local_0 = local_0 + " ORDER BY " + OrderBy;
          DataBase.command.CommandText = local_0;
          SQLiteDataReader local_2 = DataBase.command.ExecuteReader();
          ArrayList[] local_3 = new ArrayList[local_2.FieldCount];
          for (int local_4 = 0; local_4 < local_2.FieldCount; ++local_4)
            local_3[local_4] = new ArrayList();
          while (local_2.Read())
          {
            for (int local_4_1 = 0; local_4_1 < local_2.FieldCount; ++local_4_1)
            {
              if (local_2[local_4_1] == DBNull.Value)
                local_3[local_4_1].Add((object) "");
              else if (local_2[local_4_1].GetType().Name.Equals("Int64"))
              {
                try
                {
                  local_3[local_4_1].Add((object) local_2.GetInt64(local_4_1));
                }
                catch (Exception exception_1)
                {
                  local_3[local_4_1].Add((object) 0);
                }
              }
              else
                local_3[local_4_1].Add((object) local_2.GetString(local_4_1));
            }
          }
          local_2.Close();
          return local_3;
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static int CountRecords(string NameTable, string OrderBy, string Where, bool from_LOG)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_0_2 = "SELECT * FROM " + NameTable + " " + Where + " " + OrderBy;
          DataBase.command.CommandText = local_0_2;
          SQLiteDataReader local_2 = DataBase.command.ExecuteReader();
          int local_3 = 0;
          while (local_2.Read())
            ++local_3;
          local_2.Close();
          return local_3;
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void CheckTable(string NameTable)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_0 = "SELECT * FROM " + NameTable;
          DataBase.command.CommandText = local_0;
          DataBase.command.ExecuteReader().Close();
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void AddUpdRecords(string NameTable, ArrayList[] fields, int maxRec, bool delFlag)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          int local_0 = 0;
          if (maxRec == -1)
            maxRec = fields.Length;
          for (int local_2 = 0; local_2 < maxRec; ++local_2)
          {
            if (delFlag)
            {
              string local_1_2 = "DELETE FROM " + NameTable + " WHERE ID='" + DataBase.correctSingleQuotation(fields[local_2][0].ToString()) + "'";
              DataBase.command.CommandText = local_1_2;
              local_0 = DataBase.command.ExecuteNonQuery();
            }
            string local_1_3 = "INSERT INTO " + NameTable + " VALUES (";
            for (int local_3 = 0; local_3 < fields[0].Count - 1; ++local_3)
              local_1_3 = local_1_3 + "'" + DataBase.correctSingleQuotation(fields[local_2][local_3].ToString()) + "', ";
            string local_1_4 = local_1_3 + "'" + DataBase.correctSingleQuotation((string) fields[local_2][fields[0].Count - 1]) + "')";
            DataBase.command.CommandText = local_1_4;
            local_0 = DataBase.command.ExecuteNonQuery();
          }
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void AddRecord(string NameTable, ArrayList[] fields)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          int local_0 = 0;
          for (int local_2 = 0; local_2 < fields.Length; ++local_2)
          {
            string local_1_1 = "INSERT INTO " + NameTable + " VALUES (";
            for (int local_3 = 0; local_3 < fields[0].Count - 1; ++local_3)
              local_1_1 = local_1_1 + "'" + DataBase.correctSingleQuotation((string) fields[local_2][local_3]) + "', ";
            string local_1_2 = local_1_1 + "'" + DataBase.correctSingleQuotation((string) fields[local_2][fields[0].Count - 1]) + "')";
            DataBase.command.CommandText = local_1_2;
            local_0 = DataBase.command.ExecuteNonQuery();
          }
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }

    public static void DelRecord(string NameTable, string ID)
    {
      lock (DataBase.command_Lock_)
      {
        try
        {
          string local_0 = "DELETE FROM " + NameTable + " WHERE ID = '" + DataBase.correctSingleQuotation(ID) + "'";
          DataBase.command.CommandText = local_0;
          DataBase.command.ExecuteNonQuery();
        }
        catch (SQLiteException exception_0)
        {
          throw new Exception("Error reading DB:" + exception_0.Message);
        }
      }
    }
  }
}
