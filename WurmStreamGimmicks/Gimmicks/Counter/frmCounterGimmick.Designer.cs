namespace WurmStreamGimmicks {
    partial class frmCounterGimmick {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkEvents = new System.Windows.Forms.CheckBox();
            this.chkCombat = new System.Windows.Forms.CheckBox();
            this.chkSkills = new System.Windows.Forms.CheckBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.listPlayers = new System.Windows.Forms.CheckedListBox();
            this.chkCollective = new System.Windows.Forms.CheckBox();
            this.lblSession = new System.Windows.Forms.Label();
            this.numSession = new System.Windows.Forms.NumericUpDown();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.numGlobal = new System.Windows.Forms.NumericUpDown();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.cmdBrowseOutputFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGlobal)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "&Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(172, 20);
            this.txtName.TabIndex = 1;
            // 
            // chkEvents
            // 
            this.chkEvents.AutoSize = true;
            this.chkEvents.Location = new System.Drawing.Point(279, 14);
            this.chkEvents.Name = "chkEvents";
            this.chkEvents.Size = new System.Drawing.Size(59, 17);
            this.chkEvents.TabIndex = 2;
            this.chkEvents.Text = "&Events";
            this.chkEvents.UseVisualStyleBackColor = true;
            // 
            // chkCombat
            // 
            this.chkCombat.AutoSize = true;
            this.chkCombat.Location = new System.Drawing.Point(344, 14);
            this.chkCombat.Name = "chkCombat";
            this.chkCombat.Size = new System.Drawing.Size(62, 17);
            this.chkCombat.TabIndex = 3;
            this.chkCombat.Text = "Com&bat";
            this.chkCombat.UseVisualStyleBackColor = true;
            // 
            // chkSkills
            // 
            this.chkSkills.AutoSize = true;
            this.chkSkills.Location = new System.Drawing.Point(412, 14);
            this.chkSkills.Name = "chkSkills";
            this.chkSkills.Size = new System.Drawing.Size(50, 17);
            this.chkSkills.TabIndex = 4;
            this.chkSkills.Text = "&Skills";
            this.chkSkills.UseVisualStyleBackColor = true;
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(12, 41);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(44, 13);
            this.lblPattern.TabIndex = 5;
            this.lblPattern.Text = "&Pattern:";
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(101, 38);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(361, 20);
            this.txtPattern.TabIndex = 6;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(14, 67);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(42, 13);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "Out&put:";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(101, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(361, 20);
            this.txtOutput.TabIndex = 8;
            // 
            // lblHelp
            // 
            this.lblHelp.BackColor = System.Drawing.SystemColors.Control;
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Location = new System.Drawing.Point(101, 87);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(361, 46);
            this.lblHelp.TabIndex = 9;
            this.lblHelp.Text = "This is the help label.";
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(12, 201);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(45, 13);
            this.lblMonitor.TabIndex = 13;
            this.lblMonitor.Text = "&Monitor:";
            // 
            // listPlayers
            // 
            this.listPlayers.CheckOnClick = true;
            this.listPlayers.FormattingEnabled = true;
            this.listPlayers.IntegralHeight = false;
            this.listPlayers.Location = new System.Drawing.Point(101, 162);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(120, 136);
            this.listPlayers.TabIndex = 14;
            // 
            // chkCollective
            // 
            this.chkCollective.AutoSize = true;
            this.chkCollective.Location = new System.Drawing.Point(227, 281);
            this.chkCollective.Name = "chkCollective";
            this.chkCollective.Size = new System.Drawing.Size(136, 17);
            this.chkCollective.TabIndex = 15;
            this.chkCollective.Text = "Monitor AL&L characters";
            this.chkCollective.UseVisualStyleBackColor = true;
            // 
            // lblSession
            // 
            this.lblSession.AutoSize = true;
            this.lblSession.Location = new System.Drawing.Point(12, 306);
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(77, 13);
            this.lblSession.TabIndex = 16;
            this.lblSession.Text = "S&ession count:";
            // 
            // numSession
            // 
            this.numSession.Location = new System.Drawing.Point(101, 304);
            this.numSession.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numSession.Name = "numSession";
            this.numSession.Size = new System.Drawing.Size(120, 20);
            this.numSession.TabIndex = 17;
            // 
            // lblGlobal
            // 
            this.lblGlobal.AutoSize = true;
            this.lblGlobal.Location = new System.Drawing.Point(12, 332);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(67, 13);
            this.lblGlobal.TabIndex = 18;
            this.lblGlobal.Text = "&Global count";
            // 
            // numGlobal
            // 
            this.numGlobal.Location = new System.Drawing.Point(101, 330);
            this.numGlobal.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numGlobal.Name = "numGlobal";
            this.numGlobal.Size = new System.Drawing.Size(120, 20);
            this.numGlobal.TabIndex = 19;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(306, 358);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 20;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(387, 358);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 21;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(12, 139);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(58, 13);
            this.lblOutputFile.TabIndex = 10;
            this.lblOutputFile.Text = "Outpu&t file:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(101, 136);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(327, 20);
            this.txtOutputFile.TabIndex = 11;
            this.txtOutputFile.Text = ".\\output.txt";
            // 
            // cmdBrowseOutputFile
            // 
            this.cmdBrowseOutputFile.Location = new System.Drawing.Point(427, 136);
            this.cmdBrowseOutputFile.Name = "cmdBrowseOutputFile";
            this.cmdBrowseOutputFile.Size = new System.Drawing.Size(35, 20);
            this.cmdBrowseOutputFile.TabIndex = 12;
            this.cmdBrowseOutputFile.Text = "...";
            this.cmdBrowseOutputFile.UseVisualStyleBackColor = true;
            // 
            // frmCounterGimmick
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(474, 389);
            this.Controls.Add(this.cmdBrowseOutputFile);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.lblOutputFile);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.numGlobal);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.numSession);
            this.Controls.Add(this.lblSession);
            this.Controls.Add(this.chkCollective);
            this.Controls.Add(this.listPlayers);
            this.Controls.Add(this.lblMonitor);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.lblPattern);
            this.Controls.Add(this.chkSkills);
            this.Controls.Add(this.chkCombat);
            this.Controls.Add(this.chkEvents);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCounterGimmick";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Counter gimmick configuration";
            ((System.ComponentModel.ISupportInitialize)(this.numSession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGlobal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkEvents;
        private System.Windows.Forms.CheckBox chkCombat;
        private System.Windows.Forms.CheckBox chkSkills;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.CheckedListBox listPlayers;
        private System.Windows.Forms.CheckBox chkCollective;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.NumericUpDown numSession;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.NumericUpDown numGlobal;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button cmdBrowseOutputFile;
    }
}