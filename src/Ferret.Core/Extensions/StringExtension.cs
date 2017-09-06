using Ferret.Core.Ksn;
using System;
using System.Linq;

namespace Ferret.Core
{
    static internal class StringExtension
    {
        static public AcquirerCode ToAcquirerCode(this string self)
        {
            AcquirerCode[] acquirerValues = Enum.GetValues(typeof(AcquirerCode))
                .Cast<AcquirerCode>()
                .ToArray();

            foreach (AcquirerCode currentAcquirerName in acquirerValues)
            {
                if (currentAcquirerName.ToString().ToUpper().Contains(self.ToUpper()) == true)
                {
                    return currentAcquirerName;
                }
            }

            return AcquirerCode.Undefined;
        }
    }
}
