using System;
using Caliburn.Micro;

namespace GodHand.Client.ViewModels
{
    public class SettingsViewModel : Screen, ITabItem
    {
        protected SettingsViewModel(string header, bool isSelected)
        { 
            Header = header;
            IsSelected = isSelected;
        }

        public static Screen Create(string header, bool isSelected) => new SettingsViewModel(header, isSelected);

        #region Interface

        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;

        #endregion

        #region Flyout
        //private string _header;
        //public string Header
        //{
        //    get { get; set; } = _header;
        //    set
        //    {
        //        if (value == _header)
        //        {
        //            return;
        //        }

        //        _header = value;
        //        NotifyOfPropertyChange(() { get; set; } = Header);
        //    }
        //}

        //private bool _isOpen;
        //public bool IsOpen
        //{
        //    get { get; set; } = _isOpen;
        //    set
        //    {
        //        if (value.Equals(_isOpen))
        //        {
        //            return;
        //        }

        //        _isOpen = value;
        //        NotifyOfPropertyChange(() { get; set; } = IsOpen);
        //    }
        //}

        //private Position _position;
        //public Position Position
        //{
        //    get { get; set; } = _position;
        //    set
        //    {
        //        if (value == _position)
        //        {
        //            return;
        //        }

        //        _position = value;
        //        NotifyOfPropertyChange(() { get; set; } = Position);
        //    }
        //}

        #endregion

        public bool EnableRomajiTranslation
        {
            get => Sources.Settings.EnableRomajiTranslation;
            set => Sources.Settings.EnableRomajiTranslation = value;
        }

        public bool EnableGoogleTranslation
        {
            get => Sources.Settings.EnableGoogleTranslation;
            set => Sources.Settings.EnableGoogleTranslation = value;
        }

        public bool EnableKanjiToAscii
        {
            get => Sources.Settings.EnableKanjiToAscii;
            set => Sources.Settings.EnableKanjiToAscii = value;
        }

        public bool EnableHiraganaToAscii
        {
            get => Sources.Settings.EnableHiraganaToAscii;
            set => Sources.Settings.EnableHiraganaToAscii = value;
        }

        public bool EnableKatakanaToAscii
        {
            get => Sources.Settings.EnableKatakanaToAscii;
            set => Sources.Settings.EnableKatakanaToAscii = value;
        }

        public bool EnableKigouToAscii
        {
            get => Sources.Settings.EnableKigouToAscii;
            set => Sources.Settings.EnableKigouToAscii = value;
        }

        public bool EnableJisRomanToAscii
        {
            get => Sources.Settings.EnableJisRomanToAscii;
            set => Sources.Settings.EnableJisRomanToAscii = value;
        }

        public bool EnableKanaToAscii
        {
            get => Sources.Settings.EnableKanaToAscii;
            set => Sources.Settings.EnableKanaToAscii = value;
        }

        public bool EnableGraphicToAscii
        {
            get => Sources.Settings.EnableGraphicToAscii;
            set => Sources.Settings.EnableGraphicToAscii = value;
        }

        public bool InsertSeparateCharacters
        {
            get => Sources.Settings.InsertSeparateCharacters;
            set => Sources.Settings.InsertSeparateCharacters = value;
        }

        public bool CapitalizeRomaji
        {
            get => Sources.Settings.CapitalizeRomaji;
            set => Sources.Settings.CapitalizeRomaji = value;
        }

        public bool UpscaleRomaji
        {
            get => Sources.Settings.UpscaleRomaji;
            set => Sources.Settings.UpscaleRomaji = value;
        }

        public bool EnableWakitagaki
        {
            get => Sources.Settings.EnableWakitagaki;
            set => Sources.Settings.EnableWakitagaki = value;
        }

        public bool EnableHepburn
        {
            get => Sources.Settings.EnableHepburn;
            set => Sources.Settings.EnableHepburn = value;
        }

        public override void TryClose(bool? dialogResult = null)
        {
            Shared.IO.Write.Xml(Sources.Settings, Environment.CurrentDirectory + @"\Settings.xml");

            base.TryClose(dialogResult);
        }
    }
}
