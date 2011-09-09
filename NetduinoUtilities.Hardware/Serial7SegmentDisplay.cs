namespace NetduinoUtilities.Hardware
{
    using System;
    using System.IO.Ports;
    using System.IO;
    using System.Text;
    


    /// <summary>
    /// Class to Interfaace with a 7 Segment Display
    /// </summary>
    /// <remarks></remarks>
    public class Serial7SegmentDisplay : IDisposable
    {
        #region Private Static Members

        /// <summary>
        /// char array used to convert a number into a hexadecimal display
        /// </summary>
        private static readonly char[] ByteToHex = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        #endregion

        #region Private Constant/Read-Only Members

        /// <summary>
        /// 
        /// </summary>
        private const byte Brightness = 0x7A;

        #endregion

        #region Private Members

        /// <summary>
        /// 
        /// </summary>
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();
        private string p;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Serial7SegmentDisplay"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Serial7SegmentDisplay()
        {
            
            Port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            Port.Open();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Serial7SegmentDisplay"/> class.
        /// </summary>
        /// <param name="comPort">The COM port.</param>
        /// <remarks></remarks>
        public Serial7SegmentDisplay(string comPort)
        {
            this.Port = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);
            this.Port.Open();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        /// <remarks></remarks>
        public SerialPort Port
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks></remarks>
        public void Dispose()
        {
            Dispose();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Prints the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks></remarks>
        public void Print(string message)
        {
            byte[] resetDisplay = Encoding.GetBytes("v");

            Port.Write(resetDisplay, 0, 1);

            byte[] outputMessage = Encoding.GetBytes(message);
            Port.Write(outputMessage, 0, 4);
        }

        /// <summary>
        /// Sets the brightness.
        /// </summary>
        /// <param name="percentage">The percentage dimness the display must be</param>
        /// <remarks></remarks>
        public void SetBrightness(int percentage)
        {
            byte[] buffer = new byte[2];
            int maxBrightness = 254 / percentage;

            buffer[0] = 0x7A;
            buffer[1] = (byte)maxBrightness;

            this.Port.Write(buffer, 0, 2);
        }


        public void ScrollMessage(string message, int delay, int padding);
        {
                // todo: create function to scroll a message across the screen
                
                // pseudocode of what we want to do
                //display.Print("XXXX");
                //Thread.Sleep(100);
                //display.Print("XXXJ");
                //Thread.Sleep(100);
                //display.Print("XXJO");
                //Thread.Sleep(100);
                //display.Print("XJOH");
                //Thread.Sleep(100);
                //display.Print("JOHN");
                //Thread.Sleep(100);
                //display.Print("OHNX");
                //Thread.Sleep(100);
                //display.Print("HNXX");
                //Thread.Sleep(100);
                //display.Print("NXXX");
                //Thread.Sleep(100);
                //display.Print("XXXX");
                //Thread.Sleep(100);
        }


        #endregion


    }
}