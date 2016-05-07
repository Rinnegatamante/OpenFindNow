// Decompiled with JetBrains decompiler
// Type: MainExe.ZipUtil
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.IO;

namespace MainExe
{
  public static class ZipUtil
  {
    public static void ZipFiles(string inputFolderPath, string outputPathAndFile, string password)
    {
      ArrayList fileList = ZipUtil.GenerateFileList(inputFolderPath);
      int count = Directory.GetParent(inputFolderPath).ToString().Length + 1;
      ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) File.Create(inputFolderPath + "\\" + outputPathAndFile));
      if (password != null && password != string.Empty)
        zipOutputStream.Password = password;
      zipOutputStream.SetLevel(9);
      foreach (string path in fileList)
      {
        ZipEntry entry = new ZipEntry(path.Remove(0, count));
        zipOutputStream.PutNextEntry(entry);
        if (!path.EndsWith("/"))
        {
          FileStream fileStream = File.OpenRead(path);
          byte[] buffer = new byte[fileStream.Length];
          fileStream.Read(buffer, 0, buffer.Length);
          zipOutputStream.Write(buffer, 0, buffer.Length);
        }
      }
      zipOutputStream.Finish();
      zipOutputStream.Close();
    }

    private static ArrayList GenerateFileList(string Dir)
    {
      ArrayList arrayList = new ArrayList();
      bool flag = true;
      foreach (string file in Directory.GetFiles(Dir))
      {
        arrayList.Add((object) file);
        flag = false;
      }
      if (flag && Directory.GetDirectories(Dir).Length == 0)
        arrayList.Add((object) (Dir + "/"));
      foreach (string directory in Directory.GetDirectories(Dir))
      {
        foreach (object file in ZipUtil.GenerateFileList(directory))
          arrayList.Add(file);
      }
      return arrayList;
    }

    public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
    {
      ZipInputStream zipInputStream = new ZipInputStream((Stream) File.OpenRead(zipPathAndFile));
      if (password != null && password != string.Empty)
        zipInputStream.Password = password;
      string str = string.Empty;
      ZipEntry nextEntry;
      while ((nextEntry = zipInputStream.GetNextEntry()) != null)
      {
        string path1 = outputFolder;
        string fileName = Path.GetFileName(nextEntry.Name);
        if (path1 != "")
          Directory.CreateDirectory(path1);
        if (fileName != string.Empty && nextEntry.Name.IndexOf(".ini") < 0)
        {
          string path2 = (path1 + "\\" + nextEntry.Name).Replace("\\ ", "\\");
          string directoryName = Path.GetDirectoryName(path2);
          if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
          FileStream fileStream = File.Create(path2);
          byte[] buffer = new byte[2048];
          while (true)
          {
            int count = zipInputStream.Read(buffer, 0, buffer.Length);
            if (count > 0)
              fileStream.Write(buffer, 0, count);
            else
              break;
          }
          fileStream.Close();
        }
      }
      zipInputStream.Close();
      if (!deleteZipFile)
        return;
      File.Delete(zipPathAndFile);
    }

    public static ArrayList ListZipFiles(string zipPathAndFile, string password)
    {
      ArrayList arrayList = new ArrayList();
      ZipInputStream zipInputStream = new ZipInputStream((Stream) File.OpenRead(zipPathAndFile));
      if (password != null && password != string.Empty)
        zipInputStream.Password = password;
      string str = string.Empty;
      ZipEntry nextEntry;
      while ((nextEntry = zipInputStream.GetNextEntry()) != null)
        arrayList.Add((object) nextEntry);
      zipInputStream.Close();
      return arrayList;
    }
  }
}
