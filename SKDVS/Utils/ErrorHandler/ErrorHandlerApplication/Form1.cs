using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using SKDVS.Utils.ErrorHandler;
using System.Threading;

namespace WindowsApplication1
{

	//--
	//-- Jeff Atwood
	//-- http://www.codinghorror.com
	//--
	public class Form1 : System.Windows.Forms.Form
	{

		#region " Windows Form Designer generated code "

		public Form1() : base()
		{
			Load += Form1_Load;

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-BE");
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-BE");

		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if ((components != null)) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox txtWhatUserCanDo;
		internal System.Windows.Forms.TextBox txtWhatHappened;
		internal System.Windows.Forms.TextBox txtHowUserAffected;
		
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.RadioButton radioAbortRetryIgnore;
		internal System.Windows.Forms.RadioButton radioOK;
		internal System.Windows.Forms.RadioButton radioOKCancel;
		internal System.Windows.Forms.RadioButton radioRetryCancel;
		internal System.Windows.Forms.RadioButton radioYesNo;
		internal System.Windows.Forms.RadioButton radioYesNoCancel;
		internal System.Windows.Forms.RadioButton radioError;
		internal System.Windows.Forms.RadioButton radioExclamation;
		internal System.Windows.Forms.RadioButton radioQuestion;
		internal System.Windows.Forms.RadioButton radioInformation;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.RadioButton radioButtonDefault;
		internal System.Windows.Forms.RadioButton radioButton3;
		internal System.Windows.Forms.RadioButton radioButton2;
		internal System.Windows.Forms.RadioButton radioButton1;
		internal System.Windows.Forms.CheckBox checkEmailHandledException;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.TextBox txtMoreInfo;
		internal System.Windows.Forms.TextBox txtSMTPPort;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox txtSMTPServer;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.TabPage TabPage3;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.TextBox txtSMTPDomain;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.TextBox txtEmailTo;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.TextBox txtContactInfo;
        internal Button Button2;
        internal Button Button3;
        internal CheckBox CheckBox1;
        internal Button Button1;
		internal System.Windows.Forms.PictureBox PictureBox1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtContactInfo = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtSMTPDomain = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtSMTPServer = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.txtMoreInfo = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.checkEmailHandledException = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonDefault = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.radioInformation = new System.Windows.Forms.RadioButton();
            this.radioQuestion = new System.Windows.Forms.RadioButton();
            this.radioExclamation = new System.Windows.Forms.RadioButton();
            this.radioError = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.radioYesNoCancel = new System.Windows.Forms.RadioButton();
            this.radioYesNo = new System.Windows.Forms.RadioButton();
            this.radioRetryCancel = new System.Windows.Forms.RadioButton();
            this.radioOKCancel = new System.Windows.Forms.RadioButton();
            this.radioOK = new System.Windows.Forms.RadioButton();
            this.radioAbortRetryIgnore = new System.Windows.Forms.RadioButton();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtWhatUserCanDo = new System.Windows.Forms.TextBox();
            this.txtWhatHappened = new System.Windows.Forms.TextBox();
            this.txtHowUserAffected = new System.Windows.Forms.TextBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Button1 = new System.Windows.Forms.Button();
            this.TabControl1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(708, 501);
            this.TabControl1.TabIndex = 0;
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.PictureBox1);
            this.TabPage3.Controls.Add(this.txtContactInfo);
            this.TabPage3.Controls.Add(this.Label12);
            this.TabPage3.Controls.Add(this.txtEmailTo);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.txtSMTPDomain);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.txtSMTPPort);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.txtSMTPServer);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(700, 475);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "SMTP Settings";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(16, 152);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(673, 163);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBox1.TabIndex = 13;
            this.PictureBox1.TabStop = false;
            // 
            // txtContactInfo
            // 
            this.txtContactInfo.Enabled = false;
            this.txtContactInfo.Location = new System.Drawing.Point(256, 348);
            this.txtContactInfo.Name = "txtContactInfo";
            this.txtContactInfo.Size = new System.Drawing.Size(280, 20);
            this.txtContactInfo.TabIndex = 12;
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(12, 352);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(216, 16);
            this.Label12.TabIndex = 11;
            this.Label12.Text = "UnhandledExceptionManager/ContactInfo";
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Enabled = false;
            this.txtEmailTo.Location = new System.Drawing.Point(256, 324);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(280, 20);
            this.txtEmailTo.TabIndex = 10;
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(12, 328);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(200, 16);
            this.Label11.TabIndex = 9;
            this.Label11.Text = "UnhandledExceptionManager/EmailTo";
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label10.Location = new System.Drawing.Point(16, 128);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(284, 16);
            this.Label10.TabIndex = 8;
            this.Label10.Text = "Don\'t forget to set the mailto: address in App.Config!";
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(12, 12);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(460, 16);
            this.Label9.TabIndex = 7;
            this.Label9.Text = "Edit the default values in SimpleMail.vb to match your preferred outgoing mail se" +
    "rver.";
            // 
            // txtSMTPDomain
            // 
            this.txtSMTPDomain.Enabled = false;
            this.txtSMTPDomain.Location = new System.Drawing.Point(256, 36);
            this.txtSMTPDomain.Name = "txtSMTPDomain";
            this.txtSMTPDomain.Size = new System.Drawing.Size(256, 20);
            this.txtSMTPDomain.TabIndex = 6;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(12, 36);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(144, 16);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "SimpleMail.DefaultDomain";
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.Enabled = false;
            this.txtSMTPPort.Location = new System.Drawing.Point(256, 88);
            this.txtSMTPPort.MaxLength = 4;
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(56, 20);
            this.txtSMTPPort.TabIndex = 3;
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(12, 92);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(88, 16);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "SimpleMail.Port";
            // 
            // txtSMTPServer
            // 
            this.txtSMTPServer.Enabled = false;
            this.txtSMTPServer.Location = new System.Drawing.Point(256, 60);
            this.txtSMTPServer.Name = "txtSMTPServer";
            this.txtSMTPServer.Size = new System.Drawing.Size(256, 20);
            this.txtSMTPServer.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(12, 64);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(100, 16);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "SimpleMail.Server";
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.CheckBox1);
            this.TabPage1.Controls.Add(this.Button3);
            this.TabPage1.Controls.Add(this.Button2);
            this.TabPage1.Controls.Add(this.txtMoreInfo);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.checkEmailHandledException);
            this.TabPage1.Controls.Add(this.GroupBox3);
            this.TabPage1.Controls.Add(this.GroupBox2);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.txtWhatUserCanDo);
            this.TabPage1.Controls.Add(this.txtWhatHappened);
            this.TabPage1.Controls.Add(this.txtHowUserAffected);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(700, 475);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Handled Exception";
            // 
            // CheckBox1
            // 
            this.CheckBox1.Checked = true;
            this.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox1.Location = new System.Drawing.Point(32, 384);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(312, 20);
            this.CheckBox1.TabIndex = 16;
            this.CheckBox1.Text = "Use the actual exception for the \"more information\" text";
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(508, 439);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(180, 28);
            this.Button3.TabIndex = 15;
            this.Button3.Text = "Handled Exception (defaults)";
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(322, 439);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(180, 28);
            this.Button2.TabIndex = 14;
            this.Button2.Text = "Handled Exception (customized)";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // txtMoreInfo
            // 
            this.txtMoreInfo.Enabled = false;
            this.txtMoreInfo.Location = new System.Drawing.Point(32, 304);
            this.txtMoreInfo.Multiline = true;
            this.txtMoreInfo.Name = "txtMoreInfo";
            this.txtMoreInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoreInfo.Size = new System.Drawing.Size(512, 60);
            this.txtMoreInfo.TabIndex = 7;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(8, 288);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(104, 16);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "More information:";
            // 
            // checkEmailHandledException
            // 
            this.checkEmailHandledException.Checked = true;
            this.checkEmailHandledException.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEmailHandledException.Location = new System.Drawing.Point(16, 420);
            this.checkEmailHandledException.Name = "checkEmailHandledException";
            this.checkEmailHandledException.Size = new System.Drawing.Size(288, 16);
            this.checkEmailHandledException.TabIndex = 12;
            this.checkEmailHandledException.Text = "Send an email notification for this handled exception";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.radioButtonDefault);
            this.GroupBox3.Controls.Add(this.radioButton3);
            this.GroupBox3.Controls.Add(this.radioButton2);
            this.GroupBox3.Controls.Add(this.radioButton1);
            this.GroupBox3.Location = new System.Drawing.Point(556, 324);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(132, 104);
            this.GroupBox3.TabIndex = 11;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Default Button";
            // 
            // radioButtonDefault
            // 
            this.radioButtonDefault.Checked = true;
            this.radioButtonDefault.Location = new System.Drawing.Point(12, 80);
            this.radioButtonDefault.Name = "radioButtonDefault";
            this.radioButtonDefault.Size = new System.Drawing.Size(60, 16);
            this.radioButtonDefault.TabIndex = 3;
            this.radioButtonDefault.TabStop = true;
            this.radioButtonDefault.Text = "Default";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(12, 60);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(64, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Button3";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(12, 40);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(64, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Button2";
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(12, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(64, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Button1";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.radioInformation);
            this.GroupBox2.Controls.Add(this.radioQuestion);
            this.GroupBox2.Controls.Add(this.radioExclamation);
            this.GroupBox2.Controls.Add(this.radioError);
            this.GroupBox2.Location = new System.Drawing.Point(556, 192);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(132, 124);
            this.GroupBox2.TabIndex = 10;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Icon";
            // 
            // radioInformation
            // 
            this.radioInformation.Location = new System.Drawing.Point(12, 96);
            this.radioInformation.Name = "radioInformation";
            this.radioInformation.Size = new System.Drawing.Size(84, 20);
            this.radioInformation.TabIndex = 3;
            this.radioInformation.Text = "Information";
            // 
            // radioQuestion
            // 
            this.radioQuestion.Location = new System.Drawing.Point(12, 72);
            this.radioQuestion.Name = "radioQuestion";
            this.radioQuestion.Size = new System.Drawing.Size(72, 20);
            this.radioQuestion.TabIndex = 2;
            this.radioQuestion.Text = "Question";
            // 
            // radioExclamation
            // 
            this.radioExclamation.Location = new System.Drawing.Point(12, 48);
            this.radioExclamation.Name = "radioExclamation";
            this.radioExclamation.Size = new System.Drawing.Size(88, 20);
            this.radioExclamation.TabIndex = 1;
            this.radioExclamation.Text = "Exclamation";
            // 
            // radioError
            // 
            this.radioError.Checked = true;
            this.radioError.Location = new System.Drawing.Point(12, 24);
            this.radioError.Name = "radioError";
            this.radioError.Size = new System.Drawing.Size(48, 20);
            this.radioError.TabIndex = 0;
            this.radioError.TabStop = true;
            this.radioError.Text = "Error";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.radioYesNoCancel);
            this.GroupBox1.Controls.Add(this.radioYesNo);
            this.GroupBox1.Controls.Add(this.radioRetryCancel);
            this.GroupBox1.Controls.Add(this.radioOKCancel);
            this.GroupBox1.Controls.Add(this.radioOK);
            this.GroupBox1.Controls.Add(this.radioAbortRetryIgnore);
            this.GroupBox1.Location = new System.Drawing.Point(556, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(132, 172);
            this.GroupBox1.TabIndex = 9;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Buttons";
            // 
            // radioYesNoCancel
            // 
            this.radioYesNoCancel.Location = new System.Drawing.Point(12, 140);
            this.radioYesNoCancel.Name = "radioYesNoCancel";
            this.radioYesNoCancel.Size = new System.Drawing.Size(96, 20);
            this.radioYesNoCancel.TabIndex = 5;
            this.radioYesNoCancel.Text = "YesNoCancel";
            // 
            // radioYesNo
            // 
            this.radioYesNo.Location = new System.Drawing.Point(12, 116);
            this.radioYesNo.Name = "radioYesNo";
            this.radioYesNo.Size = new System.Drawing.Size(60, 20);
            this.radioYesNo.TabIndex = 4;
            this.radioYesNo.Text = "YesNo";
            // 
            // radioRetryCancel
            // 
            this.radioRetryCancel.Location = new System.Drawing.Point(12, 92);
            this.radioRetryCancel.Name = "radioRetryCancel";
            this.radioRetryCancel.Size = new System.Drawing.Size(96, 20);
            this.radioRetryCancel.TabIndex = 3;
            this.radioRetryCancel.Text = "RetryCancel";
            // 
            // radioOKCancel
            // 
            this.radioOKCancel.Location = new System.Drawing.Point(12, 68);
            this.radioOKCancel.Name = "radioOKCancel";
            this.radioOKCancel.Size = new System.Drawing.Size(76, 20);
            this.radioOKCancel.TabIndex = 2;
            this.radioOKCancel.Text = "OKCancel";
            // 
            // radioOK
            // 
            this.radioOK.Checked = true;
            this.radioOK.Location = new System.Drawing.Point(12, 44);
            this.radioOK.Name = "radioOK";
            this.radioOK.Size = new System.Drawing.Size(44, 20);
            this.radioOK.TabIndex = 1;
            this.radioOK.TabStop = true;
            this.radioOK.Text = "OK";
            // 
            // radioAbortRetryIgnore
            // 
            this.radioAbortRetryIgnore.Location = new System.Drawing.Point(12, 20);
            this.radioAbortRetryIgnore.Name = "radioAbortRetryIgnore";
            this.radioAbortRetryIgnore.Size = new System.Drawing.Size(112, 20);
            this.radioAbortRetryIgnore.TabIndex = 0;
            this.radioAbortRetryIgnore.Text = "AbortRetryIgnore";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(12, 196);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 16);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "What the user can do:";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(8, 104);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(164, 16);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "How the user will be affected:";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "What Happened:";
            // 
            // txtWhatUserCanDo
            // 
            this.txtWhatUserCanDo.Location = new System.Drawing.Point(32, 220);
            this.txtWhatUserCanDo.Multiline = true;
            this.txtWhatUserCanDo.Name = "txtWhatUserCanDo";
            this.txtWhatUserCanDo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWhatUserCanDo.Size = new System.Drawing.Size(512, 60);
            this.txtWhatUserCanDo.TabIndex = 5;
            this.txtWhatUserCanDo.Text = "List anything the user can do to resolve this problem or condition, including con" +
    "tacting (contact) or perhaps visiting http://www.codinghorror.com";
            // 
            // txtWhatHappened
            // 
            this.txtWhatHappened.Location = new System.Drawing.Point(32, 36);
            this.txtWhatHappened.Multiline = true;
            this.txtWhatHappened.Name = "txtWhatHappened";
            this.txtWhatHappened.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWhatHappened.Size = new System.Drawing.Size(508, 60);
            this.txtWhatHappened.TabIndex = 1;
            this.txtWhatHappened.Text = "Describe what happened to (app) in plain, non technical terms";
            // 
            // txtHowUserAffected
            // 
            this.txtHowUserAffected.Location = new System.Drawing.Point(32, 128);
            this.txtHowUserAffected.Multiline = true;
            this.txtHowUserAffected.Name = "txtHowUserAffected";
            this.txtHowUserAffected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHowUserAffected.Size = new System.Drawing.Size(508, 60);
            this.txtHowUserAffected.TabIndex = 3;
            this.txtHowUserAffected.Text = "Describe how the user will be affected by this problem or condition; be specific." +
    "";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Button1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(700, 475);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Unhandled Exception";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(262, 223);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(176, 28);
            this.Button1.TabIndex = 2;
            this.Button1.Text = "Generate Unhandled Exception";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(708, 501);
            this.Controls.Add(this.TabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TabControl1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void GenerateException()
		{
			//-- just a simple "object not initialized" exception (should read "as new")
			System.Collections.Specialized.NameValueCollection x = null;
			MessageBox.Show(x.Count.ToString());
		}

		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			GenerateException();
		}

		private void Button2_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                GenerateException();
            }
            catch (Exception ex)
            {
                HandledExceptionManager.EmailError = checkEmailHandledException.Checked;
                if (CheckBox1.Checked)
                {
                    // -- use exception as "more"
                    HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, ex, GetButtonType(), GetIconType(), GetDefaultButton());

                }
                else
                {
                    // -- use custom text as "more"
                    HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, txtMoreInfo.Text, GetButtonType(), GetIconType(), GetDefaultButton());
                }
            }
        }
		private void Button3_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                GenerateException();
            }
            catch (Exception ex)
            {
                HandledExceptionManager.EmailError = checkEmailHandledException.Checked;
                // -- minimal form, no extra params
                HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, ex);
            }
        }
       

		private HandledExceptionManager.UserErrorDefaultButton GetDefaultButton()
		{
			if (radioButton1.Checked) {
				return HandledExceptionManager.UserErrorDefaultButton.Button1;
			}
			if (radioButton2.Checked) {
				return HandledExceptionManager.UserErrorDefaultButton.Button2;
			}
			if (radioButton3.Checked) {
				return HandledExceptionManager.UserErrorDefaultButton.Button3;
			}
			return HandledExceptionManager.UserErrorDefaultButton.Default;
		}

		private MessageBoxIcon GetIconType()
		{
			if (radioExclamation.Checked) {
				return MessageBoxIcon.Exclamation;
			}
			if (radioQuestion.Checked) {
				return MessageBoxIcon.Question;
			}
			if (radioInformation.Checked) {
				return MessageBoxIcon.Information;
			}
			return MessageBoxIcon.Error;
		}

		private MessageBoxButtons GetButtonType()
		{
			if (radioAbortRetryIgnore.Checked) {
				return MessageBoxButtons.AbortRetryIgnore;
			}
			if (radioOKCancel.Checked) {
				return MessageBoxButtons.OKCancel;
			}
			if (radioRetryCancel.Checked) {
				return MessageBoxButtons.RetryCancel;
			}
			if (radioYesNo.Checked) {
				return MessageBoxButtons.YesNo;
			}
			if (radioYesNoCancel.Checked) {
				return MessageBoxButtons.YesNoCancel;
			}
			return MessageBoxButtons.OK;
		}


		private void CheckBox1_CheckedChanged(System.Object sender, System.EventArgs e)
        {

        }

		private void Form1_Load(object sender, System.EventArgs e)
		{
			SKDVS.Utils.ErrorHandler.SimpleMail.SMTPClient smtp = new SKDVS.Utils.ErrorHandler.SimpleMail.SMTPClient();

			txtSMTPDomain.Text = smtp.DefaultDomain;
			txtSMTPPort.Text = smtp.Port.ToString();
			txtSMTPServer.Text = smtp.Server.ToString();
			txtEmailTo.Text = AppSettings.GetString("UnhandledExceptionManager/EmailTo");
			txtContactInfo.Text = AppSettings.GetString("UnhandledExceptionManager/ContactInfo");
		}

       
	}
}
