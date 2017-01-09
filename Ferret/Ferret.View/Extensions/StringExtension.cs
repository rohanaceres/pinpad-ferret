using Ferret.View.KsnStuff.TypeCode;
using System;
using System.Linq;

namespace Ferret.View.Extensions
{
    internal static class StringExtension
    {
        public static AcquirerCode ToAcquirerCode(this string self)
        {
            AcquirerCode[] acquirerValues = Enum.GetValues(typeof(AcquirerCode))
                .Cast<AcquirerCode>()
                .ToArray();

            foreach (AcquirerCode currentAcquirerName in acquirerValues)
            {
                if (currentAcquirerName.ToString().Contains(self) == true)
                {
                    return currentAcquirerName;
                }
            }

            return AcquirerCode.Undefined;
        }
    }
}
