// Decompiled with JetBrains decompiler
// Type: MainExe.USBLibrary
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System.Runtime.InteropServices;

namespace MainExe
{
  internal class USBLibrary
  {
    [DllImport("usbcddll.dll")]
    public static extern int InitUSBCDLibrary();

    [DllImport("usbcddll.dll")]
    public static extern int CloseUSBCDLibrary();

    [DllImport("usbcddll.dll")]
    public static extern int GetDeviceNumber();

    [DllImport("usbcddll.dll")]
    public static extern int EnumDevice(int gI);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDReset(int gI);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDMoveto(int gI, int gIndex);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDGetCDDown(int gI);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDLEDON(int gI);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDLEDOFF(int gI);

    [DllImport("usbcddll.dll")]
    public static extern int USBCDGetStatus(int gI);
  }
}
