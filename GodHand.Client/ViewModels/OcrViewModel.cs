using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;
using Screen = Caliburn.Micro.Screen;

namespace GodHand.Client.ViewModels
{
    public class OcrViewModel : Screen, ITabItem
    {
       
        protected OcrViewModel(string header, bool isSelected)
        {
            Header = header;
            IsSelected = isSelected;
        }
        public static Screen Create(string header, bool isSelected) => new OcrViewModel(header, isSelected);

        #region Interface
        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public Screen Content => this;
        #endregion

        #region Propterties

        private BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        private string _tbxInput;

        public string TbxInput
        {
            get => _tbxInput;
            set
            {
                _tbxInput = value;
                NotifyOfPropertyChange(() => TbxInput);
            }
        }


        private string _tbxOutput;

        public string TbxOutput
        {
            get => _tbxOutput;
            set
            {
                _tbxOutput = value; 
                NotifyOfPropertyChange(() => TbxOutput);
            }
        }

        private Canvas _canvas { get; set; }
        private Rectangle _rectangle { get; set; }
        private Point _startPoint { get; set; }
      
        private double _x1 { get; set; }
        private double _x2 { get; set; }
        private double _y1 { get; set; }
        private double _y2 { get; set; }
        
        #endregion

        #region Methods

        public void BtnLoadImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            var dlgResult = openFileDialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                Image = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        #endregion

        #region Events

        public void View_Loaded(MetroContentControl ctrl)
        {
            _canvas = ctrl.FindName("Canvas") as Canvas;
        }

        public void Canvas_MouseDown(MouseButtonEventArgs e)
        {
            _canvas.Children.Clear();
            _startPoint = e.GetPosition(_canvas);

            _rectangle = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };
            Canvas.SetLeft(_rectangle, _startPoint.X);
            Canvas.SetTop(_rectangle, _startPoint.X);
            _canvas.Children.Add(_rectangle);
        }

        public void Canvas_MouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || _rectangle == null)
                return;

            var pos = e.GetPosition(_canvas);

            var x = Math.Min(pos.X, _startPoint.X);
            var y = Math.Min(pos.Y, _startPoint.Y);

            var w = Math.Max(pos.X, _startPoint.X) - x;
            var h = Math.Max(pos.Y, _startPoint.Y) - y;

            _rectangle.Width = w;
            _rectangle.Height = h;

            Canvas.SetLeft(_rectangle, x);
            Canvas.SetTop(_rectangle, y);

            _x1 = x;
            _y1 = y;
            _x2 = w;
            _y2 = h;
        }

        public async void Canvas_MouseUp(MouseButtonEventArgs e)
        {
            if (_rectangle != null && _rectangle.Height > 0 && _rectangle.Width > 0)
            {
                _rectangle.StrokeThickness = 0;
                Shared.IO.Write.CroppedPicToJpeg(_canvas, (int) _x1, (int) _y1, (int) _x2, (int) _y2);

                await Task.Factory.StartNew(() =>
                {
                    TbxInput = Shared.IO.Read.Picture();
                    TbxOutput = Shared.IO.Convert.ToEnglish(TbxInput);
                });

            }
            _rectangle = null;
        }

        #endregion

    }
}