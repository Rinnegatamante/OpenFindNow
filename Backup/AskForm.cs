// Decompiled with JetBrains decompiler
// Type: MainExe.AskForm
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MainExe
{
  public class AskForm : Form
  {
    public string Answer = "";
    public string exValue_AskReply = "";
    public bool Ask_OnlyNumber = false;
    private Regex InputFormat = new Regex("");
    private IContainer components = (IContainer) null;
    private PictureBox YesButton;
    private PictureBox NoButton;
    private Label AskQuestion;
    public ComboBox AskCombo;
    public TextBox AskReply;

    public AskForm()
    {
      this.FormBorderStyle = FormBorderStyle.None;
      this.TransparencyKey = Color.FromArgb(0, (int) byte.MaxValue, 0);
      this.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
      this.InitializeComponent();
    }

    public void LoadCombo(ComboBox SourceCombo)
    {
      this.AskCombo.Items.Clear();
      this.AskCombo.Items.Add((object) Navigator.NoDrive);
      for (int index = 0; index < SourceCombo.Items.Count; ++index)
        this.AskCombo.Items.Add(SourceCombo.Items[index]);
      this.AskCombo.SelectedIndex = SourceCombo.SelectedIndex;
    }

    public string WaitForReply(string DisplayText, Image ImgYes, Image ImgNo)
    {
      this.AskQuestion.Text = DisplayText;
      this.Answer = "";
      this.AskReply.Text = "<yes>";
      this.AskCombo.Visible = false;
      this.AskReply.Visible = false;
      this.AskQuestion.Left = (this.Width - this.AskQuestion.Width) / 2;
      this.AskQuestion.Top = (this.Height - this.AskQuestion.Height) / 2;
      this.YesButton.Left = this.AskQuestion.Left + this.AskQuestion.Width;
      this.YesButton.Top = (this.Height - this.YesButton.Height) / 2;
      this.YesButton.Image = ImgYes;
      this.NoButton.Left = this.AskQuestion.Left - this.YesButton.Width;
      this.NoButton.Top = (this.Height - this.NoButton.Height) / 2;
      this.NoButton.Image = ImgNo;
      return this.AskReply.Text;
    }

    public string InputString(string DisplayText, Image ImgYes, Image ImgNo, int maxChar, bool _OnlyNumber)
    {
      this.Ask_OnlyNumber = _OnlyNumber;
      this.AskQuestion.Text = DisplayText;
      this.Answer = "";
      this.AskReply.Text = "";
      this.AskReply.Visible = true;
      this.AskQuestion.Left = (this.Width - this.AskQuestion.Width) / 2;
      this.AskQuestion.Top = (this.Height - this.AskQuestion.Height) / 2;
      this.AskReply.Top = this.AskQuestion.Top + this.AskQuestion.Height;
      this.AskReply.Left = this.AskQuestion.Left;
      this.AskReply.Width = this.AskQuestion.Width;
      this.AskReply.MaxLength = maxChar;
      this.AskCombo.Top = this.AskReply.Top;
      this.AskCombo.Left = this.AskReply.Left;
      this.AskCombo.Width = this.AskReply.Width;
      this.AskCombo.Height = this.AskReply.Height;
      this.YesButton.Left = this.AskQuestion.Left + this.AskQuestion.Width;
      this.YesButton.Top = (this.Height - this.NoButton.Height) / 2;
      this.YesButton.Image = ImgYes;
      this.NoButton.Left = this.AskQuestion.Left - this.YesButton.Width;
      this.NoButton.Top = (this.Height - this.YesButton.Height) / 2;
      this.NoButton.Image = ImgNo;
      this.exValue_AskReply = this.AskReply.Text;
      return this.AskReply.Text;
    }

    private void NoButton_Click(object sender, EventArgs e)
    {
      this.Answer = "<no>";
      this.Close();
    }

    private void YesButton_Click(object sender, EventArgs e)
    {
      this.Answer = this.AskReply.Text;
      this.Close();
    }

    private void AskReply_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        this.NoButton_Click((object) null, (EventArgs) null);
      if (e.KeyCode != Keys.Return)
        return;
      this.YesButton_Click((object) null, (EventArgs) null);
    }

    private void AskReply_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.Ask_OnlyNumber)
      {
        int result = 0;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out result);
      }
      else if (this.AskReply.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
        this.AskReply.Text = this.exValue_AskReply;
      this.exValue_AskReply = this.AskReply.Text;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.AskReply = new TextBox();
      this.YesButton = new PictureBox();
      this.NoButton = new PictureBox();
      this.AskQuestion = new Label();
      this.AskCombo = new ComboBox();
      ((ISupportInitialize) this.YesButton).BeginInit();
      ((ISupportInitialize) this.NoButton).BeginInit();
      this.SuspendLayout();
      this.AskReply.Location = new Point(162, 49);
      this.AskReply.Name = "AskReply";
      this.AskReply.Size = new Size(153, 20);
      this.AskReply.TabIndex = 49;
      this.AskReply.KeyUp += new KeyEventHandler(this.AskReply_KeyUp);
      this.AskReply.KeyPress += new KeyPressEventHandler(this.AskReply_KeyPress);
      this.YesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.YesButton.BackColor = Color.Transparent;
      this.YesButton.Location = new Point(392, 30);
      this.YesButton.Name = "YesButton";
      this.YesButton.Size = new Size(64, 64);
      this.YesButton.SizeMode = PictureBoxSizeMode.StretchImage;
      this.YesButton.TabIndex = 53;
      this.YesButton.TabStop = false;
      this.YesButton.Click += new EventHandler(this.YesButton_Click);
      this.NoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.NoButton.BackColor = Color.Transparent;
      this.NoButton.Location = new Point(12, 30);
      this.NoButton.Name = "NoButton";
      this.NoButton.Size = new Size(64, 64);
      this.NoButton.SizeMode = PictureBoxSizeMode.StretchImage;
      this.NoButton.TabIndex = 54;
      this.NoButton.TabStop = false;
      this.NoButton.Click += new EventHandler(this.NoButton_Click);
      this.AskQuestion.Anchor = AnchorStyles.None;
      this.AskQuestion.AutoSize = true;
      this.AskQuestion.BackColor = Color.Gray;
      this.AskQuestion.BorderStyle = BorderStyle.FixedSingle;
      this.AskQuestion.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.AskQuestion.ForeColor = Color.White;
      this.AskQuestion.Location = new Point(212, 9);
      this.AskQuestion.Name = "AskQuestion";
      this.AskQuestion.Size = new Size(49, 17);
      this.AskQuestion.TabIndex = 56;
      this.AskQuestion.Text = "label1";
      this.AskQuestion.TextAlign = ContentAlignment.MiddleCenter;
      this.AskCombo.FormattingEnabled = true;
      this.AskCombo.Location = new Point(162, 49);
      this.AskCombo.Name = "AskCombo";
      this.AskCombo.Size = new Size(153, 21);
      this.AskCombo.TabIndex = 57;
      this.AskCombo.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(468, 114);
      this.Controls.Add((Control) this.NoButton);
      this.Controls.Add((Control) this.YesButton);
      this.Controls.Add((Control) this.AskReply);
      this.Controls.Add((Control) this.AskQuestion);
      this.Controls.Add((Control) this.AskCombo);
      this.Name = "AskForm";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "AskForm";
      this.TopMost = true;
      ((ISupportInitialize) this.YesButton).EndInit();
      ((ISupportInitialize) this.NoButton).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
