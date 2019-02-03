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
    public partial class frmCounterGimmick : Form {
        public frmCounterGimmick() {
            InitializeComponent();

            foreach (Player player in Player.Table.Values)
                listPlayers.Items.Add(player.Name, false);

            lblHelp.Text = CounterGimmick.Tooltip;
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

        internal static IGimmick GetGimmick(IGimmick gimmick, IWin32Window parent) {
            frmCounterGimmick frm = new frmCounterGimmick();

            if (gimmick != null && !(gimmick is CounterGimmick))
                throw new ArgumentException("IGimmick is not of correct type CounterGimmick", "gimmick");

            CounterGimmick counter = gimmick as CounterGimmick;

            if (counter != null) {
                frm.txtName.Text = counter.Name;
                frm.chkEvents.Checked = counter.Events;
                frm.chkCombat.Checked = counter.Combat;
                frm.chkSkills.Checked = counter.Skills;
                frm.txtPattern.Text = counter.Pattern;
                frm.txtOutput.Text = counter.Template;
                frm.chkCollective.Checked = counter.Collective;
                frm.txtOutputFile.Text = counter.OutputFile;
                frm.numGlobal.Value = counter.GlobalCount;
                frm.numSession.Value = counter.SessionCount;

                if (!frm.chkCollective.Checked && counter.Players != null) {
                    foreach (string player in counter.Players)
                        frm.listPlayers.SetItemChecked(frm.listPlayers.Items.IndexOf(player), true);
                }
            }
            
            DialogResult result = frm.ShowDialog();

            if (result != DialogResult.OK) {
                frm.Dispose();
                frm = null;

                return gimmick;
            }

            if (gimmick == null) {
                gimmick = new CounterGimmick(
                    frm.txtName.Text,
                    frm.txtPattern.Text,
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
                counter.Name = frm.txtName.Text;
                counter.Pattern = frm.txtPattern.Text;
                counter.Template = frm.txtOutput.Text;
                counter.Collective = frm.chkCollective.Checked;
                counter.Events = frm.chkEvents.Checked;
                counter.Combat = frm.chkCombat.Checked;
                counter.Skills = frm.chkSkills.Checked;
                counter.SessionCount = (int)frm.numSession.Value;
                counter.GlobalCount = (int)frm.numGlobal.Value;
                counter.OutputFile = frm.txtOutputFile.Text;
                counter.Logs =
                    (frm.chkEvents.Checked ? LogType.Events : LogType.None) |
                    (frm.chkCombat.Checked ? LogType.Combat : LogType.None) |
                    (frm.chkSkills.Checked ? LogType.Skills : LogType.None); // Bitmask.

                // Create file even though it's empty, helps with setting up OBS.
                if (!System.IO.File.Exists(counter.OutputFile))
                    System.IO.File.CreateText(counter.OutputFile).Dispose();

                if (counter.Players != null) {
                    counter.Players.Clear();
                    counter.Players = null;
                }

                if (!counter.Collective)
                    counter.Players = frm.listPlayers.CheckedItems.Cast<string>().ToList();
            }

            return gimmick;
        }
    }
}
