using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodHand.Shared.Models
{
    public class DirectoryItem : TreeViewItem
    {
        public List<TreeViewItem> Items { get; set; }

        public DirectoryItem()
        {
            Items = new List<TreeViewItem>();
        }
    }
}
