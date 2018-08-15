using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace netcfg    
{
    
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct TCmdHead
    {
        public UInt16 cmd;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] mac;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct TNetWifiCfg{
        public UInt16 MTU;//: Byte; // 1	ADDH	模块地址高字节（默认00H）	00H-FFH
        //public byte ADDL;//: Byte; // 2	ADDL	模块地址低字节（默认00H）	00H-FFH
        public byte SPED;//: Byte; 
        public byte CHAN;//: Byte; // 4	CHAN	7、6、5：保留未用
        public byte OPTION;//: Byte; // 5	OPTION	7，    定点发送使能位（类MODBUS）
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct netdata
    {
        public byte DHCP; // ①　DHCP开启关闭（1byte）（00：关闭DHCP使用静态IP，01：打开DHCP）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] IP; // ②　设备静态IP（4byte）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] gateway; // ③　设备网关（4byte）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] mask; // ④　设备子网掩码（4byte）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] dnsServer; //⑤　本地静态DNS服务器（4byte）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] dnsServerBack; //⑥　本地静态备用DNS服务器（4byte）
        public UInt16 localport; // ⑤　本地端口（2byte）
        public byte domain_type;//0 目标IP  1域名
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] desc;//: array [0 .. 63] of Byte; // ⑥　目标IP\域名（64byte）
        public UInt16 objport; // ⑤　目标端口（2byte）
        public byte protocol;//: Byte;
        // ⑦　网络协议（1byte）（0：TCP Client\1：TCP Server\2：UDP Client\3：UDP Server）
        public byte com;//: Byte; // ⑨　串口参数（1byte）（网络串口参数与无线参数一致，但在上位机上对应一个设置项） ⑪　（1byte）（0: 8N1、1: 8O1、2: 8E1）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] boad;//3byte 此串口波特率为网络转串口波特率，应与无线参数设置中的串口波特率保持一致，如：无线参数和网口波特率设置均设置为115200bps，此选项应设置为115200。）        
        public byte link;//: Byte;
        /// / ⑩　短连接开关（1byte）      // a)	值为0表示关闭功能     // b)	值为1~255 表示延时关闭时间
        public UInt16 time_root;//: word;
        // 11　超时重启（2byte）    // a)	值为0表示关闭功能      // b)	值为60~65535 表示超时重启时间，单位秒
        public byte clear; //清除换存数据  0关闭  1开启 
        public byte reg_send_mode;//: Byte; // 13　注册包发送模式（1byte）（使用复选框的方式进行组合选择）
        // a)	值为0关闭
        // b)	值为1连接时发送MAC
        // c)	值为2连接时发送自定义数据
        // d)	值为3每包数据发送MAC
        // e)	值为4每包数据发送自定义数据
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] reg_data;//: array [0 .. 39] of Byte; // 12　自定义注册包数据（40byte）
        public byte regLen; //⑰　自定义注册包长度（1byte）    
        public byte hbType;//⑱　心跳包数据类型（1byte  0：网络心跳包，1：串口心跳包）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] Heartbeat;//: array [0 .. 39] of Byte; // 14　自定义心跳包数据（40byte）  
        public byte hbLen; //⑳　自定义心跳包长度（1byte） 
        public UInt16 HeartTime;//21　自定义心跳周期（2byte）
        // a)	值为0关闭
        // b)	值为1~65535 表示心跳包发送周期，单位秒
        public UInt16 MTU;
        //  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte Version;//: array [0 .. 4] of Byte;
        public byte E_Version;
        // 17　DTU型号描述（5byte），上位机可简单修改程序添加型号，每个型号对应5个字节版本数据。（其信息包
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Other;//保留字节
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct netdata_ex
    {
        public TCmdHead xCmd;
        public netdata xdata;
    }

    public struct netdata_ex_new
    {
        public TCmdHead xCmd;
        public netdata_new xdata;
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct netdata_new
    {
        public netdata xdata;
       // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] IP; // ②　目标真实ip（4byte）
        public IPEndPoint ipEndPoint;
    }
    public class funs{
        //将Byte转换为结构体类型
        public static byte[] StructToBytes(object structObj, int size)
        {
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷贝到byte 数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }
        public static void Delay1ms(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }
        public static string Int64ToIpAdreess(long IpAdress)
        {
            string[] data = new string[4];
            for (int i = 0; i<4; i++)
            {
                data[i] = Convert.ToString((byte)IpAdress);
                IpAdress >>= 8;
            }
           return string.Join(".",data); 
            
        }
        
        //将Byte转换为结构体类型
        public static object ByteToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }
        
        public static byte[] int2byte3(int value)
        {
            byte[] bytes = new byte[3];
            bytes[0] =(byte)( value >> 16 & 0xFF);
            bytes[1] = (byte)(value >> 8 & 0xFF);
            bytes[2] = (byte)(value & 0xFF);
            return bytes;
        }

        public static string byte3_str(byte[] value)
        {
            int xValue = (value[0] << 16) | (value[1] << 8) | (value[2]);
            return xValue.ToString();
        }


        /// <summary>  
        /// 截取字节数组  
        /// </summary>  
        /// <param name="srcBytes">要截取的字节数组</param>  
        /// <param name="startIndex">开始截取位置的索引</param>  
        /// <param name="length">要截取的字节长度</param>  
        /// <returns>截取后的字节数组</returns>  
        public static byte[] SubByte(byte[] srcBytes, int startIndex, int length)
        {
            System.IO.MemoryStream bufferStream = new System.IO.MemoryStream();
            byte[] returnByte = new byte[] { };
            if (length < 1) return returnByte;
            if (srcBytes == null) { return returnByte; }
            if (startIndex < 0) { startIndex = 0; }
            if (startIndex < srcBytes.Length)
            {
                //if (length < 1 || length > srcBytes.Length - startIndex) { length = srcBytes.Length - startIndex; }
                if (length > srcBytes.Length - startIndex) { length = srcBytes.Length - startIndex; }
                bufferStream.Write(srcBytes, startIndex, length);
                returnByte = bufferStream.ToArray();
                bufferStream.SetLength(0);
                bufferStream.Position = 0;
            }
            bufferStream.Close();
            bufferStream.Dispose();
            return returnByte;
        }

        public static string ByteArrayToIPAddress(byte[] bytes)
        {
            try
            {
                IPAddress ipa = new IPAddress(bytes);
                return ipa.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static byte[] copybyte(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }


        public static byte[] Mac2Byte(string mac)
        {//處理"0B-16-21-2C-37-42"
            byte[] _mac = new byte[6];
            for (int i = 0; i < _mac.Length; i++)
            {
                _mac[i] = Convert.ToByte(mac.Substring(i * 3, 2), 16);
            }
            return _mac;
        }

        public static string Byte2Mac(byte[] mac)
        {//返回"0B-16-21-2C-37-42"
            return BitConverter.ToString(mac);
        }

        public static string StrToHex(string mStr) //返回处理后的十六进制字符串 
        {
            return BitConverter.ToString(
            Encoding.Default.GetBytes(mStr)).Replace("-", " ");
        } /* StrToHex */

        public static string HexToStr(string mHex) // 返回十六进制代表的字符串 
        {
            mHex = mHex.Replace(" ", "");
            if (mHex.Length % 2 == 1) mHex = mHex.Insert(0, "0");
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            UInt16 xx;
            string xstr = "";
            for (int i = 0; i < mHex.Length; i += 2)
            {
                if (!UInt16.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out xx))
                {
                    xx = 0;
                }
                vBytes[i / 2] = (byte)xx;
                Console.WriteLine((char)xx);
                xstr += Convert.ToChar(xx);
                //if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2])) vBytes[i / 2] = 0;
            }
            //return ASCIIEncoding.Default.GetString(vBytes);
            string str = Encoding.ASCII.GetString(vBytes);
            return xstr;// Encoding.ASCII.GetString(vBytes);
        } /* HexToStr */

        public static byte[] HexString2Bytes(string mHex)
        {
            mHex = mHex.Replace(" ", "");
            if (mHex.Length % 2 == 1) mHex = mHex.Insert(0, "0");
            byte[] vBytes = new byte[mHex.Length / 2];
            UInt16 xx;
            for (int i = 0; i < mHex.Length; i += 2)
            {
                if (!UInt16.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out xx))
                {
                    xx = 0;
                }
                vBytes[i / 2] = (byte)xx;                
            }
            return vBytes;
        }
        public static byte[] IpToByte(string ip)
        {
            char[] separator = new char[] { '.' };
            string[] items = ip.Split(separator);
            byte[] IPBuf = new byte[4];
            IPBuf[0] = byte.Parse(items[0]);
            IPBuf[1] = byte.Parse(items[1]);
            IPBuf[2] = byte.Parse(items[2]);
            IPBuf[3] = byte.Parse(items[3]);
            return IPBuf;
        }

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>        
        public static bool IsIP(string ip)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
                {
                    string[] ips = ip.Split('.');
                    if (ips.Length == 4 || ips.Length == 6)
                    {
                        if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;

                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPort(string port)
        {
            int xPort = int.Parse(port);
            return (xPort>=1)&&(xPort<65536);
        }
        public static bool IsEdTimeout(string port)
        {
            int xPort = int.Parse(port);
            return (xPort >= 60) && (xPort < 65536)|| (xPort == 0);
        }
        public static bool IsEdLink(string port)
        {
            int xPort = int.Parse(port);
            return (xPort >= 2) && (xPort < 256) || (xPort == 0);
        }
        public static bool IsBaudrate(string port)
        {
            int xPort = int.Parse(port);
            return (xPort >= 1200) && (xPort <= 921600);
        }
        public static bool IsEdheattime(string port)
        {
            int xPort = int.Parse(port);
            return (xPort >= 2) && (xPort <= 65535)||(xPort==0);
        }
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2")+ " ";
                }
            }
            return returnStr;
        }

        public static string StrToHex1(string mStr) //返回处理后的十六进制字符串 
        {
            //byte[] bs = new byte[mStr.Length];
            string returnStr = "";
            for (int i = 0; i < mStr.Length; i++) 
            { 
                byte bItem  = Convert.ToByte(mStr[i]);
                returnStr += bItem.ToString("X2")+ " ";
            }
            return returnStr;
            //return byteToHexStr(Encoding.ASCII.GetBytes(mStr));
            //return byteToHexStr(Encoding.UTF8.GetBytes(mStr)).Replace("-", " ");
        } /* StrToHex */
    }

}
