using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace performance
{
    [Serializable]
   public class RMSData
    {/// <summary>
     /// PG\TP版本比对是否开启
     /// </summary>

        public bool IsPgTpVerCheckOn { get; set; } = true;
        /// <summary>
        /// 单体1AOI检测是否开启
        /// </summary>
        public bool IsFirstDiskAoiOn { get; set; } = true;
        /// <summary>
        /// 单体2AOI检测是否开启
        /// </summary>
        public bool IsSecondDiskAoiOn { get; set; } = true;
        /// <summary>
        ///单体1MURA检测是否开启
        /// </summary>
        public bool IsFirstDiskMuraOn { get; set; } = true;
        /// <summary>
        /// 单体2MURA检测是否开启
        /// </summary>
        public bool IsSecondDiskMuraOn { get; set; } = true;
        /// <summary>
        /// 单体1PreGamma检测是否开启
        /// </summary>
        public bool IsFirstDiskPreGammaOn { get; set; } = true;
        /// <summary>
        /// 单体2PreGamma检测是否开启
        /// </summary>   
        public bool IsSecondDiskPreGammaOn { get; set; } = true;
        /// <summary>
        /// 单体1TP检测是否开启
        /// </summary>
        public bool IsFirstDiskTPOn { get; set; } = true;
        /// <summary>
        /// 单体2TP检测是否开启
        /// </summary>
        public bool IsSecondDiskTPOn { get; set; } = true;
        /// <summary>
        /// PCIM是否开启
        /// </summary>
        public bool IsPcimOn { get; set; } = true;
        /// <summary>
        /// 连续NG是否启用
        /// </summary>
        public bool IsContinusNGAlarmOn { get; set; } = true;
        /// <summary>
        /// 卡口检是否开启
        /// </summary>
        public bool IsLineOn { get; set; } = true;
        /// <summary>
        /// EFU是否开启
        /// </summary>
        public bool IsEFUOn { get; set; } = true;
    }
}
