// Decompiled with JetBrains decompiler
// Type: MainExe.Properties.Resources
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MainExe.Properties
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) MainExe.Properties.Resources.resourceMan, (object) null))
          MainExe.Properties.Resources.resourceMan = new ResourceManager("MainExe.Properties.Resources", typeof (MainExe.Properties.Resources).Assembly);
        return MainExe.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return MainExe.Properties.Resources.resourceCulture;
      }
      set
      {
        MainExe.Properties.Resources.resourceCulture = value;
      }
    }

    internal Resources()
    {
    }
  }
}
