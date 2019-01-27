using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WurmStreamGimmicks {
    public partial class frmPartsRemainingGimmick : Form {
        public frmPartsRemainingGimmick() {
            InitializeComponent();

            lblHelp.Text = PartsRemainingGimmick.Tooltip;

            foreach (Player player in Player.Table.Values)
                listPlayers.Items.Add(player.Name, false);

            chkCollective.CheckedChanged += chkCollective_CheckedChanged;
            cmdBrowseOutputFile.Click += cmdBrowseOutputFile_Click;
            cmdOK.Click += cmdOK_Click;
        }

        void chkCollective_CheckedChanged(object sender, EventArgs e) {
            listPlayers.Enabled = !chkCollective.Checked;
        }

        void cmdBrowseOutputFile_Click(object sender, EventArgs e) {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Select an output file (may override another output file)";
            save.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            save.FilterIndex = 1;
            save.CheckFileExists = false;
            save.CheckPathExists = true;
            save.AddExtension = true;
            save.CreatePrompt = false;
            save.InitialDirectory = System.IO.Path.GetDirectoryName(txtOutputFile.Text);
            save.ValidateNames = true;

            DialogResult result = save.ShowDialog(this);

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            txtOutputFile.Text = save.FileName;
            save.Dispose();
            save = null;
        }

        void cmdOK_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtName.Text)) {
                MessageBox.Show(this, "Must specify a name", "Specify a name");
                txtName.BackColor = Color.PaleVioletRed;
                return;
            }
            else txtName.BackColor = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(txtPattern.Text)) {
                MessageBox.Show(this, "Must specify a pattern to look for", "Specify pattern");
                txtPattern.BackColor = Color.PaleVioletRed;
                return;
            }
            else txtPattern.BackColor = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(txtOutput.Text)) {
                MessageBox.Show(this, "Must specify an output pattern.", "Specify output");
                txtOutput.BackColor = Color.PaleVioletRed;
                return;
            }
            else txtOutput.BackColor = SystemColors.Window;

            if (!chkEvents.Checked && !chkCombat.Checked && !chkSkills.Checked) {
                MessageBox.Show(this, "Must monitor at least one, events, skills, or combat.", "Specify logs to monitor");
                chkEvents.BackColor = chkCombat.BackColor = chkSkills.BackColor = Color.PaleVioletRed;
                return;
            }
            else chkEvents.BackColor = chkCombat.BackColor = chkSkills.BackColor = SystemColors.Window;

            if (!chkCollective.Checked && listPlayers.CheckedItems.Count <= 0) {
                MessageBox.Show(this, "Must select at least one player to monitor, or check to monitor ALL palyers.", "Specify players");
                listPlayers.BackColor = chkCollective.BackColor = Color.PaleVioletRed;
                return;
            }
            else listPlayers.BackColor = chkCollective.BackColor = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(txtOutputFile.Text)) {
                MessageBox.Show(this, "Must select an output text file. This file may be a duplicate with other text file, which means only the last tracked event will be showing up in that text file.", "Specify output text file");
                txtOutputFile.BackColor = Color.PaleVioletRed;
                return;
            }
            else txtOutputFile.BackColor = SystemColors.Window;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        internal static IGimmick GetGimmick(IGimmick gimmick, IWin32Window owner) {
            frmPartsRemainingGimmick frm = new frmPartsRemainingGimmick();
            
            if (gimmick != null && !(gimmick is PartsRemainingGimmick))
                throw new ArgumentException("IGimmick is not of correct type PartsRemainingGimmick.", "gimmick");

            PartsRemainingGimmick parts = gimmick as PartsRemainingGimmick;

            if (parts != null) {
                frm.txtName.Text = parts.Name;
                if (!string.Empty.Equals(parts.Pattern)) frm.txtPattern.Text = parts.Pattern;
                frm.txtOutput.Text = parts.Template;
                frm.lblHelp.Text = parts.Help;
                frm.chkEvents.Checked = parts.Events;
                frm.chkCombat.Checked = parts.Combat;
                frm.chkSkills.Checked = parts.Skills;
                frm.txtItemname.Text = parts.Itemname;
                frm.numTotalParts.Value = parts.TotalParts;
                frm.numPartsAttached.Value = parts.PartsAttached;
                frm.txtOutputFile.Text = parts.OutputFile;

                if (!(frm.chkCollective.Checked = parts.Collective))
                    foreach (string player in parts.Players)
                        frm.listPlayers.SetItemChecked(frm.listPlayers.Items.IndexOf(player), true);
            }

            DialogResult result = frm.ShowDialog(owner);

            if (result != DialogResult.OK) {
                frm.Dispose();
                frm = null;

                return gimmick;
            }

            if (gimmick == null) {
                gimmick = new PartsRemainingGimmick(
                    frm.txtName.Text,
                    string.Empty,
                    frm.txtOutput.Text,
                    frm.chkCollective.Checked,
                    frm.chkEvents.Checked,
                    frm.chkCombat.Checked,
                    frm.chkSkills.Checked,
                    frm.chkCollective.Checked ? null : frm.listPlayers.CheckedItems.Cast<string>().ToList());
                gimmick.OutputFile = frm.txtOutputFile.Text;
                gimmick.Logs =
                    (frm.chkEvents.Checked ? LogType.Events : LogType.None) |
                    (frm.chkCombat.Checked ? LogType.Combat : LogType.None) |
                    (frm.chkSkills.Checked ? LogType.Skills : LogType.None); // Bitmask.
            }
            else {
                parts.Name = frm.txtName.Text;
                parts.Template = frm.txtOutput.Text;
                parts.Collective = frm.chkCollective.Checked;
                parts.Events = frm.chkEvents.Checked;
                parts.Combat = frm.chkCombat.Checked;
                parts.Skills = frm.chkSkills.Checked;
                parts.OutputFile = frm.txtOutputFile.Text;
                parts.Logs =
                    (frm.chkEvents.Checked ? LogType.Events : LogType.None) |
                    (frm.chkCombat.Checked ? LogType.Combat : LogType.None) |
                    (frm.chkSkills.Checked ? LogType.Skills : LogType.None); // Bitmask.
                parts.TotalParts = (int)frm.numTotalParts.Value;
                parts.PartsAttached = (int)frm.numPartsAttached.Value;
                parts.Itemname = frm.txtItemname.Text;

                try {
                    if (!System.IO.File.Exists(gimmick.OutputFile))
                        System.IO.File.CreateText(gimmick.OutputFile).Dispose();
                }
                catch (Exception e) {
                    Core.Logger.Log(LogLevel.Severe, "Can't create output file '{0}' for {1}.", gimmick.OutputFile, gimmick.Name);
                    Core.Logger.Log(LogLevel.Severe, e.ToString());
                }

                if (parts.Players != null) {
                    parts.Players.Clear();
                    parts.Players = null;
                }
                
                if (!parts.Collective)
                    parts.Players = frm.listPlayers.CheckedItems.Cast<string>().ToList();
            }

            frm.Dispose();
            frm = null;

            return gimmick;
        }
    }
}
