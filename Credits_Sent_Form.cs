// Decompiled with JetBrains decompiler
// Type: MainExe.Credits_Sent_Form
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MainExe
{
  public class Credits_Sent_Form : Form
  {
    private IContainer components = (IContainer) null;
    private RichTextBox CreditsRichEdit;
    private Label applicationInfoLabel;
    private Label LogoLabel;
    private PictureBox logoPicture;
    public LinkLabel companyLinkLabel;
    private ArrayList Strx;

    public Credits_Sent_Form()
    {
      this.InitializeComponent();
      this.setFormLanguage();
      this.CreditsRichEdit.LoadFile(Services.getResource("Languages\\" + Language.CurrLanguageSTR + "\\Credits.rtf"), RichTextBoxStreamType.RichText);
      this.logoPicture.Image = (Image) Navigator.Logo;
      Services.setFormStyle((Form) this);
      Services.UsedFormListAdd((Form) this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Credits_Sent_Form));
      this.CreditsRichEdit = new RichTextBox();
      this.applicationInfoLabel = new Label();
      this.LogoLabel = new Label();
      this.logoPicture = new PictureBox();
      this.companyLinkLabel = new LinkLabel();
      ((ISupportInitialize) this.logoPicture).BeginInit();
      this.SuspendLayout();
      this.CreditsRichEdit.Anchor = AnchorStyles.None;
      this.CreditsRichEdit.BackColor = Color.White;
      this.CreditsRichEdit.BorderStyle = BorderStyle.FixedSingle;
      this.CreditsRichEdit.Enabled = false;
      this.CreditsRichEdit.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.CreditsRichEdit.Location = new Point(3, 119);
      this.CreditsRichEdit.Name = "CreditsRichEdit";
      this.CreditsRichEdit.ReadOnly = true;
      this.CreditsRichEdit.Size = new Size(328, 113);
      this.CreditsRichEdit.TabIndex = 3;
      this.CreditsRichEdit.Text = "";
      this.applicationInfoLabel.BackColor = Color.Transparent;
      this.applicationInfoLabel.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.applicationInfoLabel.ForeColor = Color.Black;
      this.applicationInfoLabel.Location = new Point(3, 47);
      this.applicationInfoLabel.Name = "applicationInfoLabel";
      this.applicationInfoLabel.Size = new Size(331, 49);
      this.applicationInfoLabel.TabIndex = 25;
      this.applicationInfoLabel.Text = "Information about Sentinel";
      this.applicationInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.LogoLabel.BackColor = Color.Transparent;
      this.LogoLabel.Location = new Point((int) byte.MaxValue, 2);
      this.LogoLabel.Name = "LogoLabel";
      this.LogoLabel.Size = new Size(82, 16);
      this.LogoLabel.TabIndex = 27;
      this.LogoLabel.Text = "Version 5.0";
      this.LogoLabel.TextAlign = ContentAlignment.BottomRight;
      this.logoPicture.BackColor = Color.Transparent;
      this.logoPicture.Image = (Image) componentResourceManager.GetObject("logoPicture.Image");
      this.logoPicture.Location = new Point(3, 2);
      this.logoPicture.Name = "logoPicture";
      this.logoPicture.Size = new Size(246, 42);
      this.logoPicture.TabIndex = 28;
      this.logoPicture.TabStop = false;
      this.companyLinkLabel.BackColor = Color.Transparent;
      this.companyLinkLabel.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.companyLinkLabel.LinkColor = Color.FromArgb(3, 120, 188);
      this.companyLinkLabel.Location = new Point(0, 97);
      this.companyLinkLabel.Margin = new Padding(3);
      this.companyLinkLabel.Name = "companyLinkLabel";
      this.companyLinkLabel.Size = new Size(337, 21);
      this.companyLinkLabel.TabIndex = 29;
      this.companyLinkLabel.TabStop = true;
      this.companyLinkLabel.Text = "www.microforum.com";
      this.companyLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(200, 205, 210);
      this.ClientSize = new Size(337, 235);
      this.Controls.Add((Control) this.companyLinkLabel);
      this.Controls.Add((Control) this.applicationInfoLabel);
      this.Controls.Add((Control) this.CreditsRichEdit);
      this.Controls.Add((Control) this.LogoLabel);
      this.Controls.Add((Control) this.logoPicture);
      this.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Credits_Sent_Form";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "About";
      ((ISupportInitialize) this.logoPicture).EndInit();
      this.ResumeLayout(false);
    }

    public void setFormLanguage()
    {
      this.Strx = Language.loadFormStrings(this.Name, Language.CurrLanguage);
      this.Text = this.Strx[0].ToString();
      this.LogoLabel.Text = this.Strx[5].ToString().Trim();
      Label label = this.LogoLabel;
      string str = label.Text + " " + Navigator.VersionSTR;
      label.Text = str;
      this.applicationInfoLabel.Text = this.Strx[1].ToString();
      this.companyLinkLabel.Text = this.Strx[8].ToString();
    }

    private void companyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start(this.companyLinkLabel.Text);
      }
      catch
      {
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
      }
      catch (Exception ex)
      {
      }
    }

    private enum StrIdx
    {
      Title,
      CopyrightInfo,
      Website,
      Credits,
      OK,
      Version_Gua,
      Version_XP,
      Version_Viso,
      webaddress,
    }
  }
}
