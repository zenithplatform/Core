using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Helpers
{
    //This is part of the p2pcopy project, by Pablo hosted on https://github.com/psantosl/p2pcopy 
    public static class SizeConverter
    {
        public static string ConvertToSizeString(long size)
        {
            EnumUnitSize totalSizeUnit = SizeConverter.GetSuitableUnit(size);
            return string.Format("{0:#0.##} {1}", SizeConverter.ConvertToSize(
                size, totalSizeUnit), SizeConverter.GetUnitString(totalSizeUnit));
        }

        public static float ConvertToSize(long size, EnumUnitSize unit)
        {
            return (float)size / (float)unit;
        }

        static string GetUnitString(EnumUnitSize unit)
        {
            switch (unit)
            {
                case EnumUnitSize.SizeBytes: return "bytes";
                case EnumUnitSize.SizeKiloBytes: return "KB";
                case EnumUnitSize.SizeMegaBytes: return "MB";
                case EnumUnitSize.SizeGygaBytes: return "GB";
            }
            return string.Empty;
        }

        static EnumUnitSize GetSuitableUnit(long size)
        {
            if (size >= 0 && size < (long)EnumUnitSize.SizeKiloBytes)
                return EnumUnitSize.SizeBytes;
            else if (size >= (long)EnumUnitSize.SizeKiloBytes && size <= (long)EnumUnitSize.SizeMegaBytes)
                return EnumUnitSize.SizeKiloBytes;
            else if (size >= (long)EnumUnitSize.SizeMegaBytes && size <= (long)EnumUnitSize.SizeGygaBytes)
                return EnumUnitSize.SizeMegaBytes;
            else
                return EnumUnitSize.SizeGygaBytes;
        }
    }

    public enum EnumUnitSize
    {
        SizeBytes = 1,
        SizeKiloBytes = 1024,
        SizeMegaBytes = 1024 * 1024,
        SizeGygaBytes = 1024 * 1024 * 1024
    };
}
