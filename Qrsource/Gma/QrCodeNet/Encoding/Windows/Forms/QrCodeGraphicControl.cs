namespace Gma.QrCodeNet.Encoding.Windows.Forms
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Windows.Render;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class QrCodeGraphicControl : Control
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

        protected override void OnPaint(PaintEventArgs e)
        {
            int num;
            int num2;
            int width;
            if (base.Width <= base.Height)
            {
                num = 0;
                num2 = (base.Height - base.Width) / 2;
                width = base.Width;
            }
            else
            {
                num = (base.Width - base.Height) / 2;
                num2 = 0;
                width = base.Height;
            }
            new GraphicsRenderer(new FixedCodeSize(width, this.m_QuietZoneModule), this.m_darkBrush, this.m_LightBrush).Draw(e.Graphics, this.m_QrCode.Matrix, new Point(num, num2));
            base.OnPaint(e);
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
            base.Invalidate();
        }

        public void UnLock()
        {
            this.m_isLocked = false;
            this.UpdateQrCodeCache();
        }

        internal void UpdateQrCodeCache()
        {
            if (!this.m_isLocked)
            {
                new QrEncoder(this.m_ErrorCorrectLevel).TryEncode(this.Text, out this.m_QrCode);
                this.OnQrMatrixChanged(new EventArgs());
                base.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Localizable(false), Category("Qr Code"), Browsable(true), RefreshProperties(RefreshProperties.All)]
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
                    if (!this.m_isFreezed)
                    {
                        base.Invalidate();
                    }
                }
            }
        }

        [Localizable(false), RefreshProperties(RefreshProperties.All), Category("Qr Code"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
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
                    this.UpdateQrCodeCache();
                    this.OnErrorCorrectLevelChanged(new EventArgs());
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

        [Localizable(false), EditorBrowsable(EditorBrowsableState.Always), RefreshProperties(RefreshProperties.All), Category("Qr Code"), Browsable(true)]
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
                    if (!this.m_isFreezed)
                    {
                        base.Invalidate();
                    }
                }
            }
        }

        [Localizable(false), RefreshProperties(RefreshProperties.All), Category("Qr Code"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
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
                    if (!this.m_isFreezed)
                    {
                        base.Invalidate();
                    }
                }
            }
        }
    }
}

