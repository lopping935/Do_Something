using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAlgorithmMES
{
    public class messagecls
    {
        public messagecls()
        {

        }
        public struct LabelData
        {
            public string MACHINE_NO;// 打包机组号
            public string ID_TIME;
            public string ID_LOT_PROD;//生产批号
            public Int16 ID_PART_LOT; //分批号
            public Int16 NUM_BDL;//捆号
            public Int16 SEQ_LEN;//长度顺序号
            public Int16 SEQ_OPR;//操作顺序号
            public double DIM_LEN; //米长
            public string IND_FIXED;// 定尺标志
            public double SEQ_SEND;// 下发顺序号
            public Int16 NUM_BAR;// 捆内支数
            public Int16 SEQ_LIST;// 排列序号
            public double LA_BDL_ACT;// 重量
            public string NO_LICENCE;// 许可证号
            public string NAME_PROD; //产品名称
            public string NAME_STLGD;// 执行标准
            public string ID_HEAT; //熔炼号
            public string NAME_STND; //钢牌号
            public string DES_FIPRO_SECTION; //断面规格描述
            public string ID_CREW_RL;// 轧制班别
            public string ID_CREW_CK;// 检查班别
            public string TMSTP_WEIGH;// 生产日期
            public string BAR_CODE; //条码内容
            public Int16 NUM_HEAD;//头签个数
            public Int16 NUM_TAIL;// 尾签个数
            public string TMSTP_SEND;// 发送时间
        };
        public enum EncodingType { UTF7, UTF8, UTF32, Unicode, BigEndianUnicode, ASCII, GB2312, GBK, ISO8859 ,defaul};
        public static string GetString(byte[] myByte, EncodingType encodingType)
        {
            string str = null;
            switch (encodingType)
            {
                //将要加密的字符串转换为指定编码的字节数组
                case EncodingType.UTF7:
                    str = Encoding.UTF7.GetString(myByte);
                    break;
                case EncodingType.UTF8:
                    str = Encoding.UTF8.GetString(myByte);
                    break;
                case EncodingType.UTF32:
                    str = Encoding.UTF32.GetString(myByte);
                    break;
                case EncodingType.Unicode:
                    str = Encoding.Unicode.GetString(myByte);
                    break;
                case EncodingType.BigEndianUnicode:
                    str = Encoding.BigEndianUnicode.GetString(myByte);
                    break;
                case EncodingType.ASCII:
                    str = Encoding.ASCII.GetString(myByte);
                    // str = Encoding.;
                    break;
                case EncodingType.GB2312:
                    str = Encoding.Default.GetString(myByte);
                    break;
                case EncodingType.GBK:
                    str = System.Text.Encoding.GetEncoding("GBK").GetString(myByte);
                    break;
                case EncodingType.ISO8859:
                    str = System.Text.Encoding.GetEncoding("ISO8859-1").GetString(myByte);
                    break;
                
            }
            return str;
        }
        
    }
}
