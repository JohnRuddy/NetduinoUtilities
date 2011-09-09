using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using NetduinoUtilities;
using NetduinoUtilities.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;


namespace NetduinoUtilities.Hardware.Demo
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Serial7SegmentDisplay display = new Serial7SegmentDisplay();
            int i = 100;
            while (true)
            {
                


                
                display.SetBrightness(i);

                i = i - 20;

                if (i == 0)
                    i = 100;

            }
        }
    }
}
