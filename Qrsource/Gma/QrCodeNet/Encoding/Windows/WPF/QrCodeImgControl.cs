namespace Gma.QrCodeNet.Encoding.Windows.WPF
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Windows.Render;
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class QrCodeImgControl : Control
    {
        public static readonly DependencyProperty DarkColorProperty = DependencyProperty.Register("DarkColor", typeof(Color), typeof(QrCodeImgControl), new UIPropertyMetadata(Colors.Black, new PropertyChangedCallback(QrCodeImgControl.OnVisualValueChanged)));
        public static readonly DependencyProperty ErrorCorrectLevelProperty = DependencyProperty.Register("ErrorCorrectLevel", typeof(ErrorCorrectionLevel), typeof(QrCodeImgControl), new UIPropertyMetadata(ErrorCorrectionLevel.M, new PropertyChangedCallback(QrCodeImgControl.OnQrValueChanged)));
        public static readonly DependencyProperty IsGrayImageProperty = DependencyProperty.Register("IsGrayImage", typeof(bool), typeof(QrCodeImgControl), new UIPropertyMetadata(true, new PropertyChangedCallback(QrCodeImgControl.OnVisualValueChanged)));
        public static readonly DependencyProperty LightColorProperty = DependencyProperty.Register("LightColor", typeof(Color), typeof(QrCodeImgControl), new UIPropertyMetadata(Colors.White, new PropertyChangedCallback(QrCodeImgControl.OnVisualValueChanged)));
        private int m_DpiX = 0x60;
        private int m_DpiY = 0x60;
        private bool m_isFreezed;
        private bool m_isLocked;
        private QrCode m_QrCode = new QrCode();
        public static readonly DependencyProperty QrCodeWidthInchProperty = DependencyProperty.Register("QrCodeWidth", typeof(double), typeof(QrCodeImgControl), new UIPropertyMetadata(2.08, new PropertyChangedCallback(QrCodeImgControl.OnVisualValueChanged)));
        public static readonly DependencyProperty QuietZoneModuleProperty = DependencyProperty.Register("QuietZoneModule", typeof(QuietZoneModules), typeof(QrCodeImgControl), new UIPropertyMetadata(QuietZoneModules.Two, new PropertyChangedCallback(QrCodeImgControl.OnVisualValueChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(QrCodeImgControl), new UIPropertyMetadata(string.Empty, new PropertyChangedCallback(QrCodeImgControl.OnQrValueChanged)));
        public static readonly DependencyProperty WBitmapProperty = DependencyProperty.Register("WBitmap", typeof(WriteableBitmap), typeof(QrCodeImgControl), new UIPropertyMetadata(null, null));

        public event EventHandler QrMatrixChanged;

        static QrCodeImgControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(QrCodeImgControl), new FrameworkPropertyMetadata(typeof(QrCodeImgControl)));
            FrameworkElement.HorizontalAlignmentProperty.OverrideMetadata(typeof(QrCodeImgControl), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
            FrameworkElement.VerticalAlignmentProperty.OverrideMetadata(typeof(QrCodeImgControl), new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        public QrCodeImgControl()
        {
            MatrixPoint dPI = this.GetDPI();
            this.m_DpiX = dPI.X;
            this.m_DpiY = dPI.Y;
            this.EncodeAndUpdateBitmap();
        }

        private int CalculateSuitableWidth(int width, int bitMatrixWidth)
        {
            DrawingSize size = new FixedCodeSize(width, this.QuietZoneModule).GetSize(bitMatrixWidth);
            int num = size.CodeWidth - (size.ModuleSize * (bitMatrixWidth + (QuietZoneModules.Two * this.QuietZoneModule)));
            if (num == 0)
            {
                return width;
            }
            if ((size.CodeWidth / num) < 4)
            {
                return ((size.ModuleSize + 1) * (bitMatrixWidth + (QuietZoneModules.Two * this.QuietZoneModule)));
            }
            return (size.ModuleSize * (bitMatrixWidth + (QuietZoneModules.Two * this.QuietZoneModule)));
        }

        private void CreateBitmap()
        {
            int width = ((int) this.QrCodeWidthInch) * this.m_DpiX;
            int pixelWidth = (this.m_QrCode.Matrix == null) ? this.CalculateSuitableWidth(width, 0x15) : this.CalculateSuitableWidth(width, this.m_QrCode.Matrix.Width);
            PixelFormat pixelFormat = this.IsGrayImage ? PixelFormats.Gray8 : PixelFormats.Pbgra32;
            if (this.WBitmap == null)
            {
                this.WBitmap = new WriteableBitmap(pixelWidth, pixelWidth, (double) this.m_DpiX, (double) this.m_DpiY, pixelFormat, null);
            }
            else if (((this.WBitmap.PixelHeight != pixelWidth) || (this.WBitmap.PixelWidth != pixelWidth)) || (this.WBitmap.Format != pixelFormat))
            {
                this.WBitmap = null;
                this.WBitmap = new WriteableBitmap(pixelWidth, pixelWidth, (double) this.m_DpiX, (double) this.m_DpiY, pixelFormat, null);
            }
        }

        internal void EncodeAndUpdateBitmap()
        {
            if (!this.IsLocked)
            {
                this.UpdateQrCodeCache();
                this.UpdateBitmap();
            }
        }

        public void Freeze()
        {
            this.m_isFreezed = true;
        }

        public MatrixPoint GetDPI()
        {
            PresentationSource source = PresentationSource.FromVisual(this);
            if (source != null)
            {
                Matrix transformToDevice = source.CompositionTarget.TransformToDevice;
                int x = (int) (96.0 * transformToDevice.M11);
                return new MatrixPoint(x, (int) (96.0 * transformToDevice.M22));
            }
            return new MatrixPoint(this.m_DpiX, this.m_DpiY);
        }

        public SquareBitMatrix GetQrMatrix()
        {
            if (this.m_QrCode.Matrix == null)
            {
                return null;
            }
            return new SquareBitMatrix(this.m_QrCode.Matrix.InternalArray);
        }

        public void Lock()
        {
            this.m_isLocked = true;
        }

        protected virtual void OnQrMatrixChanged(EventArgs e)
        {
            if (this.QrMatrixChanged != null)
            {
                this.QrMatrixChanged(this, e);
            }
        }

        private static void OnQrValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QrCodeImgControl) d).EncodeAndUpdateBitmap();
        }

        private static void OnVisualValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QrCodeImgControl) d).UpdateBitmap();
        }

        public void UnFreeze()
        {
            this.m_isFreezed = false;
            this.UpdateBitmap();
        }

        public void Unlock()
        {
            this.m_isLocked = false;
            this.EncodeAndUpdateBitmap();
        }

        internal void UpdateBitmap()
        {
            if (!this.IsFreezed)
            {
                this.UpdateSource();
            }
        }

        private void UpdateQrCodeCache()
        {
            new QrEncoder(this.ErrorCorrectLevel).TryEncode(this.Text, out this.m_QrCode);
            this.OnQrMatrixChanged(new EventArgs());
        }

        private void UpdateSource()
        {
            this.CreateBitmap();
            if ((this.WBitmap.PixelWidth != 0) && (this.WBitmap.PixelHeight != 0))
            {
                this.WBitmap.Clear(this.LightColor);
                if (this.m_QrCode.Matrix != null)
                {
                    new WriteableBitmapRenderer(new FixedCodeSize(this.WBitmap.PixelWidth, this.QuietZoneModule), this.DarkColor, this.LightColor).DrawDarkModule(this.WBitmap, this.m_QrCode.Matrix, 0, 0);
                }
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("QrCode")]
        public Color DarkColor
        {
            get
            {
                return (Color) base.GetValue(DarkColorProperty);
            }
            set
            {
                base.SetValue(DarkColorProperty, value);
            }
        }

        [Browsable(true), Category("QrCode"), EditorBrowsable(EditorBrowsableState.Always)]
        public ErrorCorrectionLevel ErrorCorrectLevel
        {
            get
            {
                return (ErrorCorrectionLevel) base.GetValue(ErrorCorrectLevelProperty);
            }
            set
            {
                base.SetValue(ErrorCorrectLevelProperty, value);
            }
        }

        public bool IsFreezed
        {
            get
            {
                return this.m_isFreezed;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Category("QrCode"), Browsable(true)]
        public bool IsGrayImage
        {
            get
            {
                return (bool) base.GetValue(IsGrayImageProperty);
            }
            set
            {
                base.SetValue(IsGrayImageProperty, value);
            }
        }

        public bool IsLocked
        {
            get
            {
                return this.m_isLocked;
            }
        }

        [Category("QrCode"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color LightColor
        {
            get
            {
                return (Color) base.GetValue(LightColorProperty);
            }
            set
            {
                base.SetValue(LightColorProperty, value);
            }
        }

        [Category("QrCode"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public double QrCodeWidthInch
        {
            get
            {
                return (double) base.GetValue(QrCodeWidthInchProperty);
            }
            set
            {
                base.SetValue(QrCodeWidthInchProperty, value);
            }
        }

        [Category("QrCode"), EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public QuietZoneModules QuietZoneModule
        {
            get
            {
                return (QuietZoneModules) base.GetValue(QuietZoneModuleProperty);
            }
            set
            {
                base.SetValue(QuietZoneModuleProperty, value);
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("QrCode")]
        public string Text
        {
            get
            {
                return (string) base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        public WriteableBitmap WBitmap
        {
            get
            {
                return (WriteableBitmap) base.GetValue(WBitmapProperty);
            }
            private set
            {
                base.SetValue(WBitmapProperty, value);
            }
        }
    }
}

