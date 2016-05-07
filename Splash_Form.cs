// Decompiled with JetBrains decompiler
// Type: MainExe.Splash_Form
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MainExe
{
  public class Splash_Form : Form
  {
    private IContainer components = (IContainer) null;
    private PictureBox logoPicture;
    public Label outputList;
    public Label VerLabel;

    public Splash_Form()
    {
      this.InitializeComponent();
      this.logoPicture.Image = (Image) Navigator.Logo;
      this.Enabled = true;
      this.Text = "";
      this.outputList.Text = "";
      this.VerLabel.Text = Navigator.VersionSTR;
      Services.setFormStyle((Form) this);
      Services.UsedFormListAdd((Form) this);
    }

    public void writeInLoading(string auxMessage)
    {
      Label label = this.outputList;
      string str = label.Text + auxMessage + Environment.NewLine;
      label.Text = str;
      this.outputList.Refresh();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Splash_Form));
      this.logoPicture = new PictureBox();
      this.outputList = new Label();
      this.VerLabel = new Label();
      ((ISupportInitialize) this.logoPicture).BeginInit();
      this.SuspendLayout();
      this.logoPicture.Anchor = AnchorStyles.None;
      this.logoPicture.BackColor = Color.Transparent;
      this.logoPicture.Image = (Image) componentResourceManager.GetObject("logoPicture.Image");
      this.logoPicture.Location = new Point(5, -1);
      this.logoPicture.Name = "logoPicture";
      this.logoPicture.Size = new Size(160, 31);
      this.logoPicture.SizeMode = PictureBoxSizeMode.AutoSize;
      this.logoPicture.TabIndex = 0;
      this.logoPicture.TabStop = false;
      this.outputList.BackColor = Color.Transparent;
      this.outputList.Location = new Point(0, 61);
      this.outputList.Name = "outputList";
      this.outputList.Size = new Size(196, 72);
      this.outputList.TabIndex = 1;
      this.outputList.Text = "label1";
      this.VerLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.VerLabel.BackColor = Color.Transparent;
      this.VerLabel.Location = new Point(140, 39);
      this.VerLabel.Name = "VerLabel";
      this.VerLabel.Size = new Size(66, 16);
      this.VerLabel.TabIndex = 2;
      this.VerLabel.Text = "XXXXX";
      this.VerLabel.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(200, 205, 210);
      this.ClientSize = new Size(208, 136);
      this.ControlBox = false;
      this.Controls.Add((Control) this.VerLabel);
      this.Controls.Add((Control) this.outputList);
      this.Controls.Add((Control) this.logoPicture);
      this.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "Splash_Form";
      this.Opacity = 0.0;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Sentinel";
      this.TopMost = true;
      ((ISupportInitialize) this.logoPicture).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
