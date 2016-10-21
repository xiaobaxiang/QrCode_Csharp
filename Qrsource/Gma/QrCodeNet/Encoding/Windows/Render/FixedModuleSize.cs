namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;

    public class FixedModuleSize : ISizeCalculation
    {
        private int m_ModuleSize;
        private int m_QuietZoneModule;

        public FixedModuleSize(int moduleSize, Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules quietZoneModules)
        {
            this.m_ModuleSize = moduleSize;
            this.m_QuietZoneModule = (int) quietZoneModules;
        }

        public DrawingSize GetSize(int matrixWidth)
        {
            return new DrawingSize(this.m_ModuleSize, ((this.m_QuietZoneModule * 2) + matrixWidth) * this.m_ModuleSize, (Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules) this.m_QuietZoneModule);
        }

        public int ModuleSize
        {
            get
            {
                return this.m_ModuleSize;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("ModuleSize", value, "ModuleSize can not be equal or less than zero");
                }
                this.m_ModuleSize = value;
            }
        }

        public Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules QuietZoneModules
        {
            get
            {
                return (Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules) this.m_QuietZoneModule;
            }
            set
            {
                this.m_QuietZoneModule = (int) value;
            }
        }
    }
}

