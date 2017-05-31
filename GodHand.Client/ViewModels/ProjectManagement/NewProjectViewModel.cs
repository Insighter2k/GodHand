using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GodHand.Shared.Models;
using Screen = Caliburn.Micro.Screen;

namespace GodHand.Client.ViewModels.ProjectManagement
{
    public class NewProjectViewModel : Screen, ITabItem, IProjects
    {
        protected NewProjectViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }

        public static NewProjectViewModel Create(string header, bool isSelected) => new NewProjectViewModel(header, isSelected);

        #region Interface

        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;

        #endregion

        #region Properties
        public static event PropertyChangedEventHandler CmbsProjectsPropertyChanged;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (Sources.Projects.FirstOrDefault(x => x.Name == Name) == null)
                {
                    _name = value;
                }
                else _name = null;

                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanBtnSave);
            }
        }

        private string _rootPath;
        public string RootPath
        {
            get => _rootPath;
            set
            {
                _rootPath = value;
                NotifyOfPropertyChange(() => RootPath);
                NotifyOfPropertyChange(() => CanBtnSave);
            }
        }

        #endregion

        #region Method

        public void BtnSave()
        {
            Sources.Projects.Add(new ProjectSettings() {Name = Name, RootPath = RootPath});
            Shared.IO.Write.Xml(Sources.Projects, Environment.CurrentDirectory + @"\ProjectSettings.xml");
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + $@"\projects\{Name}");

            ResetProperties();
            (ProjectsViewModel.TabItems[1] as IProjects).ResetProperties();
            (ProjectsViewModel.TabItems[2] as IProjects).ResetProperties();
            CmbsProjectsPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("CmbProjects"));
        }

        public bool CanBtnSave => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(RootPath);

        public void ResetProperties()
        {
            Name = null;
            RootPath = null;
        }

        public void BtnRootPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var dlgResult = folderBrowserDialog.ShowDialog();
            if (dlgResult == DialogResult.OK) RootPath = folderBrowserDialog.SelectedPath;
        }
        #endregion
    }
}
