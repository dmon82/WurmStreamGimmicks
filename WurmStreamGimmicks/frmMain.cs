using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WurmStreamGimmicks {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();

            // If the folder seems valid, set the enable/disable buttons and labels alright.
            if (CheckPlayersFolder()) {
                cmdDisable.Visible =
                    lblEnabled.Visible = false;
            }

            cmdEnable.Click += cmdEnable_Click;
            cmdDisable.Click += cmdDisable_Click;
            cmdBrowsePlayersFolder.Click += cmdBrowsePlayersFolder_Click;

            txtPlayersFolder.LostFocus += txtPlayersFolder_LostFocus;
        }

        void cmdEnable_Click(object sender, EventArgs e) {
            ToggleEnabled();
        }

        void cmdDisable_Click(object sender, EventArgs e) {
            ToggleEnabled();
        }

        void ToggleEnabled() {
            // Toggle buttons and labels.
            lblDisabled.Visible =
                cmdEnable.Visible = !cmdEnable.Visible;

            lblEnabled.Visible =
                cmdDisable.Visible = !cmdDisable.Visible;

            // No changes to path selection while running (may disable).
            txtPlayersFolder.Enabled =
                cmdBrowsePlayersFolder.Enabled = cmdDisable.Visible;
        }

        void cmdBrowsePlayersFolder_Click(object sender, EventArgs e) {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Pick Wurm's \"Players\" folder to parse logfiles from";
            folder.ShowNewFolderButton = false;

            DialogResult result = folder.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            txtPlayersFolder.Text = folder.SelectedPath;

            folder.Dispose();
            folder = null;
            CheckPlayersFolder();
        }

        void txtPlayersFolder_LostFocus(object sender, EventArgs e) {
            // Only check is the path textbox is enabled, which means the program is not running.
            if (txtPlayersFolder.Enabled)
                CheckPlayersFolder();
        }

        bool CheckPlayersFolder() {
            if (!Directory.Exists(txtPlayersFolder.Text)) {
                txtPlayersFolder.BackColor = Color.PaleVioletRed;

                return (listGimmicks.Enabled =
                    cmdEnable.Enabled =
                    cmdDisable.Enabled = false);
            }

            txtPlayersFolder.BackColor = SystemColors.Window;

            return (listGimmicks.Enabled =
                cmdEnable.Enabled =
                cmdDisable.Enabled = true);
        }
    }
}
