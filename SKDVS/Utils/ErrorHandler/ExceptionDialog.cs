using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

using System.Resources; //pour acceder au ressources

namespace SKDVS.Utils.ErrorHandler
{

	//--
	//-- Generic user error dialog
	//--
	//-- UI adapted from
	//--
	//-- Alan Cooper's "About Face: The Essentials of User Interface Design"
	//-- Chapter VII, "The End of Errors", pages 423-440
	//--
	//-- Jeff Atwood
	//-- http://www.codinghorror.com
	//--
	internal class ExceptionDialog : System.Windows.Forms.Form
	{

		#region " Windows Form Designer generated code "

		public ExceptionDialog() : base()
		{
			Load += UserErrorDialog_Load;

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            SetLanguage();
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
	

		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.Label lblErrorHeading;
		internal System.Windows.Forms.Label lblScopeHeading;
		internal System.Windows.Forms.Label lblActionHeading;
		internal System.Windows.Forms.Label lblMoreHeading;
		internal System.Windows.Forms.TextBox txtMore;
        internal System.Windows.Forms.RichTextBox ErrorBox;
        internal System.Windows.Forms.Button btn3;
        internal System.Windows.Forms.Button btn2;
        internal System.Windows.Forms.Button btn1;
        internal System.Windows.Forms.RichTextBox ActionBox;
        internal System.Windows.Forms.Button btnMore;
        internal System.Windows.Forms.RichTextBox ScopeBox;

		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblErrorHeading = new System.Windows.Forms.Label();
            this.lblScopeHeading = new System.Windows.Forms.Label();
            this.lblActionHeading = new System.Windows.Forms.Label();
            this.lblMoreHeading = new System.Windows.Forms.Label();
            this.txtMore = new System.Windows.Forms.TextBox();
            this.ErrorBox = new System.Windows.Forms.RichTextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.ScopeBox = new System.Windows.Forms.RichTextBox();
            this.ActionBox = new System.Windows.Forms.RichTextBox();
            this.btnMore = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(8, 8);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(32, 32);
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // lblErrorHeading
            // 
            this.lblErrorHeading.AutoSize = true;
            this.lblErrorHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblErrorHeading.Location = new System.Drawing.Point(48, 4);
            this.lblErrorHeading.Name = "lblErrorHeading";
            this.lblErrorHeading.Size = new System.Drawing.Size(96, 13);
            this.lblErrorHeading.TabIndex = 0;
            this.lblErrorHeading.Text = "What happened";
            // 
            // lblScopeHeading
            // 
            this.lblScopeHeading.AutoSize = true;
            this.lblScopeHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblScopeHeading.Location = new System.Drawing.Point(8, 92);
            this.lblScopeHeading.Name = "lblScopeHeading";
            this.lblScopeHeading.Size = new System.Drawing.Size(136, 13);
            this.lblScopeHeading.TabIndex = 2;
            this.lblScopeHeading.Text = "How this will affect you";
            // 
            // lblActionHeading
            // 
            this.lblActionHeading.AutoSize = true;
            this.lblActionHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblActionHeading.Location = new System.Drawing.Point(8, 180);
            this.lblActionHeading.Name = "lblActionHeading";
            this.lblActionHeading.Size = new System.Drawing.Size(148, 13);
            this.lblActionHeading.TabIndex = 4;
            this.lblActionHeading.Text = "What you can do about it";
            // 
            // lblMoreHeading
            // 
            this.lblMoreHeading.AutoSize = true;
            this.lblMoreHeading.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMoreHeading.Location = new System.Drawing.Point(8, 300);
            this.lblMoreHeading.Name = "lblMoreHeading";
            this.lblMoreHeading.Size = new System.Drawing.Size(105, 13);
            this.lblMoreHeading.TabIndex = 6;
            this.lblMoreHeading.Text = "More information";
            // 
            // txtMore
            // 
            this.txtMore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMore.CausesValidation = false;
            this.txtMore.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMore.Location = new System.Drawing.Point(8, 324);
            this.txtMore.Multiline = true;
            this.txtMore.Name = "txtMore";
            this.txtMore.ReadOnly = true;
            this.txtMore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMore.Size = new System.Drawing.Size(456, 212);
            this.txtMore.TabIndex = 8;
            this.txtMore.Text = "(detailed information, such as exception details)";
            // 
            // ErrorBox
            // 
            this.ErrorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorBox.BackColor = System.Drawing.SystemColors.Control;
            this.ErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorBox.CausesValidation = false;
            this.ErrorBox.Location = new System.Drawing.Point(51, 20);
            this.ErrorBox.Name = "ErrorBox";
            this.ErrorBox.ReadOnly = true;
            this.ErrorBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ErrorBox.Size = new System.Drawing.Size(416, 64);
            this.ErrorBox.TabIndex = 9;
            this.ErrorBox.Text = "(error message)";
            this.ErrorBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ErrorBox_LinkClicked);
            // 
            // btn1
            // 
            this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn1.Location = new System.Drawing.Point(235, 542);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 10;
            this.btn1.Text = "Button1";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn2.Location = new System.Drawing.Point(311, 542);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 23);
            this.btn2.TabIndex = 11;
            this.btn2.Text = "Button2";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn3.Location = new System.Drawing.Point(389, 542);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 23);
            this.btn3.TabIndex = 12;
            this.btn3.Text = "Button3";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // ScopeBox
            // 
            this.ScopeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScopeBox.BackColor = System.Drawing.SystemColors.Control;
            this.ScopeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScopeBox.CausesValidation = false;
            this.ScopeBox.Location = new System.Drawing.Point(20, 108);
            this.ScopeBox.Name = "ScopeBox";
            this.ScopeBox.ReadOnly = true;
            this.ScopeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ScopeBox.Size = new System.Drawing.Size(440, 64);
            this.ScopeBox.TabIndex = 13;
            this.ScopeBox.Text = "(scope)";
            // 
            // ActionBox
            // 
            this.ActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionBox.BackColor = System.Drawing.SystemColors.Control;
            this.ActionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ActionBox.CausesValidation = false;
            this.ActionBox.Location = new System.Drawing.Point(20, 196);
            this.ActionBox.Name = "ActionBox";
            this.ActionBox.ReadOnly = true;
            this.ActionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ActionBox.Size = new System.Drawing.Size(440, 92);
            this.ActionBox.TabIndex = 14;
            this.ActionBox.Text = "(action)";
            // 
            // btnMore
            // 
            this.btnMore.Location = new System.Drawing.Point(119, 294);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(28, 24);
            this.btnMore.TabIndex = 15;
            this.btnMore.Text = ">>";
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // ExceptionDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(472, 573);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.ActionBox);
            this.Controls.Add(this.ScopeBox);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.ErrorBox);
            this.Controls.Add(this.txtMore);
            this.Controls.Add(this.lblMoreHeading);
            this.Controls.Add(this.lblActionHeading);
            this.Controls.Add(this.lblScopeHeading);
            this.Controls.Add(this.lblErrorHeading);
            this.Controls.Add(this.PictureBox1);
            this.MinimizeBox = false;
            this.Name = "ExceptionDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "(app) has encountered a problem";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private void SetLanguage()
        {
            
            
            ResourceManager resManager = new ResourceManager(typeof(SKDVS.Utils.ErrorHandler.Languages.resources));
            this.Text = resManager.GetString("Title");
            this.lblErrorHeading.Text = resManager.GetString("What happened");
            this.lblScopeHeading.Text = resManager.GetString("How this will affect you");
            this.lblActionHeading.Text = resManager.GetString("What you can do about it");
            this.lblMoreHeading.Text = "More information";
            this.txtMore.Text = "(detailed information, such as exception details)";
        }
		const int _intSpacing = 10;
		//--
		//-- security-safe process.start wrapper
		//--
		private void LaunchLink(string strUrl)
		{
			try {
				System.Diagnostics.Process.Start(strUrl);
			} catch (System.Security.SecurityException ex) {
				//-- do nothing; we can't launch without full trust.
			}
		}

		private void SizeBox(System.Windows.Forms.RichTextBox ctl)
		{
			Graphics g = null;
			try {
				//-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
				g = Graphics.FromHwnd(ctl.Handle);
				SizeF objSizeF = g.MeasureString(ctl.Text, ctl.Font, new SizeF(ctl.Width, ctl.Height));
				g.Dispose();
				ctl.Height = Convert.ToInt32(objSizeF.Height) + 5;
			} catch (System.Security.SecurityException ex) {
				//-- do nothing; we can't set control sizes without full trust
			} finally {
				if ((g != null))
					g.Dispose();
			}
		}

		private System.Windows.Forms.DialogResult DetermineDialogResult(string strButtonText)
		{
			//-- strip any accelerator keys we might have
			strButtonText = strButtonText.Replace("&", "");
			switch (strButtonText.ToLower()) {
				case "abort":
					return System.Windows.Forms.DialogResult.Abort;
				case "cancel":
					return System.Windows.Forms.DialogResult.Cancel;
				case "ignore":
					return System.Windows.Forms.DialogResult.Ignore;
				case "no":
					return System.Windows.Forms.DialogResult.No;
				case "none":
					return System.Windows.Forms.DialogResult.None;
				case "ok":
					return System.Windows.Forms.DialogResult.OK;
				case "retry":
					return System.Windows.Forms.DialogResult.Retry;
				case "yes":
					return System.Windows.Forms.DialogResult.Yes;
                default:
                    return System.Windows.Forms.DialogResult.OK;
			}
		}

		private void btn1_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn1.Text);
		}

		private void btn2_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn2.Text);
		}

		private void btn3_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn3.Text);
		}

		private void UserErrorDialog_Load(System.Object sender, System.EventArgs e)
		{
			//-- make sure our window is on top
			this.TopMost = true;
			this.TopMost = false;

			//-- More >> has to be expanded
			this.txtMore.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtMore.Visible = false;

			//-- size the labels' height to accommodate the amount of text in them
			SizeBox(ScopeBox);
			SizeBox(ActionBox);
			SizeBox(ErrorBox);

			//-- now shift everything up
			lblScopeHeading.Top = ErrorBox.Top + ErrorBox.Height + _intSpacing;
			ScopeBox.Top = lblScopeHeading.Top + lblScopeHeading.Height + _intSpacing;

			lblActionHeading.Top = ScopeBox.Top + ScopeBox.Height + _intSpacing;
			ActionBox.Top = lblActionHeading.Top + lblActionHeading.Height + _intSpacing;

			lblMoreHeading.Top = ActionBox.Top + ActionBox.Height + _intSpacing;
			btnMore.Top = lblMoreHeading.Top - 3;

			this.Height = btnMore.Top + btnMore.Height + _intSpacing + 45;

			this.CenterToScreen();
		}

		private void btnMore_Click(System.Object sender, System.EventArgs e)
        {
            if (btnMore.Text == ">>")
            {
                this.Height = this.Height + 300;
                var _with1 = txtMore;
                _with1.Location = new System.Drawing.Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + _intSpacing);
                _with1.Height = this.ClientSize.Height - txtMore.Top - 45;
                _with1.Width = this.ClientSize.Width - 2 * _intSpacing;
                _with1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
                _with1.Visible = true;
                btn3.Focus();
                btnMore.Text = "<<";
            }
            else
            {
                this.SuspendLayout();
                btnMore.Text = ">>";
                this.Height = btnMore.Top + btnMore.Height + _intSpacing + 45;
                txtMore.Visible = false;
                txtMore.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.ResumeLayout();
            }
        }

		private void ErrorBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}

		private void ScopeBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}

		private void ActionBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}

        
	}
}
