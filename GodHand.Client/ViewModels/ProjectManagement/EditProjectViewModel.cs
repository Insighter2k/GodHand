using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Screen = Caliburn.Micro.Screen;

namespace GodHand.Client.ViewModels.ProjectManagement
{
    public class EditProjectViewModel : Screen, ITabItem, IProjects
    {
        protected EditProjectViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }

        public static EditProjectViewModel Create(string header, bool isSelected) => new EditProjectViewModel(header, isSelected);

        #region Interface

        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;

        #endregion

        #region Properties
        public static event PropertyChangedEventHandler CmbsProjectsPropertyChanged;
        private List<string> _cmbProjects = new List<string>(Sources.Projects.Select(x => x.Name).ToList());

        public List<string> CmbProjects
        {
            get => _cmbProjects;
            set
            {
                _cmbProjects = value; 
                NotifyOfPropertyChange(() => CmbProjects);
            }
        }

        private string _selectedCmbProjects;
        public string SelectedCmbProjects
        {
            get => _selectedCmbProjects;
            set
            {
                _selectedCmbProjects = value;
                if (value != null)
                {
                    var project = Sources.Projects.Single(x => x.Name == value);
                    Name = project.Name;
                    RootPath = project.RootPath;
                   
                    NotifyOfPropertyChange(() => Name);
                    NotifyOfPropertyChange(() => RootPath);
                }
                NotifyOfPropertyChange(() => SelectedCmbProjects);
                NotifyOfPropertyChange(() => CanBtnSave);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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
            var project = Sources.Projects.Single(x => x.Name == SelectedCmbProjects);
            if (project.Name != Name)
            {
                string path = Environment.CurrentDirectory + @"\projects\";
                Directory.Move(path + project.Name, path + Name);
                project.Name = Name;
            }
            if (project.RootPath != RootPath) project.RootPath = RootPath;
            Shared.IO.Write.Xml(Sources.Projects, Environment.CurrentDirectory + @"\ProjectSettings.xml");

            ResetProperties();
            (ProjectsViewModel.TabItems[2] as IProjects).ResetProperties();
            CmbsProjectsPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("CmbProjects"));
        }

        public void ResetProperties()
        {
            SelectedCmbProjects = null;
            Name = null;
            RootPath = null;
            CmbProjects = new List<string>(Sources.Projects.Select(x => x.Name).ToList());
           
        }

        public bool CanBtnSave => !string.IsNullOrEmpty(SelectedCmbProjects) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(RootPath);

        public void BtnRootPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var dlgResult = folderBrowserDialog.ShowDialog();
            if (dlgResult == DialogResult.OK) RootPath = folderBrowserDialog.SelectedPath;
        }
        #endregion
    }
}
