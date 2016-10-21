namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;

    public class FixedCodeSize : ISizeCalculation
    {
        private int m_QrCodeWidth;
        private int m_QuietZoneModules = 2;

        public FixedCodeSize(int qrCodeWidth, Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules quietZone)
        {
            this.m_QrCodeWidth = qrCodeWidth;
            this.m_QuietZoneModules = (int) quietZone;
        }

        public DrawingSize GetSize(int matrixWidth)
        {
            return new DrawingSize(this.m_QrCodeWidth / (matrixWidth + (this.m_QuietZoneModules * 2)), this.m_QrCodeWidth, (Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules) this.m_QuietZoneModules);
        }

        public int QrCodeWidth
        {
            get
            {
                return this.m_QrCodeWidth;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("QrCodeWidth", value, "QrCodeWidth can not be equal or less than zero");
                }
                this.m_QrCodeWidth = value;
            }
        }

        public Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules QuietZoneModules
        {
            get
            {
                return (Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules) this.m_QuietZoneModules;
            }
            set
            {
                this.m_QuietZoneModules = (int) value;
            }
        }
    }
}

