using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public class MyFormater:IFormatProvider,ICustomFormatter
        {
            public object GetFormat(Type format)
            {
                if (format == typeof(ICustomFormatter))
                    return this;
                if (format == typeof(NumberFormatInfo))
                    return "*" + this + "*";
                return null;
            }

            public string Format(string format, object arg, IFormatProvider provider)
            {
                if (format == null)
                {
                    if (arg is IFormattable)
                        return ((IFormattable)arg).ToString(format, provider);
                    return arg.ToString();
                }
                else
                {
                    if (format == "MyFormater")
                    {
                        return "**" + arg.ToString();
                    }
                    else
                    {
                        if (arg is IFormattable)
                            return ((IFormattable)arg).ToString(format, provider);
                        return arg.ToString();
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            byte[] by = new byte[4];
            by[0] = 239;
            by[1] = 187;
            by[2] = 191;
            by[3] = 49;
            string num = Encoding.UTF8.GetString(by);
            Console.WriteLine(num);
            string str = "1";
            Console.Write(str.GetTypeCode().ToString());
            byte[] b= Encoding.UTF8.GetBytes(str);
            string result = (Convert.ToInt32(str,new MyFormater())).ToString();
            Console.Write(result);
            string isOk = "";
            if (result == "1")
            {
                isOk = "true";
            }
            else
            {
                isOk = "false";
            }
            Console.Write(isOk);
            Console.ReadKey();
        }
    }
}
