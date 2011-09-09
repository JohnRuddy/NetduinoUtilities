using System;
using System.IO;
using System.IO.Ports;
using System.Text;


namespace NetduinoUtilities.Hardware
{
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
        private static readonly char[] ByteToHex = new[]
                                                       {
                                                           '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B',
                                                           'C',
                                                           'D', 'E', 'F'
                                                       };

        /// <summary>
        /// The encoding.
        /// </summary>
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();

        #endregion

        #region Private Constant/Read-Only Members

        /// <summary>
        /// The brightness.
        /// </summary>
        private const byte Brightness = 0x7A;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Serial7SegmentDisplay"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Serial7SegmentDisplay()
        {
            this.Port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            this.Port.Open();
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Serial7SegmentDisplay"/> class.
        /// </summary>
        /// <param name="comPort">The COM port.</param>
        /// <param name="parity">The parity.</param>
        /// <remarks></remarks>
        public Serial7SegmentDisplay(string comPort, Parity parity)
        {
            this.Port = new SerialPort(comPort, 9600, parity, 8, StopBits.One);
            this.Port.Open();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Serial7SegmentDisplay"/> class.
        /// </summary>
        /// <param name="comPort">The COM port.</param>
        /// <param name="parity">The parity.</param>
        /// <param name="stopbits">The stopbits.</param>
        /// <remarks></remarks>
        public Serial7SegmentDisplay(string comPort, Parity parity, StopBits stopbits)
        {
            this.Port = new SerialPort(comPort, 9600, parity, 8, stopbits);
            this.Port.Open();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The serial port used for communication</value>
        /// <remarks></remarks>
        public SerialPort Port { get; set; }

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

        /// <summary>
        /// Prints the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks></remarks>
        public void Print(string message)
        {
            byte[] resetDisplay = Encoding.GetBytes("v");

            this.Port.Write(resetDisplay, 0, 1);

            byte[] outputMessage = Encoding.GetBytes(message);
            this.Port.Write(outputMessage, 0, 4);
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

        /// <summary>
        /// Scrolls the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="delay">The delay.</param>
        /// <remarks></remarks>
        public void ScrollMessage(string message, int delay)
        {
            string messageString = this.CreateStringWithPadding(message, 4);
            char[] messageChars = messageString.ToCharArray();

            // output the first four characters of the string
            int currentIndex = 0;

            while (true)
            {
                string displayMessage = string.Empty;

                for (int i = 0; i < 4; i++)
                {
                    displayMessage = displayMessage + messageChars[currentIndex];

                    if (currentIndex >= messageString.Length - 1)
                    {
                        currentIndex = 0;
                    }
                    else
                    {
                        currentIndex++;
                    }
                }

                if (currentIndex > 3)
                {
                    currentIndex = currentIndex - 3;
                }

                this.Print(displayMessage);

                System.Threading.Thread.Sleep(delay);
            }
        }

        /// <summary>
        /// Creates the string with padding.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>The create string with padding.</returns>
        /// <remarks></remarks>
        private string CreateStringWithPadding(string message, int padding)
        {
            char[] letters = message.ToCharArray();
            string messageString = string.Empty;

            // add a blank for any spaces in the string
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == ' ')
                {
                    // blank character
                    letters[i] = 'X';
                }

                messageString = messageString + letters[i];
            }

            // add the padding to the string
            string paddingString = string.Empty;

            for (int i = 0; i < padding; i++)
            {
                paddingString = paddingString + 'X';
            }

            return paddingString + messageString + paddingString;
        }

        #endregion

        /// <summary>
        /// Blinks the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <remarks></remarks>
        public void BlinkMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}