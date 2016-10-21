using Gma.QrCodeNet.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeTest_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(@"Type some text to QR code: ");
            string sampleText = Console.ReadLine();
            QrEncoder qrEncoder2 = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode2 = qrEncoder2.Encode(sampleText);
            for (int j = 0; j < qrCode2.Matrix.Width; j++)
            {
                for (int i = 0; i < qrCode2.Matrix.Width; i++)
                {
                    string charToPrint = qrCode2.Matrix[i, j] ? "█" : "  ";
                    Console.Write(charToPrint);
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"Press any key to quit.");
            Console.ReadKey();

        }
    }
}
