using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GodHand.Shared.IO;
using GodHand.Shared.Models;
using Screen = Caliburn.Micro.Screen;


namespace GodHand.Client.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            DisplayName = "GodHand - Japanese Game Translator - powered by Insight2k";
        }

        #region Properties

        private bool _isOpening = false;

        private bool IsOpening
        {
            get => _isOpening;
            set
            {
                _isOpening = value;
                NotifyOfPropertyChange(() => IsOpening);
                NotifyOfPropertyChange(() => CanBtnOpenFile);
                NotifyOfPropertyChange(() => CanBtnSaveFile);
                NotifyOfPropertyChange(() => CanBtnSelectFile);
            }
        }

        private string _lblSelectedFile;
        public string LblSelectedFile
        {
            get => _lblSelectedFile;
            set
            {
                _lblSelectedFile = value;
                NotifyOfPropertyChange(() => LblSelectedFile);
                NotifyOfPropertyChange(() => CanBtnOpenFile);
            }
        }

        private string _lblFilesize;
        public string LblFilesize
        {
            get => _lblFilesize;
            set
            {
                _lblFilesize = value;
                NotifyOfPropertyChange(() => LblFilesize);
            }
        }

        private string _lblLastChange;
        public string LblLastChange
        {
            get => _lblLastChange;
            set
            {
                _lblLastChange = value;
                NotifyOfPropertyChange(() => LblLastChange);
            }
        }

        private long _tbxStartOffset = -1;
        public long TbxStartOffset
        {
            get => _tbxStartOffset;
            set
            {
                _tbxStartOffset = value;
                NotifyOfPropertyChange(() => TbxStartOffset);
            }
        }

        private long _tbxOffsetLength = -1;
        public long TbxOffsetLength
        {
            get => _tbxOffsetLength;
            set
            {
                _tbxOffsetLength = value;
                NotifyOfPropertyChange(() => TbxOffsetLength);
            }
        }

        private ObservableCollection<ByteInformation> _collection = new ObservableCollection<ByteInformation>();

        public ObservableCollection<Shared.Models.ByteInformation> Collection
        {
            get => _collection;
            set
            {
                _collection = value;
                NotifyOfPropertyChange(() => Collection);
            }
        }

        #endregion

        #region Methods
        
        public void BtnSelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;

            var dlgResult = openFileDialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                LblSelectedFile = fi.FullName;
                LblFilesize = $"{(fi.Length / 1000)} KB";
                LblLastChange = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public bool CanBtnSelectFile => !_isOpening;

        public async void BtnOpenFile()
        {
            IsOpening = true;
            await Task.Factory.StartNew(() =>
            {
                Collection = Read.File(LblSelectedFile, TbxStartOffset, TbxOffsetLength);
            });
            IsOpening = false;
        }

        public bool CanBtnOpenFile => !string.IsNullOrEmpty(LblSelectedFile) && !_isOpening;

        public void BtnSaveFile()
        {
            ObservableCollection<ByteInformation> collection = new ObservableCollection<ByteInformation>(
                Collection.Where(x => x.HasChange));

            Write.ValueToFile(LblSelectedFile, collection);
        }

        public bool CanBtnSaveFile => new ObservableCollection<ByteInformation>(
            Collection.Where(x => x.HasChange)).Any() && !_isOpening;

        #endregion








    }
}
