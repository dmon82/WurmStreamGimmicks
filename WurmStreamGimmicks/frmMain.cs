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

            Core.Logger.Logged += Logger_Logged;
            Core.Logger.Log(LogLevel.Config, "Starting main GUI.");

            // Restore window and gimmick list column widths.
            this.Size = Core.Config.MainWindowSize;
            for (int i = 0; i < Core.Config.GimmickColumnSize.Length; i++)
                listGimmicks.Columns[i].Width = Core.Config.GimmickColumnSize[i];

            // If the folder seems valid, set the enable/disable buttons and labels alright.
            if (CheckPlayersFolder()) {
                cmdDisable.Visible =
                    lblEnabled.Visible = false;
            }

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
            
            if (cmdDisable.Visible)
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
            Core.Logger.Log(LogLevel.Info, "Toggling message monitoring.");

            // Toggle buttons and labels.
            lblDisabled.Visible =
                cmdEnable.Visible = !cmdEnable.Visible;

            lblEnabled.Visible =
                cmdDisable.Visible = !cmdDisable.Visible;

            // No changes to path selection while running (may disable).
            txtPlayersFolder.Enabled =
                cmdBrowsePlayersFolder.Enabled = !cmdDisable.Visible;

            if (cmdDisable.Visible) { // Enabled 
                foreach (IGimmick gimmick in Core.Config.Gimmicks)
                    if (gimmick.Enabled) {
                        foreach (Player player in Player.Table.Values)
                            if (!player.Watching && player.Gimmicks.Contains(gimmick)) player.BeginWatch();

                        File.WriteAllText(gimmick.OutputFile, gimmick.Compile());
                    }
            }
            else {
                foreach (Player player in Player.Table.Values)
                    if (player.Watching) player.EndWatch();
            }
        }

        void cmdBrowsePlayersFolder_Click(object sender, EventArgs e) {
            Core.Logger.Log(LogLevel.Info, "Browsing for .\\players\\ folder.");

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
            Core.Logger.Log(LogLevel.Config, "Checking validity of players folder path.");
            Core.Config.PlayersFolder = txtPlayersFolder.Text;

            if (!Directory.Exists(txtPlayersFolder.Text)) {
                txtPlayersFolder.BackColor = Color.PaleVioletRed;

                Core.Logger.Log(LogLevel.Config, "Chosen directory does not exist.");

                return (listGimmicks.Enabled =
                    cmdEnable.Enabled =
                    cmdDisable.Enabled = false);
            }

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

            return (listGimmicks.Enabled =
                cmdEnable.Enabled =
                cmdDisable.Enabled = true);
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
