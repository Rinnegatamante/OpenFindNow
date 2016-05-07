// Decompiled with JetBrains decompiler
// Type: MainExe.Services
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using Dotnetrix.Controls;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainExe
{
  public static class Services
  {
    public static ArrayList UsedFormList = new ArrayList();
    internal const int SE_PRIVILEGE_ENABLED = 2;
    internal const int TOKEN_QUERY = 8;
    internal const int TOKEN_ADJUST_PRIVILEGES = 32;
    internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
    internal const int EWX_LOGOFF = 0;
    internal const int EWX_SHUTDOWN = 1;
    internal const int EWX_REBOOT = 2;
    internal const int EWX_FORCE = 4;
    internal const int EWX_POWEROFF = 8;
    internal const int EWX_FORCEIFHUNG = 16;
    public const uint LVM_FIRST = 4096;
    public const uint LVM_GETITEMCOUNT = 4100;
    public const uint LVM_GETITEMW = 4171;
    public const uint LVM_GETITEMPOSITION = 4112;
    public const uint PROCESS_VM_OPERATION = 8;
    public const uint PROCESS_VM_READ = 16;
    public const uint PROCESS_VM_WRITE = 32;
    public const uint MEM_COMMIT = 4096;
    public const uint MEM_RELEASE = 32768;
    public const uint MEM_RESERVE = 8192;
    public const uint PAGE_READWRITE = 4;
    public const int LVIF_TEXT = 1;
    public static Icon MIco;
    public static Bitmap BFac;
    public static Bitmap BImg;
    public static Color MainColor_Fore_Label;
    public static Color MainColor_Back_Label;
    public static Color MainColor_Fore_Menu;
    public static Color MainColor_BackTabPage;
    public static Color MainColor_BackCamScreen;
    public static Color MainColor_Fore_Button;
    public static Color MainColor_Fore_Text;
    public static Color MainColor_Back_Text;
    public static FileInfo CurrFileInfo;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref Services.IconInfo pIconInfo);

    [DllImport("user32.dll")]
    public static extern IntPtr CreateIconIndirect(ref Services.IconInfo icon);

    public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
    {
      IntPtr hicon = bmp.GetHicon();
      Services.IconInfo iconInfo = new Services.IconInfo();
      Services.GetIconInfo(hicon, ref iconInfo);
      iconInfo.xHotspot = xHotSpot;
      iconInfo.yHotspot = yHotSpot;
      iconInfo.fIcon = false;
      return new Cursor(Services.CreateIconIndirect(ref iconInfo));
    }

    public static void GetFileSubFolders(ref ArrayList FileList, string CurrFolder, string pattern)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo(CurrFolder);
      DirectoryInfo[] directories = directoryInfo1.GetDirectories();
      FileInfo[] files = directoryInfo1.GetFiles(pattern);
      if (directories.Length > 0)
      {
        foreach (DirectoryInfo directoryInfo2 in directories)
          Services.GetFileSubFolders(ref FileList, directoryInfo2.FullName, pattern);
      }
      if (files.Length <= 0)
        return;
      foreach (FileInfo fileInfo in files)
      {
        try
        {
          Services.CurrFileInfo = fileInfo;
          FileList.Add((object) fileInfo);
        }
        catch
        {
        }
      }
    }

    public static long FileSize(string FileName)
    {
      try
      {
        return new FileInfo(FileName).Length;
      }
      catch
      {
      }
      return 0;
    }

    public static Stream getResource(string resourceName)
    {
      Assembly.GetExecutingAssembly();
      return new StreamReader(Path.Combine(Application.StartupPath, "Resources\\" + resourceName)).BaseStream;
    }

    public static void DateTimeToString(DateTime currDateTime, ref string DateString, ref string TimeString)
    {
      string str1 = "00" + (object) currDateTime.Hour;
      string str2 = "00" + (object) currDateTime.Minute;
      string str3 = "00" + (object) currDateTime.Second;
      string str4 = str1.Substring(str1.Length - 2, 2);
      string str5 = str2.Substring(str2.Length - 2, 2);
      string str6 = str3.Substring(str3.Length - 2, 2);
      TimeString = str4 + ":" + str5 + ":" + str6;
      string str7 = "0000" + (object) currDateTime.Year;
      string str8 = "00" + (object) currDateTime.Month;
      string str9 = "00" + (object) currDateTime.Day;
      string str10 = str7.Substring(str7.Length - 4, 4);
      string str11 = str8.Substring(str8.Length - 2, 2);
      string str12 = str9.Substring(str9.Length - 2, 2);
      DateString = str10 + "/" + str11 + "/" + str12;
    }

    public static string GetMACAddress()
    {
      ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
      string str = string.Empty;
      foreach (ManagementObject managementObject in instances)
      {
        if (str == string.Empty && (bool) managementObject["IPEnabled"])
          str = managementObject["MacAddress"].ToString();
        managementObject.Dispose();
      }
      return str.Replace(":", "");
    }

    public static long getDiskFreeSpaceInMB(string recordingPath)
    {
      long num = 0;
      if (recordingPath.StartsWith("\\\\"))
        return num;
      try
      {
        DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(recordingPath));
        if (driveInfo.IsReady)
          num = driveInfo.AvailableFreeSpace / 1048576L;
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static long getDiskFreeSpaceInMB()
    {
      return Services.getDiskFreeSpaceInMB(Application.StartupPath);
    }

    public static byte[] shortToBytes(short value)
    {
      ushort num = (ushort) value;
      return new byte[2]
      {
        (byte) ((uint) num >> 8),
        (byte) num
      };
    }

    public static string NormalizedTime(string inString)
    {
      string str1 = "00:00:00";
      try
      {
        int result1 = 0;
        int result2 = 0;
        int result3 = 0;
        string str2 = "00";
        int length1 = inString.IndexOf(":");
        if (length1 > -1)
        {
          string s1 = inString.Substring(0, length1);
          int.TryParse(s1, out result1);
          int num = inString.IndexOf(":", length1 + 1);
          if (num > -1)
          {
            string s2 = inString.Substring(length1 + 1, num - length1 - 1);
            int.TryParse(s2, out result2);
            int length2 = inString.Length;
            if (length2 > -1)
            {
              int.TryParse(inString.Substring(num + 1, length2 - num - 1), out result3);
              string str3 = "00" + result1.ToString();
              string str4 = "00" + result2.ToString();
              string str5 = "00" + result3.ToString();
              s1 = str3.Substring(str3.Length - 2, 2);
              s2 = str4.Substring(str4.Length - 2, 2);
              str2 = str5.Substring(str5.Length - 2, 2);
            }
            str1 = s1 + ":" + s2 + ":" + str2;
          }
        }
      }
      catch (Exception ex)
      {
      }
      return str1;
    }

    public static int ConvertToMillisec(string TimeIn)
    {
      int num = 0;
      int result1 = 0;
      int result2 = 0;
      int result3 = 0;
      int.TryParse(TimeIn.Substring(0, 2), out result1);
      int.TryParse(TimeIn.Substring(3, 2), out result2);
      int.TryParse(TimeIn.Substring(6, 2), out result3);
      return num + result1 * 60 * 60 * 1000 + result2 * 60 * 1000 + result3 * 1000;
    }

    public static string ConvertToHour(int millisec)
    {
      int num1 = millisec / 1000;
      int num2 = num1 / 3600 % 24;
      int num3 = num1 / 60 % 60;
      int num4 = num1 % 60;
      string str1 = "0" + (object) num2;
      string str2 = "0" + (object) num3;
      string str3 = "0" + (object) num4;
      return str1.Substring(str1.Length - 2, 2) + ":" + str2.Substring(str2.Length - 2, 2) + ":" + str3.Substring(str3.Length - 2, 2);
    }

    public static void fade(Form myForm, int fadeSpeed)
    {
      int num1 = Math.Abs(fadeSpeed);
      int num2 = 0;
      while (num2 < 100 + num1)
      {
        myForm.Opacity = fadeSpeed <= 0 ? (double) (100 - num2) / 100.0 : (double) num2 / 100.0;
        myForm.Refresh();
        Thread.Sleep(50);
        num2 += num1;
      }
    }

    private static int getOSArchitecture()
    {
      string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
      return string.IsNullOrEmpty(environmentVariable) || string.Compare(environmentVariable, 0, "x86", 0, 3, true) == 0 ? 32 : 64;
    }

    public static string getOSInfo()
    {
      OperatingSystem osVersion = Environment.OSVersion;
      Version version = osVersion.Version;
      string str1 = "";
      if (osVersion.Platform == PlatformID.Win32Windows)
      {
        switch (version.Minor)
        {
          case 0:
            str1 = "95";
            break;
          case 10:
            str1 = !(version.Revision.ToString() == "2222A") ? "98" : "98SE";
            break;
          case 90:
            str1 = "Me";
            break;
        }
      }
      else if (osVersion.Platform == PlatformID.Win32NT)
      {
        switch (version.Major)
        {
          case 3:
            str1 = "NT 3.51";
            break;
          case 4:
            str1 = "NT 4.0";
            break;
          case 5:
            str1 = version.Minor != 0 ? "XP" : "2000";
            break;
          case 6:
            str1 = version.Minor != 0 ? "7" : "Vista";
            break;
        }
      }
      if (str1 != "")
      {
        string str2 = "Windows " + str1;
        if (osVersion.ServicePack != "")
          str2 = str2 + " " + osVersion.ServicePack;
        str1 = str2 + " " + Services.getOSArchitecture().ToString() + "-bit";
      }
      return str1;
    }

    public static void UsedFormListAdd(Form myForm)
    {
      if (Services.UsedFormList.Contains((object) myForm))
        return;
      Services.UsedFormList.Add((object) myForm);
    }

    public static void SaveAllFormLayout()
    {
      int index = 0;
      DataBase.BeginTrans();
      for (; index < Services.UsedFormList.Count; ++index)
        Services.SaveFormLayout((Form) Services.UsedFormList[index]);
      DataBase.EndTrans();
    }

    public static void SaveFormLayout(Form currForm)
    {
      DataBase.BeginTrans();
      string name = currForm.Name;
      int left = currForm.Left;
      int top = currForm.Top;
      int width = currForm.Width;
      int height = currForm.Height;
      if (currForm.WindowState != FormWindowState.Minimized)
      {
        DataBase.setParameter("application", name + "_X", left.ToString());
        DataBase.setParameter("application", name + "_Y", top.ToString());
        DataBase.setParameter("application", name + "_W", width.ToString());
        DataBase.setParameter("application", name + "_H", height.ToString());
      }
      DataBase.EndTrans();
    }

    public static void LoadFormLayout(Form currForm)
    {
      if (DataBase.myConnection == null)
        return;
      bool flag = currForm.FormBorderStyle == FormBorderStyle.Sizable || currForm.FormBorderStyle == FormBorderStyle.SizableToolWindow;
      string name = currForm.Name;
      string parameter1 = DataBase.getParameter("application", name + "_X");
      if (!parameter1.Equals(""))
      {
        int result = 0;
        int.TryParse(parameter1, out result);
        if (result != 0)
          currForm.Left = result;
      }
      string parameter2 = DataBase.getParameter("application", name + "_Y");
      if (!parameter2.Equals(""))
      {
        int result = 0;
        int.TryParse(parameter2, out result);
        if (result != 0)
          currForm.Top = result;
      }
      if (flag)
      {
        string parameter3 = DataBase.getParameter("application", name + "_W");
        if (!parameter3.Equals(""))
        {
          int result = 0;
          int.TryParse(parameter3, out result);
          if (result != 0)
            currForm.Width = result;
        }
        string parameter4 = DataBase.getParameter("application", name + "_H");
        if (!parameter4.Equals(""))
        {
          int result = 0;
          int.TryParse(parameter4, out result);
          if (result != 0)
            currForm.Height = result;
        }
      }
      Screen screen = Screen.FromControl((Control) currForm);
      int left = currForm.Left;
      Rectangle bounds = screen.Bounds;
      int x1 = bounds.X;
      int num1;
      if (left < x1)
      {
        int width1 = currForm.Width;
        bounds = screen.Bounds;
        int width2 = bounds.Width;
        num1 = width1 <= width2 ? 1 : 0;
      }
      else
        num1 = 1;
      if (num1 != 0)
        return;
      currForm.WindowState = FormWindowState.Maximized;
      Form form1 = currForm;
      bounds = screen.Bounds;
      int x2 = bounds.X;
      bounds = screen.Bounds;
      int num2 = bounds.Width / 6;
      int num3 = x2 + num2;
      form1.Left = num3;
      Form form2 = currForm;
      bounds = screen.Bounds;
      int y = bounds.Y;
      bounds = screen.Bounds;
      int num4 = bounds.Height / 6;
      int num5 = y + num4;
      form2.Top = num5;
      if (flag)
      {
        Form form3 = currForm;
        bounds = screen.Bounds;
        int num6 = bounds.Width * 2 / 3;
        form3.Width = num6;
        Form form4 = currForm;
        bounds = screen.Bounds;
        int num7 = bounds.Height * 2 / 3;
        form4.Height = num7;
      }
    }

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetCurrentProcess();

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref Services.TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool ExitWindowsEx(int flg, int rea);

    [DllImport("shell32.dll")]
    public static extern long ShellExecute(int hWnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, long nShowCmd);

    public static void DoExitWin(int flg)
    {
      IntPtr currentProcess = Services.GetCurrentProcess();
      IntPtr phtok = IntPtr.Zero;
      bool flag = Services.OpenProcessToken(currentProcess, 40, ref phtok);
      Services.TokPriv1Luid newst;
      newst.Count = 1;
      newst.Luid = 0L;
      newst.Attr = 2;
      flag = Services.LookupPrivilegeValue((string) null, "SeShutdownPrivilege", ref newst.Luid);
      flag = Services.AdjustTokenPrivileges(phtok, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero);
      flag = Services.ExitWindowsEx(flg, 0);
    }

    [DllImport("kernel32.dll")]
    public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr handle);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    [DllImport("user32.DLL")]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.DLL")]
    public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

    [DllImport("user32.DLL")]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);

    public static void GetIconPosition()
    {
      IntPtr windowEx = Services.FindWindowEx(Services.FindWindowEx(Services.FindWindow("Progman", "Program Manager"), IntPtr.Zero, "SHELLDLL_DefView", (string) null), IntPtr.Zero, "SysListView32", "FolderView");
      int num1 = Services.SendMessage(windowEx, 4100U, 0, 0);
      uint dwProcessId;
      int num2 = (int) Services.GetWindowThreadProcessId(windowEx, out dwProcessId);
      IntPtr hProcess = Services.OpenProcess(56U, false, dwProcessId);
      IntPtr lpBaseAddress = Services.VirtualAllocEx(hProcess, IntPtr.Zero, 4096U, 12288U, 4U);
      try
      {
        for (int wParam = 0; wParam < num1; ++wParam)
        {
          byte[] bytes = new byte[256];
          Services.LVITEM[] lvitemArray = new Services.LVITEM[1];
          lvitemArray[0].mask = 1;
          lvitemArray[0].iItem = wParam;
          lvitemArray[0].iSubItem = 0;
          lvitemArray[0].cchTextMax = bytes.Length;
          lvitemArray[0].pszText = (IntPtr) ((int) lpBaseAddress + Marshal.SizeOf(typeof (Services.LVITEM)));
          uint vNumberOfBytesRead = 0;
          Services.WriteProcessMemory(hProcess, lpBaseAddress, Marshal.UnsafeAddrOfPinnedArrayElement((Array) lvitemArray, 0), Marshal.SizeOf(typeof (Services.LVITEM)), ref vNumberOfBytesRead);
          Services.SendMessage(windowEx, 4171U, wParam, lpBaseAddress.ToInt32());
          Services.ReadProcessMemory(hProcess, (IntPtr) ((int) lpBaseAddress + Marshal.SizeOf(typeof (Services.LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement((Array) bytes, 0), bytes.Length, ref vNumberOfBytesRead);
          Encoding.Unicode.GetString(bytes, 0, (int) vNumberOfBytesRead).Replace("\0", "");
          Services.SendMessage(windowEx, 4112U, wParam, lpBaseAddress.ToInt32());
          Point[] pointArray = new Point[1];
          Services.ReadProcessMemory(hProcess, lpBaseAddress, Marshal.UnsafeAddrOfPinnedArrayElement((Array) pointArray, 0), Marshal.SizeOf(typeof (Point)), ref vNumberOfBytesRead);
          pointArray[0].ToString();
        }
      }
      catch
      {
      }
    }

    public static void BuildButtonFaces(Control.ControlCollection myControls)
    {
      Font font = new Font("Arial", 8.25f, FontStyle.Bold);
      for (int index1 = 0; index1 < myControls.Count; ++index1)
      {
        string name = myControls[index1].GetType().Name;
        if (name == "Button")
        {
          ((ButtonBase) myControls[index1]).Image = (Image) Services.BFac;
          myControls[index1].ForeColor = Services.MainColor_Fore_Button;
          myControls[index1].BackColor = Color.Transparent;
          ((ButtonBase) myControls[index1]).FlatStyle = FlatStyle.Flat;
          ((ButtonBase) myControls[index1]).FlatAppearance.BorderSize = 0;
          myControls[index1].Font = font;
        }
        else if (name == "Label")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Color.Transparent;
          ((Label) myControls[index1]).FlatStyle = FlatStyle.Popup;
        }
        else if (name == "ComboBox")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Text;
          myControls[index1].BackColor = Services.MainColor_Back_Text;
          myControls[index1].Font = font;
        }
        else if (name == "TextBox")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Text;
          myControls[index1].BackColor = Services.MainColor_Back_Text;
          myControls[index1].Font = font;
        }
        else if (name == "TreeView")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Text;
          myControls[index1].BackColor = Services.MainColor_Back_Text;
          myControls[index1].Font = font;
        }
        else if (name == "NumericUpDown")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Text;
          myControls[index1].BackColor = Services.MainColor_Back_Text;
          myControls[index1].Font = font;
        }
        else if (name == "ListBox")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Services.MainColor_BackTabPage;
          myControls[index1].Font = font;
        }
        else if (name == "TrackBar")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Color.Transparent;
          myControls[index1].Font = font;
        }
        else if (name == "CheckedListBox")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Services.MainColor_BackTabPage;
          myControls[index1].Font = font;
        }
        else if (name == "ToolStrip")
        {
          ((ToolStrip) myControls[index1]).ForeColor = Services.MainColor_Fore_Menu;
          ((ToolStrip) myControls[index1]).BackColor = Color.Transparent;
          myControls[index1].Font = font;
          for (int index2 = 0; index2 < ((ToolStrip) myControls[index1]).Items.Count; ++index2)
          {
            ((ToolStrip) myControls[index1]).Items[index2].ForeColor = Services.MainColor_Fore_Menu;
            ((ToolStrip) myControls[index1]).Items[index2].BackColor = Color.Transparent;
            ((ToolStrip) myControls[index1]).Items[index2].Font = font;
          }
        }
        else if (name == "MenuStrip")
        {
          ((ToolStrip) myControls[index1]).ForeColor = Services.MainColor_Fore_Menu;
          ((ToolStrip) myControls[index1]).BackColor = Color.Transparent;
          myControls[index1].Font = font;
          for (int index2 = 0; index2 < ((ToolStrip) myControls[index1]).Items.Count; ++index2)
          {
            ((ToolStrip) myControls[index1]).Items[index2].ForeColor = Services.MainColor_Fore_Menu;
            ((ToolStrip) myControls[index1]).Items[index2].BackColor = Color.Transparent;
            ((ToolStrip) myControls[index1]).Items[index2].Font = font;
          }
        }
        else if (name == "CheckBox")
        {
          if (((CheckBox) myControls[index1]).Appearance == Appearance.Button)
          {
            ((ButtonBase) myControls[index1]).Image = (Image) Services.BFac;
            ((ButtonBase) myControls[index1]).FlatStyle = FlatStyle.Flat;
            ((ButtonBase) myControls[index1]).FlatAppearance.BorderSize = 0;
            myControls[index1].Font = font;
            myControls[index1].ForeColor = Services.MainColor_Fore_Label;
            myControls[index1].ForeColor = ((CheckBox) myControls[index1]).Checked ? Color.FromArgb((int) byte.MaxValue, 100, 100, 100) : Services.MainColor_Fore_Menu;
          }
          else
            myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Color.Transparent;
        }
        else if (name == "RadioButton")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Color.Transparent;
        }
        else if (name == "DataGridView")
        {
          ((DataGridView) myControls[index1]).RowHeadersDefaultCellStyle.ForeColor = Services.MainColor_Fore_Label;
          ((DataGridView) myControls[index1]).RowHeadersDefaultCellStyle.BackColor = Services.MainColor_Back_Label;
          ((DataGridView) myControls[index1]).RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 5, 10, 15);
          ((DataGridView) myControls[index1]).RowHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(50, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          ((DataGridView) myControls[index1]).ColumnHeadersDefaultCellStyle.ForeColor = Services.MainColor_Fore_Label;
          ((DataGridView) myControls[index1]).ColumnHeadersDefaultCellStyle.BackColor = Services.MainColor_Back_Label;
          ((DataGridView) myControls[index1]).ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb((int) byte.MaxValue, 5, 10, 15);
          ((DataGridView) myControls[index1]).ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          ((DataGridView) myControls[index1]).AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 220);
          ((DataGridView) myControls[index1]).AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
          ((DataGridView) myControls[index1]).DefaultCellStyle.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          ((DataGridView) myControls[index1]).DefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
          myControls[index1].Font = font;
          ((DataGridView) myControls[index1]).SelectionMode = DataGridViewSelectionMode.FullRowSelect;
          ((DataGridView) myControls[index1]).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }
        else if (name == "GroupBox")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Color.Transparent;
          Services.BuildButtonFaces(myControls[index1].Controls);
        }
        else if (name == "TabControlEX")
        {
          ((TabControlEX) myControls[index1]).ForeColor = Services.MainColor_Fore_Menu;
          ((TabControlEX) myControls[index1]).BackColor = Color.Transparent;
          ((TabControlEX) myControls[index1]).Appearance = TabAppearanceEX.Bevel;
          Services.BuildButtonFaces(myControls[index1].Controls);
        }
        else if (name == "TabControl")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Services.MainColor_Back_Label;
          Services.BuildButtonFaces(myControls[index1].Controls);
        }
        else if (name == "TabPageEX")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Services.MainColor_BackTabPage;
          Services.BuildButtonFaces(myControls[index1].Controls);
        }
        else if (name == "TabPage")
        {
          myControls[index1].ForeColor = Services.MainColor_Fore_Menu;
          myControls[index1].BackColor = Services.MainColor_BackTabPage;
          Services.BuildButtonFaces(myControls[index1].Controls);
        }
      }
    }

    public static void setFormStyle(Form myForm)
    {
      myForm.BackColor = Color.FromArgb(200, 205, 210);
      myForm.BackgroundImage = (Image) Services.BImg;
      myForm.BackgroundImageLayout = ImageLayout.Stretch;
      myForm.Icon = Services.MIco;
      Services.BuildButtonFaces(myForm.Controls);
      Services.LoadFormLayout(myForm);
    }

    public static string GetVersion()
    {
      return new Version(Application.ProductVersion).ToString();
    }

    public static bool isApplicationAlreadyRunning()
    {
      return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.ModuleName)).Length > 1;
    }

    public static Icon MakeIcon(Image img, int size, bool keepAspectRatio)
    {
      Bitmap bitmap = new Bitmap(size, size);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      int y;
      int x;
      int height;
      int width;
      if (!keepAspectRatio || img.Height == img.Width)
      {
        x = y = 0;
        width = height = size;
      }
      else
      {
        float num = (float) img.Width / (float) img.Height;
        if ((double) num > 1.0)
        {
          width = size;
          height = (int) ((double) size / (double) num);
          x = 0;
          y = (size - height) / 2;
        }
        else
        {
          width = (int) ((double) size * (double) num);
          height = size;
          y = 0;
          x = (size - width) / 2;
        }
      }
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(img, x, y, width, height);
      graphics.Flush();
      return Icon.FromHandle(bitmap.GetHicon());
    }

    public struct IconInfo
    {
      public bool fIcon;
      public int xHotspot;
      public int yHotspot;
      public IntPtr hbmMask;
      public IntPtr hbmColor;
    }

    internal struct TokPriv1Luid
    {
      public int Count;
      public long Luid;
      public int Attr;
    }

    public struct LVITEM
    {
      public int mask;
      public int iItem;
      public int iSubItem;
      public int state;
      public int stateMask;
      public IntPtr pszText;
      public int cchTextMax;
      public int iImage;
      public IntPtr lParam;
      public int iIndent;
      public int iGroupId;
      public int cColumns;
      public IntPtr puColumns;
    }
  }
}
