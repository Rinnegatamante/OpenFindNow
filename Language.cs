// Decompiled with JetBrains decompiler
// Type: MainExe.Language
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;

namespace MainExe
{
  public class Language
  {
    public static string CurrLanguageSTR = "English";
    public static int CurrLanguage = 0;
    public static int MaxLanguages = 2;

    public static void setCurrLanguage(int nLang)
    {
      Language.CurrLanguage = nLang % Language.MaxLanguages;
      Language.CurrLanguageSTR = ((Language.Languages) Language.CurrLanguage).ToString();
      Navigator.BLan = new Bitmap(Services.getResource("Languages\\" + Language.CurrLanguageSTR + "\\Ban.gif"));
      DataBase.setParameter("application", "language", Language.CurrLanguage.ToString());
    }

    public static ArrayList loadFormStrings(string formName, int nLang)
    {
      Language.CurrLanguage = nLang;
      string @string = ((Language.Languages) nLang).ToString();
      ArrayList arrayList = (ArrayList) null;
      StreamReader streamReader = (StreamReader) null;
      Stream stream = (Stream) null;
      try
      {
        stream = Services.getResource("Languages\\" + @string + "\\" + formName + ".txt");
        if (stream == null)
          throw new Exception("Language file missing");
        Encoding fileEncoding = Language.GetFileEncoding(stream);
        stream.ReadByte();
        stream.Seek(0L, SeekOrigin.Begin);
        streamReader = new StreamReader(stream, fileEncoding);
        arrayList = new ArrayList();
        int num = 0;
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
          arrayList.Add((object) str);
          ++num;
        }
      }
      catch (Exception ex)
      {
        return (ArrayList) null;
      }
      finally
      {
        if (stream != null)
          stream.Close();
        if (streamReader != null)
          streamReader.Close();
      }
      return arrayList;
    }

    private static Encoding GetFileEncoding(Stream FStream)
    {
      Encoding encoding = (Encoding) null;
      try
      {
        Encoding[] encodingArray = new Encoding[3]
        {
          Encoding.BigEndianUnicode,
          Encoding.Unicode,
          Encoding.UTF8
        };
        for (int index1 = 0; encoding == null && index1 < encodingArray.Length; ++index1)
        {
          FStream.Position = 0L;
          byte[] preamble = encodingArray[index1].GetPreamble();
          bool flag = true;
          for (int index2 = 0; flag && index2 < preamble.Length; ++index2)
            flag = (int) preamble[index2] == FStream.ReadByte();
          if (flag)
            encoding = encodingArray[index1];
        }
      }
      catch (IOException ex)
      {
      }
      finally
      {
        if (encoding == null)
          encoding = Encoding.Default;
      }
      return encoding;
    }

    public enum Languages
    {
      English,
      Italiano,
    }
  }
}
