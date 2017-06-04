using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GodHand.Shared.Models;
using Screen = Caliburn.Micro.Screen;
using GodHand.Client.ViewModels.ProjectManagement;

namespace GodHand.Client.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            DisplayName = "GodHand - Japanese Game Translator - powered by Insight2k";

            NewProjectViewModel.CmbsProjectsPropertyChanged += NotifyProperty;
            EditProjectViewModel.CmbsProjectsPropertyChanged += NotifyProperty;
            DeleteProjectViewModel.CmbsProjectsPropertyChanged += NotifyProperty;
        }

        #region Properties
        public ObservableCollection<Screen> TabItems { get; } = new ObservableCollection<Screen>();

        private static ObservableCollection<ProjectSettings> _cmbProjects;
        public ObservableCollection<ProjectSettings> CmbProjects
        {
            get => _cmbProjects;
            set
            {
                _cmbProjects = value;
                NotifyOfPropertyChange(() => CmbProjects);
            }
        }

        private static ProjectSettings _selectedCmbProjects;
        public ProjectSettings SelectedCmbProjects
        {
            get => _selectedCmbProjects;
            set
            {
                _selectedCmbProjects = value;
                NotifyOfPropertyChange(() => SelectedCmbProjects);
            }
        }
        
        #endregion

        #region Methods

        private void ShowTabs(Screen tabItem)
        {            
            var item = TabItems.FirstOrDefault(x => (x as ITabItem).Header == (tabItem as ITabItem).Header);
            if (item == null) TabItems.Add(tabItem);
            else
            {
                item.TryClose();
                TabItems.Remove(item);
            }
        }
        public void BtnShowSettings()
        {
            ShowTabs(SettingsViewModel.Create("Settings", true)); 
        }

        public void BtnProjectMgmt()
        {
            ShowTabs(ProjectsViewModel.Create("Project Management", true));
        }

        public void ResetProperties()
        {
             CmbProjects = Sources.Projects;
        }
        #endregion

        #region Events

        public void View_Loaded()
        {
            Sources.Settings = Shared.IO.Read.Xml<Shared.Models.Settings>(Environment.CurrentDirectory+@"\Settings.xml");
            Sources.Projects = Shared.IO.Read.Xml<ObservableCollection<ProjectSettings>>(Environment.CurrentDirectory + @"\ProjectSettings.xml");
            CmbProjects = Sources.Projects;
            TabItems.Insert(TabItems.Count,FileViewModel.Create("File", true));
            TabItems.Insert(TabItems.Count, OcrViewModel.Create("OCR", false));
            TabItems.Insert(TabItems.Count, PatchingViewModel.Create("Patching", false));
        }

        public void SplitButton_Click()
        {
            if (SelectedCmbProjects == null) return;
            else
            {
                
                var item = TabItems.FirstOrDefault(x => (x as ITabItem).Header == SelectedCmbProjects.Name);
                if (item == null) TabItems.Add(ProjectWorkspaceViewModel.Create(SelectedCmbProjects.Name, true, SelectedCmbProjects));
                else
                {
                    item.TryClose();
                    TabItems.Remove(item);
                }
            }
        }

        private void NotifyProperty(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var property = propertyChangedEventArgs.PropertyName;
            if (property == "CmbProjects") CmbProjects = new ObservableCollection<ProjectSettings>(Sources.Projects);

            NotifyOfPropertyChange(propertyChangedEventArgs.PropertyName);
        }

        #endregion
    }
}
