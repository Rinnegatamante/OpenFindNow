// Decompiled with JetBrains decompiler
// Type: MainExe.SearchForm
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MainExe
{
  public class SearchForm : Form
  {
    private int SearchType = 0;
    private int SearchStart = 0;
    private bool SearchFlag = false;
    private IContainer components = (IContainer) null;
    private ArrayList[] FileList;
    private ArrayList[] VolumeList;
    private Navigator Nav_Parent;
    private TextBox SearchBox;
    private PictureBox SearchGadget;
    private PictureBox CloseGadget;
    private Timer SearchTimer;
    private DataGridViewTextBoxColumn UnitName;
    private DataGridViewTextBoxColumn Position;
    private DataGridViewTextBoxColumn Volume;
    private DataGridViewTextBoxColumn FolderName;
    private DataGridViewTextBoxColumn FileName;
    private DataGridViewTextBoxColumn FileSize;
    private DataGridViewTextBoxColumn FileDate;
    private PictureBox SearchTypePic;
    public DataGridView FileGridView;

    public SearchForm(Navigator Nav_Parent_)
    {
      this.Nav_Parent = Nav_Parent_;
      this.FormBorderStyle = FormBorderStyle.None;
      this.InitializeComponent();
      Services.setFormStyle((Form) this);
      this.SearchBox.BackColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      this.TransparencyKey = Color.FromArgb(0, 0, 0);
      this.BackColor = Color.FromArgb(0, 0, 0);
      this.BackgroundImage = (Image) null;
      this.FileGridView.DefaultCellStyle.Font = new Font("Tahoma", 8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.FileGridView.DefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 50, 50, 50);
      this.FileGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.FileGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb((int) byte.MaxValue, 55, 55, 155);
      this.FileGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 50, 50, 50);
      this.FileGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      this.FileGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb((int) byte.MaxValue, 250, 250, 250);
      Services.LoadFormLayout((Form) this);
    }

    public void setFormLanguage()
    {
      this.FileGridView.Columns[0].HeaderText = this.Nav_Parent.Strx[21].ToString().Trim();
      this.FileGridView.Columns[1].HeaderText = this.Nav_Parent.Strx[13].ToString().Trim();
      this.FileGridView.Columns[2].HeaderText = this.Nav_Parent.Strx[14].ToString().Trim();
      this.FileGridView.Columns[3].HeaderText = this.Nav_Parent.Strx[22].ToString().Trim();
      this.FileGridView.Columns[4].HeaderText = this.Nav_Parent.Strx[23].ToString().Trim();
      this.FileGridView.Columns[5].HeaderText = this.Nav_Parent.Strx[24].ToString().Trim();
      this.FileGridView.Columns[6].HeaderText = this.Nav_Parent.Strx[25].ToString().Trim();
    }

    public void InitForm()
    {
      this.SearchGadget.Image = (Image) Navigator.Magn;
      this.CloseGadget.Image = (Image) Navigator.Cros;
    }

    public void AnimForm(int DestY)
    {
      while (Math.Abs(this.Top - DestY) > 1)
      {
        this.Top += (DestY - this.Top) / 4 + Math.Sign(DestY - this.Top);
        this.Refresh();
        Application.DoEvents();
      }
    }

    private void SearchBox_TextChanged(object sender, EventArgs e)
    {
      this.SearchFlag = true;
      this.SearchStart = 0;
    }

    public void SearchString(string InSearch, int type)
    {
      this.SearchType = type;
      if (this.SearchType == 0)
        this.SearchTypePic.Image = (Image) Navigator.Fils;
      if (this.SearchType == 1)
        this.SearchTypePic.Image = (Image) Navigator.CDRM;
      this.SearchBox.Text = InSearch;
    }

    private void SearchFile(string InString)
    {
      this.Cursor = Cursors.WaitCursor;
      this.FileList = !InString.Equals("") ? DataBase.GetAllRecords("FileList", "FileName LIMIT 1000 ", "[FileName] LIKE '%" + InString + "%'", false) : (ArrayList[]) null;
      this.FileGridView.RowCount = 0;
      this.FileGridView.RowCount = 10;
      if (this.FileList != null)
      {
        this.FileGridView.RowCount = this.FileList[0].Count;
        int rowCount = this.FileGridView.RowCount;
        for (int index1 = 0; index1 < rowCount; ++index1)
        {
          Navigator.VolumeDisc volumeById = Navigator.GetVolume_by_ID(this.FileList[1][index1].ToString());
          this.FileGridView[0, index1].Value = (object) Navigator.GetHolderName_by_DeviceID(volumeById.DeviceID);
          this.FileGridView[1, index1].Value = (object) volumeById.HolderPS;
          this.FileGridView[2, index1].Value = (object) volumeById.Label;
          string @string = this.FileList[2][index1].ToString();
          this.FileGridView[3, index1].Value = @string.Length < 3 ? (object) ".\\" : (object) Path.Combine(".\\", @string.Substring(3, @string.Length - 3));
          this.FileGridView[4, index1].Value = this.FileList[3][index1];
          int num1 = int.Parse(this.FileList[5][index1].ToString());
          int num2 = num1;
          int num3 = 0;
          int index2 = 0;
          int num4 = 1;
          for (; num1 > 1024 && index2 < 5; ++index2)
          {
            num1 /= 1024;
            num3 = num2 / num4 % 1024 / 100;
            num4 *= 1024;
          }
          if (num4 == 1)
            this.FileGridView[5, index1].Value = (object) (num1.ToString() + " " + Navigator.FileSize_Units[index2]);
          else
            this.FileGridView[5, index1].Value = (object) (num1.ToString() + "." + (object) num3 + " " + Navigator.FileSize_Units[index2]);
          this.FileGridView[6, index1].Value = this.FileList[7][index1];
        }
      }
      this.Cursor = Cursors.Default;
    }

    private void SearchVolume(string InString)
    {
      this.VolumeList = !InString.Equals("") ? Navigator.GetDBVolumeList("Label", "[Label] LIKE '%" + InString + "%'") : (ArrayList[]) null;
      this.FileGridView.RowCount = 0;
      if (this.VolumeList != null)
        this.FileGridView.RowCount = this.VolumeList[0].Count;
      int num1 = this.FileGridView.RowCount;
      if (num1 > 20)
        num1 = 20;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        this.FileGridView[0, index1].Value = (object) Navigator.GetHolderName_by_DeviceID(int.Parse(this.VolumeList[3][index1].ToString()));
        this.FileGridView[1, index1].Value = (object) this.VolumeList[4][index1].ToString();
        this.FileGridView[2, index1].Value = (object) this.VolumeList[2][index1].ToString();
        string str = this.VolumeList[1][index1].ToString() + "\\";
        this.FileGridView[3, index1].Value = (object) Path.Combine(".\\", str.Substring(3, str.Length - 3));
        this.FileGridView[4, index1].Value = (object) "-";
        int num2 = 0;
        int num3 = num2;
        int num4 = 0;
        int index2 = 0;
        int num5 = 1;
        for (; num2 > 1024 && index2 < 5; ++index2)
        {
          num2 /= 1024;
          num4 = num3 / num5 % 1024 / 100;
          num5 *= 1024;
        }
        if (num5 == 1)
          this.FileGridView[5, index1].Value = (object) (num2.ToString() + " " + Navigator.FileSize_Units[index2]);
        else
          this.FileGridView[5, index1].Value = (object) (num2.ToString() + "." + (object) num4 + " " + Navigator.FileSize_Units[index2]);
        this.FileGridView[6, index1].Value = this.VolumeList[5][index1];
      }
    }

    private void FileGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int rowIndex = e.RowIndex;
      if (rowIndex <= -1)
        return;
      int numberByDeviceId = Navigator.GetHolderNumber_by_DeviceID(this.Nav_Parent.GetHolder_By_Name(this.FileGridView[0, rowIndex].Value.ToString()).ID);
      int CurrCDNumber = int.Parse(this.FileGridView[1, rowIndex].Value.ToString());
      if (Navigator.HolderArray[numberByDeviceId].PS_Curr != CurrCDNumber && e.ColumnIndex < 2)
        Navigator.GoDevicePos(numberByDeviceId, CurrCDNumber);
      else if (this.SearchType == 0 && !this.FileList[6][rowIndex].ToString().Equals("Zip"))
      {
        string path1 = this.FileList[2][rowIndex].ToString();
        if (path1.Length > 3)
          path1 = path1.Substring(3, path1.Length - 3);
        string str = this.Nav_Parent.CheckIfFileIsOnline(Path.Combine(path1, this.FileList[3][rowIndex].ToString()), int.Parse(this.FileList[5][rowIndex].ToString()), this.FileList[7][rowIndex].ToString());
        if (File.Exists(str))
        {
          try
          {
            Process.Start(str);
          }
          catch
          {
          }
        }
      }
    }

    private void SearchForm_MouseUp(object sender, MouseEventArgs e)
    {
      Services.SaveFormLayout((Form) this);
    }

    private void CloseGadget_Click(object sender, EventArgs e)
    {
      this.SearchTimer.Enabled = false;
      this.AnimForm(Screen.PrimaryScreen.Bounds.Height);
      this.Hide();
    }

    private void SearchForm_Shown(object sender, EventArgs e)
    {
      this.SearchTimer.Enabled = true;
      this.SearchStart = 0;
      this.SearchFlag = false;
      this.AnimForm(Screen.PrimaryScreen.Bounds.Height - this.Height - 30);
      this.SearchBox.Focus();
    }

    private void SearchTimer_Tick(object sender, EventArgs e)
    {
      ++this.SearchStart;
      if (this.SearchStart <= 10 || !this.SearchFlag)
        return;
      this.SearchFlag = false;
      this.SearchStart = 0;
      if (this.SearchType == 0)
        this.SearchFile(this.SearchBox.Text);
      else if (this.SearchType == 1)
        this.SearchVolume(this.SearchBox.Text);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.SearchBox = new TextBox();
      this.FileGridView = new DataGridView();
      this.UnitName = new DataGridViewTextBoxColumn();
      this.Position = new DataGridViewTextBoxColumn();
      this.Volume = new DataGridViewTextBoxColumn();
      this.FolderName = new DataGridViewTextBoxColumn();
      this.FileName = new DataGridViewTextBoxColumn();
      this.FileSize = new DataGridViewTextBoxColumn();
      this.FileDate = new DataGridViewTextBoxColumn();
      this.SearchGadget = new PictureBox();
      this.CloseGadget = new PictureBox();
      this.SearchTimer = new Timer(this.components);
      this.SearchTypePic = new PictureBox();
      ((ISupportInitialize) this.FileGridView).BeginInit();
      ((ISupportInitialize) this.SearchGadget).BeginInit();
      ((ISupportInitialize) this.CloseGadget).BeginInit();
      ((ISupportInitialize) this.SearchTypePic).BeginInit();
      this.SuspendLayout();
      this.SearchBox.BackColor = Color.Gray;
      this.SearchBox.BorderStyle = BorderStyle.FixedSingle;
      this.SearchBox.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.SearchBox.ForeColor = Color.White;
      this.SearchBox.Location = new Point(41, 13);
      this.SearchBox.Name = "SearchBox";
      this.SearchBox.Size = new Size(190, 21);
      this.SearchBox.TabIndex = 50;
      this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
      this.FileGridView.AllowUserToAddRows = false;
      this.FileGridView.AllowUserToDeleteRows = false;
      this.FileGridView.AllowUserToResizeRows = false;
      this.FileGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.FileGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.FileGridView.Columns.AddRange((DataGridViewColumn) this.UnitName, (DataGridViewColumn) this.Position, (DataGridViewColumn) this.Volume, (DataGridViewColumn) this.FolderName, (DataGridViewColumn) this.FileName, (DataGridViewColumn) this.FileSize, (DataGridViewColumn) this.FileDate);
      this.FileGridView.Location = new Point(3, 39);
      this.FileGridView.Name = "FileGridView";
      this.FileGridView.ReadOnly = true;
      this.FileGridView.RowHeadersVisible = false;
      this.FileGridView.RowHeadersWidth = 38;
      this.FileGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.FileGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.FileGridView.Size = new Size(787, 244);
      this.FileGridView.TabIndex = 49;
      this.FileGridView.CellClick += new DataGridViewCellEventHandler(this.FileGridView_CellClick);
      this.UnitName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.UnitName.HeaderText = "Unit";
      this.UnitName.Name = "UnitName";
      this.UnitName.ReadOnly = true;
      this.UnitName.Width = 50;
      this.Position.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Position.HeaderText = "Pos";
      this.Position.Name = "Position";
      this.Position.ReadOnly = true;
      this.Position.Resizable = DataGridViewTriState.False;
      this.Position.Width = 49;
      this.Volume.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Volume.HeaderText = "Volume";
      this.Volume.Name = "Volume";
      this.Volume.ReadOnly = true;
      this.Volume.Width = 66;
      this.FolderName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.FolderName.HeaderText = "Folder";
      this.FolderName.Name = "FolderName";
      this.FolderName.ReadOnly = true;
      this.FolderName.Width = 60;
      this.FileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.FileName.HeaderText = "FileName";
      this.FileName.Name = "FileName";
      this.FileName.ReadOnly = true;
      this.FileName.Width = 75;
      this.FileSize.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.FileSize.HeaderText = "Size";
      this.FileSize.Name = "FileSize";
      this.FileSize.ReadOnly = true;
      this.FileSize.Width = 51;
      this.FileDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.FileDate.HeaderText = "Date";
      this.FileDate.Name = "FileDate";
      this.FileDate.ReadOnly = true;
      this.FileDate.Width = 54;
      this.SearchGadget.Location = new Point(237, 2);
      this.SearchGadget.Name = "SearchGadget";
      this.SearchGadget.Size = new Size(32, 32);
      this.SearchGadget.SizeMode = PictureBoxSizeMode.StretchImage;
      this.SearchGadget.TabIndex = 51;
      this.SearchGadget.TabStop = false;
      this.CloseGadget.Location = new Point(758, 2);
      this.CloseGadget.Name = "CloseGadget";
      this.CloseGadget.Size = new Size(32, 32);
      this.CloseGadget.SizeMode = PictureBoxSizeMode.StretchImage;
      this.CloseGadget.TabIndex = 52;
      this.CloseGadget.TabStop = false;
      this.CloseGadget.Click += new EventHandler(this.CloseGadget_Click);
      this.SearchTimer.Tick += new EventHandler(this.SearchTimer_Tick);
      this.SearchTypePic.Location = new Point(3, 2);
      this.SearchTypePic.Name = "SearchTypePic";
      this.SearchTypePic.Size = new Size(32, 32);
      this.SearchTypePic.SizeMode = PictureBoxSizeMode.StretchImage;
      this.SearchTypePic.TabIndex = 53;
      this.SearchTypePic.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(792, 295);
      this.Controls.Add((Control) this.SearchTypePic);
      this.Controls.Add((Control) this.CloseGadget);
      this.Controls.Add((Control) this.SearchGadget);
      this.Controls.Add((Control) this.SearchBox);
      this.Controls.Add((Control) this.FileGridView);
      this.Name = "SearchForm";
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "SearchForm";
      this.MouseUp += new MouseEventHandler(this.SearchForm_MouseUp);
      this.Shown += new EventHandler(this.SearchForm_Shown);
      ((ISupportInitialize) this.FileGridView).EndInit();
      ((ISupportInitialize) this.SearchGadget).EndInit();
      ((ISupportInitialize) this.CloseGadget).EndInit();
      ((ISupportInitialize) this.SearchTypePic).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
