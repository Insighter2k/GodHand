using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using GodHand.Shared.Models;

namespace GodHand.Client.ViewModels
{
    public class ProjectWorkspaceViewModel : Screen, ITabItem
    {
        protected ProjectWorkspaceViewModel(string header, bool isSelected, ProjectSettings settings)
        {
            Header = header;
            IsSelected = isSelected;
            _settings = settings;
        }

        public static ProjectWorkspaceViewModel Create(string header, bool isSelected, ProjectSettings settings) => new ProjectWorkspaceViewModel(header, isSelected, settings);

        #region Interface
        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;
        #endregion

        #region Properties

        private ProjectSettings _settings;

        private List<TreeViewItem> _treeViewItems;

        public List<TreeViewItem> TreeViewItems
        {
            get => _treeViewItems;
            set
            {
                _treeViewItems = value;
                NotifyOfPropertyChange(() => TreeViewItems);
            }
        }

        private List<TreeViewItem> _files;

        public List<TreeViewItem> Files
        {
            get => _files;
            set
            {
                _files = value;
                NotifyOfPropertyChange(() => Files);
            }
        }

        #endregion

        #region Methods
        public void View_Loaded()
        {
            TreeViewItems = Shared.IO.Read.DirectoriesOfPath(_settings.RootPath);
        }
        #endregion

        #region Events

        public void TreeView_SelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as TreeViewItem;
            Files = Shared.IO.Read.FilesOfDirectory(item?.Fullpath);

        }
        #endregion
    }
}
