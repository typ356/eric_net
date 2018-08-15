using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace netcfg
{
    class translate
    {   
        public static Dictionary<string, Dictionary<string,string[]>> infos;
        public static int cur_language;
        public static void doInit()
        {//infos[lang][form][key]
            infos=new Dictionary<string, Dictionary<string,string[]>>();
            Dictionary<string, string[]> form1_Info = new Dictionary<string, string[]>();
            Dictionary<string, string[]> form2_Info = new Dictionary<string, string[]>();
            Dictionary<string, string[]> form3_Info = new Dictionary<string, string[]>();
            Dictionary<string, string[]> E70_Info = new Dictionary<string, string[]>();
            Dictionary<string, string[]> HomePage_Info = new Dictionary<string, string[]>();
            infos.Add("form1", form1_Info);
            infos.Add("form2", form2_Info);
            infos.Add("form3", form3_Info);
            infos.Add("e70", E70_Info);
            infos.Add("HomePage", HomePage_Info);


            form1_Info.Add("caption",new string[]{"E810配置软件V1.2", "E810 Configuratio V1.2" }); //先是中文，后是英文
            form1_Info.Add("toolStripButton1", new string[] { "搜索设备", "Search" });
            form1_Info.Add("toolStripButton2", new string[] { "配置设备", "Configuration" });
            form1_Info.Add("toolStripButton6", new string[] { "重启设备", "Reboot" });
            form1_Info.Add("toolStripButton5", new string[] { "修改mac地址", "Modify Mac" });
            form1_Info.Add("toolStripButton3", new string[] { "清空设备", "Clear All" });
            form1_Info.Add("toolStripButton4", new string[] { "退出", "Exit" });
            form1_Info.Add("toolStripDropDownButton1", new string[] { "语言", "Language" });            
            form1_Info.Add("Columns0", new string[] { "序号", "Seq" });
            form1_Info.Add("Columns1", new string[] { "Mac地址", "Mac" });
            form1_Info.Add("Columns2", new string[] { "网络协议", "NetProtocol" });
            form1_Info.Add("Columns3", new string[] { "目标IP", "RemoteIP " });
            form1_Info.Add("Columns4", new string[] { "目标端口", "RemotePort" });
            form1_Info.Add("Columns5", new string[] { "本地IP", "LocalIP" });
            form1_Info.Add("Columns6", new string[] { "本地端口","LocalPort" });
            form1_Info.Add("Columns7", new string[] { "型号", "Model" });
            form1_Info.Add("Columns8", new string[] { "版本", "Version" });
            form1_Info.Add("btn2_show", new string[] { "请选中要设置的设备", "Please select the device to set" });
            form1_Info.Add("btn6_show", new string[] { "请选中要重新启动的设备", "Please select the device to reboot" });
            form1_Info.Add("btn5_show", new string[] { "请选中要修改Mac地址的设备", "Please select the device to modify the Mac address" });
            // form1_Info.Add("cmd_result_cfg", new string[] { "配置指令返回:", "Configuration instruction return:" });
            form1_Info.Add("cmd_result_cfg", new string[] { "    配置成功！", "Configuration successful!" });

            form1_Info.Add("cmd_result_mac", new string[] { "     配置成功！", "Configuration successful!" });
            form1_Info.Add("cmd_result_reboot", new string[] { "      重启成功！", "Reboot successful!" });
            form1_Info.Add("cmd_error", new string[] { "当前版本不支持此模块，请更新配置软件版本！", "The current version does not support this module, please update the configuration software version!" });

            form3_Info.Add("caption", new string[] { "Mac地址配置", "Modify Mac" }); //先是中文，后是英文
            form3_Info.Add("btn_ok", new string[] { "确定", "OK" }); 
            form3_Info.Add("btn_cancel", new string[] { "取消", "Cancel" });
            form3_Info.Add("msg", new string[] { "请检查Mac地址是否正确", "Please check whether the Mac is correct" });
            
            form2_Info.Add("caption", new string[] { "参数配置", "" }); //先是中文，后是英文
            form2_Info.Add("btn_ok", new string[] { "确定", "OK" });
            form2_Info.Add("btn_cancel", new string[] { "取消", "Cancel" });
            form2_Info.Add("btn6_null", new string[] { "请选择型号", "Cancel" });
            form2_Info.Add("label13", new string[] { "IP地址类型", "IP Type" });
            form2_Info.Add("label1", new string[] { "静态IP地址", "Static IP" });
            form2_Info.Add("label2", new string[] { "子网掩码", "Subnet Mask" });
            form2_Info.Add("label3", new string[] { "网关", "Gateway" });
            form2_Info.Add("label30", new string[] { "静态DNS服务器", "Static DNS Server" });
            form2_Info.Add("label29", new string[] { "静态备用DNS服务器", "Static Standby DNS Server" });
            form2_Info.Add("label28", new string[] { "本地端口", "LocalPort" });
            form2_Info.Add("label6", new string[] { "目标端口", "RemotePort" });
            form2_Info.Add("label32", new string[] { "目标类型", "Target Type" });
            form2_Info.Add("label5", new string[] { "目标IP/域名", "RemoteIP/Domain Name" });
            form2_Info.Add("label14", new string[] { "协议", "Protocol" });
            form2_Info.Add("label4", new string[] { "串口波特率", "BaudRate" });
            form2_Info.Add("label12", new string[] { "串口参数", "Length/Parity" });
            form2_Info.Add("label11", new string[] { "短连接开关", "Short Link Switch" });
            form2_Info.Add("label10", new string[] { "超时重启", "Timeout To Reboot" });
            form2_Info.Add("label15", new string[] { "注册包发送模式", "Register Package Send Mode" });
            form2_Info.Add("label9", new string[] { "自定义注册包数据", "Customize Registry" });
            form2_Info.Add("label8", new string[] { "自定义心跳包数据", "Custom Heartbeat Packet Data" });
            form2_Info.Add("label16", new string[] { "工作模式", "Mode" });
            form2_Info.Add("label17", new string[] { "模块地址", "Module Addresses" });
            form2_Info.Add("label18", new string[] { "清除缓存数据", "Wipe Cache Partition" });            
            form2_Info.Add("label7", new string[] { "心跳包发送周期", "Heartbeat Packet Delivery Cycle" });
            form2_Info.Add("ck_heath", new string[] { "16进制", "Hex" });
            form2_Info.Add("ck_reg", new string[] { "16进制", "Hex" });
            form2_Info.Add("label31", new string[] { "心跳包数据类型", "Heartbeat Packet Data Type" });
            form2_Info.Add("label26", new string[] { "发射功率", "" });
            form2_Info.Add("label27", new string[] { "FEC开关", "" });
            form2_Info.Add("label23", new string[] { "唤醒时间", "" });
            form2_Info.Add("label24", new string[] { "前向纠错", "" });
            form2_Info.Add("label25", new string[] { "传输模式", "Transmission Mode" });
            form2_Info.Add("label22", new string[] { "通信信道", "" });
            form2_Info.Add("label21", new string[] { "空中速率", "" });
            
            form2_Info.Add("label20", new string[] { "校验位", "Parity" });
            form2_Info.Add("label19", new string[] { "波特率", "Baudrate" });

            form2_Info.Add("RF_label0", new string[] { "工作模式", "Mode" });
            form2_Info.Add("RF_label1", new string[] { "模块地址", "Addresses" });
            form2_Info.Add("RF_label2", new string[] { "波特率", "BaudRate" });
            form2_Info.Add("RF_label3", new string[] { "校验位", "Parity" });
            form2_Info.Add("RF_label4", new string[] { "频率信道", "Chan" });
            form2_Info.Add("RF_label5", new string[] { "空中速率", "Air Rate" });
            form2_Info.Add("RF_label6", new string[] { "唤醒时间", "Wake-up Time" });
            form2_Info.Add("RF_label7", new string[] { "前向纠错", "FEC" });
            form2_Info.Add("RF_label8", new string[] { "传输模式", "Transmission Mode" });
            form2_Info.Add("RF_label9", new string[] { "发射功率", "RF Power" });

            form2_Info.Add("groupBox2", new string[] { "速率参数", "" });
            form2_Info.Add("groupBox1", new string[] { "无线配置参数", "" });
            form2_Info.Add("msg0", new string[] { "请检测IP地址是否正确！", "Please check the IP address!" });
            form2_Info.Add("msg1", new string[] { "请检测Mask地址是否正确！", "Please check if the Mask address is correct!" });
            form2_Info.Add("msg2", new string[] { "请检测网关地址是否正确！", "Please check if the gateway address is correct!" });
            form2_Info.Add("msg3", new string[] { "请检测DNS地址是否正确！", "Please check if DNS address is correct!" });
            form2_Info.Add("msg4", new string[] { "请检测备用DNS地址是否正确！", "Please check if the standby DNS address is correct!" });
            form2_Info.Add("msg5", new string[] { "请检测端口是否正确！", "Please check if the port is correct！" });
            form2_Info.Add("msg6", new string[] { "请检测本地端口是否正确！", "Please check if the local port is correct!" });
            form2_Info.Add("msg7", new string[] { "请检测DTU型号描述是否正确！", "" });
            form2_Info.Add("msg8", new string[] { "请检测模块地址是否正确！", "Please check whether the address of the module is correct!" });
            form2_Info.Add("msg9", new string[] { "请检范围是否正确！", "Please check whether the scope is correct!" });
            
            form2_Info.Add("cmIPmode", new string[] { "", "" });
            form2_Info.Add("edIP", new string[] { "", "" });
            form2_Info.Add("edMask", new string[] { "", "" });
            form2_Info.Add("edGaway", new string[] { "", "" });
            form2_Info.Add("eddns", new string[] { "", "" });
            form2_Info.Add("eddnsBack", new string[] { "", "" });
            form2_Info.Add("edlocalport", new string[] { "端口范围是1~65535", "Port:1~65535" });
            form2_Info.Add("edPort", new string[] { "端口范围是1~65535", "Port:1~65535" });
            form2_Info.Add("cmObj", new string[] { "", "" });
            form2_Info.Add("edDomain", new string[] { "", "" });
            form2_Info.Add("cmProtocol", new string[] { "", "" });
            form2_Info.Add("edBord", new string[] { "1200~921600bps", "1200~921600bps" });
            form2_Info.Add("comboBox1", new string[] { "", "" });
            form2_Info.Add("edLink", new string[] { "0关闭，2~255秒", "0，2~255" });
            form2_Info.Add("edTimeout", new string[] {"0关闭，60~65535秒", "0，60~65535" });
            form2_Info.Add("cmRegMode", new string[] { "", "" });
            form2_Info.Add("edregdata", new string[] { "", "" });
            form2_Info.Add("edheatdata", new string[] { "0关闭，2~255秒", "0，2~255" });
            form2_Info.Add("edDTU", new string[] { "", "" });
            form2_Info.Add("edheattime", new string[] { "0关闭，2~65535秒", "0，2s~65535" });
            form2_Info.Add("cmhbType", new string[] { "", "" });
            form2_Info.Add("cmpower", new string[] { "", "" });
            form2_Info.Add("cmFEC", new string[] { "", "" });
            form2_Info.Add("cmWakeTime", new string[] { "", "" });
            form2_Info.Add("cmIOMode", new string[] { "", "" });
            form2_Info.Add("cmMode", new string[] { "", "" });
            form2_Info.Add("edChan", new string[] { "输入10进制", "Dec" });
            form2_Info.Add("cmSpeed", new string[] { "", "" });
            form2_Info.Add("comBoadInfo", new string[] { "", "" });
            form2_Info.Add("cmComInfo", new string[] { "", "" });
            form2_Info.Add("edaddh", new string[] { "输入16进制", "Hex" });

            form2_Info.Add("cmIPmode_items", new string[] { "静态IP\r\n动态IP", "Static IP\r\nDynamic IP" });
            form2_Info.Add("cmObj_items", new string[] { "目标IP\r\n域名", "RemoteIP\r\nDomain" });
            form2_Info.Add("cmProtocol_items", new string[] { "TCP Client\r\nTCP Server\r\nUDP Client\r\nUDP Server", "TCP Client\r\nTCP Server\r\nUDP Client\r\nUDP Server" });
            form2_Info.Add("comboBox1_items", new string[] { "8N1\r\n8O1\r\n8E1", "8N1\r\n8O1\r\n8E1" });
            form2_Info.Add("cmRegMode_items", new string[] { "关闭\r\n连接时发送MAC\r\n连接时发送自定义数据\r\n每包数据发送MAC\r\n每包数据发送自定义数据", "Close\r\nSend a MAC when you connect\r\nSend custom data when connected\r\nEach packet of data sends a MAC\r\nEach packet of data sends a custom data" });
            form2_Info.Add("cmComInfo_items", new string[] { "8N1\r\n8O1\r\n8E1", "8N1\r\n8O1\r\n8E1" });
            form2_Info.Add("cmMode_items", new string[] { "透明传输模式\r\n定点传输模式", "" });
            form2_Info.Add("cmIOMode_items", new string[] { "开路输出，RXD开路输入\r\n推挽输出，RXD上拉输入", "" });
            form2_Info.Add("cmFEC_items", new string[] { "关闭FEC纠错\r\n打开FEC纠错", "" });
            form2_Info.Add("comboBox2_items", new string[] { "关闭\r\n打开", "Close\r\nOpen" });
            form2_Info.Add("cmhbType_items", new string[] { "网络心跳包\r\n串口心跳包", "Network Heartbeat Packet\r\nSerial Heartbeat Packet" });
            form2_Info.Add("tabPage1", new string[] { "网口设置", "Ehernet Setting" });
            form2_Info.Add("tabPage2", new string[] { "射频设置", "RF Setting" });
            form2_Info.Add("button3", new string[] { "读取参数", "Read OpenParameters" });
            form2_Info.Add("button4", new string[] { "恢复出厂设置", "Factory Settings" });



            E70_Info.Add("button_SerialOpen", new string[] { "打开串口", "Open" });
            E70_Info.Add("button_SerialClose", new string[] { "关闭串口", "Close" });
            E70_Info.Add("button_WindowsSave", new string[] { "保存窗口", "Save" });
            E70_Info.Add("button_RecvClear", new string[] { "清除接收", "Clear Recv" });
            E70_Info.Add("button_Send", new string[] { "发送", "Send" });
            E70_Info.Add("button_SendClear", new string[] { "清除发送", "Clear Send" });
            E70_Info.Add("button_AT0", new string[] { "进入AT", "Enter AT Mode " });
            E70_Info.Add("button_AT1", new string[] { "退出AT", "Exit AT Mode" });
            E70_Info.Add("button_AT2", new string[] { "帮助", "HELP" });
            E70_Info.Add("button_AT3", new string[] { "恢复默认参数", "Factory Settings " });
            E70_Info.Add("button_AT4", new string[] { "重启设备", "Reboot" });
            E70_Info.Add("button_AT5", new string[] { "读取版本", "Read Version" });
            E70_Info.Add("button_AT6", new string[] { "清除网络信息", "Clear Network Information" });
            E70_Info.Add("button_AT7", new string[] { "查询", "Inquire" });
            E70_Info.Add("button_AT8", new string[] { "查询", "Inquire" });
            E70_Info.Add("button_AT9", new string[] { "查询", "Inquire" });
            E70_Info.Add("button1", new string[] { "上一页", "Page Up" });
            E70_Info.Add("button2", new string[] { "下一页", "Page Down" });
            E70_Info.Add("button3", new string[] { "多条发送", "More Than Send" });
            E70_Info.Add("button_PageFist", new string[] { "首页", "First Page" });
            E70_Info.Add("button_PageSub", new string[] { "上一页", "Page Up" });
            E70_Info.Add("button_PageAdd", new string[] { "下一页", "Page Up" });
            E70_Info.Add("button_PageEnd", new string[] { "尾页", "End Page" });

            E70_Info.Add("label_bz", new string[] { "备注：", "Notes:" });
            E70_Info.Add("label_Port", new string[] { "端口号：", "COM:" });
            E70_Info.Add("label_Baud", new string[] { "波特率：", "BaudRate:" });
            E70_Info.Add("label_StopBit", new string[] { "停止位：", "StopBits:" });
            E70_Info.Add("label_DataBit", new string[] { "数据位：", "DataBits:" });
            E70_Info.Add("label_Parity", new string[] { "校验位：", "Parity:" });
            E70_Info.Add("label6", new string[] { "串口操作", "Open/Close" });
            E70_Info.Add("label2", new string[] { "休眠时间：", "Dormancy Time:" });
            E70_Info.Add("label3", new string[] { "自动重启时间：", "Restart Time:" });

            E70_Info.Add("checkBox2", new string[] { "定时发送", "Circle Send" });
            E70_Info.Add("checkBox1", new string[] { "发送新行", "Send Newline" });

            E70_Info.Add("tabPage1", new string[] { "单条发送", "Send Single" });
            E70_Info.Add("tabPage2", new string[] { "AT发送", "AT Send" });
            E70_Info.Add("tabPage3", new string[] { "自定义多条发送", "More Message to Send" });


            E70_Info.Add("comboBox_AT0_Dis", new string[] { "查询当前的工作模式\r\n协调器\r\n普通节点\r\n休眠节点\r\n休眠模式",
                "Gets working mode\r\nCoordinator\r\nNormal Node\r\nDormant Node\r\nSleep Mode" });
            E70_Info.Add("comboBox_AT0_Val", new string[] { "AT+WMCFG=?\r\nAT+WMCFG=0\r\nAT+WMCFG=1\r\nAT+WMCFG=2\r\nAT+WMCFG=3",
                "AT+WMCFG=?\r\nAT+WMCFG=0\r\nAT+WMCFG=1\r\nAT+WMCFG=2\r\nAT+WMCFG=3" });
            E70_Info.Add("comboBox_AT8_Dis", new string[] { "获取所有节点的短地址和长地址\r\n获取自身的短地址\r\n获取自身的长地址",
                "Query the short and long address of all nodes\r\nGet short address\r\nGet long address." });
            E70_Info.Add("comboBox_AT8_Val", new string[] { "AT+DINFO=ALLNODE\r\nAT+DINFO=SELFS\r\nAT+DINFO=SELFE",
                "AT+DINFO=ALLNODE\r\nAT+DINFO=SELFS\r\nAT+DINFO=SELFE" });
            E70_Info.Add("comboBox_AT9_Dis", new string[] { "获取当前输出传输格式配置\r\n输出:有效数据（透传）\r\n输出:有效数据+发送设备长地址\r\n输出:有效数据+发送设备短地址 \r\n输出:有效数据+RSSI\r\n输出:有效数据+发送设备长地址+发送设备短地址\r\n输出:有效数据+发送设备长地址+RSSI\r\n输出:有效数据+发送设备短地址+RSSI\r\n输出:有效数据+发送设备长地址+发送设备短地址+RSSI",
                "Gets output format configuration\r\nValid Data\r\nValid Data +Long Address\r\nValid Data +Short Address\r\nValid Data+RSSI\r\nValid Data+Long Address+Short Address\r\nValid Data+Long Address+RSSI\r\nValid Data+Short Address+RSSI\r\nValid Data+Long Address+Short Address+RSSI" });
            E70_Info.Add("comboBox_AT9_Val", new string[] { "AT+TFOCFG=?\r\nAT+TFOCFG=0\r\nAT+TFOCFG=1\r\nAT+TFOCFG=2\r\nAT+TFOCFG=3\r\nAT+TFOCFG=4\r\nAT+TFOCFG=5\r\nAT+TFOCFG=6\r\nAT+TFOCFG=7",
               "AT+TFOCFG=?\r\nAT+TFOCFG=0\r\nAT+TFOCFG=1\r\nAT+TFOCFG=2\r\nAT+TFOCFG=3\r\nAT+TFOCFG=4\r\nAT+TFOCFG=5\r\nAT+TFOCFG=6\r\nAT+TFOCFG=7"});
            E70_Info.Add("comboBox_AT7_Dis", new string[] { "获取当前输入传输格式配置\r\n广播(仅协调器有效)\r\n接收设备短地址 + 数据(仅协调器有效)\r\n接收设备长地址 + 数据",
                "Gets input transport format configuration\r\nBroadcast(Only the coordinator works)\r\nShort Address+Data(Only the coordinator works)\r\nLong Address+Data" });
            E70_Info.Add("comboBox_AT7_Val", new string[] { "AT+TFICFG=?\r\nAT+ TFICFG=0\r\nAT+TFICFG=1\r\nAT+TFICFG=2",
                "AT+TFICFG=?\r\nAT+ TFICFG=0\r\nAT+TFICFG=1\r\nAT+TFICFG=2" });
            E70_Info.Add("comboBox_AT4_Dis", new string[] { "获取当前的传输模式配置\r\n长距离模式，LRM\r\n标准传输模式，GFSK",
                "Gets transport mode configuration\r\nLong Range mode，LRM\r\nGaussian Frequency Shift Keying，GFSK" });
            E70_Info.Add("comboBox_AT4_Val", new string[] { "AT+TMCFG=?\r\nAT+TMCFG=0\r\nAT+TMCFG=1",
                "AT+TMCFG=?\r\nAT+TMCFG=0\r\nAT+TMCFG=1" });
            E70_Info.Add("comboBox_AT5_Dis", new string[] { "获取当前设备串口波特率参数配置\r\n1200\r\n2400\r\n4800\r\n9600\r\n19200\r\n38400\r\n57600\r\n115200",
                "Obtains the baud rate parameter\r\n1200\r\n2400\r\n4800\r\n9600\r\n19200\r\n38400\r\n57600\r\n115200" });
            E70_Info.Add("comboBox_AT5_Val", new string[] { "AT+UBCFG=?\r\nAT+UBCFG=0\r\nAT+UBCFG=1\r\nAT+UBCFG=2\r\nAT+UBCFG=3\r\nAT+UBCFG=4\r\nAT+UBCFG=5\r\nAT+UBCFG=6\r\nAT+UBCFG=7",
                "AT+UBCFG=?\r\nAT+UBCFG=0\r\nAT+UBCFG=1\r\nAT+UBCFG=2\r\nAT+UBCFG=3\r\nAT+UBCFG=4\r\nAT+UBCFG=5\r\nAT+UBCFG=6\r\nAT+UBCFG=7" });
            E70_Info.Add("comboBox_AT6_Dis", new string[] { "获取当前设备串口校验位参数配置\r\n无校验\r\n奇校验\r\n偶校验",
                "Gets the device parity parameter\r\nNone\r\nOdd\r\nEven" });
            E70_Info.Add("comboBox_AT6_Val", new string[] { "AT+UPCFG=?\r\nAT+UPCFG=0\r\nAT+UPCFG=1\r\nAT+UPCFG=2",
                "AT+UPCFG=?\r\nAT+UPCFG=0\r\nAT+UPCFG=1\r\nAT+UPCFG=2" });
            E70_Info.Add("comboBox_AT3_Dis", new string[] { "获取当前设备功率参数配置\r\n极高\r\n高 \r\n中 \r\n低",
                "Gets the power parameter\r\nPolar Altitude\r\nHigh \r\nMedium \r\nLow" });
            E70_Info.Add("comboBox_AT3_Val", new string[] { "AT+PWCFG=?\r\nAT+PWCFG=0\r\nAT+PWCFG=1\r\nAT+PWCFG=2\r\nAT+PWCFG=3",
                "AT+PWCFG=?\r\nAT+PWCFG=0\r\nAT+PWCFG=1\r\nAT+PWCFG=2\r\nAT+PWCFG=3" });
            E70_Info.Add("comboBox_AT1_Dis", new string[] { "获取当前设备IO口参数配置\r\n推挽 \r\n开漏",
                "Gets the IO parameter\r\nPush-Pull \r\nOpen-Drain" });
            E70_Info.Add("comboBox_AT1_Val", new string[] { "AT+IOCFG=?\r\nAT+IOCFG=0\r\nAT+IOCFG=1",
                "AT+IOCFG=?\r\nAT+IOCFG=0\r\nAT+IOCFG=1" });
            E70_Info.Add("comboBox_AT2_Dis", new string[] { "关闭回显\r\n打开回显",
                "Close the echo\r\nOpen the echo" });
            E70_Info.Add("comboBox_AT2_Val", new string[] { "AT+ECHO=1\r\nAT+ECHO=0",
                "AT+ECHO=1\r\nAT+ECHO=0" });
            E70_Info.Add("comboBox_AT10_Dis", new string[] { "获取当前模块并发性能参数配置\r\n并发性能低 \r\n并发性能中\r\n并发性能高\r\n并发性能极高",
                "Gets the concurrency performance parameter\r\nLow concurrency\r\nMedium concurrency\r\nHigh concurrency\r\nHighest concurrency" });
            E70_Info.Add("comboBox_AT10_Val", new string[] { "AT+TLCFG=?\r\nAT+TLCFG=0\r\nAT+TLCFG=1\r\nAT+TLCFG=2\r\nAT+TLCFG=3",
                "AT+TLCFG=?\r\nAT+TLCFG=0\r\nAT+TLCFG=1\r\nAT+TLCFG=2\r\nAT+TLCFG=3" });


            //comboBox_AT0
            HomePage_Info.Add("toolStripDropDownButton2", new string[] { "型号选择", "Model Selection" });
            HomePage_Info.Add("toolStripButton1", new string[] { "退出", "Exit" });
            HomePage_Info.Add("HomePage", new string[] { "亿佰特网络配置工具_V1.6", "Ebyte Configuration Tools_V1.6" });
        }


        public static string getInfo(string form,string key){
           Dictionary<string,string[]> form_info;
           if (infos.TryGetValue(form, out form_info)){
               string [] values ;
               if (form_info.TryGetValue(key,out values)){
                   return values[cur_language];
               }
           }

            return "";
        }
    }
}
