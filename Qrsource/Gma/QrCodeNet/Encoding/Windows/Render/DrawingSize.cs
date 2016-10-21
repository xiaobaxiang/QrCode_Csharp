namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DrawingSize
    {
        public int ModuleSize { get; private set; }
        public int CodeWidth { get; private set; }
        public Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules QuietZoneModules { get; private set; }
        public DrawingSize(int moduleSize, int codeWidth, Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules quietZoneModules)
        {
            this = new DrawingSize();
            this.ModuleSize = moduleSize;
            this.CodeWidth = codeWidth;
            this.QuietZoneModules = quietZoneModules;
        }
    }
}

