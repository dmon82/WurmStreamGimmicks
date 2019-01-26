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
        private bool _Running = false;

        public frmMain() {
            InitializeComponent();

            Core.Logger.Logged += Logger_Logged;
            Core.Logger.Log(LogLevel.Config, "Starting main GUI.");

            this.Text = String.Format("Wurm stream gimmicks {0}", Core.VersionString);
            this.txtPlayersFolder.Tag =
                this.txtPlayersFolder.Text = Core.Config.PlayersFolder;

            // Restore window and gimmick list column widths.
            this.Size = Core.Config.MainWindowSize;
            for (int i = 0; i < Core.Config.GimmickColumnSize.Length; i++)
                listGimmicks.Columns[i].Width = Core.Config.GimmickColumnSize[i];

            // If the folder seems valid, set the enable/disable buttons and labels alright.
            cmdEnable.Enabled = CheckPlayersFolder();
            cmdDisable.Visible =
                lblEnabled.Visible = false;

            Core.Logger.Log(LogLevel.Config, "Setting up event callbacks.");
            cmdEnable.Click += cmdEnable_Click;
            cmdDisable.Click += cmdDisable_Click;
            cmdBrowsePlayersFolder.Click += cmdBrowsePlayersFolder_Click;
            
            txtPlayersFolder.LostFocus += txtPlayersFolder_LostFocus;

            listGimmicks.ListViewItemSorter = new GimmickListViewSorter();
            listGimmicks.DoubleClick += listGimmicks_DoubleClick;
            listGimmicks.KeyUp += listGimmicks_KeyUp;
            listGimmicks.ColumnClick += listGimmicks_ColumnClick;
            listGimmicks.ColumnWidthChanged += listGimmicks_ColumnWidthChanged;

            this.ResizeEnd += frmMain_ResizeEnd;
            /*
             * Gimmick list context menu
             * ***/
            ContextMenu context = new ContextMenu();
            MenuItem item = context.MenuItems.Add("Add gimmick");

            Type[] types = Core.CoreAssembly.GetTypes();
            Type gimmickType = typeof(IGimmick);

            foreach (Type type in types)
                if (!type.Equals(gimmickType) && gimmickType.IsAssignableFrom(type)) {
                    MenuItem subItem = item.MenuItems.Add(type.Name);
                    subItem.Tag = type;
                    subItem.Click += AddGimmick_Click;
                }

            types = null;

            context.MenuItems.Add("Configure...").Click += ConfigureGimmick_Click;

            listGimmicks.ContextMenu = context;

            /*
             * Import gimmicks from config.
             * ***/
            foreach (IGimmick gimmick in Core.Config.Gimmicks)
                AddGimmickToList(gimmick);
        }

        void listGimmicks_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
            Core.Config.GimmickColumnSize[e.ColumnIndex] =
                listGimmicks.Columns[e.ColumnIndex].Width;
        }

        void frmMain_ResizeEnd(object sender, EventArgs e) {
            Core.Config.MainWindowSize = this.Size;
        }

        void listGimmicks_ColumnClick(object sender, ColumnClickEventArgs e) {
            GimmickListViewSorter sorter = (GimmickListViewSorter)listGimmicks.ListViewItemSorter;
            
            if (sorter.Column == e.Column) sorter.Ascending = !sorter.Ascending;
            else { sorter.Column = e.Column; sorter.Ascending = true; }

            listGimmicks.SuspendLayout();
            listGimmicks.Sort();
            listGimmicks.ResumeLayout();
        }

        void listGimmicks_KeyUp(object sender, KeyEventArgs e) {
            ListViewItem lvi = null;

            if (listGimmicks.SelectedItems.Count > 0)
                lvi = listGimmicks.SelectedItems[0];

            if (e.KeyCode == Keys.Delete) {
                if (lvi == null) return;
                int index = listGimmicks.SelectedItems.Count - 1;

                for (int i = index; i >= 0; i--) {
                    IGimmick gimmick = listGimmicks.Items[listGimmicks.SelectedIndices[i]].Tag as IGimmick;

                    // Remove gimmick from players.
                    foreach (string playername in gimmick.Players)
                        Player.Table[playername].Gimmicks.Remove(gimmick);

                    // Remove gimmick globally from config.
                    Core.Config.Gimmicks.Remove(gimmick);

                    // Remove gimmick from GUI.
                    listGimmicks.Items.RemoveAt(listGimmicks.SelectedIndices[i]);
                }
            }
            else if (e.KeyCode == Keys.Space) {
                // Toggle active.
                listGimmicks_DoubleClick(listGimmicks, new EventArgs());
            }
        }

        void ConfigureGimmick_Click(object sender, EventArgs e) {
            if (listGimmicks.SelectedItems.Count <= 0) return;
            ListViewItem lvi = listGimmicks.SelectedItems[0];
            IGimmick gimmick = lvi.Tag as IGimmick;

            if (gimmick == null) return;
            AddOrEditGimmick(gimmick.GetType(), gimmick);
        }

        void listGimmicks_DoubleClick(object sender, EventArgs e) {
            if (listGimmicks.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listGimmicks.SelectedItems[0];
            if (lvi == null) return;

            IGimmick gimmick = lvi.Tag as IGimmick;
            if (gimmick == null) return;

            //AddOrEditGimmick(gimmick.GetType(), gimmick);
            gimmick.Enabled = !gimmick.Enabled;
            lvi.BackColor = gimmick.Enabled ? Color.PaleGreen : SystemColors.Window;
            lvi.SubItems[2].Text = gimmick.Enabled ? "On" : "Off";
        }

        void AddGimmick_Click(object sender, EventArgs e) {
            Type type = ((MenuItem)sender).Tag as Type;
            AddOrEditGimmick(type, null);
        }

        void AddOrEditGimmick(Type type, IGimmick gimmick) {
            switch (type.Name) {
                case "CounterGimmick":
                    gimmick = frmCounterGimmick.GetGimmick(gimmick, this);
                    break;
                default:
                    throw new NotImplementedException("This gimmick has not been implemented properly?");
            }

            if (gimmick == null)
                return;

            AddGimmickToList(gimmick);
            if (!Core.Config.Gimmicks.Contains(gimmick)) Core.Config.Gimmicks.Add(gimmick);

            foreach (Player player in Player.Table.Values)
                if (gimmick.Players.Contains(player.Name) && !player.Gimmicks.Contains(gimmick))
                    player.Gimmicks.Add(gimmick);
                else if (!gimmick.Players.Contains(player.Name) && player.Gimmicks.Contains(gimmick))
                    player.Gimmicks.Remove(gimmick);
        }

        void AddGimmickToList(IGimmick gimmick) {
            int count = listGimmicks.Items.Count;
            ListViewItem lvi = null;

            for (int i = 0; i < count; i++) {
                if (listGimmicks.Items[i].Tag == gimmick) {
                    lvi = listGimmicks.Items[i];
                    break;
                }
            }

            if (lvi == null) {
                lvi = new ListViewItem(gimmick.GetType().Name);
                lvi.SubItems.Add(gimmick.Name);
                lvi.SubItems.Add(gimmick.Enabled ? "On" : "Off");
                lvi.SubItems.Add(gimmick.Template);
                lvi.Tag = gimmick;

                if (gimmick.Enabled && lvi.BackColor != Color.PaleGreen) lvi.BackColor = Color.PaleGreen;
                else if (!gimmick.Enabled && lvi.BackColor != SystemColors.Window) lvi.BackColor = SystemColors.Window;
                
                listGimmicks.Items.Add(lvi);
            }
            else {
                lvi.SubItems[1].Text = gimmick.Name;
                lvi.SubItems[2].Text = gimmick.Enabled ? "On" : "Off";
                lvi.SubItems[3].Text = gimmick.Template;
                
                if (gimmick.Enabled && lvi.BackColor != Color.PaleGreen) lvi.BackColor = Color.PaleGreen;
                else if (!gimmick.Enabled && lvi.BackColor != SystemColors.Window) lvi.BackColor = SystemColors.Window;
            }

            listGimmicks.SuspendLayout();
            listGimmicks.Sort();
            listGimmicks.ResumeLayout();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            Core.Logger.Logged -= Logger_Logged;
            
            if (_Running)
                ToggleEnabled();
                        
            base.OnFormClosing(e);
        }

        void cmdEnable_Click(object sender, EventArgs e) {
            ToggleEnabled();
        }

        void cmdDisable_Click(object sender, EventArgs e) {
            ToggleEnabled();
        }

        void ToggleEnabled() {
            _Running = !_Running;

            Core.Logger.Log(LogLevel.Info, "Toggling message monitoring to {0}.", _Running ? "ON" : "OFF");

            Core.Logger.Log(LogLevel.Finest, "Swapping Enable/Disable buttons.");

            cmdEnable.Visible =
                lblDisabled.Visible = !lblDisabled.Visible;

            cmdDisable.Visible
                = lblEnabled.Visible = !lblEnabled.Visible;

            Core.Logger.Log(LogLevel.Finest, "Switching 'player folders' text box and 'browse' button to {0}.", !_Running ? "enabled" : "disabled");

            // No changes to path selection while running (may disable).
            txtPlayersFolder.Enabled =
                cmdBrowsePlayersFolder.Enabled = !_Running;

            if (_Running) {
                Core.Logger.Log(LogLevel.Fine, "Initialising gimmicks (dry run).");

                foreach (IGimmick gimmick in Core.Config.Gimmicks)
                    if (gimmick.Enabled) {
                        Core.Logger.Log(LogLevel.Config, "Initialising enabled gimmick '{0}'.", gimmick.Name);

                        foreach (Player player in Player.Table.Values)
                            if (!player.Watching && player.Gimmicks.Contains(gimmick)) player.BeginWatch();

                        string output = gimmick.Compile();

                        Core.Logger.Log(LogLevel.Config, "'{0}' output: '{1}'.", gimmick.Name, output);
                        File.WriteAllText(gimmick.OutputFile, output);
                        output = null;
                    }
            }
            else {
                Core.Logger.Log(LogLevel.Config, "Ending watch on all players.");

                foreach (Player player in Player.Table.Values)
                    if (player.Watching) player.EndWatch();
            }
        }

        void cmdBrowsePlayersFolder_Click(object sender, EventArgs e) {
            Core.Logger.Log(LogLevel.Info, "Browsing for .\\players\\ folder.");

            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Pick Wurm's \"Players\" folder to parse logfiles from";
            folder.ShowNewFolderButton = false;
            folder.SelectedPath = txtPlayersFolder.Text;

            DialogResult result = folder.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK) {
                Core.Logger.Log(LogLevel.Config, "Browsing aborted.");
                return;
            }

            if (folder.SelectedPath.Equals(txtPlayersFolder.Tag)) {
                Core.Logger.Log(LogLevel.Config, "New new directory selected.");

                return;
            }

            txtPlayersFolder.Tag 
                = Core.Config.PlayersFolder
                = txtPlayersFolder.Text 
                = folder.SelectedPath;

            Core.Logger.Log(LogLevel.Config, "Using '{0}' as players folder.", txtPlayersFolder.Text);

            folder.Dispose();
            folder = null;

            lblEnabled.Enabled = CheckPlayersFolder();

            if (!lblEnabled.Enabled) Core.Logger.Log(LogLevel.Warning, "Folder not valid.");
        }

        void txtPlayersFolder_LostFocus(object sender, EventArgs e) {
            txtPlayersFolder.Text = txtPlayersFolder.Text.Trim();

            // Only check is the path textbox is enabled, which means the program is not running.
            if (txtPlayersFolder.Enabled && (txtPlayersFolder.Tag == null || !txtPlayersFolder.Text.Equals(txtPlayersFolder.Tag))) {
                cmdEnable.Enabled = CheckPlayersFolder();

                // Update text box and config.
                txtPlayersFolder.Tag
                    = Core.Config.PlayersFolder
                    = txtPlayersFolder.Text;
            }
        }

        bool CheckPlayersFolder() {
            Core.Logger.Log(LogLevel.Config, "Checking validity of players folder path.");
            Core.Config.PlayersFolder = txtPlayersFolder.Text;

            Core.Config.PlayersFolder = txtPlayersFolder.Text;

            if (!Directory.Exists(txtPlayersFolder.Text)) {
                txtPlayersFolder.BackColor = Color.PaleVioletRed;

                Core.Logger.Log(LogLevel.Config, "Chosen directory does not exist.");

                return false;
            }

            if (Directory.GetDirectories(txtPlayersFolder.Text).Length <= 0)
                Core.Logger.Log(LogLevel.Warning, "Chosen folder has no sub-folders (should be the player names).");

            txtPlayersFolder.BackColor = SystemColors.Window;
            Core.Logger.Log(LogLevel.Config, "Chosen directory exists.");

            Core.Logger.Log(LogLevel.Config, "Resetting players list.");
            Player.Table.Clear();

            string[] playernames = Directory.GetDirectories(txtPlayersFolder.Text);
            foreach (string playername in playernames) {
                string substr = playername.Substring(playername.LastIndexOf('\\') + 1);
                Player.Table.Add(substr, new Player(substr));
            }

            foreach (IGimmick gimmick in Core.Config.Gimmicks) {
                foreach (Player player in Player.Table.Values) {
                    if (gimmick.Collective || gimmick.Players.Contains(player.Name))
                        player.Gimmicks.Add(gimmick);
                }
            }

            return true;
        }

        void Logger_Logged(string message) {
            if (!this.IsDisposed) {
                if (txtLogger.InvokeRequired) txtLogger.Invoke(new MethodInvoker(() => { InvokeLoggerLogged(message); }));
                else txtLogger.AppendText(message + Environment.NewLine);
            }
        }

        void InvokeLoggerLogged(string message) {
            txtLogger.AppendText(message + Environment.NewLine);
        }
    }
}
