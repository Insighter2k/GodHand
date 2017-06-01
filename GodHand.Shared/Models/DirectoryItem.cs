using System.Collections.Generic;

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
