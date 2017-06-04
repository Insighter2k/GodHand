using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Screen = Caliburn.Micro.Screen;

namespace GodHand.Client.ViewModels.ProjectManagement
{
    public class DeleteProjectViewModel : Screen, ITabItem, IProjects
    {
        protected DeleteProjectViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }

        public static DeleteProjectViewModel Create(string header, bool isSelected) => new DeleteProjectViewModel(header,isSelected);

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
              
                NotifyOfPropertyChange(() => SelectedCmbProjects);
                NotifyOfPropertyChange(() => CanBtnDelete);
            }
        }

        #endregion

        #region Method

        public void BtnDelete()
        {
            var dlgResult = MessageBox.Show("You are going to delete the whole folder structure and your progress. Are you sure?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dlgResult == DialogResult.Yes)
            {
                var project = Sources.Projects.Single(x => x.Name == SelectedCmbProjects);
                Sources.Projects.Remove(project);

                string path = Environment.CurrentDirectory + @"\projects\";
                Directory.Delete(path + project.Name);

                Shared.IO.Write.Xml(Sources.Projects, Environment.CurrentDirectory + @"\ProjectSettings.xml");

                ResetProperties();
                (ProjectsViewModel.TabItems[1] as IProjects).ResetProperties();
                CmbsProjectsPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("CmbProjects"));
            }
        }

        public bool CanBtnDelete => !string.IsNullOrEmpty(SelectedCmbProjects);

        public void ResetProperties()
        {
            SelectedCmbProjects = null;
            CmbProjects = new List<string>(Sources.Projects.Select(x => x.Name).ToList());
        }
        #endregion
    }
}
