// Decompiled with JetBrains decompiler
// Type: MainExe.Navigator
// Assembly: FindNow, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null
// MVID: 3A47578E-DA44-499F-9F9D-DFDF0549F517
// Assembly location: C:\Program Files (x86)\FindNow\FindNow.exe

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MainExe
{
  public class Navigator : Form
  {
    public static string NoDrive = "None";
    public static string AppName = "shelook";
    public static string OSVersion = "Linux";
    public static string VersionSTR = "Linux";
    public static Font myFont = (Font) null;
    private static Random rnd = new Random();
    public static Navigator.Holder null_Holder = new Navigator.Holder();
    public static Navigator.VolumeDisc null_Volume = new Navigator.VolumeDisc();
    public static int LastUpdateTime = 0;
    public static int numDevice = 0;
    public static int ActiveVolumePos = -1;
    public static int exActiveVolumePos = -1;
    private static bool ExValueFlag = true;
    private static string ExValue = "";
    public static string EMPTY_VOLUME = "<EMPTY>";
    public static string[] FileSize_Units = new string[10];
    private static int iFile = 0;
    private static int AnimPerc = 0;
    private static int cSlot = 0;
    private IContainer components = (IContainer) null;
    private bool ExchangeFlag = false;
    private int ExchangePos = 0;
    private int ExchangeHolder = 0;
    public ArrayList Strx = (ArrayList) null;
    private Bitmap ScreenBMP = (Bitmap) null;
    private bool MoveIcon = false;
    private int dAnimStep = 100;
    private int dAnimDest = 50;
    private float[] dAnim = new float[1000];
    private int MoveIconX = 0;
    private int MoveIconY = 0;
    private int StartIconX = 0;
    private int StartIconY = 0;
    private int Spacing_X = 80;
    private int Spacing_Y = 80;
    public bool FrameReady = false;
    private int cursorX = 0;
    private int cursorY = 0;
    private int Level = 0;
    private int newLevel = 0;
    private int ActiveHolder = 0;
    private int ExActiveHolder = -1;
    private int[] OptionX = new int[1000];
    private int[] OptionY = new int[1000];
    private int myWidth = 0;
    private int myHeight = 0;
    private DataGridView VolumeGrid;
    private PictureBox BufferImage;
    private DataGridViewTextBoxColumn Column1;
    private DataGridViewTextBoxColumn Column2;
    public ComboBox ComboHolder;
    public ComboBox ComboDrives;
    private PictureBox SwapCursorImage;
    private System.Windows.Forms.Timer timerSwap;
    public static Credits_Sent_Form Credit_Form;
    public static Progress_Form Progress_Form;
    public static Splash_Form Load_Form;
    public static AskForm Asking_Form;
    public static SearchForm Search_Form;
    public static Navigator Naviga_Form;
    public static Language Main_Language;
    public string MAC_ADDRESS;
    public static Navigator.Holder[] HolderArray;
    public static Bitmap Logo;
    public static Bitmap Eggs;
    public static Bitmap Hold;
    public static Bitmap Fold;
    public static Bitmap Exch;
    public static Bitmap Pape;
    public static Bitmap CapS;
    public static Bitmap CDRM;
    public static Bitmap Grid;
    public static Bitmap Fils;
    public static Bitmap Magn;
    public static Bitmap Libr;
    public static Bitmap Tick;
    public static Bitmap Cros;
    public static Bitmap Plus;
    public static Bitmap Minu;
    public static Bitmap Pens;
    public static Bitmap BLan;
    private int MainX;
    private int MainY;
    private Size SystemIconSize;
    private Size SheLookIconSize_64;
    private Size SheLookIconSize_48;
    private Size SheLookIconSize_32;
    private Graphics LocGraph;
    private Brush myBrush;
    private Brush myBrush_Blue;
    private Brush myBrush_Black;
    private Brush myBrush_Gray;
    private Size IconSpacing;
    private static ArrayList[] fields;

    public Navigator()
    {
      this.InitDB();
      Navigator.loadLanguageSettings();
      this.InitializeComponent();
      this.setFormLanguage();
      this.InitUSBDevices();
      this.InitComboUnit();
      this.FormBorderStyle = FormBorderStyle.None;
      this.TransparencyKey = Color.FromArgb(0, 0, 0);
      this.BackColor = Color.FromArgb(0, 0, 0);
      this.IconSpacing = this.GetIconSpacing();
      Services.GetIconPosition();
      this.SystemIconSize = SystemInformation.IconSize;
      this.SheLookIconSize_64 = new Size(64, 64);
      this.SheLookIconSize_48 = new Size(48, 48);
      this.SheLookIconSize_32 = new Size(32, 32);
      this.BufferImage.Left = 0;
      this.BufferImage.Top = 0;
      this.BufferImage.Width = Screen.PrimaryScreen.Bounds.Width;
      this.BufferImage.Height = Screen.PrimaryScreen.Bounds.Height;
      this.BufferImage.Image = (Image) new Bitmap(this.BufferImage.Width, this.BufferImage.Height);
      this.VolumeGrid.DefaultCellStyle.Font = new Font("Tahoma", 8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.VolumeGrid.DefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      this.VolumeGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.VolumeGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb((int) byte.MaxValue, 55, 55, 155);
      this.VolumeGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      this.VolumeGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb((int) byte.MaxValue, 250, 250, 250);
      Services.UsedFormListAdd((Form) this);
      Services.LoadFormLayout((Form) this);
      this.RefreshHolderList();
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
      this.VolumeGrid = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.Column2 = new DataGridViewTextBoxColumn();
      this.BufferImage = new PictureBox();
      this.ComboHolder = new ComboBox();
      this.ComboDrives = new ComboBox();
      this.SwapCursorImage = new PictureBox();
      this.timerSwap = new System.Windows.Forms.Timer(this.components);
      ((ISupportInitialize) this.VolumeGrid).BeginInit();
      ((ISupportInitialize) this.BufferImage).BeginInit();
      ((ISupportInitialize) this.SwapCursorImage).BeginInit();
      this.SuspendLayout();
      this.VolumeGrid.AllowUserToAddRows = false;
      this.VolumeGrid.AllowUserToDeleteRows = false;
      this.VolumeGrid.AllowUserToResizeColumns = false;
      this.VolumeGrid.AllowUserToResizeRows = false;
      this.VolumeGrid.BorderStyle = BorderStyle.None;
      this.VolumeGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.VolumeGrid.ColumnHeadersHeight = 15;
      this.VolumeGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.VolumeGrid.Columns.AddRange((DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2);
      this.VolumeGrid.Location = new Point(87, 12);
      this.VolumeGrid.Name = "VolumeGrid";
      this.VolumeGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.VolumeGrid.RowHeadersVisible = false;
      this.VolumeGrid.Size = new Size(183, 190);
      this.VolumeGrid.TabIndex = 2;
      this.VolumeGrid.Visible = false;
      this.VolumeGrid.CellEndEdit += new DataGridViewCellEventHandler(this.VolumeGrid_CellEndEdit);
      this.VolumeGrid.CellClick += new DataGridViewCellEventHandler(this.VolumeGrid_CellClick);
      this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.Column1.HeaderText = "Pos";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Resizable = DataGridViewTriState.False;
      this.Column1.Width = 48;
      this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.Column2.HeaderText = "Volume";
      this.Column2.Name = "Column2";
      this.BufferImage.Location = new Point(12, 12);
      this.BufferImage.Name = "BufferImage";
      this.BufferImage.Size = new Size(41, 42);
      this.BufferImage.TabIndex = 3;
      this.BufferImage.TabStop = false;
      this.BufferImage.Visible = false;
      this.ComboHolder.FormattingEnabled = true;
      this.ComboHolder.Location = new Point(37, 172);
      this.ComboHolder.Name = "ComboHolder";
      this.ComboHolder.Size = new Size(207, 21);
      this.ComboHolder.TabIndex = 4;
      this.ComboHolder.Visible = false;
      this.ComboDrives.FormattingEnabled = true;
      this.ComboDrives.Location = new Point(37, 145);
      this.ComboDrives.Name = "ComboDrives";
      this.ComboDrives.Size = new Size(160, 21);
      this.ComboDrives.TabIndex = 5;
      this.ComboDrives.Visible = false;
      this.SwapCursorImage.Location = new Point(12, 60);
      this.SwapCursorImage.Name = "SwapCursorImage";
      this.SwapCursorImage.Size = new Size(41, 42);
      this.SwapCursorImage.TabIndex = 6;
      this.SwapCursorImage.TabStop = false;
      this.SwapCursorImage.Visible = false;
      this.timerSwap.Enabled = true;
      this.timerSwap.Tick += new EventHandler(this.timerSwap_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(275, 205);
      this.Controls.Add((Control) this.SwapCursorImage);
      this.Controls.Add((Control) this.ComboDrives);
      this.Controls.Add((Control) this.ComboHolder);
      this.Controls.Add((Control) this.BufferImage);
      this.Controls.Add((Control) this.VolumeGrid);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = "Navigator";
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.TopMost = true;
      this.MouseUp += new MouseEventHandler(this.Navigator_MouseUp);
      this.Leave += new EventHandler(this.Navigator_Leave);
      this.MouseDown += new MouseEventHandler(this.Navigator_MouseDown);
      this.MouseLeave += new EventHandler(this.Navigator_MouseLeave);
      this.MouseMove += new MouseEventHandler(this.Navigator_MouseMove);
      ((ISupportInitialize) this.VolumeGrid).EndInit();
      ((ISupportInitialize) this.BufferImage).EndInit();
      ((ISupportInitialize) this.SwapCursorImage).EndInit();
      this.ResumeLayout(false);
    }

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Navigator.VersionSTR = Services.GetVersion();
      Navigator.OSVersion = Services.getOSInfo();
      Navigator.FileSize_Units[0] = "b";
      Navigator.FileSize_Units[1] = "KB";
      Navigator.FileSize_Units[2] = "MB";
      Navigator.FileSize_Units[3] = "GB";
      Navigator.FileSize_Units[4] = "TB";
      Navigator.LoadGraph(Navigator.AppName);
      Navigator.Load_Form = new Splash_Form();
      Navigator.Load_Form.Show();
      Services.fade((Form) Navigator.Load_Form, 10);
      Services.MainColor_Fore_Label = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      Services.MainColor_Back_Label = Color.FromArgb(11, 38, 50);
      Services.MainColor_Fore_Menu = Color.FromArgb(20, 20, 20);
      Services.MainColor_BackTabPage = Color.FromArgb(200, 215, 205);
      Services.MainColor_BackCamScreen = Color.FromArgb(172, 172, 172);
      Services.MainColor_Fore_Button = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      Services.MainColor_Fore_Text = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 190, 0);
      Services.MainColor_Back_Text = Color.FromArgb(20, 20, 20);
      Navigator.Naviga_Form = new Navigator();
      Navigator.Search_Form = new SearchForm(Navigator.Naviga_Form);
      Navigator.Asking_Form = new AskForm();
      Navigator.Credit_Form = new Credits_Sent_Form();
      Navigator.Search_Form.InitForm();
      Navigator.Naviga_Form.ResetFormsLanguages();
      Navigator.Naviga_Form.Show();
      Navigator.Naviga_Form.InitAnimation();
      Navigator.Naviga_Form.Animation(true);
      Navigator.Load_Form.Hide();
      Application.Run((Form) Navigator.Naviga_Form);
    }

    public void ResetFormsLanguages()
    {
      Navigator.Naviga_Form.setFormLanguage();
      Navigator.Credit_Form.setFormLanguage();
      Navigator.Search_Form.setFormLanguage();
    }

    public static void exitApplication(bool exit_real)
    {
      if (exit_real)
        Services.SaveAllFormLayout();
      DataBase.DeInitDB();
      Thread.Sleep(100);
      if (!exit_real)
        return;
      Services.fade((Form) Navigator.Load_Form, -10);
      Environment.Exit(0);
    }

    public static void loadLanguageSettings()
    {
      try
      {
        Language.setCurrLanguage(int.Parse(DataBase.getParameter("application", "language")));
      }
      catch
      {
        Language.setCurrLanguage(0);
        DataBase.setParameter("application", "language", Language.CurrLanguage.ToString());
      }
    }

    public void setFormLanguage()
    {
      this.Strx = Language.loadFormStrings(this.Name, Language.CurrLanguage);
      this.VolumeGrid.Columns[0].HeaderText = this.Strx[13].ToString().Trim();
      this.VolumeGrid.Columns[1].HeaderText = this.Strx[14].ToString().Trim();
    }

    public static void LoadGraph(string AppName)
    {
      Navigator.Libr = new Bitmap(Services.getResource("Images\\Library.gif"));
      Navigator.Hold = new Bitmap(Services.getResource("Images\\Holder.gif"));
      Navigator.Pape = new Bitmap(Services.getResource("Images\\Paper.gif"));
      Navigator.Fold = new Bitmap(Services.getResource("Images\\Folder.gif"));
      Navigator.Exch = new Bitmap(Services.getResource("Images\\Exchange.gif"));
      Navigator.CDRM = new Bitmap(Services.getResource("Images\\Cdrom.gif"));
      Navigator.Grid = new Bitmap(Services.getResource("Images\\NaviGrid.gif"));
      Navigator.Fils = new Bitmap(Services.getResource("Images\\File.gif"));
      Navigator.CapS = new Bitmap(Services.getResource("Images\\Cap.gif"));
      Navigator.Magn = new Bitmap(Services.getResource("Images\\Magnifier.gif"));
      Navigator.Cros = new Bitmap(Services.getResource("Images\\Cross.gif"));
      Navigator.Tick = new Bitmap(Services.getResource("Images\\Tick.gif"));
      Navigator.Plus = new Bitmap(Services.getResource("Images\\Plus.gif"));
      Navigator.Minu = new Bitmap(Services.getResource("Images\\Minus.gif"));
      Navigator.Pens = new Bitmap(Services.getResource("Images\\Pen.gif"));
      Navigator.Logo = new Bitmap(Services.getResource("Images\\Logo.gif"));
      Services.BImg = new Bitmap(Services.getResource("Images\\BckImg.bmp"));
      Services.BFac = new Bitmap(Services.getResource("Images\\Button.gif"));
      Services.MIco = Services.MakeIcon((Image) new Bitmap(Services.getResource("Images\\icon.gif")), 32, true);
    }

    public void InitAnimation()
    {
      this.MainX = this.IconSpacing.Width / 2 - this.SystemIconSize.Width / 2;
      this.MainY = this.IconSpacing.Height / 2 - this.SystemIconSize.Height;
      this.Width = this.MainX + this.SystemIconSize.Width;
      this.Height = this.MainY + this.SystemIconSize.Height + 32;
      this.LocGraph = Graphics.FromImage(this.BufferImage.Image);
      this.myBrush = (Brush) new SolidBrush(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.myBrush_Blue = (Brush) new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0, 200));
      this.myBrush_Black = (Brush) new SolidBrush(Color.FromArgb((int) byte.MaxValue, 50, 50, 50));
      this.myBrush_Gray = (Brush) new SolidBrush(Color.FromArgb((int) byte.MaxValue, 150, 150, 150));
      Navigator.myFont = SystemFonts.IconTitleFont;
      this.TopMost = false;
      this.SendToBack();
      this.Animation(false);
    }

    public void SetMaxWH(int maxW, int maxH)
    {
      if (this.myWidth < maxW)
        this.myWidth = maxW;
      if (this.myHeight >= maxH)
        return;
      this.myHeight = maxH;
    }

    public void DrawSingleIcon()
    {
      this.OptionX[0] = this.MainX;
      this.OptionY[0] = this.MainY;
      Size IconSize = new Size((int) ((double) this.SheLookIconSize_64.Width - (double) (this.SheLookIconSize_64.Width - this.SystemIconSize.Width) * (double) this.dAnim[0] / 100.0), (int) ((double) this.SheLookIconSize_64.Height - (double) (this.SheLookIconSize_64.Height - this.SystemIconSize.Height) * (double) this.dAnim[0] / 100.0));
      float opacity = 1f;
      float num = 0.5f;
      if (this.ExchangeFlag)
        opacity = num;
      this.PutIcon((Image) Navigator.CapS, (Image) null, this.Strx[0].ToString(), this.OptionX[0], this.OptionY[0], IconSize, opacity);
      this.SetMaxWH(this.OptionX[0] + this.Spacing_X, this.OptionY[0] + this.Spacing_Y);
    }

    public void DrawMainMenu()
    {
      this.OptionX[10] = this.MainX;
      this.OptionY[10] = this.MainY;
      this.OptionX[11] = this.Level != 30 && this.Level != 40 ? this.MainX : this.MainX + (int) (32.0 * (double) this.dAnim[30] / 100.0);
      this.OptionY[11] = this.MainY + (int) ((double) this.Spacing_Y * (double) this.dAnim[10] / 100.0);
      this.OptionX[12] = this.Level != 20 ? this.MainX : this.MainX + (int) (32.0 * (double) this.dAnim[20] / 100.0);
      this.OptionY[12] = this.MainY + (int) ((double) (this.Spacing_Y * 2) * (double) this.dAnim[10] / 100.0);
      this.OptionX[13] = this.MainX;
      this.OptionY[13] = this.MainY + (int) ((double) (this.Spacing_Y * 3) * (double) this.dAnim[10] / 100.0);
      Size IconSize = new Size((int) (64.0 * (double) this.dAnim[10] / 100.0), (int) (64.0 * (double) this.dAnim[10] / 100.0));
      float num = (float) (((double) this.dAnim[10] - 50.0) / 100.0);
      float opacity1 = this.ExchangeFlag ? num : (float) (((double) this.dAnim[10] - 50.0) / 50.0);
      this.PutIcon((Image) Navigator.CapS, (Image) null, this.Strx[0].ToString(), this.OptionX[10], this.OptionY[10], IconSize, opacity1);
      float opacity2 = this.Level != 30 && this.Level != 10 || this.ExchangeFlag ? num : (float) (((double) this.dAnim[10] - 50.0) / 50.0);
      this.PutIcon((Image) Navigator.Hold, (Image) null, this.Strx[1].ToString(), this.OptionX[11], this.OptionY[11], IconSize, opacity2);
      float opacity3 = this.Level != 20 && this.Level != 10 || this.ExchangeFlag ? num : (float) (((double) this.dAnim[10] - 50.0) / 50.0);
      this.PutIcon((Image) Navigator.Magn, (Image) null, this.Strx[2].ToString(), this.OptionX[12], this.OptionY[12], IconSize, opacity3);
      float opacity4 = this.Level != 10 || this.ExchangeFlag ? num : (float) (((double) this.dAnim[10] - 50.0) / 50.0);
      this.PutIcon((Image) Navigator.BLan, (Image) null, Language.CurrLanguageSTR, this.OptionX[13], this.OptionY[13], IconSize, opacity4);
      this.SetMaxWH(this.OptionX[10] + this.Spacing_X, this.OptionY[13] + this.Spacing_Y);
    }

    public void DrawSearchMenu()
    {
      this.OptionX[20] = this.OptionX[12] + this.Spacing_X;
      this.OptionY[20] = this.OptionY[12];
      this.OptionX[21] = this.OptionX[12] + this.Spacing_X;
      this.OptionY[21] = this.OptionY[12] + this.Spacing_Y;
      this.PutIcon((Image) Navigator.Fils, (Image) Navigator.Magn, this.Strx[3].ToString(), this.OptionX[20], this.OptionY[20], this.SheLookIconSize_64, this.dAnim[20] / 100f);
      this.PutIcon((Image) Navigator.CDRM, (Image) Navigator.Magn, this.Strx[4].ToString(), this.OptionX[21], this.OptionY[21], this.SheLookIconSize_64, this.dAnim[20] / 100f);
      this.SetMaxWH(this.OptionX[20] + this.Spacing_X, this.OptionY[21] + this.Spacing_Y);
    }

    public void DrawHolderList()
    {
      int num = Navigator.numDevice;
      for (int index = 0; index < num; ++index)
      {
        Navigator.HolderArray[index].iconX = this.OptionX[11] + this.Spacing_X;
        Navigator.HolderArray[index].iconY = this.OptionY[11] + index * this.Spacing_Y;
        if (this.ActiveHolder == index && this.Level == 40)
          Navigator.HolderArray[index].iconX += (int) (32.0 * (double) this.dAnim[40] / 100.0);
        float opacity = this.dAnim[30] / 200f;
        if (this.ActiveHolder == index || this.Level == 30)
          opacity = this.dAnim[30] / 100f;
        this.PutIcon(Navigator.HolderArray[index].DeviceImg, (Image) null, Navigator.HolderArray[index].LabelName, Navigator.HolderArray[index].iconX, Navigator.HolderArray[index].iconY, this.SheLookIconSize_64, opacity);
      }
      this.OptionX[30] = this.OptionX[11] + this.Spacing_X;
      this.OptionY[30] = this.OptionY[11] + num * this.Spacing_Y;
      float opacity1 = this.Level != 30 || this.ExchangeFlag ? this.dAnim[30] / 200f : this.dAnim[30] / 100f;
      this.PutIcon((Image) Navigator.Hold, (Image) Navigator.Plus, this.Strx[5].ToString(), this.OptionX[30], this.OptionY[30], this.SheLookIconSize_64, opacity1);
      this.SetMaxWH(this.OptionX[30] + this.Spacing_X, this.OptionY[30] + this.Spacing_Y);
    }

    public void DrawVolumeList()
    {
      this.VolumeGrid.Visible = (double) this.dAnim[40] > 98.0;
      this.VolumeGrid.Left = Navigator.HolderArray[this.ActiveHolder].iconX + 64;
      this.VolumeGrid.Top = Navigator.HolderArray[this.ActiveHolder].iconY + 16;
      this.DrawImageAlpha((Image) Navigator.Grid, this.VolumeGrid.Left, this.VolumeGrid.Top, this.VolumeGrid.Width, this.VolumeGrid.Height, this.dAnim[40] / 100f);
      this.OptionX[40] = this.VolumeGrid.Left;
      this.OptionY[40] = this.VolumeGrid.Top + this.VolumeGrid.Height;
      this.OptionX[41] = this.OptionX[40] + 64;
      this.OptionY[41] = this.OptionY[40];
      this.OptionX[42] = this.OptionX[40];
      this.OptionY[42] = this.OptionY[40] + 64;
      this.OptionX[43] = this.OptionX[40] + 64;
      this.OptionY[43] = this.OptionY[40] + 64;
      this.OptionX[44] = this.OptionX[40] + 64 + 64;
      this.OptionY[44] = this.OptionY[40];
      float opacity = this.dAnim[40] / 100f;
      float num = this.dAnim[40] / 200f;
      if (this.ExchangeFlag)
        opacity = num;
      this.PutIcon((Image) Navigator.CDRM, (Image) Navigator.Plus, this.Strx[6].ToString(), this.OptionX[40], this.OptionY[40], this.SheLookIconSize_48, opacity);
      this.PutIcon((Image) Navigator.CDRM, (Image) Navigator.Minu, this.Strx[7].ToString(), this.OptionX[41], this.OptionY[41], this.SheLookIconSize_48, opacity);
      this.PutIcon((Image) Navigator.Hold, (Image) Navigator.Pens, this.Strx[9].ToString(), this.OptionX[42], this.OptionY[42], this.SheLookIconSize_48, opacity);
      this.PutIcon((Image) Navigator.Hold, (Image) Navigator.Minu, this.Strx[10].ToString(), this.OptionX[43], this.OptionY[43], this.SheLookIconSize_48, opacity);
      this.PutIcon((Image) Navigator.CDRM, (Image) Navigator.Exch, this.Strx[8].ToString(), this.OptionX[44], this.OptionY[44], this.SheLookIconSize_48, this.dAnim[40] / 100f);
      this.SetMaxWH(this.OptionX[40] + this.VolumeGrid.Width, this.OptionY[43] + 64 + 32);
    }

    public bool Animation(bool transp)
    {
      bool flag1 = true;
      while (Math.Abs(this.dAnimStep - this.dAnimDest) > 0 || flag1)
      {
        this.dAnimStep += (this.dAnimDest - this.dAnimStep) / 3 + Math.Sign(this.dAnimDest - this.dAnimStep);
        flag1 = false;
        this.dAnim[this.Level] = (float) this.dAnimStep;
        this.myWidth = 0;
        this.myHeight = 0;
        bool flag2 = 1 == 0;
        this.LocGraph.Clear(Color.Transparent);
        if (this.Level == 0)
          this.DrawSingleIcon();
        if (this.Level >= 10)
          this.DrawMainMenu();
        if (this.Level == 20)
          this.DrawSearchMenu();
        if (this.Level == 30 || this.Level == 40)
          this.DrawHolderList();
        if (this.Level == 40)
          this.DrawVolumeList();
        this.BufferImage.Refresh();
        this.BackgroundImage = this.BufferImage.Image;
        this.Width = this.myWidth;
        this.Height = this.myHeight;
        this.Refresh();
        if (Math.Abs(this.dAnimStep - this.dAnimDest) == 1)
        {
          this.Level = this.newLevel;
          this.dAnimStep = 99;
          this.dAnimDest = 100;
        }
      }
      return true;
    }

    public void DrawRoundRect(Graphics g, Pen p, float x, float y, float width, float height, float radius)
    {
      GraphicsPath path = new GraphicsPath();
      path.AddLine(x + radius, y, (float) ((double) x + (double) width - (double) radius * 2.0), y);
      path.AddArc((float) ((double) x + (double) width - (double) radius * 2.0), y, radius * 2f, radius * 2f, 270f, 90f);
      path.AddLine(x + width, y + radius, x + width, (float) ((double) y + (double) height - (double) radius * 2.0));
      path.AddArc((float) ((double) x + (double) width - (double) radius * 2.0), (float) ((double) y + (double) height - (double) radius * 2.0), radius * 2f, radius * 2f, 0.0f, 90f);
      path.AddLine((float) ((double) x + (double) width - (double) radius * 2.0), y + height, x + radius, y + height);
      path.AddArc(x, (float) ((double) y + (double) height - (double) radius * 2.0), radius * 2f, radius * 2f, 90f, 90f);
      path.AddLine(x, (float) ((double) y + (double) height - (double) radius * 2.0), x, y + radius);
      path.AddArc(x, y, radius * 2f, radius * 2f, 180f, 90f);
      path.CloseFigure();
      g.DrawPath(p, path);
      path.Dispose();
    }

    private void PutIcon(Image IconImg, Image IconImg_2nd, string IconName, int _IconX, int _IconY, Size IconSize, float opacity)
    {
      int width = IconSize.Width;
      int height = IconSize.Height;
      int w = width / 2;
      int h = height / 2;
      int num1 = _IconX + w;
      int num2 = _IconY + h;
      SizeF sizeF = this.LocGraph.MeasureString(IconName, Navigator.myFont);
      int num3 = (int) sizeF.Width;
      int num4 = (int) sizeF.Height;
      int num5 = num3 / 2;
      int num6 = num4 / 2;
      int num7 = num1;
      int y = num2;
      this.DrawImageAlpha(IconImg, num7 - w, y - h, width, height, opacity);
      if (IconImg_2nd != null)
        this.DrawImageAlpha(IconImg_2nd, num7 - w, y, w, h, opacity);
      Brush brush1 = (Brush) new SolidBrush(Color.FromArgb((int) ((double) byte.MaxValue * (double) opacity), (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      Brush brush2 = (Brush) new SolidBrush(Color.FromArgb((int) ((double) byte.MaxValue * (double) opacity), 20, 20, 20));
      this.LocGraph.DrawString(IconName, Navigator.myFont, brush2, (PointF) new Point(num7 - num5, y + h - 1));
      this.LocGraph.DrawString(IconName, Navigator.myFont, brush2, (PointF) new Point(num7 - num5 - 1, y + h));
      this.LocGraph.DrawString(IconName, Navigator.myFont, brush2, (PointF) new Point(num7 - num5, y + h + 1));
      this.LocGraph.DrawString(IconName, Navigator.myFont, brush2, (PointF) new Point(num7 - num5 + 1, y + h));
      this.LocGraph.DrawString(IconName, Navigator.myFont, brush1, (PointF) new Point(num7 - num5, y + h));
    }

    private void Navigator_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.MoveIcon)
      {
        this.Left += e.X - this.MoveIconX;
        this.Top += e.Y - this.MoveIconY;
      }
      this.cursorX = e.X;
      this.cursorY = e.Y;
    }

    public bool CheckIcon(int cursorX, int cursorY, int inX, int inY, Size IconSize)
    {
      return cursorX > inX && cursorY > inY && (cursorX < inX + IconSize.Width && cursorY < inY + IconSize.Height);
    }

    public Size GetIconSpacing()
    {
      return new Size(-1, -1)
      {
        Width = SystemInformation.IconHorizontalSpacing,
        Height = SystemInformation.IconVerticalSpacing
      };
    }

    private void Navigator_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.MoveIcon = true;
        this.StartIconX = this.Left;
        this.StartIconY = this.Top;
        this.MoveIconX = e.X;
        this.MoveIconY = e.Y;
        this.Animation(true);
      }
      if (e.Button != MouseButtons.Right || !Navigator.AskYesNo(this.Strx[26].ToString()))
        return;
      Navigator.exitApplication(true);
    }

    private void Navigator_MouseUp(object sender, MouseEventArgs e)
    {
      this.MoveIcon = false;
      if (Math.Abs(this.StartIconX - this.Left) < 5 && Math.Abs(this.StartIconY - this.Top) < 5)
      {
        this.DriveMenu();
      }
      else
      {
        Services.SaveFormLayout((Form) this);
        this.ScreenBMP = (Bitmap) null;
      }
    }

    public void FillVolumeDataGrid()
    {
      this.Cursor = Cursors.WaitCursor;
      Navigator.RefreshVolumeList(this.ActiveHolder, ref this.VolumeGrid);
      this.Cursor = Cursors.Default;
      Navigator.ActiveVolumePos = 1;
    }

    private void VolumeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;
      Navigator.GoDevicePos(this.ActiveHolder, int.Parse(this.VolumeGrid[0, e.RowIndex].Value.ToString()));
      Navigator.ActiveVolumePos = e.RowIndex + 1;
      if (!this.ExchangeFlag)
        return;
      Navigator.VolumeDisc VolumeDisc1 = new Navigator.VolumeDisc();
      VolumeDisc1.HolderPS = Navigator.ActiveVolumePos;
      VolumeDisc1.DeviceID = Navigator.HolderArray[this.ActiveHolder].DeviceID;
      ArrayList[] dbVolumeList1 = Navigator.GetDBVolumeList("ID", "DeviceID=" + (object) VolumeDisc1.DeviceID + " and HolderPS=" + (object) VolumeDisc1.HolderPS);
      if (dbVolumeList1[0].Count > 0)
      {
        VolumeDisc1.ID = int.Parse(dbVolumeList1[0][0].ToString());
        VolumeDisc1.Drive = dbVolumeList1[1][0].ToString();
        VolumeDisc1.Label = dbVolumeList1[2][0].ToString();
        VolumeDisc1.RegDate = dbVolumeList1[5][0].ToString();
      }
      else
      {
        VolumeDisc1.ID = 0;
        VolumeDisc1.Drive = "";
        VolumeDisc1.Label = Navigator.EMPTY_VOLUME;
        VolumeDisc1.RegDate = "";
      }
      Navigator.VolumeDisc VolumeDisc2 = new Navigator.VolumeDisc();
      VolumeDisc2.HolderPS = this.ExchangePos;
      VolumeDisc2.DeviceID = Navigator.HolderArray[this.ExchangeHolder].DeviceID;
      ArrayList[] dbVolumeList2 = Navigator.GetDBVolumeList("ID", "DeviceID=" + (object) VolumeDisc2.DeviceID + " and HolderPS=" + (object) VolumeDisc2.HolderPS);
      if (dbVolumeList2[0].Count > 0)
      {
        VolumeDisc2.ID = int.Parse(dbVolumeList2[0][0].ToString());
        VolumeDisc2.Drive = dbVolumeList2[1][0].ToString();
        VolumeDisc2.Label = dbVolumeList2[2][0].ToString();
        VolumeDisc2.RegDate = dbVolumeList2[5][0].ToString();
      }
      else
      {
        VolumeDisc2.ID = 0;
        VolumeDisc2.Drive = "";
        VolumeDisc2.Label = Navigator.EMPTY_VOLUME;
        VolumeDisc2.RegDate = "";
      }
      Navigator.VolumeDisc volumeDisc1 = new Navigator.VolumeDisc();
      Navigator.VolumeDisc volumeDisc2 = new Navigator.VolumeDisc();
      Navigator.VolumeDisc volumeDisc3 = VolumeDisc1;
      Navigator.VolumeDisc volumeDisc4 = VolumeDisc2;
      VolumeDisc1.DeviceID = volumeDisc4.DeviceID;
      VolumeDisc1.HolderPS = volumeDisc4.HolderPS;
      VolumeDisc2.DeviceID = volumeDisc3.DeviceID;
      VolumeDisc2.HolderPS = volumeDisc3.HolderPS;
      if (VolumeDisc1.Label.Equals(Navigator.EMPTY_VOLUME))
        this.DeleteVolume(this.ActiveHolder, volumeDisc3.HolderPS);
      if (VolumeDisc2.Label.Equals(Navigator.EMPTY_VOLUME))
        this.DeleteVolume(this.ExchangeHolder, volumeDisc4.HolderPS);
      if (!VolumeDisc2.Label.Equals(Navigator.EMPTY_VOLUME))
        Navigator.EditVolume(VolumeDisc2);
      if (!VolumeDisc1.Label.Equals(Navigator.EMPTY_VOLUME))
        Navigator.EditVolume(VolumeDisc1);
      Navigator.RefreshVolumeList(this.ActiveHolder, ref this.VolumeGrid);
      this.ExchangePos = -1;
      this.ExchangeHolder = -1;
      this.ExchangeFlag = false;
      this.Animation(false);
    }

    private void StartSearch(int type)
    {
      Navigator.Search_Form.Hide();
      Navigator.Search_Form.SearchString("", type);
      Navigator.Search_Form.Left = (Screen.PrimaryScreen.Bounds.Width - Navigator.Search_Form.Width) / 2;
      Navigator.Search_Form.Top = Screen.PrimaryScreen.Bounds.Height;
      int num = (int) Navigator.Search_Form.ShowDialog();
    }

    private void GetScreenShot()
    {
      this.Visible = false;
      Size size1 = Screen.PrimaryScreen.Bounds.Size;
      int width = size1.Width;
      size1 = Screen.PrimaryScreen.Bounds.Size;
      int height = size1.Height;
      this.ScreenBMP = new Bitmap(width, height);
      Graphics graphics1 = Graphics.FromImage((Image) this.ScreenBMP);
      Graphics graphics2 = graphics1;
      Rectangle bounds = Screen.PrimaryScreen.Bounds;
      int x = bounds.X;
      bounds = Screen.PrimaryScreen.Bounds;
      int y = bounds.Y;
      int destinationX = 0;
      int destinationY = 0;
      bounds = Screen.PrimaryScreen.Bounds;
      Size size2 = bounds.Size;
      int num = 13369376;
      graphics2.CopyFromScreen(x, y, destinationX, destinationY, size2, (CopyPixelOperation) num);
      graphics1.Dispose();
      this.Visible = true;
    }

    private bool DriveSingleIcon()
    {
      if (!this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[0], this.OptionY[0], this.SystemIconSize))
        return false;
      this.TopMost = true;
      this.BringToFront();
      this.Level = 10;
      this.newLevel = 10;
      this.dAnimStep = 50;
      this.dAnimDest = 100;
      this.VolumeGrid.Visible = false;
      this.ScreenBMP = (Bitmap) null;
      this.Animation(false);
      return true;
    }

    private bool DriveMainMenu()
    {
      if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[10], this.OptionY[10], this.SheLookIconSize_64))
      {
        this.TopMost = false;
        this.SendToBack();
        this.Level = 10;
        this.newLevel = 0;
        this.dAnimStep = 100;
        this.dAnimDest = 50;
        this.VolumeGrid.Visible = false;
        this.Animation(false);
        return true;
      }
      if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[11], this.OptionY[11], this.SheLookIconSize_64))
      {
        if (this.Level == 10 || this.Level == 20)
        {
          this.dAnimStep = 0;
          this.dAnimDest = 100;
          this.Level = 30;
          this.newLevel = 30;
        }
        else if (this.Level == 30 || this.Level == 40)
        {
          this.dAnimStep = 100;
          this.dAnimDest = 0;
          this.Level = 30;
          this.newLevel = 10;
        }
        this.Animation(false);
        return true;
      }
      if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[12], this.OptionY[12], this.SheLookIconSize_64))
      {
        if (this.Level != 20)
        {
          this.dAnimStep = 0;
          this.dAnimDest = 100;
          this.Level = 20;
          this.newLevel = 20;
        }
        else
        {
          this.dAnimStep = 100;
          this.dAnimDest = 0;
          this.Level = 20;
          this.newLevel = 10;
        }
        this.VolumeGrid.Visible = false;
        this.Animation(false);
        return true;
      }
      if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[13], this.OptionY[13], this.SheLookIconSize_64))
      {
        this.Cursor = Cursors.WaitCursor;
        ++Language.CurrLanguage;
        if (Language.CurrLanguage > Language.MaxLanguages - 1)
          Language.CurrLanguage = 0;
        Language.setCurrLanguage(Language.CurrLanguage);
        this.ResetFormsLanguages();
        this.Cursor = Cursors.Default;
        this.Animation(false);
      }
      return false;
    }

    private bool DriveSearchMenu()
    {
      if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[20], this.OptionY[20], this.SheLookIconSize_64))
      {
        this.StartSearch(0);
        return true;
      }
      if (!this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[21], this.OptionY[21], this.SheLookIconSize_64))
        return false;
      this.StartSearch(1);
      return true;
    }

    private bool DriveHolderListMenu()
    {
      for (int index = 0; index < Navigator.numDevice; ++index)
      {
        if (this.CheckIcon(this.cursorX, this.cursorY, Navigator.HolderArray[index].iconX, Navigator.HolderArray[index].iconY, this.SheLookIconSize_64))
        {
          bool flag = false;
          if (this.Level == 40)
          {
            this.Level = 40;
            this.newLevel = 30;
            this.dAnimStep = 100;
            this.dAnimDest = 0;
            this.VolumeGrid.Visible = false;
            if (this.ActiveHolder != index)
              this.Animation(false);
            else
              flag = true;
          }
          if (!flag && (this.Level == 30 || this.Level == 40))
          {
            this.Level = 40;
            this.newLevel = 40;
            this.dAnimStep = 0;
            this.dAnimDest = 100;
            this.ExActiveHolder = this.ActiveHolder;
            this.ActiveHolder = index;
            this.ComboHolder.SelectedIndex = this.ActiveHolder;
            this.VolumeGrid.Visible = true;
            this.FillVolumeDataGrid();
          }
          this.Animation(false);
          return true;
        }
      }
      if (this.ExchangeFlag || !this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[30], this.OptionY[30], this.SheLookIconSize_64))
        return false;
      this.AddHolderProc();
      this.Animation(false);
      return true;
    }

    public void AddHolderProc()
    {
      Navigator.Holder currHolder = new Navigator.Holder();
      currHolder.DeviceTP = 0;
      currHolder.DeviceID = Navigator.GetNewID();
      currHolder.DeviceImg = (Image) Navigator.Hold;
      currHolder.LabelName = Navigator.GetNewName(0);
      currHolder.PS_Avail = 150;
      currHolder.ID = currHolder.DeviceID;
      int num = 0;
      if (Navigator.HolderArray != null)
        num = Navigator.HolderArray.Length;
      currHolder.LabelName = Navigator.AskString(this.Strx[19].ToString(), this.Strx[21].ToString() + " #" + (object) num, 10, false);
      if (currHolder.LabelName.Equals("<no>"))
        return;
      string s = Navigator.AskString(this.Strx[20].ToString(), "", 3, true);
      if (s.Equals("<no>"))
        return;
      int.TryParse(s, out currHolder.PS_Avail);
      if (currHolder.PS_Avail < 1)
        return;
      Navigator.EditHolder(currHolder);
      this.RefreshHolderList();
    }

    public void AddVolumeProc()
    {
      this.InitComboUnit();
      Navigator.Asking_Form.LoadCombo(this.ComboDrives);
      string DisplayText = "Select Source Unit: ";
      Navigator.Asking_Form.InputString(DisplayText, (Image) Navigator.Tick, (Image) Navigator.Cros, 8, false);
      Navigator.Asking_Form.AskCombo.Visible = true;
      Navigator.Asking_Form.AskReply.Visible = false;
      int num = (int) Navigator.Asking_Form.ShowDialog();
      if (Navigator.Asking_Form.Answer.Equals("<no>"))
        return;
      this.ComboDrives.Text = Navigator.Asking_Form.AskCombo.Text;
      if (Navigator.ActiveVolumePos < 0)
        return;
      this.ReadVolume(this.ComboDrives.Text, this.ComboHolder.SelectedIndex, Navigator.ActiveVolumePos);
    }

    private bool DriveVolumeListMenu()
    {
      if (!this.ExchangeFlag)
      {
        if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[40], this.OptionY[40], this.SheLookIconSize_48))
        {
          this.AddVolumeProc();
          this.FillVolumeDataGrid();
          return true;
        }
        if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[41], this.OptionY[41], this.SheLookIconSize_48))
        {
          if (!Navigator.AskYesNo(this.Strx[11].ToString()))
            return true;
          this.DeleteVolume(this.ActiveHolder, Navigator.ActiveVolumePos);
          this.FillVolumeDataGrid();
          return true;
        }
        if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[42], this.OptionY[42], this.SheLookIconSize_48))
        {
          string @string = this.Strx[12].ToString();
          string inName = Navigator.HolderArray[this.ActiveHolder].LabelName;
          Navigator.Asking_Form.InputString(@string, (Image) Navigator.Tick, (Image) Navigator.Cros, 16, false);
          Navigator.Asking_Form.AskReply.Text = inName;
          Navigator.Asking_Form.AskCombo.Visible = false;
          int num = (int) Navigator.Asking_Form.ShowDialog();
          string str = Navigator.Asking_Form.Answer;
          if (!str.Equals("<no>"))
          {
            Navigator.Holder holderByName = this.GetHolder_By_Name(inName);
            holderByName.LabelName = str;
            Navigator.EditHolder(holderByName);
            this.RefreshHolderList();
            this.Animation(false);
          }
          return true;
        }
        if (this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[43], this.OptionY[43], this.SheLookIconSize_48))
        {
          if (!Navigator.AskYesNo(this.Strx[11].ToString()) || !this.DeleteHolder(this.ActiveHolder))
            return true;
          this.Level = 30;
          this.Animation(false);
          return true;
        }
      }
      if (!this.CheckIcon(this.cursorX, this.cursorY, this.OptionX[44], this.OptionY[44], this.SheLookIconSize_48))
        return false;
      if (this.ExchangeFlag)
      {
        this.ExchangeFlag = false;
        this.ExchangePos = 0;
        this.ExchangeHolder = 0;
        this.VolumeGrid[1, Navigator.ActiveVolumePos - 1].Style.BackColor = Color.FromArgb((int) byte.MaxValue, 200, (int) byte.MaxValue, 200);
        this.VolumeGrid[1, Navigator.ActiveVolumePos - 1].Style.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      }
      else if (!this.VolumeGrid[1, Navigator.ActiveVolumePos - 1].ReadOnly)
      {
        this.ExchangeFlag = true;
        this.ExchangePos = Navigator.ActiveVolumePos;
        this.ExchangeHolder = this.ActiveHolder;
        this.VolumeGrid[1, Navigator.ActiveVolumePos - 1].Style.BackColor = Color.FromArgb((int) byte.MaxValue, 220, 120, 120);
        this.VolumeGrid[1, Navigator.ActiveVolumePos - 1].Style.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
      }
      this.Animation(false);
      return true;
    }

    private void DriveMenu()
    {
      if (!this.ExchangeFlag && (this.Level == 0 && this.DriveSingleIcon() || this.Level >= 10 && this.DriveMainMenu() || this.Level == 20 && this.DriveSearchMenu()))
        return;
      if ((this.Level == 30 || this.Level == 40) && this.DriveHolderListMenu() || (this.Level != 40 || !this.DriveVolumeListMenu()))
        ;
    }

    private void DrawImageAlpha(Image my_image, int x, int y, int w, int h, float opacity)
    {
      float[][] newColorMatrix1 = new float[5][];
      float[][] numArray1 = newColorMatrix1;
      int index1 = 0;
      float[] numArray2 = new float[5];
      numArray2[0] = 1f;
      float[] numArray3 = numArray2;
      numArray1[index1] = numArray3;
      float[][] numArray4 = newColorMatrix1;
      int index2 = 1;
      float[] numArray5 = new float[5];
      numArray5[1] = 1f;
      float[] numArray6 = numArray5;
      numArray4[index2] = numArray6;
      float[][] numArray7 = newColorMatrix1;
      int index3 = 2;
      float[] numArray8 = new float[5];
      numArray8[2] = 1f;
      float[] numArray9 = numArray8;
      numArray7[index3] = numArray9;
      float[][] numArray10 = newColorMatrix1;
      int index4 = 3;
      float[] numArray11 = new float[5];
      numArray11[3] = opacity;
      float[] numArray12 = numArray11;
      numArray10[index4] = numArray12;
      float[][] numArray13 = newColorMatrix1;
      int index5 = 4;
      float[] numArray14 = new float[5];
      numArray14[4] = 1f;
      float[] numArray15 = numArray14;
      numArray13[index5] = numArray15;
      ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
      ImageAttributes imageAttr = new ImageAttributes();
      imageAttr.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      this.LocGraph.DrawImage(my_image, new Rectangle(x, y, w, h), 0, 0, my_image.Width, my_image.Height, GraphicsUnit.Pixel, imageAttr);
    }

    private void VolumeGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex != 1)
        return;
      string @string = this.VolumeGrid[e.ColumnIndex, e.RowIndex].Value.ToString();
      Navigator.VolumeDisc volumeByHolderPs = Navigator.GetVolume_By_HolderPS(this.ActiveHolder, e.RowIndex + 1);
      volumeByHolderPs.Label = @string;
      if (volumeByHolderPs.Drive == null)
        volumeByHolderPs.Drive = "";
      if (volumeByHolderPs.RegDate == null)
        volumeByHolderPs.RegDate = "";
      Navigator.EditVolume(volumeByHolderPs);
      Navigator.RefreshVolumeList(this.ActiveHolder, ref this.VolumeGrid);
    }

    public void RefreshHolderList()
    {
      this.ComboHolder.Items.Clear();
      ArrayList[] dbHolderList = Navigator.GetDBHolderList("");
      if (dbHolderList == null)
        return;
      Navigator.numDevice = dbHolderList[0].Count;
      Navigator.HolderArray = new Navigator.Holder[Navigator.numDevice];
      for (int index = 0; index < Navigator.numDevice; ++index)
      {
        Navigator.HolderArray[index].LabelName = dbHolderList[1][index].ToString();
        Navigator.HolderArray[index].DeviceID = int.Parse(dbHolderList[2][index].ToString());
        Navigator.HolderArray[index].ID = Navigator.HolderArray[index].DeviceID;
        Navigator.HolderArray[index].DeviceTP = int.Parse(dbHolderList[3][index].ToString());
        Navigator.HolderArray[index].PS_Avail = int.Parse(dbHolderList[4][index].ToString());
        Navigator.HolderArray[index].DeviceImg = Navigator.HolderArray[index].DeviceTP != 0 ? (Image) Navigator.Libr : (Image) Navigator.Hold;
        this.ComboHolder.Items.Add((object) Navigator.HolderArray[index].LabelName);
      }
      if (this.ComboHolder.Items.Count <= 0)
        return;
      this.ComboHolder.SelectedIndex = 0;
      Navigator.RefreshVolumeList(this.ComboHolder.SelectedIndex, ref this.VolumeGrid);
    }

    public bool DeleteHolder(int HolderNumber)
    {
      int num = Navigator.HolderArray[HolderNumber].DeviceID;
      ArrayList[] dbVolumeList = Navigator.GetDBVolumeList("ID", "DeviceID=" + (object) num);
      for (int index = 0; index < dbVolumeList[0].Count; ++index)
      {
        DataBase.delAllEntriesWhere("FileList", "ID=" + dbVolumeList[0][index].ToString());
        DataBase.delAllEntriesWhere("VolumeList", "DeviceID=" + (object) num);
      }
      DataBase.delAllEntriesWhere("HolderList", "DeviceID=" + (object) num);
      this.RefreshHolderList();
      Navigator.RefreshVolumeList(this.ComboHolder.SelectedIndex, ref this.VolumeGrid);
      return true;
    }

    public void ReadVolume(string ComboDrive, int HolderNumber, int HolderPS)
    {
      if (!ComboDrive.Equals(Navigator.NoDrive) && !this.CheckIfExistsDrive(ComboDrive))
        return;
      ArrayList arrayList = new ArrayList();
      DirectoryInfo directoryInfo = new DirectoryInfo(ComboDrive.Substring(0, 2) + "\\");
      Navigator.VolumeDisc VolumeDisc = new Navigator.VolumeDisc();
      VolumeDisc.DeviceID = Navigator.HolderArray[HolderNumber].DeviceID;
      VolumeDisc.HolderPS = HolderPS;
      if (!this.CheckFreePosition(HolderNumber, HolderPS))
      {
        if (!Navigator.AskYesNo(this.Strx[16].ToString() + " " + Navigator.GetVolume_By_HolderPS(HolderNumber, HolderPS).Label + "? "))
          return;
        this.DeleteVolume(HolderNumber, HolderPS);
      }
      VolumeDisc.ID = this.GetFreeVolumeID();
      if (!ComboDrive.Equals(Navigator.NoDrive))
      {
        VolumeDisc.Drive = directoryInfo.FullName.Substring(0, 2);
        DriveInfo driveInfo = new DriveInfo(VolumeDisc.Drive);
        int num = driveInfo.VolumeLabel == null ? 0 : (!driveInfo.VolumeLabel.Equals("") ? 1 : 0);
        VolumeDisc.Label = num != 0 ? driveInfo.VolumeLabel : driveInfo.Name;
      }
      else
      {
        VolumeDisc.Drive = "X:\\";
        VolumeDisc.Label = "New Volume";
      }
      if (this.CheckIfExistsVolume(HolderNumber, VolumeDisc.Label))
      {
        string Label = VolumeDisc.Label;
        bool flag = false;
        while (this.CheckIfExistsVolume(HolderNumber, Label) && !flag)
        {
          string str;
          Label = Navigator.AskString(Label + " " + this.Strx[17].ToString(), str = Label + "#", 16, false);
          if (Navigator.Asking_Form.Answer.Equals("<no>"))
          {
            if (!Navigator.AskYesNo(VolumeDisc.Label + " " + this.Strx[18].ToString()))
              return;
            int volumePosByVolumeName = Navigator.GetVolumePos_By_VolumeName(HolderNumber, VolumeDisc.Label);
            this.DeleteVolume(HolderNumber, volumePosByVolumeName);
            flag = true;
          }
          else
          {
            VolumeDisc.Label = Navigator.Asking_Form.Answer;
            flag = true;
          }
        }
      }
      DateTime now = DateTime.Now;
      string str1 = now.Date.ToString().Substring(0, 10);
      string str2 = Services.NormalizedTime(now.TimeOfDay.ToString()).Substring(0, 5);
      VolumeDisc.RegDate = str1 + " " + str2;
      Navigator.EditVolume(VolumeDisc);
      if (!ComboDrive.Equals(Navigator.NoDrive))
      {
        Navigator.LocGetFileSubFolders(VolumeDisc.ID, this.ComboDrives.Text.Substring(0, 3), "*.*");
        if (Navigator.Progress_Form.abortFlag)
          this.DeleteVolume(HolderNumber, HolderPS);
      }
      Navigator.RefreshVolumeList(this.ComboHolder.SelectedIndex, ref this.VolumeGrid);
    }

    public static void GoDevicePos(int CurrUnitNumber, int CurrCDNumber)
    {
      if (CurrUnitNumber >= Navigator.numDevice || CurrUnitNumber <= -1 || Navigator.HolderArray[CurrUnitNumber].PS_Curr == CurrCDNumber)
        return;
      int gI = Navigator.HolderArray[CurrUnitNumber].DeviceID;
      int num = USBLibrary.USBCDGetCDDown(gI);
      num = USBLibrary.USBCDMoveto(gI, CurrCDNumber);
      Navigator.HolderArray[CurrUnitNumber].PS_Curr = CurrCDNumber;
    }

    public static ArrayList[] GetDBHolderList(string Where)
    {
      ArrayList[] allRecords;
      try
      {
        allRecords = DataBase.GetAllRecords("HolderList", "ID", Where, false);
      }
      catch
      {
        Navigator.CreateTable_HolderList();
        allRecords = DataBase.GetAllRecords("HolderList", "ID", Where, false);
      }
      return allRecords;
    }

    public static ArrayList[] GetDBFileList(string Where)
    {
      ArrayList[] allRecords;
      try
      {
        allRecords = DataBase.GetAllRecords("FileList", "ID", Where, false);
      }
      catch
      {
        Navigator.CreateTable_FileList();
        allRecords = DataBase.GetAllRecords("FileList", "ID", Where, false);
      }
      return allRecords;
    }

    public static int GetHolderNumber_by_DeviceID(int DeviceID)
    {
      for (int index = 0; index < Navigator.numDevice; ++index)
      {
        if (Navigator.HolderArray[index].DeviceID == DeviceID)
          return index;
      }
      return 0;
    }

    public static Navigator.VolumeDisc BuildVolume(ArrayList[] VolumeList_Temp)
    {
      Navigator.VolumeDisc volumeDisc = new Navigator.VolumeDisc();
      if (VolumeList_Temp[0].Count > 0)
      {
        volumeDisc.ID = int.Parse(VolumeList_Temp[0][0].ToString());
        volumeDisc.Drive = VolumeList_Temp[1][0].ToString();
        volumeDisc.Label = VolumeList_Temp[2][0].ToString();
        volumeDisc.DeviceID = int.Parse(VolumeList_Temp[3][0].ToString());
        volumeDisc.HolderPS = int.Parse(VolumeList_Temp[4][0].ToString());
        volumeDisc.RegDate = VolumeList_Temp[5][0].ToString();
      }
      return volumeDisc;
    }

    public static Navigator.VolumeDisc GetVolume_By_VolumeName(int HolderNumber, string VolumeLabel)
    {
      int num = Navigator.HolderArray[HolderNumber].DeviceID;
      return Navigator.BuildVolume(Navigator.GetDBVolumeList("ID", "Label='" + VolumeLabel + "' and DeviceID='" + (object) num + "'"));
    }

    public static Navigator.VolumeDisc GetVolume_by_ID(string ID)
    {
      return Navigator.BuildVolume(Navigator.GetDBVolumeList("ID", "ID=" + ID));
    }

    public static Navigator.VolumeDisc GetVolume_By_HolderPS(int HolderNumber, int HolderPS)
    {
      return Navigator.BuildVolume(Navigator.GetDBVolumeList("ID", "DeviceID=" + (object) Navigator.HolderArray[HolderNumber].DeviceID + " and HolderPS=" + (object) HolderPS));
    }

    public static int GetVolumePos_By_VolumeName(int HolderNumber, string VolumeLabel)
    {
      int num = Navigator.HolderArray[HolderNumber].DeviceID;
      ArrayList[] dbVolumeList = Navigator.GetDBVolumeList("ID", "Label='" + VolumeLabel + "' and DeviceID='" + (object) num + "'");
      if (dbVolumeList[0].Count == 0)
        return -1;
      return int.Parse(dbVolumeList[4][0].ToString());
    }

    public static ArrayList[] GetDBVolumeList(string OrderBy, string Where)
    {
      ArrayList[] allRecords;
      try
      {
        allRecords = DataBase.GetAllRecords("VolumeList", OrderBy, Where, false);
      }
      catch
      {
        Navigator.CreateTable_VolumeList();
        allRecords = DataBase.GetAllRecords("VolumeList", OrderBy, Where, false);
      }
      return allRecords;
    }

    public static void CreateTable_FileList()
    {
      string[] fields = new string[8];
      string[] typefields = new string[8];
      fields[0] = "[ID]";
      fields[1] = "[VolumeID]";
      fields[2] = "[Folder]";
      fields[3] = "[FileName]";
      fields[4] = "[Extension]";
      fields[5] = "[FileSize]";
      fields[6] = "[Zip]";
      fields[7] = "[LastModified]";
      typefields[0] = "INTEGER";
      typefields[1] = "INTEGER";
      typefields[2] = "VARCHAR(255)";
      typefields[3] = "VARCHAR(255)";
      typefields[4] = "VARCHAR(255)";
      typefields[5] = "INTEGER";
      typefields[6] = "VARCHAR(16)";
      typefields[7] = "VARCHAR(16)";
      DataBase.CreateTable("FileList", fields, typefields);
    }

    public static void CreateTable_VolumeList()
    {
      string[] fields = new string[6];
      string[] typefields = new string[6];
      fields[0] = "[ID]";
      fields[1] = "[Drive]";
      fields[2] = "[Label]";
      fields[3] = "[DeviceID]";
      fields[4] = "[HolderPS]";
      fields[5] = "[RegDate]";
      typefields[0] = "INTEGER";
      typefields[1] = "VARCHAR(16)";
      typefields[2] = "VARCHAR(32)";
      typefields[3] = "INTEGER";
      typefields[4] = "INTEGER";
      typefields[5] = "VARCHAR(16)";
      DataBase.CreateTable("VolumeList", fields, typefields);
    }

    public static void CreateTable_HolderList()
    {
      string[] fields = new string[6];
      string[] typefields = new string[6];
      fields[0] = "[ID]";
      fields[1] = "[Label]";
      fields[2] = "[DeviceID]";
      fields[3] = "[DeviceTP]";
      fields[4] = "[PS_avail]";
      fields[5] = "[RegDate]";
      typefields[0] = "INTEGER";
      typefields[1] = "VARCHAR(32)";
      typefields[2] = "INTEGER";
      typefields[3] = "INTEGER";
      typefields[4] = "INTEGER";
      typefields[5] = "VARCHAR(16)";
      DataBase.CreateTable("HolderList", fields, typefields);
    }

    private void ComboHolderUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
      Navigator.RefreshVolumeList(this.ComboHolder.SelectedIndex, ref this.VolumeGrid);
    }

    public static void RefreshVolumeList(int HolderNum, ref DataGridView DataGridToFill)
    {
      if (HolderNum < 0)
        return;
      int num1 = Navigator.HolderArray[HolderNum].DeviceID;
      Navigator.ExValueFlag = false;
      Navigator.ActiveVolumePos = -1;
      Navigator.exActiveVolumePos = -1;
      ArrayList[] dbVolumeList = Navigator.GetDBVolumeList("HolderPS", "DeviceID='" + (object) num1 + "'");
      DataGridToFill.RowCount = Navigator.HolderArray[HolderNum].PS_Avail;
      int index1 = -1;
      int num2 = 0;
      for (int index2 = 0; index2 < DataGridToFill.RowCount; ++index2)
      {
        while (num2 < index2 + 1 && index1 < dbVolumeList[0].Count)
        {
          ++index1;
          if (index1 < dbVolumeList[0].Count)
            num2 = int.Parse(dbVolumeList[4][index1].ToString());
        }
        DataGridToFill[0, index2].Value = (object) (index2 + 1);
        if (num2 == index2 + 1 && index1 < dbVolumeList[0].Count)
        {
          DataGridToFill[1, index2].Value = dbVolumeList[2][index1];
          DataGridToFill[1, index2].Style.BackColor = Color.FromArgb((int) byte.MaxValue, 220, 220, 220);
          DataGridToFill[1, index2].Style.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
          DataGridToFill[1, index2].ReadOnly = false;
        }
        else
        {
          DataGridToFill[1, index2].Value = (object) Navigator.EMPTY_VOLUME;
          DataGridToFill[1, index2].Style.BackColor = Color.FromArgb((int) byte.MaxValue, 200, (int) byte.MaxValue, 200);
          DataGridToFill[1, index2].Style.ForeColor = Color.FromArgb((int) byte.MaxValue, 20, 20, 20);
          DataGridToFill[1, index2].ReadOnly = true;
        }
        DataGridToFill.Rows[index2].Selected = false;
      }
      if (Navigator.ActiveVolumePos < DataGridToFill.RowCount && Navigator.ActiveVolumePos > -1)
        DataGridToFill.Rows[Navigator.ActiveVolumePos].Selected = true;
      if (DataGridToFill.RowCount > 0)
      {
        try
        {
          DataGridToFill.CurrentCell = DataGridToFill[1, 0];
        }
        catch
        {
        }
        Navigator.ActiveVolumePos = 1;
      }
      else
        Navigator.ActiveVolumePos = -1;
      Navigator.ExValueFlag = true;
    }

    private void DelVolume_Click(object sender, EventArgs e)
    {
      if (Navigator.ActiveVolumePos < 0)
        return;
      this.DeleteVolume(this.ComboHolder.SelectedIndex, Navigator.ActiveVolumePos);
    }

    public void DeleteVolume(int HolderNumber, int HolderPS)
    {
      int num = Navigator.HolderArray[HolderNumber].DeviceID;
      ArrayList[] dbVolumeList = Navigator.GetDBVolumeList("ID", "DeviceID=" + (object) num + " and HolderPS=" + (object) HolderPS);
      if (dbVolumeList[0].Count <= 0)
        return;
      string @string = dbVolumeList[0][0].ToString();
      DataBase.delAllEntriesWhere("VolumeList", "DeviceID=" + (object) num + " and HolderPS=" + (object) HolderPS);
      DataBase.delAllEntriesWhere("FileList", "VolumeID=" + @string);
      Navigator.RefreshVolumeList(this.ComboHolder.SelectedIndex, ref this.VolumeGrid);
    }

    private void exchange_Click(object sender, EventArgs e)
    {
      DataGridViewSelectedRowCollection selectedRows = this.VolumeGrid.SelectedRows;
      if (selectedRows.Count > 0)
        Navigator.exActiveVolumePos = selectedRows[0].Index;
      Navigator.exActiveVolumePos = Navigator.exActiveVolumePos;
    }

    private void VolumeGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex <= 0 || e.RowIndex < 0 || !Navigator.ExValueFlag)
        return;
      string @string = this.VolumeGrid[e.ColumnIndex, e.RowIndex].Value.ToString();
      if (@string.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
      {
        this.VolumeGrid[e.ColumnIndex, e.RowIndex].Value = (object) Navigator.ExValue;
      }
      else
      {
        this.VolumeGrid[e.ColumnIndex, e.RowIndex].Value = (object) @string;
        Navigator.ExValue = @string;
      }
    }

    private void VolumeGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      Navigator.ExValue = this.VolumeGrid[1, e.RowIndex].Value.ToString();
    }

    public Navigator.Holder GetHolder_By_Name(string inName)
    {
      for (int index = 0; index < Navigator.numDevice; ++index)
      {
        if (inName.Equals(this.ComboHolder.Items[index]))
          return Navigator.HolderArray[index];
      }
      return Navigator.null_Holder;
    }

    public static string GetHolderName_by_DeviceID(int DeviceID)
    {
      for (int index = 0; index < Navigator.numDevice; ++index)
      {
        if (Navigator.HolderArray[index].DeviceID == DeviceID)
          return Navigator.HolderArray[index].LabelName;
      }
      return "Unknown";
    }

    public string CheckIfFileIsOnline(string InFileName, int FileSize, string FileDate)
    {
      for (int index = 0; index < this.ComboDrives.Items.Count; ++index)
      {
        string str1 = Path.Combine(this.ComboDrives.Items[index].ToString().Substring(0, 2) + "\\", InFileName);
        if (File.Exists(str1) && Services.FileSize(str1) == (long) FileSize)
        {
          DateTime lastWriteTime = new FileInfo(str1).LastWriteTime;
          string str2 = lastWriteTime.Date.ToString().Substring(0, 10);
          string str3 = Services.NormalizedTime(lastWriteTime.TimeOfDay.ToString()).Substring(0, 5);
          if (FileDate.Equals(str2 + " " + str3))
            return str1;
        }
      }
      return "";
    }

    public static int GetNewID()
    {
      int num = 100000000 + Navigator.rnd.Next(1000);
      ArrayList[] dbHolderList = Navigator.GetDBHolderList("ID = '" + (object) num + "'");
      if (dbHolderList != null && dbHolderList[0].Count > 0)
        return Navigator.GetNewID();
      return num;
    }

    public static string GetNewName(int nDevice)
    {
      string str = "Desk #" + (object) nDevice;
      ArrayList[] dbHolderList = Navigator.GetDBHolderList("Label = '" + str + "'");
      if (dbHolderList != null && dbHolderList[0].Count > 0)
        return Navigator.GetNewName(nDevice + 1);
      return str;
    }

    private void sheLook_Home_MouseUp(object sender, MouseEventArgs e)
    {
      Services.SaveFormLayout((Form) this);
    }

    private void AddHolder_Click(object sender, EventArgs e)
    {
    }

    public static bool AskYesNo(string MessageSTR)
    {
      Navigator.Asking_Form.WaitForReply(MessageSTR, (Image) Navigator.Tick, (Image) Navigator.Cros);
      Navigator.Asking_Form.AskCombo.Visible = false;
      Navigator.Asking_Form.AskReply.Visible = false;
      int num = (int) Navigator.Asking_Form.ShowDialog();
      return Navigator.Asking_Form.Answer.Equals("<yes>");
    }

    public static string AskString(string MessageSTR, string ProposedString, int maxChar, bool OnlyNumbers)
    {
      Navigator.Asking_Form.InputString(MessageSTR, (Image) Navigator.Tick, (Image) Navigator.Cros, maxChar, OnlyNumbers);
      Navigator.Asking_Form.AskCombo.Visible = false;
      Navigator.Asking_Form.AskReply.Visible = true;
      Navigator.Asking_Form.AskReply.Text = ProposedString;
      int num = (int) Navigator.Asking_Form.ShowDialog();
      return Navigator.Asking_Form.Answer;
    }

    public void InitDB()
    {
      DataBase.DB_PATH = Path.Combine(Application.StartupPath, "settings");
      DataBase.DB_PATH_LOG = Path.Combine(Application.StartupPath, "logfile");
      DataBase.InitDB();
      try
      {
        DataBase.CheckTable("FileList");
      }
      catch
      {
        Navigator.CreateTable_FileList();
      }
      try
      {
        DataBase.CheckTable("VolumeList");
      }
      catch
      {
        Navigator.CreateTable_VolumeList();
      }
      try
      {
        DataBase.CheckTable("HolderList");
      }
      catch
      {
        Navigator.CreateTable_HolderList();
      }
    }

    public void InitUSBDevices()
    {
      int num = USBLibrary.InitUSBCDLibrary();
      int deviceNumber = USBLibrary.GetDeviceNumber();
      Navigator.Holder[] holderArray = new Navigator.Holder[deviceNumber];
      for (int index = 0; index < deviceNumber; ++index)
      {
        holderArray[index].DeviceID = USBLibrary.EnumDevice(index);
        holderArray[index].ID = holderArray[index].DeviceID;
        holderArray[index].DeviceTP = 1;
        holderArray[index].DeviceImg = (Image) Navigator.Libr;
        holderArray[index].LabelName = this.Strx[15].ToString() + " #" + (object) index;
        holderArray[index].PS_Avail = 150;
        Navigator.EditHolder(holderArray[index]);
        num = USBLibrary.USBCDLEDON(holderArray[index].DeviceID);
        Navigator.GoDevicePos(index, 1);
      }
    }

    public void InitComboUnit()
    {
      this.ComboDrives.Items.Clear();
      DriveInfo[] drives = DriveInfo.GetDrives();
      for (int index = 0; index < drives.Length; ++index)
      {
        string str = "";
        try
        {
          string volumeLabel = drives[index].VolumeLabel;
          this.ComboDrives.Items.Add((object) (drives[index].RootDirectory.ToString() + " " + volumeLabel));
        }
        catch
        {
          str = drives[index].DriveType.ToString();
        }
      }
    }

    private bool CheckIfExistsDrive(string inDrv)
    {
      DriveInfo driveInfo = new DriveInfo(inDrv);
      try
      {
        string volumeLabel = driveInfo.VolumeLabel;
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static void LocGetFileSubFolders(int VolumeID, string CurrFolder, string pattern)
    {
      DataBase.BeginTrans();
      Navigator.fields = new ArrayList[1000];
      Navigator.Naviga_Form.Enabled = false;
      Navigator.Progress_Form = new Progress_Form();
      Navigator.Progress_Form.abortFlag = false;
      Navigator.Progress_Form.Pic_Folder_Start.Image = (Image) Navigator.CDRM;
      Navigator.Progress_Form.Pic_Folder_End.Image = (Image) Navigator.Fold;
      Navigator.Progress_Form.Pic_File.Image = (Image) Navigator.Pape;
      Navigator.Progress_Form.progressBar.Minimum = 0;
      Navigator.Progress_Form.progressBar.Maximum = 100;
      Navigator.Progress_Form.Show();
      Navigator.Progress_Form.Refresh();
      Application.DoEvents();
      Navigator.iFile = 0;
      Navigator.AnimPerc = 0;
      Navigator.cSlot = 0;
      Navigator.GetFileSubFolders(VolumeID, CurrFolder, pattern);
      Navigator.fields = (ArrayList[]) null;
      Navigator.Naviga_Form.Enabled = true;
      Navigator.Progress_Form.Hide();
      DataBase.EndTrans();
    }

    public static void GetFileSubFolders(int VolumeID, string CurrFolder, string pattern)
    {
      string str1 = "";
      Exception exception;
      try
      {
        DirectoryInfo directoryInfo1 = new DirectoryInfo(CurrFolder);
        DirectoryInfo[] directories = directoryInfo1.GetDirectories();
        FileInfo[] files = directoryInfo1.GetFiles(pattern);
        if (directories.Length > 0)
        {
          for (int index = 0; index < directories.Length; ++index)
          {
            DirectoryInfo directoryInfo2 = directories[index];
            try
            {
              Navigator.GetFileSubFolders(VolumeID, directoryInfo2.FullName, pattern);
            }
            catch (Exception ex)
            {
              exception = ex;
            }
            if (Navigator.Progress_Form.abortFlag)
              return;
          }
        }
        if (files.Length > 0)
        {
          for (int index = 0; index < files.Length; ++index)
          {
            FileInfo currFile = files[index];
            try
            {
              int tickCount = Environment.TickCount;
              if (currFile.Extension.ToLower().Equals(".zip"))
              {
                foreach (ZipEntry listZipFile in ZipUtil.ListZipFiles(currFile.FullName, ""))
                {
                  if (listZipFile.IsFile)
                  {
                    Navigator.fields[Navigator.cSlot] = new ArrayList();
                    string fullName = currFile.FullName;
                    DateTime dateTime = listZipFile.DateTime;
                    string str2 = dateTime.Date.ToString().Substring(0, 10);
                    string str3 = Services.NormalizedTime(dateTime.TimeOfDay.ToString()).Substring(0, 5);
                    int num = VolumeID * 100000000 + Navigator.iFile;
                    string str4 = fullName + "__" + Path.GetDirectoryName(currFile.Name) + "__";
                    string extension = Path.GetExtension(currFile.Name);
                    if (extension.Length > 0)
                      str1 = extension.Substring(1, extension.Length - 1);
                    Navigator.fields[Navigator.cSlot].Add((object) num);
                    Navigator.fields[Navigator.cSlot].Add((object) VolumeID);
                    Navigator.fields[Navigator.cSlot].Add((object) str4);
                    Navigator.fields[Navigator.cSlot].Add((object) Path.GetFileName(listZipFile.Name));
                    Navigator.fields[Navigator.cSlot].Add((object) Path.GetExtension(listZipFile.Name));
                    Navigator.fields[Navigator.cSlot].Add((object) listZipFile.Size);
                    Navigator.fields[Navigator.cSlot].Add((object) "Zip");
                    Navigator.fields[Navigator.cSlot].Add((object) (str2 + " " + str3));
                    ++Navigator.iFile;
                    if (Environment.TickCount - Navigator.LastUpdateTime > 100)
                    {
                      Navigator.UpdateProgress(currFile);
                      if (Navigator.Progress_Form.abortFlag)
                        return;
                    }
                    ++Navigator.cSlot;
                    if (Navigator.cSlot >= Navigator.fields.Length)
                    {
                      DataBase.AddUpdRecords("FileList", Navigator.fields, Navigator.cSlot, false);
                      Navigator.cSlot = 0;
                    }
                  }
                }
              }
              if (Navigator.cSlot >= Navigator.fields.Length)
                Navigator.cSlot = 0;
              Navigator.fields[Navigator.cSlot] = new ArrayList();
              DateTime lastWriteTime = currFile.LastWriteTime;
              string str5 = lastWriteTime.Date.ToString().Substring(0, 10);
              string str6 = Services.NormalizedTime(lastWriteTime.TimeOfDay.ToString()).Substring(0, 5);
              int num1 = VolumeID * 100000000 + Navigator.iFile;
              string str7 = currFile.Extension;
              if (str7.Length > 0)
                str7 = str7.Substring(1, str7.Length - 1);
              Navigator.fields[Navigator.cSlot].Add((object) num1);
              Navigator.fields[Navigator.cSlot].Add((object) VolumeID);
              Navigator.fields[Navigator.cSlot].Add((object) currFile.DirectoryName);
              Navigator.fields[Navigator.cSlot].Add((object) currFile.Name);
              Navigator.fields[Navigator.cSlot].Add((object) str7);
              Navigator.fields[Navigator.cSlot].Add((object) currFile.Length);
              Navigator.fields[Navigator.cSlot].Add((object) "No_Zip");
              Navigator.fields[Navigator.cSlot].Add((object) (str5 + " " + str6));
              ++Navigator.iFile;
              if (Environment.TickCount - Navigator.LastUpdateTime > 100)
              {
                Navigator.UpdateProgress(currFile);
                if (Navigator.Progress_Form.abortFlag)
                  return;
              }
              ++Navigator.cSlot;
              if (Navigator.cSlot >= Navigator.fields.Length)
              {
                DataBase.AddUpdRecords("FileList", Navigator.fields, Navigator.cSlot, false);
                Navigator.cSlot = 0;
              }
            }
            catch (Exception ex)
            {
              exception = ex;
            }
          }
          if (Navigator.cSlot > 0)
          {
            DataBase.AddUpdRecords("FileList", Navigator.fields, Navigator.cSlot, false);
            Navigator.cSlot = 0;
          }
        }
      }
      catch (Exception ex)
      {
        exception = ex;
      }
    }

    private static void UpdateProgress(FileInfo currFile)
    {
      Navigator.LastUpdateTime = Environment.TickCount;
      Navigator.AnimPerc += 5;
      string name = currFile.Name;
      string directoryName = currFile.DirectoryName;
      int length1 = directoryName.Length;
      if (length1 > 50)
        length1 = 50;
      Navigator.Progress_Form.DirectoryLabel.Text = directoryName.Substring(0, length1) + "...";
      int length2 = name.Length;
      if (length2 > 50)
        length2 = 50;
      Navigator.Progress_Form.CurrFileLabel.Text = name.Substring(0, length2) + "...";
      Navigator.Progress_Form.TotFiles.Text = string.Concat((object) Navigator.iFile);
      Navigator.Progress_Form.Animation(Navigator.AnimPerc % 100);
      Navigator.Progress_Form.Refresh();
      Application.DoEvents();
    }

    public bool CheckIfExistsVolume(int HolderNumber, string Label)
    {
      return Navigator.GetDBVolumeList("ID", "Label = '" + Label + "' and DeviceID = '" + (object) Navigator.HolderArray[HolderNumber].DeviceID + "'")[0].Count != 0;
    }

    public static void EditHolder(Navigator.Holder currHolder)
    {
      ArrayList[] fields = new ArrayList[1]
      {
        new ArrayList()
      };
      DateTime now = DateTime.Now;
      string str1 = now.Date.ToString().Substring(0, 10);
      string str2 = Services.NormalizedTime(now.TimeOfDay.ToString()).Substring(0, 5);
      fields[0].Add((object) currHolder.ID);
      fields[0].Add((object) currHolder.LabelName);
      fields[0].Add((object) currHolder.DeviceID);
      fields[0].Add((object) currHolder.DeviceTP);
      fields[0].Add((object) currHolder.PS_Avail);
      fields[0].Add((object) (str1 + " " + str2));
      DataBase.AddUpdRecords("HolderList", fields, -1, true);
    }

    private bool CheckFreePosition(int HolderNumber, int Position)
    {
      return Navigator.GetDBVolumeList("ID", "DeviceID = '" + (object) Navigator.HolderArray[HolderNumber].DeviceID + "' and HolderPS = '" + (object) Position + "'")[0].Count == 0;
    }

    private int GetFreePSAvail(int DeviceID)
    {
      return this.GetFreeID(Navigator.GetDBVolumeList("ID", "DeviceID = '" + (object) DeviceID + "'"), 4, 1);
    }

    private int GetFreeVolumeID()
    {
      return this.GetFreeID(Navigator.GetDBVolumeList("ID", ""), 0, 1);
    }

    private int GetFreeHolderID()
    {
      return this.GetFreeID(Navigator.GetDBHolderList(""), 0, 1);
    }

    private int GetFreeFileID()
    {
      return this.GetFreeID(Navigator.GetDBFileList(""), 0, 0);
    }

    private int GetFreeID(ArrayList[] myRecords, int nField, int min)
    {
      bool flag = false;
      int num1 = min;
      int result = 0;
      int num2 = 0;
      if (myRecords[0].Count > 0)
      {
        for (int index = 0; index < myRecords[0].Count; ++index)
        {
          if (!flag)
          {
            ++num2;
            int.TryParse(myRecords[nField][index].ToString(), out result);
            if (num2 < result)
            {
              num1 = num2;
              index = myRecords[0].Count;
              flag = true;
            }
          }
        }
        if (!flag)
          num1 = min + myRecords[0].Count;
      }
      return num1;
    }

    public static void EditVolume(Navigator.VolumeDisc VolumeDisc)
    {
      ArrayList[] fields = new ArrayList[1]
      {
        new ArrayList()
      };
      fields[0].Add((object) VolumeDisc.ID);
      fields[0].Add((object) VolumeDisc.Drive);
      fields[0].Add((object) VolumeDisc.Label);
      fields[0].Add((object) VolumeDisc.DeviceID);
      fields[0].Add((object) VolumeDisc.HolderPS);
      fields[0].Add((object) VolumeDisc.RegDate);
      DataBase.AddUpdRecords("VolumeList", fields, -1, true);
    }

    public static void EditFile(FileInfo currFile, int VolumeID, int FileID)
    {
      ArrayList[] fields = new ArrayList[1]
      {
        new ArrayList()
      };
      DateTime lastWriteTime = currFile.LastWriteTime;
      string str1 = lastWriteTime.Date.ToString().Substring(0, 10);
      string str2 = Services.NormalizedTime(lastWriteTime.TimeOfDay.ToString()).Substring(0, 5);
      FileID = VolumeID * 100000000 + FileID;
      string str3 = currFile.Extension;
      if (str3.Length > 0)
        str3 = str3.Substring(1, str3.Length - 1);
      fields[0].Add((object) FileID);
      fields[0].Add((object) VolumeID);
      fields[0].Add((object) currFile.DirectoryName);
      fields[0].Add((object) currFile.Name);
      fields[0].Add((object) str3);
      fields[0].Add((object) currFile.Length);
      fields[0].Add((object) "No_Zip");
      fields[0].Add((object) (str1 + " " + str2));
      DataBase.AddUpdRecords("FileList", fields, -1, true);
    }

    public static void EditFileZipped(string ZipName, ZipEntry currFile, int VolumeID, int FileID)
    {
      ArrayList[] fields = new ArrayList[1]
      {
        new ArrayList()
      };
      DateTime dateTime = currFile.DateTime;
      string str1 = dateTime.Date.ToString().Substring(0, 10);
      string str2 = Services.NormalizedTime(dateTime.TimeOfDay.ToString()).Substring(0, 5);
      FileID = VolumeID * 100000000 + FileID;
      string str3 = ZipName + "__" + Path.GetDirectoryName(currFile.Name) + "__";
      string str4 = Path.GetExtension(currFile.Name);
      if (str4.Length > 0)
        str4 = str4.Substring(1, str4.Length - 1);
      fields[0].Add((object) FileID);
      fields[0].Add((object) VolumeID);
      fields[0].Add((object) str3);
      fields[0].Add((object) Path.GetFileName(currFile.Name));
      fields[0].Add((object) str4);
      fields[0].Add((object) currFile.Size);
      fields[0].Add((object) "Zip");
      fields[0].Add((object) (str1 + " " + str2));
      DataBase.AddUpdRecords("FileList", fields, -1, true);
    }

    private void Navigator_Leave(object sender, EventArgs e)
    {
      this.Animation(true);
    }

    private void Navigator_MouseLeave(object sender, EventArgs e)
    {
      this.Animation(true);
    }

    private void timerSwap_Tick(object sender, EventArgs e)
    {
    }

    public enum StrIdx
    {
      Title,
      Holders,
      Search,
      SearchFile,
      SearchVolume,
      AddHolder,
      AddVolume,
      DelVolume,
      SwpVolume,
      RenHolder,
      DelHolder,
      AreYouSure,
      NewNameHolder,
      Pos,
      Vol,
      UnitUSBName,
      YouWillOverWrite,
      AlreadyExistsNewName,
      AlreadyExistsOverwrite,
      EnterHolderName,
      EnterHolderSlot,
      NewHolder,
      Folder,
      FileName,
      Size,
      Date,
      ExitApp,
    }

    public struct Holder
    {
      public int ID;
      public int DeviceID;
      public int DeviceTP;
      public int PS_Avail;
      public int PS_Curr;
      public string LabelName;
      public Image DeviceImg;
      public int iconX;
      public int iconY;
    }

    public struct VolumeDisc
    {
      public int ID;
      public string Drive;
      public string Label;
      public int DeviceID;
      public int HolderPS;
      public string RegDate;
    }
  }
}
