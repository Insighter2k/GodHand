using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GodHand.Shared.IO;
using GodHand.Shared.Models;
using Screen = Caliburn.Micro.Screen;

namespace GodHand.Client.ViewModels
{
    public class PatchingViewModel : Screen, ITabItem
    {
        protected PatchingViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }
        public static Screen Create(string header, bool isSelected) => new PatchingViewModel(header, isSelected);

        #region Interface
        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;
        #endregion

        private string _targetPath;
        public string TargetPath
        {
            get => _targetPath;
            set
            {
                _targetPath = value;
                NotifyOfPropertyChange(() => TargetPath);
                NotifyOfPropertyChange(() => CanBtnStart);
            }
        }

        private string _patchPath;
        public string PatchPath
        {
            get => _patchPath;
            set
            {
                _patchPath = value;
                NotifyOfPropertyChange(() => PatchPath);
                NotifyOfPropertyChange(() => CanBtnStart);
            }
        }

        private string _output;
        public string Output
        {
            get => _output;
            set
            {
                _output = _output + DateTime.Now.ToString("HH:mm:ss ") + value + Environment.NewLine;
                NotifyOfPropertyChange(() => Output);
            }
        }

        public async void BtnStart()
        {
            await Task.Factory.StartNew(() =>
            {
                Output = "Starting Process.";
                Output = "Getting necessary directories.";
                var targetDir = Read.DirectoriesOfPath(TargetPath);
                Output = "Getting necessary files.";
                var patchDir = Read.DirectoriesOfPath(PatchPath);

                Output = "Comparing directories and files.";
                var dirList = Check.ForDirectories(targetDir, patchDir);
                var fileList = Check.ForFiles(dirList);

                Output = "Patching files.";
                foreach (var item in fileList)
                {
                    Output = $"Patching {item.Item1}";
                    var patchFile = Read.PatchFile(item.Item2);
                    Write.PatchToFile(item.Item1, patchFile);
                }

                Output = "Process completed.";
            });
        }


        public bool CanBtnStart => !string.IsNullOrEmpty(TargetPath) && !string.IsNullOrEmpty(PatchPath);

        public void BtnTarget()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var dlgResult = folderBrowserDialog.ShowDialog();
            if (dlgResult == DialogResult.OK) TargetPath = folderBrowserDialog.SelectedPath;
        }

        public void BtnPatch()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var dlgResult = folderBrowserDialog.ShowDialog();
            if (dlgResult == DialogResult.OK) PatchPath = folderBrowserDialog.SelectedPath;
        }
    }
}
