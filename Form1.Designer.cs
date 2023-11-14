namespace keyboard_typer_v1
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			gamepanel = new Panel();
			precisionlabel = new Label();
			scorelabel = new Label();
			settingsbutton = new Button();
			languagebutton = new Button();
			inputBox = new TextBox();
			wordLabel = new Label();
			settingspanel = new Panel();
			selectresultbutton = new Button();
			startbutton = new Button();
			infolabel = new Label();
			gamepanel.SuspendLayout();
			settingspanel.SuspendLayout();
			SuspendLayout();
			// 
			// gamepanel
			// 
			gamepanel.Controls.Add(precisionlabel);
			gamepanel.Controls.Add(scorelabel);
			gamepanel.Controls.Add(settingsbutton);
			gamepanel.Controls.Add(languagebutton);
			gamepanel.Controls.Add(inputBox);
			gamepanel.Controls.Add(wordLabel);
			gamepanel.Location = new Point(0, 0);
			gamepanel.Name = "gamepanel";
			gamepanel.Size = new Size(578, 92);
			gamepanel.TabIndex = 0;
			gamepanel.Visible = false;
			// 
			// precisionlabel
			// 
			precisionlabel.AutoSize = true;
			precisionlabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			precisionlabel.Location = new Point(9, 26);
			precisionlabel.Name = "precisionlabel";
			precisionlabel.Size = new Size(76, 17);
			precisionlabel.TabIndex = 5;
			precisionlabel.Text = "Precision: 0";
			// 
			// scorelabel
			// 
			scorelabel.AutoSize = true;
			scorelabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			scorelabel.Location = new Point(9, 9);
			scorelabel.Name = "scorelabel";
			scorelabel.Size = new Size(55, 17);
			scorelabel.TabIndex = 4;
			scorelabel.Text = "Score: 0";
			// 
			// settingsbutton
			// 
			settingsbutton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			settingsbutton.Location = new Point(459, 9);
			settingsbutton.Name = "settingsbutton";
			settingsbutton.Size = new Size(64, 30);
			settingsbutton.TabIndex = 3;
			settingsbutton.Text = "Settings";
			settingsbutton.UseVisualStyleBackColor = true;
			settingsbutton.Click += settingsbutton_Click;
			// 
			// languagebutton
			// 
			languagebutton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			languagebutton.Location = new Point(529, 9);
			languagebutton.Name = "languagebutton";
			languagebutton.Size = new Size(40, 30);
			languagebutton.TabIndex = 2;
			languagebutton.Text = "En";
			languagebutton.UseVisualStyleBackColor = true;
			languagebutton.Click += languagebutton_Click;
			// 
			// inputBox
			// 
			inputBox.BackColor = SystemColors.HighlightText;
			inputBox.Enabled = false;
			inputBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			inputBox.Location = new Point(9, 54);
			inputBox.Name = "inputBox";
			inputBox.ReadOnly = true;
			inputBox.Size = new Size(560, 29);
			inputBox.TabIndex = 1;
			inputBox.TextAlign = HorizontalAlignment.Center;
			inputBox.TextChanged += inputBox_TextChanged;
			// 
			// wordLabel
			// 
			wordLabel.AutoSize = true;
			wordLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			wordLabel.Location = new Point(242, 9);
			wordLabel.Name = "wordLabel";
			wordLabel.Size = new Size(108, 30);
			wordLabel.TabIndex = 0;
			wordLabel.Text = "wordLabel";
			wordLabel.Click += wordLabel_Click;
			// 
			// settingspanel
			// 
			settingspanel.Controls.Add(gamepanel);
			settingspanel.Controls.Add(selectresultbutton);
			settingspanel.Controls.Add(startbutton);
			settingspanel.Controls.Add(infolabel);
			settingspanel.Location = new Point(3, 3);
			settingspanel.Name = "settingspanel";
			settingspanel.Size = new Size(578, 92);
			settingspanel.TabIndex = 1;
			// 
			// selectresultbutton
			// 
			selectresultbutton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			selectresultbutton.Location = new Point(178, 55);
			selectresultbutton.Name = "selectresultbutton";
			selectresultbutton.Size = new Size(133, 30);
			selectresultbutton.TabIndex = 3;
			selectresultbutton.Text = "Select result";
			selectresultbutton.UseVisualStyleBackColor = true;
			selectresultbutton.Click += selectresultbutton_Click;
			// 
			// startbutton
			// 
			startbutton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			startbutton.Location = new Point(317, 55);
			startbutton.Name = "startbutton";
			startbutton.Size = new Size(62, 30);
			startbutton.TabIndex = 0;
			startbutton.Text = "Start";
			startbutton.UseVisualStyleBackColor = true;
			startbutton.Click += startbutton_Click;
			// 
			// infolabel
			// 
			infolabel.AutoSize = true;
			infolabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			infolabel.Location = new Point(11, 10);
			infolabel.Name = "infolabel";
			infolabel.Size = new Size(556, 40);
			infolabel.TabIndex = 1;
			infolabel.Text = "Select the result file compiled by Keyboard Capture application.\r\nThis app will mimic human typing process, including mistakes and processing time.";
			infolabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(584, 98);
			Controls.Add(settingspanel);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Name = "Form1";
			Text = "Keyboard typer";
			Load += Form1_Load;
			gamepanel.ResumeLayout(false);
			gamepanel.PerformLayout();
			settingspanel.ResumeLayout(false);
			settingspanel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel gamepanel;
		private Button languagebutton;
		private Label wordLabel;
		private Button settingsbutton;
		private Label precisionlabel;
		private Label scorelabel;
		private Panel settingspanel;
		private Label infolabel;
		private Button startbutton;
		private Button selectresultbutton;
		private TextBox inputBox;
	}
}