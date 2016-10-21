namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /*
     * 这是一个QrCode对外调用的入口类
     * 二维码生成算法，参考http://blog.csdn.net/longteng1116/article/details/12753541
     * http://qrcodenet.codeplex.com/wikipage?title=QrCode&referringTitle=Documentation
     */
    public class QrEncoder
    {
        public QrEncoder() : this(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M)//调用下面重载的有参数的构造函数
        {
        }

        /*
         * 纠错编码，设置纠错级别
         */
        public QrEncoder(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel errorCorrectionLevel)
        {
            this.ErrorCorrectionLevel = errorCorrectionLevel;
        }

        public QrCode Encode(IEnumerable<byte> content)
        {
            if (content == null)
            {
                throw new InputOutOfBoundaryException("Input should not be empty or null");
            }
            return new QrCode(QRCodeEncode.Encode(content, this.ErrorCorrectionLevel));
        }

        /*
         * 根据传入的字符串内容，返回编码的QrCode，
         * 这个QrCode中包含一个只读的二维矩阵，包含了二维码每一位的着色信息
         */
        public QrCode Encode(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new InputOutOfBoundaryException("Input should not be empty or null");
            }
            return new QrCode(QRCodeEncode.Encode(content, this.ErrorCorrectionLevel));
        }

        public bool TryEncode(IEnumerable<byte> content, out QrCode qrCode)
        {
            try
            {
                qrCode = this.Encode(content);
                return true;
            }
            catch (InputOutOfBoundaryException)
            {
                qrCode = new QrCode();
                return false;
            }
        }

        public bool TryEncode(string content, out QrCode qrCode)
        {
            try
            {
                qrCode = this.Encode(content);
                return true;
            }
            catch (InputOutOfBoundaryException)
            {
                qrCode = new QrCode();
                return false;
            }
        }

        public Gma.QrCodeNet.Encoding.ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }
    }
}

