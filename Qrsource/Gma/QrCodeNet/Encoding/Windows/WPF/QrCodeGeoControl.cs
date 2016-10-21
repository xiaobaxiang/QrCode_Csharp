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

    public class QrCodeGeoControl : Control
    {
        public static readonly DependencyProperty DarkBrushProperty = DependencyProperty.Register("DarkBrush", typeof(Brush), typeof(QrCodeGeoControl), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty ErrorCorrecLeveltProperty = DependencyProperty.Register("ErrorCorrectLevel", typeof(ErrorCorrectionLevel), typeof(QrCodeGeoControl), new UIPropertyMetadata(ErrorCorrectionLevel.M, new PropertyChangedCallback(QrCodeGeoControl.OnMatrixValueChanged)));
        public static readonly DependencyProperty LightBrushProperty = DependencyProperty.Register("LightBrush", typeof(Brush), typeof(QrCodeGeoControl), new UIPropertyMetadata(Brushes.White));
        private bool m_isLocked;
        private QrCode m_QrCode = new QrCode();
        private int m_width = 0x15;
        public static readonly DependencyProperty QrGeometryProperty = DependencyProperty.Register("QrGeometry", typeof(Geometry), typeof(QrCodeGeoControl), null);
        public static readonly DependencyProperty QuietZoneModuleProperty = DependencyProperty.Register("QuietZoneModule", typeof(QuietZoneModules), typeof(QrCodeGeoControl), new UIPropertyMetadata(QuietZoneModules.Two, new PropertyChangedCallback(QrCodeGeoControl.OnQuietZonePixelSizeChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(QrCodeGeoControl), new UIPropertyMetadata(string.Empty, new PropertyChangedCallback(QrCodeGeoControl.OnMatrixValueChanged)));

        public event EventHandler QrMatrixChanged;

        static QrCodeGeoControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(QrCodeGeoControl), new FrameworkPropertyMetadata(typeof(QrCodeGeoControl)));
            FrameworkElement.HorizontalAlignmentProperty.OverrideMetadata(typeof(QrCodeGeoControl), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
            FrameworkElement.VerticalAlignmentProperty.OverrideMetadata(typeof(QrCodeGeoControl), new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        public QrCodeGeoControl()
        {
            this.UpdateGeometry();
            this.UpdatePadding();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double num = (arrangeBounds.Width < arrangeBounds.Height) ? arrangeBounds.Width : arrangeBounds.Height;
            double num2 = num / ((double) (this.m_width + (QuietZoneModules.Two * this.QuietZoneModule)));
            base.Padding = new Thickness(num2 * ((double) this.QuietZoneModule));
            return base.ArrangeOverride(arrangeBounds);
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

        private static void OnMatrixValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            QrCodeGeoControl control = (QrCodeGeoControl) d;
            control.UpdateGeometry();
            control.UpdatePadding();
        }

        protected virtual void OnQrMatrixChanged(EventArgs e)
        {
            if (this.QrMatrixChanged != null)
            {
                this.QrMatrixChanged(this, e);
            }
        }

        private static void OnQuietZonePixelSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QrCodeGeoControl) d).UpdatePadding();
        }

        public void Unlock()
        {
            this.m_isLocked = false;
            this.UpdateGeometry();
            this.UpdatePadding();
        }

        internal void UpdateGeometry()
        {
            if (!this.m_isLocked)
            {
                new QrEncoder(this.ErrorCorrectLevel).TryEncode(this.Text, out this.m_QrCode);
                this.OnQrMatrixChanged(new EventArgs());
                this.m_width = (this.m_QrCode.Matrix == null) ? 0x15 : this.m_QrCode.Matrix.Width;
                this.QrGeometry = new DrawingBrushRenderer(new FixedCodeSize(200, this.QuietZoneModule), this.DarkBrush, this.LightBrush).DrawGeometry(this.m_QrCode.Matrix, 0, 0);
            }
        }

        internal void UpdatePadding()
        {
            if (!this.m_isLocked)
            {
                double num = (base.ActualWidth < base.ActualHeight) ? base.ActualWidth : base.ActualHeight;
                double num2 = num / ((double) (this.m_width + (QuietZoneModules.Two * this.QuietZoneModule)));
                base.Padding = new Thickness(num2 * ((double) this.QuietZoneModule));
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Qr Code")]
        public Brush DarkBrush
        {
            get
            {
                return (Brush) base.GetValue(DarkBrushProperty);
            }
            set
            {
                base.SetValue(DarkBrushProperty, value);
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Qr Code")]
        public ErrorCorrectionLevel ErrorCorrectLevel
        {
            get
            {
                return (ErrorCorrectionLevel) base.GetValue(ErrorCorrecLeveltProperty);
            }
            set
            {
                base.SetValue(ErrorCorrecLeveltProperty, value);
            }
        }

        public bool IsLocked
        {
            get
            {
                return this.m_isLocked;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Category("Qr Code"), Browsable(true)]
        public Brush LightBrush
        {
            get
            {
                return (Brush) base.GetValue(LightBrushProperty);
            }
            set
            {
                base.SetValue(LightBrushProperty, value);
            }
        }

        public Geometry QrGeometry
        {
            get
            {
                return (Geometry) base.GetValue(QrGeometryProperty);
            }
            private set
            {
                base.SetValue(QrGeometryProperty, value);
            }
        }

        [Browsable(true), Category("Qr Code"), EditorBrowsable(EditorBrowsableState.Always)]
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

        [EditorBrowsable(EditorBrowsableState.Always), Category("Qr Code"), Browsable(true)]
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
    }
}

