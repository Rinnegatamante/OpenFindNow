// Decompiled with JetBrains decompiler
// Type: MainExe.SystemInfoBrowserForm
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MainExe
{
  public class SystemInfoBrowserForm : Form
  {
    private ListBox listBox1;
    private TextBox textBox1;

    public SystemInfoBrowserForm()
    {
      this.SuspendLayout();
      this.InitForm();
      PropertyInfo[] properties = typeof (SystemInformation).GetProperties();
      for (int index = 0; index < properties.Length; ++index)
        this.listBox1.Items.Add((object) properties[index].Name);
      this.textBox1.Text = "The SystemInformation class has " + properties.Length.ToString() + " properties.\r\n";
      this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
      this.ResumeLayout(false);
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBox1.SelectedIndex == -1)
        return;
      string text = this.listBox1.Text;
      if (text == "PowerStatus")
      {
        this.textBox1.Text += "\r\nThe value of the PowerStatus property is:";
        PropertyInfo[] properties = typeof (PowerStatus).GetProperties();
        for (int index = 0; index < properties.Length; ++index)
        {
          object obj = properties[index].GetValue((object) SystemInformation.PowerStatus, (object[]) null);
          TextBox textBox = this.textBox1;
          string str = textBox.Text + "\r\n    PowerStatus." + properties[index].Name + " is: " + obj.ToString();
          textBox.Text = str;
        }
      }
      else
      {
        PropertyInfo[] properties = typeof (SystemInformation).GetProperties();
        PropertyInfo propertyInfo = (PropertyInfo) null;
        for (int index = 0; index < properties.Length; ++index)
        {
          if (properties[index].Name == text)
          {
            propertyInfo = properties[index];
            break;
          }
        }
        object obj = propertyInfo.GetValue((object) null, (object[]) null);
        TextBox textBox = this.textBox1;
        string str = textBox.Text + "\r\nThe value of the " + text + " property is: " + obj.ToString();
        textBox.Text = str;
      }
    }

    private void InitForm()
    {
      this.listBox1 = new ListBox();
      this.textBox1 = new TextBox();
      this.listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listBox1.Location = new Point(8, 16);
      this.listBox1.Size = new Size(172, 496);
      this.listBox1.TabIndex = 0;
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.textBox1.Location = new Point(188, 16);
      this.textBox1.Multiline = true;
      this.textBox1.ScrollBars = ScrollBars.Vertical;
      this.textBox1.Size = new Size(420, 496);
      this.textBox1.TabIndex = 1;
      this.ClientSize = new Size(616, 525);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.listBox1);
      this.Text = "Select a SystemInformation property to get the value of";
    }
  }
}
