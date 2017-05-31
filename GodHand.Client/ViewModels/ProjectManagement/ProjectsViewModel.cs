using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace GodHand.Client.ViewModels.ProjectManagement
{
    public class ProjectsViewModel:Screen, ITabItem
    {
        protected ProjectsViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }
        public static ProjectsViewModel Create(string header, bool isSelected) => new ProjectsViewModel(header, isSelected);

        #region Interface

        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;

        #endregion


        #region Properties
        public static ObservableCollection<Screen> TabItems { get; } = new ObservableCollection<Screen>()
        {
           NewProjectViewModel.Create("Add", true),
           EditProjectViewModel.Create("Edit", false),
           DeleteProjectViewModel.Create("Delete", false)
        };

        #endregion

        #region Methods

        public void BtnClose()
        {
            this.TryClose();
        }
        #endregion

        #region Events

        #endregion
    }
}
