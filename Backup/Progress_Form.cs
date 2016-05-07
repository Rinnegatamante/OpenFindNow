// Decompiled with JetBrains decompiler
// Type: MainExe.Progress_Form
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MainExe
{
  public class Progress_Form : Form
  {
    public bool abortFlag = false;
    private IContainer components = (IContainer) null;
    public ProgressBar progressBar;
    public Label CurrFileLabel;
    public PictureBox Pic_Folder_Start;
    public PictureBox Pic_Folder_End;
    public PictureBox Pic_File;
    public Label TotFiles;
    public Label DirectoryLabel;
    private PictureBox CloseGadget;

    public Progress_Form()
    {
      this.InitializeComponent();
      Services.setFormStyle((Form) this);
      Services.UsedFormListAdd((Form) this);
      this.CloseGadget.Image = (Image) Navigator.Cros;
    }

    private void CloseGadget_Click(object sender, EventArgs e)
    {
      this.abortFlag = true;
      this.Hide();
    }

    public void Animation(int percentage)
    {
      this.Pic_File.Left = (this.Width - this.Pic_File.Width) * percentage / 100;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.progressBar = new ProgressBar();
      this.CurrFileLabel = new Label();
      this.Pic_Folder_Start = new PictureBox();
      this.Pic_Folder_End = new PictureBox();
      this.Pic_File = new PictureBox();
      this.TotFiles = new Label();
      this.DirectoryLabel = new Label();
      this.CloseGadget = new PictureBox();
      ((ISupportInitialize) this.Pic_Folder_Start).BeginInit();
      ((ISupportInitialize) this.Pic_Folder_End).BeginInit();
      ((ISupportInitialize) this.Pic_File).BeginInit();
      ((ISupportInitialize) this.CloseGadget).BeginInit();
      this.SuspendLayout();
      this.progressBar.Location = new Point(4, 103);
      this.progressBar.Maximum = 1000;
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new Size(421, 14);
      this.progressBar.TabIndex = 0;
      this.progressBar.Visible = false;
      this.CurrFileLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.CurrFileLabel.Location = new Point(0, 91);
      this.CurrFileLabel.Name = "CurrFileLabel";
      this.CurrFileLabel.Size = new Size(385, 19);
      this.CurrFileLabel.TabIndex = 1;
      this.CurrFileLabel.Text = "label1";
      this.CurrFileLabel.TextAlign = ContentAlignment.MiddleLeft;
      this.Pic_Folder_Start.BackColor = Color.Transparent;
      this.Pic_Folder_Start.Location = new Point(3, 42);
      this.Pic_Folder_Start.Name = "Pic_Folder_Start";
      this.Pic_Folder_Start.Size = new Size(25, 25);
      this.Pic_Folder_Start.SizeMode = PictureBoxSizeMode.StretchImage;
      this.Pic_Folder_Start.TabIndex = 2;
      this.Pic_Folder_Start.TabStop = false;
      this.Pic_Folder_End.BackColor = Color.Transparent;
      this.Pic_Folder_End.Location = new Point(404, 42);
      this.Pic_Folder_End.Name = "Pic_Folder_End";
      this.Pic_Folder_End.Size = new Size(25, 25);
      this.Pic_Folder_End.SizeMode = PictureBoxSizeMode.StretchImage;
      this.Pic_Folder_End.TabIndex = 3;
      this.Pic_Folder_End.TabStop = false;
      this.Pic_File.BackColor = Color.Transparent;
      this.Pic_File.Location = new Point(202, 42);
      this.Pic_File.Name = "Pic_File";
      this.Pic_File.Size = new Size(25, 25);
      this.Pic_File.SizeMode = PictureBoxSizeMode.StretchImage;
      this.Pic_File.TabIndex = 4;
      this.Pic_File.TabStop = false;
      this.TotFiles.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.TotFiles.Location = new Point(377, 91);
      this.TotFiles.Name = "TotFiles";
      this.TotFiles.Size = new Size(52, 19);
      this.TotFiles.TabIndex = 5;
      this.TotFiles.Text = "label1";
      this.TotFiles.TextAlign = ContentAlignment.MiddleRight;
      this.DirectoryLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.DirectoryLabel.Location = new Point(0, 72);
      this.DirectoryLabel.Name = "DirectoryLabel";
      this.DirectoryLabel.Size = new Size(385, 19);
      this.DirectoryLabel.TabIndex = 6;
      this.DirectoryLabel.Text = "label1";
      this.DirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
      this.CloseGadget.BackColor = Color.Transparent;
      this.CloseGadget.Location = new Point(400, 0);
      this.CloseGadget.Name = "CloseGadget";
      this.CloseGadget.Size = new Size(32, 32);
      this.CloseGadget.SizeMode = PictureBoxSizeMode.StretchImage;
      this.CloseGadget.TabIndex = 53;
      this.CloseGadget.TabStop = false;
      this.CloseGadget.Click += new EventHandler(this.CloseGadget_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(432, 115);
      this.ControlBox = false;
      this.Controls.Add((Control) this.CloseGadget);
      this.Controls.Add((Control) this.DirectoryLabel);
      this.Controls.Add((Control) this.TotFiles);
      this.Controls.Add((Control) this.Pic_Folder_End);
      this.Controls.Add((Control) this.Pic_Folder_Start);
      this.Controls.Add((Control) this.CurrFileLabel);
      this.Controls.Add((Control) this.progressBar);
      this.Controls.Add((Control) this.Pic_File);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Name = "Progress_Form";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      ((ISupportInitialize) this.Pic_Folder_Start).EndInit();
      ((ISupportInitialize) this.Pic_Folder_End).EndInit();
      ((ISupportInitialize) this.Pic_File).EndInit();
      ((ISupportInitialize) this.CloseGadget).EndInit();
      this.ResumeLayout(false);
    }
  }
}
