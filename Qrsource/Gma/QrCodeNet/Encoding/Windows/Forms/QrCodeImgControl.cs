namespace Gma.QrCodeNet.Encoding.Windows.Forms
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Windows.Render;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class QrCodeImgControl : PictureBox
    {
        private Brush m_darkBrush = Brushes.Black;
        private ErrorCorrectionLevel m_ErrorCorrectLevel = ErrorCorrectionLevel.M;
        private bool m_isFreezed;
        private bool m_isLocked;
        private Brush m_LightBrush = Brushes.White;
        private QrCode m_QrCode = new QrCode();
        private QuietZoneModules m_QuietZoneModule = QuietZoneModules.Two;

        public event EventHandler DarkBrushChanged;

        public event EventHandler ErrorCorrectLevelChanged;

        public event EventHandler LightBrushChanged;

        public event EventHandler QrMatrixChanged;

        public event EventHandler QuietZoneModuleChanged;

        public QrCodeImgControl()
        {
            this.UpdateImage();
        }

        private int CalculateSuitableWidth(int width, int bitMatrixWidth)
        {
            DrawingSize size = new FixedCodeSize(width, this.m_QuietZoneModule).GetSize(bitMatrixWidth);
            int num = size.CodeWidth - (size.ModuleSize * (bitMatrixWidth + (QuietZoneModules.Two * this.m_QuietZoneModule)));
            if (num == 0)
            {
                return width;
            }
            if ((size.CodeWidth / num) < 6)
            {
                return ((size.ModuleSize + 1) * (bitMatrixWidth + (QuietZoneModules.Two * this.m_QuietZoneModule)));
            }
            return (size.ModuleSize * (bitMatrixWidth + (QuietZoneModules.Two * this.m_QuietZoneModule)));
        }

        public void Freeze()
        {
            this.m_isFreezed = true;
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

        protected virtual void OnDarkBrushChanged(EventArgs e)
        {
            if (this.DarkBrushChanged != null)
            {
                this.DarkBrushChanged(this, e);
            }
        }

        protected virtual void OnErrorCorrectLevelChanged(EventArgs e)
        {
            if (this.ErrorCorrectLevelChanged != null)
            {
                this.ErrorCorrectLevelChanged(this, e);
            }
        }

        protected virtual void OnLightBrushChanged(EventArgs e)
        {
            if (this.LightBrushChanged != null)
            {
                this.LightBrushChanged(this, e);
            }
        }

        protected virtual void OnQrMatrixChanged(EventArgs e)
        {
            if (this.QrMatrixChanged != null)
            {
                this.QrMatrixChanged(this, e);
            }
        }

        protected virtual void OnQuietZoneModuleChanged(EventArgs e)
        {
            if (this.QuietZoneModuleChanged != null)
            {
                this.QuietZoneModuleChanged(this, e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.UpdateQrCodeCache();
            base.OnTextChanged(e);
        }

        public void UnFreeze()
        {
            this.m_isFreezed = false;
            this.UpdateImage();
        }

        public void UnLock()
        {
            this.m_isLocked = false;
            this.UpdateQrCodeCache();
        }

        internal void UpdateImage()
        {
            if (!this.m_isFreezed)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    int width = (base.Width < base.Height) ? base.Width : base.Height;
                    int qrCodeWidth = (this.m_QrCode.Matrix == null) ? this.CalculateSuitableWidth(width, 0x15) : this.CalculateSuitableWidth(width, this.m_QrCode.Matrix.Width);
                    new GraphicsRenderer(new FixedCodeSize(qrCodeWidth, this.m_QuietZoneModule), this.m_darkBrush, this.m_LightBrush).WriteToStream(this.m_QrCode.Matrix, ImageFormat.Png, stream);
                    Bitmap bitmap = new Bitmap(stream);
                    base.Image = bitmap;
                }
            }
        }

        internal void UpdateQrCodeCache()
        {
            if (!this.m_isLocked)
            {
                new QrEncoder(this.m_ErrorCorrectLevel).TryEncode(this.Text, out this.m_QrCode);
                this.OnQrMatrixChanged(new EventArgs());
                this.UpdateImage();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Category("Qr Code"), RefreshProperties(RefreshProperties.All), Localizable(false), Browsable(true)]
        public Brush DarkBrush
        {
            get
            {
                return this.m_darkBrush;
            }
            set
            {
                if (this.m_darkBrush != value)
                {
                    this.m_darkBrush = value;
                    this.OnDarkBrushChanged(new EventArgs());
                    this.UpdateImage();
                }
            }
        }

        [Category("Qr Code"), EditorBrowsable(EditorBrowsableState.Always), RefreshProperties(RefreshProperties.All), Localizable(false), Browsable(true)]
        public ErrorCorrectionLevel ErrorCorrectLevel
        {
            get
            {
                return this.m_ErrorCorrectLevel;
            }
            set
            {
                if (this.m_ErrorCorrectLevel != value)
                {
                    this.m_ErrorCorrectLevel = value;
                    this.OnErrorCorrectLevelChanged(new EventArgs());
                    this.UpdateQrCodeCache();
                }
            }
        }

        public bool IsFreezed
        {
            get
            {
                return this.m_isFreezed;
            }
        }

        public bool IsLocked
        {
            get
            {
                return this.m_isLocked;
            }
        }

        [Category("Qr Code"), EditorBrowsable(EditorBrowsableState.Always), RefreshProperties(RefreshProperties.All), Browsable(true), Localizable(false)]
        public Brush LightBrush
        {
            get
            {
                return this.m_LightBrush;
            }
            set
            {
                if (this.m_LightBrush != value)
                {
                    this.m_LightBrush = value;
                    this.OnLightBrushChanged(new EventArgs());
                    this.UpdateImage();
                }
            }
        }

        [Category("Qr Code"), EditorBrowsable(EditorBrowsableState.Always), Localizable(false), Browsable(true), RefreshProperties(RefreshProperties.All)]
        public QuietZoneModules QuietZoneModule
        {
            get
            {
                return this.m_QuietZoneModule;
            }
            set
            {
                if (this.m_QuietZoneModule != value)
                {
                    this.m_QuietZoneModule = value;
                    this.OnQuietZoneModuleChanged(new EventArgs());
                    this.UpdateImage();
                }
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Qr Code"), Bindable(true), RefreshProperties(RefreshProperties.All)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
    }
}

