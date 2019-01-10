using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WurmStreamGimmicks {
    class GimmickListViewSorter : IComparer {
        public bool Ascending { get; set; }
        public int Column { get; set; }

        public GimmickListViewSorter(bool ascending, int column) {
            Ascending = ascending;
            Column = column;
        }

        public GimmickListViewSorter(bool ascending)
            : this(ascending, 0) {
        }

        public GimmickListViewSorter(int column)
            : this(true, column) {
        }

        public GimmickListViewSorter()
            : this(true, 0) {
        }

        public int Compare(object obj0, object obj1) {
            if (obj0 == null && obj1 == null)
                return 0;

            if (obj0 == null) return 1;
            if (obj1 == null) return -1;

            ListViewItem left = obj0 as ListViewItem;
            ListViewItem right = obj1 as ListViewItem;

            if (left == null && right == null) return 0;
            if (left == null) return 1;
            if (right == null) return -1;

            int result = 0;

            switch (Column) {
                default: return 0;
                case 0: result = left.SubItems[0].Text.CompareTo(right.SubItems[0].Text); break;
                case 1: result = left.SubItems[1].Text.CompareTo(right.SubItems[1].Text); break;
                case 2: result = left.SubItems[2].Text.CompareTo(right.SubItems[2].Text); break;
                case 3: result = left.SubItems[3].Text.CompareTo(right.SubItems[3].Text); break;
            }

            return result * (Ascending ? 1 : -1);
        }
    }
}
