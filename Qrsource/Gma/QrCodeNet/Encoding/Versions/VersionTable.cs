﻿namespace Gma.QrCodeNet.Encoding.Versions
{
    using System;

    public static class VersionTable
    {
        private static QRCodeVersion[] version = initialize();

        internal static QRCodeVersion GetVersionByNum(int versionNum)
        {
            if ((versionNum < 1) || (versionNum > 40))
            {
                throw new InvalidOperationException(string.Format("Unexpected version number: {0}", versionNum));
            }
            return version[versionNum - 1];
        }

        internal static QRCodeVersion GetVersionByWidth(int matrixWidth)
        {
            if (((matrixWidth - 0x11) % 4) != 0)
            {
                throw new ArgumentException("Incorrect matrix width");
            }
            return GetVersionByNum((matrixWidth - 0x11) / 4);
        }

        private static QRCodeVersion[] initialize()
        {
            return new QRCodeVersion[] { 
                new QRCodeVersion(1, 0x1a, new ErrorCorrectionBlocks(7, new ErrorCorrectionBlock(1, 0x13)), new ErrorCorrectionBlocks(10, new ErrorCorrectionBlock(1, 0x10)), new ErrorCorrectionBlocks(13, new ErrorCorrectionBlock(1, 13)), new ErrorCorrectionBlocks(0x11, new ErrorCorrectionBlock(1, 9))), new QRCodeVersion(2, 0x2c, new ErrorCorrectionBlocks(10, new ErrorCorrectionBlock(1, 0x22)), new ErrorCorrectionBlocks(0x10, new ErrorCorrectionBlock(1, 0x1c)), new ErrorCorrectionBlocks(0x16, new ErrorCorrectionBlock(1, 0x16)), new ErrorCorrectionBlocks(0x1c, new ErrorCorrectionBlock(1, 0x10))), new QRCodeVersion(3, 70, new ErrorCorrectionBlocks(15, new ErrorCorrectionBlock(1, 0x37)), new ErrorCorrectionBlocks(0x1a, new ErrorCorrectionBlock(1, 0x2c)), new ErrorCorrectionBlocks(0x24, new ErrorCorrectionBlock(2, 0x11)), new ErrorCorrectionBlocks(0x2c, new ErrorCorrectionBlock(2, 13))), new QRCodeVersion(4, 100, new ErrorCorrectionBlocks(20, new ErrorCorrectionBlock(1, 80)), new ErrorCorrectionBlocks(0x24, new ErrorCorrectionBlock(2, 0x20)), new ErrorCorrectionBlocks(0x34, new ErrorCorrectionBlock(2, 0x18)), new ErrorCorrectionBlocks(0x40, new ErrorCorrectionBlock(4, 9))), new QRCodeVersion(5, 0x86, new ErrorCorrectionBlocks(0x1a, new ErrorCorrectionBlock(1, 0x6c)), new ErrorCorrectionBlocks(0x30, new ErrorCorrectionBlock(2, 0x2b)), new ErrorCorrectionBlocks(0x48, new ErrorCorrectionBlock(2, 15), new ErrorCorrectionBlock(2, 0x10)), new ErrorCorrectionBlocks(0x58, new ErrorCorrectionBlock(2, 11), new ErrorCorrectionBlock(2, 12))), new QRCodeVersion(6, 0xac, new ErrorCorrectionBlocks(0x24, new ErrorCorrectionBlock(2, 0x44)), new ErrorCorrectionBlocks(0x40, new ErrorCorrectionBlock(4, 0x1b)), new ErrorCorrectionBlocks(0x60, new ErrorCorrectionBlock(4, 0x13)), new ErrorCorrectionBlocks(0x70, new ErrorCorrectionBlock(4, 15))), new QRCodeVersion(7, 0xc4, new ErrorCorrectionBlocks(40, new ErrorCorrectionBlock(2, 0x4e)), new ErrorCorrectionBlocks(0x48, new ErrorCorrectionBlock(4, 0x1f)), new ErrorCorrectionBlocks(0x6c, new ErrorCorrectionBlock(2, 14), new ErrorCorrectionBlock(4, 15)), new ErrorCorrectionBlocks(130, new ErrorCorrectionBlock(4, 13), new ErrorCorrectionBlock(1, 14))), new QRCodeVersion(8, 0xf2, new ErrorCorrectionBlocks(0x30, new ErrorCorrectionBlock(2, 0x61)), new ErrorCorrectionBlocks(0x58, new ErrorCorrectionBlock(2, 0x26), new ErrorCorrectionBlock(2, 0x27)), new ErrorCorrectionBlocks(0x84, new ErrorCorrectionBlock(4, 0x12), new ErrorCorrectionBlock(2, 0x13)), new ErrorCorrectionBlocks(0x9c, new ErrorCorrectionBlock(4, 14), new ErrorCorrectionBlock(2, 15))), new QRCodeVersion(9, 0x124, new ErrorCorrectionBlocks(60, new ErrorCorrectionBlock(2, 0x74)), new ErrorCorrectionBlocks(110, new ErrorCorrectionBlock(3, 0x24), new ErrorCorrectionBlock(2, 0x25)), new ErrorCorrectionBlocks(160, new ErrorCorrectionBlock(4, 0x10), new ErrorCorrectionBlock(4, 0x11)), new ErrorCorrectionBlocks(0xc0, new ErrorCorrectionBlock(4, 12), new ErrorCorrectionBlock(4, 13))), new QRCodeVersion(10, 0x15a, new ErrorCorrectionBlocks(0x48, new ErrorCorrectionBlock(2, 0x44), new ErrorCorrectionBlock(2, 0x45)), new ErrorCorrectionBlocks(130, new ErrorCorrectionBlock(4, 0x2b), new ErrorCorrectionBlock(1, 0x2c)), new ErrorCorrectionBlocks(0xc0, new ErrorCorrectionBlock(6, 0x13), new ErrorCorrectionBlock(2, 20)), new ErrorCorrectionBlocks(0xe0, new ErrorCorrectionBlock(6, 15), new ErrorCorrectionBlock(2, 0x10))), new QRCodeVersion(11, 0x194, new ErrorCorrectionBlocks(80, new ErrorCorrectionBlock(4, 0x51)), new ErrorCorrectionBlocks(150, new ErrorCorrectionBlock(1, 50), new ErrorCorrectionBlock(4, 0x33)), new ErrorCorrectionBlocks(0xe0, new ErrorCorrectionBlock(4, 0x16), new ErrorCorrectionBlock(4, 0x17)), new ErrorCorrectionBlocks(0x108, new ErrorCorrectionBlock(3, 12), new ErrorCorrectionBlock(8, 13))), new QRCodeVersion(12, 0x1d2, new ErrorCorrectionBlocks(0x60, new ErrorCorrectionBlock(2, 0x5c), new ErrorCorrectionBlock(2, 0x5d)), new ErrorCorrectionBlocks(0xb0, new ErrorCorrectionBlock(6, 0x24), new ErrorCorrectionBlock(2, 0x25)), new ErrorCorrectionBlocks(260, new ErrorCorrectionBlock(4, 20), new ErrorCorrectionBlock(6, 0x15)), new ErrorCorrectionBlocks(0x134, new ErrorCorrectionBlock(7, 14), new ErrorCorrectionBlock(4, 15))), new QRCodeVersion(13, 0x214, new ErrorCorrectionBlocks(0x68, new ErrorCorrectionBlock(4, 0x6b)), new ErrorCorrectionBlocks(0xc6, new ErrorCorrectionBlock(8, 0x25), new ErrorCorrectionBlock(1, 0x26)), new ErrorCorrectionBlocks(0x120, new ErrorCorrectionBlock(8, 20), new ErrorCorrectionBlock(4, 0x15)), new ErrorCorrectionBlocks(0x160, new ErrorCorrectionBlock(12, 11), new ErrorCorrectionBlock(4, 12))), new QRCodeVersion(14, 0x245, new ErrorCorrectionBlocks(120, new ErrorCorrectionBlock(3, 0x73), new ErrorCorrectionBlock(1, 0x74)), new ErrorCorrectionBlocks(0xd8, new ErrorCorrectionBlock(4, 40), new ErrorCorrectionBlock(5, 0x29)), new ErrorCorrectionBlocks(320, new ErrorCorrectionBlock(11, 0x10), new ErrorCorrectionBlock(5, 0x11)), new ErrorCorrectionBlocks(0x180, new ErrorCorrectionBlock(11, 12), new ErrorCorrectionBlock(5, 13))), new QRCodeVersion(15, 0x28f, new ErrorCorrectionBlocks(0x84, new ErrorCorrectionBlock(5, 0x57), new ErrorCorrectionBlock(1, 0x58)), new ErrorCorrectionBlocks(240, new ErrorCorrectionBlock(5, 0x29), new ErrorCorrectionBlock(5, 0x2a)), new ErrorCorrectionBlocks(360, new ErrorCorrectionBlock(5, 0x18), new ErrorCorrectionBlock(7, 0x19)), new ErrorCorrectionBlocks(0x1b0, new ErrorCorrectionBlock(11, 12), new ErrorCorrectionBlock(7, 13))), new QRCodeVersion(0x10, 0x2dd, new ErrorCorrectionBlocks(0x90, new ErrorCorrectionBlock(5, 0x62), new ErrorCorrectionBlock(1, 0x63)), new ErrorCorrectionBlocks(280, new ErrorCorrectionBlock(7, 0x2d), new ErrorCorrectionBlock(3, 0x2e)), new ErrorCorrectionBlocks(0x198, new ErrorCorrectionBlock(15, 0x13), new ErrorCorrectionBlock(2, 20)), new ErrorCorrectionBlocks(480, new ErrorCorrectionBlock(3, 15), new ErrorCorrectionBlock(13, 0x10))), 
                new QRCodeVersion(0x11, 0x32f, new ErrorCorrectionBlocks(0xa8, new ErrorCorrectionBlock(1, 0x6b), new ErrorCorrectionBlock(5, 0x6c)), new ErrorCorrectionBlocks(0x134, new ErrorCorrectionBlock(10, 0x2e), new ErrorCorrectionBlock(1, 0x2f)), new ErrorCorrectionBlocks(0x1c0, new ErrorCorrectionBlock(1, 0x16), new ErrorCorrectionBlock(15, 0x17)), new ErrorCorrectionBlocks(0x214, new ErrorCorrectionBlock(2, 14), new ErrorCorrectionBlock(0x11, 15))), new QRCodeVersion(0x12, 0x385, new ErrorCorrectionBlocks(180, new ErrorCorrectionBlock(5, 120), new ErrorCorrectionBlock(1, 0x79)), new ErrorCorrectionBlocks(0x152, new ErrorCorrectionBlock(9, 0x2b), new ErrorCorrectionBlock(4, 0x2c)), new ErrorCorrectionBlocks(0x1f8, new ErrorCorrectionBlock(0x11, 0x16), new ErrorCorrectionBlock(1, 0x17)), new ErrorCorrectionBlocks(0x24c, new ErrorCorrectionBlock(2, 14), new ErrorCorrectionBlock(0x13, 15))), new QRCodeVersion(0x13, 0x3df, new ErrorCorrectionBlocks(0xc4, new ErrorCorrectionBlock(3, 0x71), new ErrorCorrectionBlock(4, 0x72)), new ErrorCorrectionBlocks(0x16c, new ErrorCorrectionBlock(3, 0x2c), new ErrorCorrectionBlock(11, 0x2d)), new ErrorCorrectionBlocks(0x222, new ErrorCorrectionBlock(0x11, 0x15), new ErrorCorrectionBlock(4, 0x16)), new ErrorCorrectionBlocks(650, new ErrorCorrectionBlock(9, 13), new ErrorCorrectionBlock(0x10, 14))), new QRCodeVersion(20, 0x43d, new ErrorCorrectionBlocks(0xe0, new ErrorCorrectionBlock(3, 0x6b), new ErrorCorrectionBlock(5, 0x6c)), new ErrorCorrectionBlocks(0x1a0, new ErrorCorrectionBlock(3, 0x29), new ErrorCorrectionBlock(13, 0x2a)), new ErrorCorrectionBlocks(600, new ErrorCorrectionBlock(15, 0x18), new ErrorCorrectionBlock(5, 0x19)), new ErrorCorrectionBlocks(700, new ErrorCorrectionBlock(15, 15), new ErrorCorrectionBlock(10, 0x10))), new QRCodeVersion(0x15, 0x484, new ErrorCorrectionBlocks(0xe0, new ErrorCorrectionBlock(4, 0x74), new ErrorCorrectionBlock(4, 0x75)), new ErrorCorrectionBlocks(0x1ba, new ErrorCorrectionBlock(0x11, 0x2a)), new ErrorCorrectionBlocks(0x284, new ErrorCorrectionBlock(0x11, 0x16), new ErrorCorrectionBlock(6, 0x17)), new ErrorCorrectionBlocks(750, new ErrorCorrectionBlock(0x13, 0x10), new ErrorCorrectionBlock(6, 0x11))), new QRCodeVersion(0x16, 0x4ea, new ErrorCorrectionBlocks(0xfc, new ErrorCorrectionBlock(2, 0x6f), new ErrorCorrectionBlock(7, 0x70)), new ErrorCorrectionBlocks(0x1dc, new ErrorCorrectionBlock(0x11, 0x2e)), new ErrorCorrectionBlocks(690, new ErrorCorrectionBlock(7, 0x18), new ErrorCorrectionBlock(0x10, 0x19)), new ErrorCorrectionBlocks(0x330, new ErrorCorrectionBlock(0x22, 13))), new QRCodeVersion(0x17, 0x554, new ErrorCorrectionBlocks(270, new ErrorCorrectionBlock(4, 0x79), new ErrorCorrectionBlock(5, 0x7a)), new ErrorCorrectionBlocks(0x1f8, new ErrorCorrectionBlock(4, 0x2f), new ErrorCorrectionBlock(14, 0x30)), new ErrorCorrectionBlocks(750, new ErrorCorrectionBlock(11, 0x18), new ErrorCorrectionBlock(14, 0x19)), new ErrorCorrectionBlocks(900, new ErrorCorrectionBlock(0x10, 15), new ErrorCorrectionBlock(14, 0x10))), new QRCodeVersion(0x18, 0x5c2, new ErrorCorrectionBlocks(300, new ErrorCorrectionBlock(6, 0x75), new ErrorCorrectionBlock(4, 0x76)), new ErrorCorrectionBlocks(560, new ErrorCorrectionBlock(6, 0x2d), new ErrorCorrectionBlock(14, 0x2e)), new ErrorCorrectionBlocks(810, new ErrorCorrectionBlock(11, 0x18), new ErrorCorrectionBlock(0x10, 0x19)), new ErrorCorrectionBlocks(960, new ErrorCorrectionBlock(30, 0x10), new ErrorCorrectionBlock(2, 0x11))), new QRCodeVersion(0x19, 0x634, new ErrorCorrectionBlocks(0x138, new ErrorCorrectionBlock(8, 0x6a), new ErrorCorrectionBlock(4, 0x6b)), new ErrorCorrectionBlocks(0x24c, new ErrorCorrectionBlock(8, 0x2f), new ErrorCorrectionBlock(13, 0x30)), new ErrorCorrectionBlocks(870, new ErrorCorrectionBlock(7, 0x18), new ErrorCorrectionBlock(0x16, 0x19)), new ErrorCorrectionBlocks(0x41a, new ErrorCorrectionBlock(0x16, 15), new ErrorCorrectionBlock(13, 0x10))), new QRCodeVersion(0x1a, 0x6aa, new ErrorCorrectionBlocks(0x150, new ErrorCorrectionBlock(10, 0x72), new ErrorCorrectionBlock(2, 0x73)), new ErrorCorrectionBlocks(0x284, new ErrorCorrectionBlock(0x13, 0x2e), new ErrorCorrectionBlock(4, 0x2f)), new ErrorCorrectionBlocks(0x3b8, new ErrorCorrectionBlock(0x1c, 0x16), new ErrorCorrectionBlock(6, 0x17)), new ErrorCorrectionBlocks(0x456, new ErrorCorrectionBlock(0x21, 0x10), new ErrorCorrectionBlock(4, 0x11))), new QRCodeVersion(0x1b, 0x724, new ErrorCorrectionBlocks(360, new ErrorCorrectionBlock(8, 0x7a), new ErrorCorrectionBlock(4, 0x7b)), new ErrorCorrectionBlocks(700, new ErrorCorrectionBlock(0x16, 0x2d), new ErrorCorrectionBlock(3, 0x2e)), new ErrorCorrectionBlocks(0x3fc, new ErrorCorrectionBlock(8, 0x17), new ErrorCorrectionBlock(0x1a, 0x18)), new ErrorCorrectionBlocks(0x4b0, new ErrorCorrectionBlock(12, 15), new ErrorCorrectionBlock(0x1c, 0x10))), new QRCodeVersion(0x1c, 0x781, new ErrorCorrectionBlocks(390, new ErrorCorrectionBlock(3, 0x75), new ErrorCorrectionBlock(10, 0x76)), new ErrorCorrectionBlocks(0x2d8, new ErrorCorrectionBlock(3, 0x2d), new ErrorCorrectionBlock(0x17, 0x2e)), new ErrorCorrectionBlocks(0x41a, new ErrorCorrectionBlock(4, 0x18), new ErrorCorrectionBlock(0x1f, 0x19)), new ErrorCorrectionBlocks(0x4ec, new ErrorCorrectionBlock(11, 15), new ErrorCorrectionBlock(0x1f, 0x10))), new QRCodeVersion(0x1d, 0x803, new ErrorCorrectionBlocks(420, new ErrorCorrectionBlock(7, 0x74), new ErrorCorrectionBlock(7, 0x75)), new ErrorCorrectionBlocks(0x310, new ErrorCorrectionBlock(0x15, 0x2d), new ErrorCorrectionBlock(7, 0x2e)), new ErrorCorrectionBlocks(0x474, new ErrorCorrectionBlock(1, 0x17), new ErrorCorrectionBlock(0x25, 0x18)), new ErrorCorrectionBlocks(0x546, new ErrorCorrectionBlock(0x13, 15), new ErrorCorrectionBlock(0x1a, 0x10))), new QRCodeVersion(30, 0x889, new ErrorCorrectionBlocks(450, new ErrorCorrectionBlock(5, 0x73), new ErrorCorrectionBlock(10, 0x74)), new ErrorCorrectionBlocks(0x32c, new ErrorCorrectionBlock(0x13, 0x2f), new ErrorCorrectionBlock(10, 0x30)), new ErrorCorrectionBlocks(0x4b0, new ErrorCorrectionBlock(15, 0x18), new ErrorCorrectionBlock(0x19, 0x19)), new ErrorCorrectionBlocks(0x5a0, new ErrorCorrectionBlock(0x17, 15), new ErrorCorrectionBlock(0x19, 0x10))), new QRCodeVersion(0x1f, 0x913, new ErrorCorrectionBlocks(480, new ErrorCorrectionBlock(13, 0x73), new ErrorCorrectionBlock(3, 0x74)), new ErrorCorrectionBlocks(0x364, new ErrorCorrectionBlock(2, 0x2e), new ErrorCorrectionBlock(0x1d, 0x2f)), new ErrorCorrectionBlocks(0x50a, new ErrorCorrectionBlock(0x2a, 0x18), new ErrorCorrectionBlock(1, 0x19)), new ErrorCorrectionBlocks(0x5fa, new ErrorCorrectionBlock(0x17, 15), new ErrorCorrectionBlock(0x1c, 0x10))), new QRCodeVersion(0x20, 0x9a1, new ErrorCorrectionBlocks(510, new ErrorCorrectionBlock(0x11, 0x73)), new ErrorCorrectionBlocks(0x39c, new ErrorCorrectionBlock(10, 0x2e), new ErrorCorrectionBlock(0x17, 0x2f)), new ErrorCorrectionBlocks(0x546, new ErrorCorrectionBlock(10, 0x18), new ErrorCorrectionBlock(0x23, 0x19)), new ErrorCorrectionBlocks(0x654, new ErrorCorrectionBlock(0x13, 15), new ErrorCorrectionBlock(0x23, 0x10))), 
                new QRCodeVersion(0x21, 0xa33, new ErrorCorrectionBlocks(540, new ErrorCorrectionBlock(0x11, 0x73), new ErrorCorrectionBlock(1, 0x74)), new ErrorCorrectionBlocks(980, new ErrorCorrectionBlock(14, 0x2e), new ErrorCorrectionBlock(0x15, 0x2f)), new ErrorCorrectionBlocks(0x5a0, new ErrorCorrectionBlock(0x1d, 0x18), new ErrorCorrectionBlock(0x13, 0x19)), new ErrorCorrectionBlocks(0x6ae, new ErrorCorrectionBlock(11, 15), new ErrorCorrectionBlock(0x2e, 0x10))), new QRCodeVersion(0x22, 0xac9, new ErrorCorrectionBlocks(570, new ErrorCorrectionBlock(13, 0x73), new ErrorCorrectionBlock(6, 0x74)), new ErrorCorrectionBlocks(0x40c, new ErrorCorrectionBlock(14, 0x2e), new ErrorCorrectionBlock(0x17, 0x2f)), new ErrorCorrectionBlocks(0x5fa, new ErrorCorrectionBlock(0x2c, 0x18), new ErrorCorrectionBlock(7, 0x19)), new ErrorCorrectionBlocks(0x708, new ErrorCorrectionBlock(0x3b, 0x10), new ErrorCorrectionBlock(1, 0x11))), new QRCodeVersion(0x23, 0xb3c, new ErrorCorrectionBlocks(570, new ErrorCorrectionBlock(12, 0x79), new ErrorCorrectionBlock(7, 0x7a)), new ErrorCorrectionBlocks(0x428, new ErrorCorrectionBlock(12, 0x2f), new ErrorCorrectionBlock(0x1a, 0x30)), new ErrorCorrectionBlocks(0x636, new ErrorCorrectionBlock(0x27, 0x18), new ErrorCorrectionBlock(14, 0x19)), new ErrorCorrectionBlocks(0x762, new ErrorCorrectionBlock(0x16, 15), new ErrorCorrectionBlock(0x29, 0x10))), new QRCodeVersion(0x24, 0xbda, new ErrorCorrectionBlocks(600, new ErrorCorrectionBlock(6, 0x79), new ErrorCorrectionBlock(14, 0x7a)), new ErrorCorrectionBlocks(0x460, new ErrorCorrectionBlock(6, 0x2f), new ErrorCorrectionBlock(0x22, 0x30)), new ErrorCorrectionBlocks(0x690, new ErrorCorrectionBlock(0x2e, 0x18), new ErrorCorrectionBlock(10, 0x19)), new ErrorCorrectionBlocks(0x7bc, new ErrorCorrectionBlock(2, 15), new ErrorCorrectionBlock(0x40, 0x10))), new QRCodeVersion(0x25, 0xc7c, new ErrorCorrectionBlocks(630, new ErrorCorrectionBlock(0x11, 0x7a), new ErrorCorrectionBlock(4, 0x7b)), new ErrorCorrectionBlocks(0x4b4, new ErrorCorrectionBlock(0x1d, 0x2e), new ErrorCorrectionBlock(14, 0x2f)), new ErrorCorrectionBlocks(0x6ea, new ErrorCorrectionBlock(0x31, 0x18), new ErrorCorrectionBlock(10, 0x19)), new ErrorCorrectionBlocks(0x834, new ErrorCorrectionBlock(0x18, 15), new ErrorCorrectionBlock(0x2e, 0x10))), new QRCodeVersion(0x26, 0xd22, new ErrorCorrectionBlocks(660, new ErrorCorrectionBlock(4, 0x7a), new ErrorCorrectionBlock(0x12, 0x7b)), new ErrorCorrectionBlocks(0x4ec, new ErrorCorrectionBlock(13, 0x2e), new ErrorCorrectionBlock(0x20, 0x2f)), new ErrorCorrectionBlocks(0x744, new ErrorCorrectionBlock(0x30, 0x18), new ErrorCorrectionBlock(14, 0x19)), new ErrorCorrectionBlocks(0x8ac, new ErrorCorrectionBlock(0x2a, 15), new ErrorCorrectionBlock(0x20, 0x10))), new QRCodeVersion(0x27, 0xdcc, new ErrorCorrectionBlocks(720, new ErrorCorrectionBlock(20, 0x75), new ErrorCorrectionBlock(4, 0x76)), new ErrorCorrectionBlocks(0x524, new ErrorCorrectionBlock(40, 0x2f), new ErrorCorrectionBlock(7, 0x30)), new ErrorCorrectionBlocks(0x79e, new ErrorCorrectionBlock(0x2b, 0x18), new ErrorCorrectionBlock(0x16, 0x19)), new ErrorCorrectionBlocks(0x906, new ErrorCorrectionBlock(10, 15), new ErrorCorrectionBlock(0x43, 0x10))), new QRCodeVersion(40, 0xe7a, new ErrorCorrectionBlocks(750, new ErrorCorrectionBlock(0x13, 0x76), new ErrorCorrectionBlock(6, 0x77)), new ErrorCorrectionBlocks(0x55c, new ErrorCorrectionBlock(0x12, 0x2f), new ErrorCorrectionBlock(0x1f, 0x30)), new ErrorCorrectionBlocks(0x7f8, new ErrorCorrectionBlock(0x22, 0x18), new ErrorCorrectionBlock(0x22, 0x19)), new ErrorCorrectionBlocks(0x97e, new ErrorCorrectionBlock(20, 15), new ErrorCorrectionBlock(0x3d, 0x10)))
             };
        }
    }
}

