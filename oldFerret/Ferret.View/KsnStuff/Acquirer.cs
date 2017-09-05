using Ferret.View.KsnStuff.TypeCode;
using Pinpad.Sdk.Model.TypeCode;
using System.Collections.Generic;

namespace Ferret.View.KsnStuff
{
    /// <summary>
    /// Information obtained from the GDU command, from ABECS specs.
    /// </summary>
    internal sealed class Acquirer
    {
        /// <summary>
        /// Serial number of the pinpad which support this acquirer.
        /// </summary>
        public string PinpadSerialNumber { get; set; }
        /// <summary>
        /// Acquirer name in upper case.
        /// </summary>
        public string Name
        {
            get
            {
                return this.AcquirerCode.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Acquirer code.
        /// </summary>
        public AcquirerCode AcquirerCode { get; set; }
        /// <summary>
        /// Index on pinpad table.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Key Serial Number (KSN) is returned from the encrypting device, 
        /// along with the cryptogram. The KSN is formed from the device's unique 
        /// identifier, and an internal transaction counter.
        /// </summary>
        public Dictionary<CryptographyMode, string> Ksns { get; set; }
    }
}
