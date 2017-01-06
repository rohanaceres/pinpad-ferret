﻿using Ferret.View.KsnStuff.TypeCode;

namespace Ferret.View.KsnStuff
{
    /// <summary>
    /// Information obtained from the GDU command, from ABECS specs.
    /// </summary>
    internal struct Acquirer
    {
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
        /// Encryption method.
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Key Serial Number (KSN) is returned from the encrypting device, 
        /// along with the cryptogram. The KSN is formed from the device's unique 
        /// identifier, and an internal transaction counter.
        /// </summary>
        public string Ksn { get; set; }
    }
}
