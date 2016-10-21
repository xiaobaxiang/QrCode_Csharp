namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Runtime.CompilerServices;

    /*
     * 包含了一个QrCode的只读二维矩阵信息的，实体类，最终QrEncoder返回的对象就是这个
     */
    public class QrCode
    {
        public QrCode()
        {
            this.isContainMatrix = false;
            this.Matrix = null;
        }

        internal QrCode(BitMatrix matrix)
        {
            this.Matrix = matrix;
            this.isContainMatrix = true;
        }

        public bool isContainMatrix { get; private set; }

        public BitMatrix Matrix { get; private set; }
    }
}

