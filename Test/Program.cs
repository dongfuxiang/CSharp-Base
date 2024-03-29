using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool r1 = true;
            bool r2 = false;
            bool r3=true;
            r3 &= r1 | r2;
            SysConfig config = new SysConfig();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SysConfig));
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, config);
                string contents = Encoding.UTF8.GetString(stream.ToArray());
                File.WriteAllText("test.xml", contents);
            };

        }
    }

    [Serializable]
    public class SysConfig
    {
        private string _saveResultDataFolder = "D:\\ResultData";
        private string _saveMIMFilesFolder = "D:\\RawDataFiles";
        private string _saveExceptionFolder = "D:\\ExceptionImage";
        private string _pgGammaTxtFolder = @"D:\Withsystem\WAgent\log\rcv\DEMURA_GAMMMA_DATA\";
        private int _saveResultDataTime = 3;
        private int _saveLogTime = 7;

        #region  其他参数
        [Category("其它设置"), DisplayName("单体编号"), Description("（设置当前单体的编号）")]
        public string SSourceType { get; set; } = "PLCBaseCtrl";

        [Category("其它设置"), DisplayName("单体编号"), Description("（设置当前单体的编号）")]
        public int DemuraUnitNum { get; set; } = 1;

        [Category("其它设置"), DisplayName("强制给断电OK"), Description("（功能开启后，当断电失败时,强制给OK）")]
        public bool AlwaysAckPowerOffOK { get; set; }

        [Category("其它设置"), DisplayName("使用时间码"), Description("(当PLC中无ID时,使用时间码)")]
        public bool UseDateTimeID { get; set; }
        #endregion

        #region 流程配置
        [Category("流程配置"), DisplayName("设置点灯后擦除流程"), Description("（上电完成后进行擦除操作，后续不进行擦除动作）")]
        public bool PowerOnAfterErase { get; set; } = false;

        [Category("流程配置"), DisplayName("屏蔽清晰度NG后重拍"), Description("检测过程中，如果清晰度超出阈值是否需要重拍，最多进行3次，true重拍，false不重拍")]
        public bool ArtNGRePhoto { get; set; } = true;

        [Category("流程配置"), DisplayName("需要重拍的画面限制的最大曝光值"), Description("检测过程中，在该设置的曝光值以下的画面如果清晰度NG会进行重拍动作")]
        public double RePhotoGrayExposure { get; set; } = 2000;

        [Category("流程配置"), DisplayName("定位图拍照稳定时间"), Description("检测过程中，定位图拍照稳定时间")]
        public int ThreadTimeAfterParticle { get; set; } = 100;

        [Category("流程配置"), DisplayName("屏蔽RGB画面清晰度判定"), Description("检测过程中，屏蔽RGB画面的清晰度判定")]
        public bool IsShieldRGBArtCheck { get; set; } = true;
        #endregion

        #region 文件保存设置
        [Category("文件保存设置"), DisplayName("是否保存MIM文件"), Description("（保存后生效，无需重启UI）")]
        public bool IsSaveMIM { get; set; }

        [Category("文件保存设置"), DisplayName("ResultData保存路径"), Description("（保存后生效，无需重启UI）")]
        public string SaveResultDataFolder
        {

            get { return _saveResultDataFolder; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _saveResultDataFolder = value;
                }
                else
                {

                }
            }
        }

        [Category("文件保存设置"), DisplayName("Gamma.Channel.txt文件地址"), Description("（保存后生效，无需重启UI）")]
        public string SaveGammaDataFolder
        {
            get { return _saveResultDataFolder; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _saveResultDataFolder = value;
                }
                else
                {

                }
            }
        }
        [Category("文件保存设置"), DisplayName("MIM文件保存路径"), Description("（保存后生效，无需重启UI）")]
        public string SaveMIMFilesFolder
        {
            get { return _saveMIMFilesFolder; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _saveMIMFilesFolder = value;
                }
                else
                {

                }
            }
        }
        [Category("文件保存设置"), DisplayName("异常图片文件保存路径"), Description("（保存后生效，无需重启UI）")]
        public string SaveExceptionFolder
        {
            get { return _saveExceptionFolder; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _saveExceptionFolder = value;
                }
                else
                {

                }
            }
        }
        [Category("文件保存设置"), DisplayName("Gamma文件下载路径"), Description("Gamma文件下载路径")]
        public string DwonGammaFolder { get; set; }

        [Category("文件保存设置"), DisplayName("文件保存时长"), Description("原图及检测结果保存的最长时间")]
        public int FileSaveTime { get; set; } = 30;

        [Category("文件保存设置"), DisplayName("Pg Gamma文件生成路径"), Description("（保存后生效，无需重启UI）")]
        public string PgGammaTxtPath
        {
            get { return _pgGammaTxtFolder; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _pgGammaTxtFolder = value;
                }
                else
                {

                }
            }
        }
        #endregion

        #region 屏蔽设置
        [Category("屏蔽设置"), DisplayName("屏蔽清晰度检测"), Description("（保存后生效，无需重启UI）")]
        public bool isShieldArtCheck { get; set; }
        [Category("屏蔽设置"), DisplayName("屏蔽灰度检测"), Description("（保存后生效，无需重启UI）")]
        public bool isShieldGrayCheck { get; set; }

        [Category("屏蔽设置"), DisplayName("屏蔽AA区检测"), Description("（保存后生效，无需重启UI）")]
        public bool isShieldAAera { get; set; }

        [Category("屏蔽设置"), DisplayName("屏蔽theta角调整"), Description("（保存后生效，无需重启UI）")]
        public bool isShieldAlignment { get; set; }

        [Category("屏蔽设置"), DisplayName("工艺屏蔽"), Description("屏蔽Demura工艺，包含拍照及算法")]
        public bool isShieldDemura { get; set; }

        [Category("屏蔽设置"), DisplayName("屏蔽相机灰尘检测"), Description("是否需要开启相机灰尘检测")]
        public bool IsShiledParticle { get; set; }

        [Category("屏蔽设置"), DisplayName("算法屏蔽"), Description("屏蔽算法，包含前后处理及烧录")]
        public bool isShieldAlg { get; set; }
        [Category("屏蔽设置"), DisplayName("算法版本比对屏蔽"), Description("屏蔽算法，包含前后处理及烧录")]
        public bool isPreAndBehCheck { get; set; } = true;
        [Category("屏蔽设置"), DisplayName("良率报警屏蔽"), Description("屏蔽算法，包含前后处理及烧录")]
        public bool isOKLianglv { get; set; } = true;
        #endregion       
    }
}
