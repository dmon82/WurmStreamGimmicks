namespace WurmStreamGimmicks {
    partial class frmPartsRemainingGimmick {
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
            this.cmdBrowseOutputFile = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.chkCollective = new System.Windows.Forms.CheckBox();
            this.listPlayers = new System.Windows.Forms.CheckedListBox();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.chkSkills = new System.Windows.Forms.CheckBox();
            this.chkCombat = new System.Windows.Forms.CheckBox();
            this.chkEvents = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtItemname = new System.Windows.Forms.TextBox();
            this.numTotalParts = new System.Windows.Forms.NumericUpDown();
            this.numPartsAttached = new System.Windows.Forms.NumericUpDown();
            this.lblItemname = new System.Windows.Forms.Label();
            this.lblTotalParts = new System.Windows.Forms.Label();
            this.lblPartsAttached = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPartsAttached)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdBrowseOutputFile
            // 
            this.cmdBrowseOutputFile.Location = new System.Drawing.Point(420, 136);
            this.cmdBrowseOutputFile.Name = "cmdBrowseOutputFile";
            this.cmdBrowseOutputFile.Size = new System.Drawing.Size(35, 20);
            this.cmdBrowseOutputFile.TabIndex = 12;
            this.cmdBrowseOutputFile.Text = "...";
            this.cmdBrowseOutputFile.UseVisualStyleBackColor = true;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(94, 136);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(327, 20);
            this.txtOutputFile.TabIndex = 11;
            this.txtOutputFile.Text = ".\\output.txt";
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(5, 139);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(58, 13);
            this.lblOutputFile.TabIndex = 10;
            this.lblOutputFile.Text = "Outpu&t file:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(380, 388);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 23;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(299, 388);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 22;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // chkCollective
            // 
            this.chkCollective.AutoSize = true;
            this.chkCollective.Location = new System.Drawing.Point(220, 281);
            this.chkCollective.Name = "chkCollective";
            this.chkCollective.Size = new System.Drawing.Size(136, 17);
            this.chkCollective.TabIndex = 15;
            this.chkCollective.Text = "Monitor AL&L characters";
            this.chkCollective.UseVisualStyleBackColor = true;
            // 
            // listPlayers
            // 
            this.listPlayers.CheckOnClick = true;
            this.listPlayers.FormattingEnabled = true;
            this.listPlayers.IntegralHeight = false;
            this.listPlayers.Location = new System.Drawing.Point(94, 162);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(120, 136);
            this.listPlayers.TabIndex = 14;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(5, 201);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(45, 13);
            this.lblMonitor.TabIndex = 13;
            this.lblMonitor.Text = "&Monitor:";
            // 
            // lblHelp
            // 
            this.lblHelp.BackColor = System.Drawing.SystemColors.Control;
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Location = new System.Drawing.Point(94, 87);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(361, 46);
            this.lblHelp.TabIndex = 9;
            this.lblHelp.Text = "This is the help label.";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(94, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(361, 20);
            this.txtOutput.TabIndex = 8;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(7, 67);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(42, 13);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "Out&put:";
            // 
            // txtPattern
            // 
            this.txtPattern.Enabled = false;
            this.txtPattern.Location = new System.Drawing.Point(94, 38);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(361, 20);
            this.txtPattern.TabIndex = 6;
            this.txtPattern.Text = "You attach.+to the {itemname}";
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(5, 41);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(44, 13);
            this.lblPattern.TabIndex = 5;
            this.lblPattern.Text = "&Pattern:";
            // 
            // chkSkills
            // 
            this.chkSkills.AutoSize = true;
            this.chkSkills.Location = new System.Drawing.Point(405, 14);
            this.chkSkills.Name = "chkSkills";
            this.chkSkills.Size = new System.Drawing.Size(50, 17);
            this.chkSkills.TabIndex = 4;
            this.chkSkills.Text = "&Skills";
            this.chkSkills.UseVisualStyleBackColor = true;
            // 
            // chkCombat
            // 
            this.chkCombat.AutoSize = true;
            this.chkCombat.Location = new System.Drawing.Point(337, 14);
            this.chkCombat.Name = "chkCombat";
            this.chkCombat.Size = new System.Drawing.Size(62, 17);
            this.chkCombat.TabIndex = 3;
            this.chkCombat.Text = "Com&bat";
            this.chkCombat.UseVisualStyleBackColor = true;
            // 
            // chkEvents
            // 
            this.chkEvents.AutoSize = true;
            this.chkEvents.Location = new System.Drawing.Point(272, 14);
            this.chkEvents.Name = "chkEvents";
            this.chkEvents.Size = new System.Drawing.Size(59, 17);
            this.chkEvents.TabIndex = 2;
            this.chkEvents.Text = "&Events";
            this.chkEvents.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(172, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "&Name:";
            // 
            // txtItemname
            // 
            this.txtItemname.Location = new System.Drawing.Point(94, 304);
            this.txtItemname.Name = "txtItemname";
            this.txtItemname.Size = new System.Drawing.Size(361, 20);
            this.txtItemname.TabIndex = 17;
            // 
            // numTotalParts
            // 
            this.numTotalParts.Location = new System.Drawing.Point(94, 330);
            this.numTotalParts.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numTotalParts.Name = "numTotalParts";
            this.numTotalParts.Size = new System.Drawing.Size(120, 20);
            this.numTotalParts.TabIndex = 19;
            // 
            // numPartsAttached
            // 
            this.numPartsAttached.Location = new System.Drawing.Point(94, 356);
            this.numPartsAttached.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numPartsAttached.Name = "numPartsAttached";
            this.numPartsAttached.Size = new System.Drawing.Size(120, 20);
            this.numPartsAttached.TabIndex = 21;
            // 
            // lblItemname
            // 
            this.lblItemname.AutoSize = true;
            this.lblItemname.Location = new System.Drawing.Point(5, 307);
            this.lblItemname.Name = "lblItemname";
            this.lblItemname.Size = new System.Drawing.Size(59, 13);
            this.lblItemname.TabIndex = 16;
            this.lblItemname.Text = "&Item name:";
            // 
            // lblTotalParts
            // 
            this.lblTotalParts.AutoSize = true;
            this.lblTotalParts.Location = new System.Drawing.Point(5, 332);
            this.lblTotalParts.Name = "lblTotalParts";
            this.lblTotalParts.Size = new System.Drawing.Size(60, 13);
            this.lblTotalParts.TabIndex = 18;
            this.lblTotalParts.Text = "Total parts:";
            // 
            // lblPartsAttached
            // 
            this.lblPartsAttached.AutoSize = true;
            this.lblPartsAttached.Location = new System.Drawing.Point(5, 358);
            this.lblPartsAttached.Name = "lblPartsAttached";
            this.lblPartsAttached.Size = new System.Drawing.Size(79, 13);
            this.lblPartsAttached.TabIndex = 20;
            this.lblPartsAttached.Text = "Parts attached:";
            // 
            // frmPartsRemainingGimmick
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 421);
            this.Controls.Add(this.lblPartsAttached);
            this.Controls.Add(this.lblTotalParts);
            this.Controls.Add(this.lblItemname);
            this.Controls.Add(this.numPartsAttached);
            this.Controls.Add(this.numTotalParts);
            this.Controls.Add(this.txtItemname);
            this.Controls.Add(this.cmdBrowseOutputFile);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.lblOutputFile);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
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
            this.Name = "frmPartsRemainingGimmick";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parts remaining";
            ((System.ComponentModel.ISupportInitialize)(this.numTotalParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPartsAttached)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdBrowseOutputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckBox chkCollective;
        private System.Windows.Forms.CheckedListBox listPlayers;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.CheckBox chkSkills;
        private System.Windows.Forms.CheckBox chkCombat;
        private System.Windows.Forms.CheckBox chkEvents;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtItemname;
        private System.Windows.Forms.NumericUpDown numTotalParts;
        private System.Windows.Forms.NumericUpDown numPartsAttached;
        private System.Windows.Forms.Label lblItemname;
        private System.Windows.Forms.Label lblTotalParts;
        private System.Windows.Forms.Label lblPartsAttached;
    }
}