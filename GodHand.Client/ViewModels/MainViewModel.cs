using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Caliburn.Micro;
using GodHand.Shared.IO;
using GodHand.Shared.Models;
using Screen = Caliburn.Micro.Screen;


namespace GodHand.Client.ViewModels
{
    public class MainViewModel : Screen
    {
        private WindowManager _winMan = new WindowManager();
        
        public MainViewModel()
        {
            DisplayName = "GodHand - Japanese Game Translator - powered by Insight2k";
        }

        #region Properties
        private static ObservableCollection<Screen> _tabItems = new ObservableCollection<Screen>();

        public ObservableCollection<Screen> TabItems => _tabItems;

        //private ObservableCollection<Screen> _flyouts = new ObservableCollection<Screen>();
        //public ObservableCollection<Screen> Flyouts
        //{
        //    get => _flyouts;
        //    set
        //    {
        //        _flyouts = value;
        //        NotifyOfPropertyChange(() => Flyouts);
        //    }
        //}

        #endregion

        #region Methods

        public void BtnShowSettings()
        {
            //var flyout = Flyouts[0];
            //((IFlyout) flyout).IsOpen = !((IFlyout) flyout).IsOpen;
               
            _winMan.ShowDialog(SettingsViewModel.Create());
        }
        #endregion

        #region Events

        public void View_Loaded()
        {
            //Flyouts.Add(SettingsViewModel.Create());
            Sources.Settings = Shared.IO.Read.Xml<Shared.Models.Settings>(Environment.CurrentDirectory+@"\Settings.xml");
            TabItems.Add(FileViewModel.Create("File", true));
            TabItems.Add(OcrViewModel.Create("OCR", false));
        }

        #endregion
    }
}
