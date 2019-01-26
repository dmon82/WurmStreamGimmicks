namespace WurmStreamGimmicks {
    partial class frmMain {
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
            this.lblPlayersFolder = new System.Windows.Forms.Label();
            this.txtPlayersFolder = new System.Windows.Forms.TextBox();
            this.cmdBrowsePlayersFolder = new System.Windows.Forms.Button();
            this.listGimmicks = new System.Windows.Forms.ListView();
            this.colGimmickType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGimmickName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGimmickStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGimmickNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblEnabled = new System.Windows.Forms.Label();
            this.lblDisabled = new System.Windows.Forms.Label();
            this.cmdEnable = new System.Windows.Forms.Button();
            this.cmdDisable = new System.Windows.Forms.Button();
            this.txtLogger = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPlayersFolder
            // 
            this.lblPlayersFolder.AutoSize = true;
            this.lblPlayersFolder.Location = new System.Drawing.Point(12, 15);
            this.lblPlayersFolder.Name = "lblPlayersFolder";
            this.lblPlayersFolder.Size = new System.Drawing.Size(116, 13);
            this.lblPlayersFolder.TabIndex = 0;
            this.lblPlayersFolder.Text = "Wurm .\\players\\ folder:";
            // 
            // txtPlayersFolder
            // 
            this.txtPlayersFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlayersFolder.Location = new System.Drawing.Point(134, 12);
            this.txtPlayersFolder.Name = "txtPlayersFolder";
            this.txtPlayersFolder.Size = new System.Drawing.Size(373, 20);
            this.txtPlayersFolder.TabIndex = 1;
            // 
            // cmdBrowsePlayersFolder
            // 
            this.cmdBrowsePlayersFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowsePlayersFolder.Location = new System.Drawing.Point(507, 12);
            this.cmdBrowsePlayersFolder.Name = "cmdBrowsePlayersFolder";
            this.cmdBrowsePlayersFolder.Size = new System.Drawing.Size(41, 20);
            this.cmdBrowsePlayersFolder.TabIndex = 2;
            this.cmdBrowsePlayersFolder.Text = "...";
            this.cmdBrowsePlayersFolder.UseVisualStyleBackColor = true;
            // 
            // listGimmicks
            // 
            this.listGimmicks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listGimmicks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGimmickType,
            this.colGimmickName,
            this.colGimmickStatus,
            this.colGimmickNotes});
            this.listGimmicks.FullRowSelect = true;
            this.listGimmicks.GridLines = true;
            this.listGimmicks.Location = new System.Drawing.Point(12, 38);
            this.listGimmicks.Name = "listGimmicks";
            this.listGimmicks.Size = new System.Drawing.Size(536, 256);
            this.listGimmicks.TabIndex = 3;
            this.listGimmicks.UseCompatibleStateImageBehavior = false;
            this.listGimmicks.View = System.Windows.Forms.View.Details;
            // 
            // colGimmickType
            // 
            this.colGimmickType.Text = "Type";
            this.colGimmickType.Width = 78;
            // 
            // colGimmickName
            // 
            this.colGimmickName.Text = "Name";
            this.colGimmickName.Width = 112;
            // 
            // colGimmickStatus
            // 
            this.colGimmickStatus.Text = "Status";
            this.colGimmickStatus.Width = 42;
            // 
            // colGimmickNotes
            // 
            this.colGimmickNotes.Text = "Notes";
            this.colGimmickNotes.Width = 273;
            // 
            // lblEnabled
            // 
            this.lblEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblEnabled.Location = new System.Drawing.Point(392, 297);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(75, 23);
            this.lblEnabled.TabIndex = 4;
            this.lblEnabled.Text = "Enabled";
            this.lblEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDisabled
            // 
            this.lblDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDisabled.Location = new System.Drawing.Point(473, 297);
            this.lblDisabled.Name = "lblDisabled";
            this.lblDisabled.Size = new System.Drawing.Size(75, 23);
            this.lblDisabled.TabIndex = 5;
            this.lblDisabled.Text = "Disabled";
            this.lblDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdEnable
            // 
            this.cmdEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnable.Location = new System.Drawing.Point(392, 297);
            this.cmdEnable.Name = "cmdEnable";
            this.cmdEnable.Size = new System.Drawing.Size(75, 23);
            this.cmdEnable.TabIndex = 6;
            this.cmdEnable.Text = "&Enable";
            this.cmdEnable.UseVisualStyleBackColor = true;
            // 
            // cmdDisable
            // 
            this.cmdDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDisable.Location = new System.Drawing.Point(473, 297);
            this.cmdDisable.Name = "cmdDisable";
            this.cmdDisable.Size = new System.Drawing.Size(75, 23);
            this.cmdDisable.TabIndex = 7;
            this.cmdDisable.Text = "&Disable";
            this.cmdDisable.UseVisualStyleBackColor = true;
            // 
            // txtLogger
            // 
            this.txtLogger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogger.Location = new System.Drawing.Point(12, 326);
            this.txtLogger.Multiline = true;
            this.txtLogger.Name = "txtLogger";
            this.txtLogger.ReadOnly = true;
            this.txtLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogger.Size = new System.Drawing.Size(536, 164);
            this.txtLogger.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 502);
            this.Controls.Add(this.txtLogger);
            this.Controls.Add(this.cmdDisable);
            this.Controls.Add(this.cmdEnable);
            this.Controls.Add(this.lblDisabled);
            this.Controls.Add(this.lblEnabled);
            this.Controls.Add(this.listGimmicks);
            this.Controls.Add(this.cmdBrowsePlayersFolder);
            this.Controls.Add(this.txtPlayersFolder);
            this.Controls.Add(this.lblPlayersFolder);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlayersFolder;
        private System.Windows.Forms.TextBox txtPlayersFolder;
        private System.Windows.Forms.Button cmdBrowsePlayersFolder;
        private System.Windows.Forms.ListView listGimmicks;
        private System.Windows.Forms.ColumnHeader colGimmickType;
        private System.Windows.Forms.ColumnHeader colGimmickName;
        private System.Windows.Forms.ColumnHeader colGimmickStatus;
        private System.Windows.Forms.ColumnHeader colGimmickNotes;
        private System.Windows.Forms.Label lblEnabled;
        private System.Windows.Forms.Label lblDisabled;
        private System.Windows.Forms.Button cmdEnable;
        private System.Windows.Forms.Button cmdDisable;
        private System.Windows.Forms.TextBox txtLogger;
    }
}