using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Drawing;

namespace SDK
{
    public interface IMagService
    {
        bool Initialize();
        void DeInitialize();
        bool IsInitialized();

        bool IsLanConnected();
        bool IsUsingStaticIp();
        uint GetLocalIp();

        bool IsDHCPServerRunning();
        bool StartDHCPServer();
        void StopDHCPServer();

        void EnableAutoReConnect(bool bEnable);

        bool EnumCameras();
        uint GetTerminalList(GroupSDK.ENUM_INFO[] list, uint unit_count);
        uint GetTerminalCount();

        int CompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize, uint intQuality);
        int DeCompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize);
    }

    public interface IMagDevice
    {
        bool Initialize();
        void DeInitialize();
        bool IsInitialized();

        GroupSDK.CAMERA_INFO GetCamInfo();
        GroupSDK.CAMERA_INFO_EX GetCamInfoEx();
        GroupSDK.CAMERA_REGCONTENT GetRegContent();

        void ConvertPos2XY(uint intPos, ref uint x, ref uint y);
        uint ConvertXY2Pos(uint x, uint y);

        bool IsLinked();

        bool LinkCamera(string sIP, uint intTimeOut);
        bool LinkCamera(uint intIP, uint intTimeOut);

        bool LinkCameraEx(string sIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut);
        bool LinkCameraEx(uint intIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut);

        void DisLinkCamera();
        uint GetRecentHeartBeat();

        bool SetReConnectCallBack(GroupSDK.DelegateReconnect pCallBack, IntPtr pUserData);

	    bool ResetCamera();
	    bool TriggerFFC();
	    bool AutoFocus();
        bool SetIoAlarmState(bool bAlarm);
	    bool SetPTZCmd(GroupSDK.PTZIRCMD cmd, uint dwPara);
	    bool QueryPTZState(GroupSDK.PTZQuery query, ref int intValue, uint intTimeout);
	    bool SetSerialCmd(byte[] buffer, uint intBufferLen);

        bool SetSerialCallBack(GroupSDK.DelegateSerial pCallBack, IntPtr pUserData);
    	
	    bool GetCameraTemperature([MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]int[] intT, uint intTimeout);
	    bool SetCameraRegContent(GroupSDK.CAMERA_REGCONTENT RegContent);

	    bool SetUserROIs(GroupSDK.USER_ROI rois);
        bool SetUserROIsEx(GroupSDK.RECT_ROIS rois, uint intROINum);
        bool SetIrregularROIs(GroupSDK.IRREGULAR_ROIS rois, uint intROINum);

        bool SetROIReportCallBack(GroupSDK.DelegateROIReport pCallBack, IntPtr pUserData);
        bool SetIrregularROIReportCallBack(GroupSDK.DelegateROIReport pCallBack, IntPtr pUserData);
        bool SetIrregularROIReportExCallBack(GroupSDK.DelegateIrregularROIReport pCallBack, IntPtr pUserData);


        bool IsProcessingImage();
	    bool StartProcessImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData);
	    bool StartProcessPulseImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData);
	    bool TransferPulseImage();
	    void StopProcessImage();

	    void SetColorPalette(GroupSDK.COLOR_PALETTE ColorPaletteIndex);
	    bool SetSubsectionEnlargePara(int intX1, int intX2, byte byteY1, byte byteY2);
	    void SetAutoEnlargePara(uint dwAutoEnlargeRange, int intBrightOffset, int intContrastOffset);

        void SetIsothermalPara(int intLowerLimit, int intUpperLimit);

        void SetIsothermalParaEx(int intLowerLimit, int intUpperLimit, byte R, byte G, byte B);

        void SetEnhancedROI(uint intEnhancedRatio, uint x0, uint y0, uint x1, uint y1);

        bool GetApproximateGray2TemperatureLUT(ref int pLut, uint intBufferSize);



	    void SetEXLevel(GroupSDK.EX ex, int intCenterX, int intCenterY);
	    GroupSDK.EX GetEXLevel();
	    void SetDetailEnhancement(int intDDE, int isQuickDDE);
	    bool SetVideoContrast(int intContrastOffset);
	    bool SetVideoBrightness(int intBrightnessOffset);

	    void GetFixPara(ref GroupSDK.FIX_PARAM param);
        float SetFixPara(ref GroupSDK.FIX_PARAM param, bool isEnableCameraCorrect);
	    int FixTemperature(int intT, float fEmissivity, uint dwPosX, uint dwPosY);

	    bool GetOutputBMPdata(ref IntPtr pData, ref IntPtr pInfo);
	    bool GetOutputColorBardata(ref IntPtr pData, ref IntPtr pInfo);
        bool GetOutputBMPdataRGB24(IntPtr pRGBBuffer, uint intBufferSize, bool bOrderBGR);
        bool GetOutputBMPdataRGB24byte(byte[] cRGBBuffer, bool bOrderBGR);
	    bool GetOutputVideoData(ref IntPtr pData, ref IntPtr pInfo);

	    uint GetDevIPAddress();
	    GroupSDK.TEMP_STATE GetFrameStatisticalData();
	    bool GetTemperatureData(int[] data, uint intBufferSize, int isEnableExtCorrect);
	    bool GetTemperatureDataRaw(int[] data, uint intBufferSize, int isEnableExtCorrect);
	    int GetTemperatureProbe(uint dwPosX, uint dwPosY, uint intSize);
	    int GetLineTemperatureInfo(int[] buffer, uint intBufferSizeByte, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]int[] info, uint x0, uint y0, uint x1, uint y1);
        int GetLineTemperatureInfo2(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);
	    bool GetRectTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);
	    bool GetEllipseTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);
	    bool GetRgnTemperatureInfo(uint[] pos, uint intPosNumber, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);	

	    bool UseTemperatureMask(int isUse);
	    bool IsUsingTemperatureMask();

	    bool SaveBMP(uint dwIndex, [MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    bool SaveMGT([MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    bool SaveDDT([MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    int SaveDDT2Buffer(byte[] buffer, uint intBufferSize);
	    bool LoadDDT(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);
	    bool LoadBufferedDDT(GroupSDK.OUTPUT_PARAM param, IntPtr pBuffer, uint size, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);

        bool SetAsyncCompressCallBack(GroupSDK.DelegateDDTCompressComplete funcDDTCompressComplete, int intQuality);
        bool GrabAndAsyncCompressDDT(IntPtr pUserData);

        bool LocalStorageAviStart([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod);
        void LocalStorageAviStop();
        bool IsLocalAviRecording();
        bool LocalStorageMgsRecord([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod);
        int LocalStorageMgsPlay([MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);
        bool LocalStorageMgsSeekFrame(uint hFrame);
        bool LocalStorageMgsPopFrame();
        void LocalStorageMgsStop();
        bool IsLocalMgsRecording();
        bool IsLocalMgsPlaying();

        bool SDStorageMGT();
	    bool SDStorageBMP();
	    bool SDStorageMGSStart();
	    bool SDStorageMGSStop();
	    bool SDStorageAviStart();
	    bool SDStorageAviStop();
        bool SDCardStorage(uint hDevice, GroupSDK.SDStorageFileType filetype, uint para);

	    void Lock();
	    void Unlock();

        int EstimateUnderArmTempFromForeheadRect(uint x0, uint y0, uint x1, uint y1);

        bool ConvertIrCorr2VisCorr(int intIrX, int intIrY, float fDistance, ref int intVisX, ref int intVisY);
        bool ConvertVisCorr2IrCorr(int intVisX, int intVisY, float fDistance, ref int intIrX, ref int intIrY);

        bool SaveFIR([MarshalAs(UnmanagedType.LPWStr)] string sFileName);

        bool LoadFIR(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);
    }

    public class MagService : IMagService
    {
        const uint MAG_DEFAULT_TIMEOUT = 500;
        static bool m_bIsIntialized = false;
        IntPtr m_hWndMsg = IntPtr.Zero;

        public MagService(IntPtr hWndMsg)
        {
            m_hWndMsg = hWndMsg;
        }

        public bool Initialize()
        {
            if (m_bIsIntialized)
            {
                return true;
            }

            if (!GroupSDK.MAG_IsChannelAvailable(0))
            {
                GroupSDK.MAG_NewChannel(0);
            }

            m_bIsIntialized = GroupSDK.MAG_Initialize(0, m_hWndMsg);
            

            return m_bIsIntialized;
        }

        public void DeInitialize()
        {
            if (!m_bIsIntialized)
            {
                return;
            }

            if (GroupSDK.MAG_IsDHCPServerRunning())
            {
                GroupSDK.MAG_StopDHCPServer();
            }

            if (GroupSDK.MAG_IsInitialized(0))
            {
                GroupSDK.MAG_Free(0);
            }

            if (GroupSDK.MAG_IsChannelAvailable(0))
            {
                GroupSDK.MAG_DelChannel(0);
            }

            m_bIsIntialized = false;
        }

        public bool IsInitialized()
        {
            return m_bIsIntialized;
        }

        public bool IsLanConnected()
        {
            return GroupSDK.MAG_IsLanConnected();
        }

        /// <summary>
        /// 是否使用静态IP
        /// </summary>
        /// <returns></returns>
        public bool IsUsingStaticIp()
        {
            return GroupSDK.MAG_IsUsingStaticIp();
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public uint GetLocalIp()
        {
            if (!m_bIsIntialized)
            {
                Initialize();
            }

            return m_bIsIntialized ? GroupSDK.MAG_GetLocalIp() : 0;
        }

        public bool IsDHCPServerRunning()
        {
            return GroupSDK.MAG_IsDHCPServerRunning();
        }

        public bool StartDHCPServer()
        {
            return GroupSDK.MAG_StartDHCPServer(m_hWndMsg);
        }

        public void StopDHCPServer()
        {
            GroupSDK.MAG_StopDHCPServer();
        }

        /// <summary>
        /// 开启/关闭自动重连功能
        /// </summary>
        /// <param name="bEnable">true 开启  false 关闭</param>
        public void EnableAutoReConnect(bool bEnable)
        {
            GroupSDK.MAG_EnableAutoReConnect(bEnable);
        }

        /// <summary>
        /// 枚举在线相机,异步更新相机列表，因此立即GetTerminalList可能拿不到最新的，推荐sleep（50）
        /// </summary>
        /// <returns></returns>
        public bool EnumCameras()
        {
            if (!m_bIsIntialized)
            {
                Initialize();
            }

            return m_bIsIntialized ? GroupSDK.MAG_EnumCameras() : false;
        }

        /// <summary>
        /// 获取在线相机列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="size">list总的字节数</param>
        /// <returns></returns>
        public uint GetTerminalList(GroupSDK.ENUM_INFO[] list, uint unit_count)
        {
            if (!m_bIsIntialized)
            {
                return 0;
            }

            uint size = (uint)Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)) * unit_count;

            IntPtr ptr = Marshal.AllocHGlobal((int)size);
            IntPtr ptrBackup = ptr;

            uint dev_num = GroupSDK.MAG_GetTerminalList(ptr, size);

            for (int i = 0; i < dev_num; i++)
            {
                list[i] = (GroupSDK.ENUM_INFO)Marshal.PtrToStructure(ptr, typeof(GroupSDK.ENUM_INFO));
                ptr = (IntPtr)((int)ptr + Marshal.SizeOf(typeof(GroupSDK.ENUM_INFO)));
            }

            Marshal.FreeHGlobal(ptrBackup);

            return dev_num;
        }

        public uint GetTerminalCount()
        {
            return m_bIsIntialized ? GroupSDK.MAG_GetTerminalList(IntPtr.Zero, 0) : 0;
        }

        public int CompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize, uint intQuality)
        {
	        return GroupSDK.MAG_CompressDDT(pDstBuffer, intDstBufferSize, pSrcBuffer, intSrcBufferSize, intQuality);
        }

        public int DeCompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize)
        {
	        return GroupSDK.MAG_DeCompressDDT(pDstBuffer, intDstBufferSize, pSrcBuffer, intSrcBufferSize);
        }
    }

    public class MagDevice : IMagDevice
    {
        const uint MAG_DEFAULT_TIMEOUT = 500;
        const uint MAX_CHANNELINDEX = 32;

        bool m_bIsInitialized = false;
        IntPtr m_hWndMsg = IntPtr.Zero;
        IntPtr m_RGBBuffer = IntPtr.Zero;

        uint m_intChannelIndex = 0xffffffff;
        uint m_intCameraIPAddr = 0;

        // 用户层回调
        GroupSDK.DelegateIrregularROIReport m_IrregularCallbackUserLayer = null;
        // 底层回调
        GroupSDK.DelegateIrregularROIReportBottomLayer m_IrregularCallbackBottomLayer = null;

        GroupSDK.CAMERA_INFO m_CamInfo = new GroupSDK.CAMERA_INFO();
        GroupSDK.CAMERA_INFO_EX m_CamInfoEx = new GroupSDK.CAMERA_INFO_EX();
        GroupSDK.CAMERA_REGCONTENT m_RegContent = new GroupSDK.CAMERA_REGCONTENT();
        GroupSDK.IRREGULAR_ROI_REPORT[] m_IrrReports = null;

        bool m_bIsRecordingAvi = false;
        bool m_bIsRecordingMgs = false;

        bool m_bIsRecordingLocalAvi = false;
        bool m_bIsRecordingLocalMgs = false;
        bool m_bIsPlayingLocalMgs = false;


        public MagDevice(IntPtr hWndMsg)
        {
            m_hWndMsg = hWndMsg;
        }

        public bool Initialize()
        {
	        if (m_bIsInitialized)
	        {
		        return true;
	        }

            if (m_intChannelIndex <= 0 || m_intChannelIndex > MAX_CHANNELINDEX)
	        {
		        for (uint i = 1; i <= MAX_CHANNELINDEX; i++)
		        {
			        if (!GroupSDK.MAG_IsChannelAvailable(i))//find an unused channel
			        {
				        GroupSDK.MAG_NewChannel(i);
				        m_intChannelIndex = i;

				        break;
			        }
		        }
	        }

	        if (m_intChannelIndex > 0 && m_intChannelIndex <= MAX_CHANNELINDEX)
	        {
		        m_bIsInitialized = GroupSDK.MAG_Initialize(m_intChannelIndex, m_hWndMsg);
	        }

	        return m_bIsInitialized;
        }

        public void DeInitialize()
        {
            if (!m_bIsInitialized)
            {
                return;
            }

            if (GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                GroupSDK.MAG_StopProcessImage(m_intChannelIndex);
            }

            if (GroupSDK.MAG_IsLinked(m_intChannelIndex))
            {
                DisLinkCamera();//include stop sd storage
            }

            if (GroupSDK.MAG_IsInitialized(m_intChannelIndex))
            {
                GroupSDK.MAG_Free(m_intChannelIndex);
            }

            if (GroupSDK.MAG_IsChannelAvailable(m_intChannelIndex))
            {
                GroupSDK.MAG_DelChannel(m_intChannelIndex);
            }

            if (m_RGBBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_RGBBuffer);
            }
        }

        public bool IsInitialized()
        {
            return m_bIsInitialized;
        }

        public GroupSDK.CAMERA_INFO GetCamInfo()
        {
            return m_CamInfo;
        }

        public GroupSDK.CAMERA_INFO_EX GetCamInfoEx()
        {
            return m_CamInfoEx;
        }

        public GroupSDK.CAMERA_REGCONTENT GetRegContent()
        {
            return m_RegContent;
        }

        public void ConvertPos2XY(uint intPos, ref uint x, ref uint y)
        {
            uint w = (uint)m_CamInfo.intFPAWidth;

	        if (w != 0)
	        {
		        y = intPos / w;
		        x = intPos - y * w;
	        }
        }

        public uint ConvertXY2Pos(uint x, uint y)
        {
            return y * (uint)m_CamInfo.intFPAWidth + x;
        }

        public bool IsLinked()
        {
            return GroupSDK.MAG_IsLinked(m_intChannelIndex);
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="intTimeOut">ms</param>
        /// <returns></returns>
        public bool LinkCamera(string sIP, uint intTimeOut)
        {
            return LinkCamera(WINAPI.inet_addr(sIP), intTimeOut);
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="intIP"></param>
        /// <param name="intTimeOut">ms</param>
        /// <returns></returns>
        public bool LinkCamera(uint intIP, uint intTimeOut)
        {
            if (GroupSDK.MAG_LinkCamera(m_intChannelIndex, intIP, intTimeOut))
	        {
		        m_intCameraIPAddr = intIP;
		        GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
                GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, intTimeOut, 0);

		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

        public bool LinkCameraEx(string sIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut)
        {
            return LinkCameraEx(WINAPI.inet_addr(sIP), shortCmdPort, shortImgPort, charCloudUser, charCloudPwd,
                intCamSN, charCamUser, charCamPwd, intTimeOut);
        }

        public bool LinkCameraEx(uint intIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut)
        {
            if (GroupSDK.MAG_LinkCameraEx(m_intChannelIndex, intIP, shortCmdPort, shortImgPort, charCloudUser, charCloudPwd,
                intCamSN, charCamUser, charCamPwd, intTimeOut))
            {
                m_intCameraIPAddr = intIP;
                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
                GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, intTimeOut, 0);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisLinkCamera()
        {
            //remember to stop sd storage before dislink
	        if (m_bIsRecordingMgs)
	        {
		        SDStorageMGSStop();
	        }

	        if (m_bIsRecordingAvi)
	        {
		        SDStorageAviStop();
	        }

	        m_intCameraIPAddr = 0;

	        GroupSDK.MAG_DisLinkCamera(m_intChannelIndex);
        }

        public uint GetRecentHeartBeat()
        {
            return GroupSDK.MAG_GetRecentHeartBeat(m_intChannelIndex);
        }

        public bool SetReConnectCallBack(GroupSDK.DelegateReconnect pCallBack, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetReConnectCallBack(m_intChannelIndex, pCallBack, pUserData);
        }

        /// <summary>
        /// 重启热像仪
        /// </summary>
        /// <returns></returns>
	    public bool ResetCamera()
        {
            //the user should stop image process before reset
	        //if you forget, the sdk will call MAG_StopProcessImage()

	        //remember to stop sd storage before reset
	        if (m_bIsRecordingMgs)
	        {
		        SDStorageMGSStop();
	        }

	        if (m_bIsRecordingAvi)
	        {
		        SDStorageAviStop();
	        }

	        if (GroupSDK.MAG_ResetCamera(m_intChannelIndex))
	        {
		        //MAG_ResetCamera() will call MAG_Free() and MAG_DelChannel()
		        //so the channel is invalid now
		        m_bIsInitialized = false;
		        m_intChannelIndex = 0xffffffff;

		        //this object is reusable after call Initialize()

		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

	    public bool TriggerFFC()
        {
            return GroupSDK.MAG_TriggerFFC(m_intChannelIndex);
        }

	    public bool AutoFocus()
        {
            return GroupSDK.MAG_SetPTZCmd(m_intChannelIndex, GroupSDK.PTZIRCMD.PTZFocusAuto, 0);
        }

        public bool SetIoAlarmState(bool bAlarm)
        {
            return GroupSDK.MAG_SetIoAlarmState(m_intChannelIndex, bAlarm);
        }

	    public bool SetPTZCmd(GroupSDK.PTZIRCMD cmd, uint dwPara)
        {
            return GroupSDK.MAG_SetPTZCmd(m_intChannelIndex, cmd, dwPara);
        }

	    public bool QueryPTZState(GroupSDK.PTZQuery query, ref int intValue, uint intTimeout)
        {
            return GroupSDK.MAG_QueryPTZState(m_intChannelIndex, query, ref intValue, intTimeout);
        }

	    public bool SetSerialCmd(byte[] buffer, uint intBufferLen)
        {
            return GroupSDK.MAG_SetSerialCmd(m_intChannelIndex, buffer, intBufferLen);
        }
    	
        public bool SetSerialCallBack(GroupSDK.DelegateSerial pCallBack, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetSerialCallBack(m_intChannelIndex, pCallBack, pUserData);
        }

	    public bool GetCameraTemperature([MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]int[] intT, uint intTimeout)
        {
            return GroupSDK.MAG_GetCameraTemperature(m_intChannelIndex, intT, intTimeout);
        }

	    public bool SetCameraRegContent(GroupSDK.CAMERA_REGCONTENT RegContent)
        {
            if (GroupSDK.MAG_SetCameraRegContent(m_intChannelIndex, ref RegContent))
	        {
		        GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, 2 * MAG_DEFAULT_TIMEOUT, 0);
		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

	    public bool SetUserROIs(GroupSDK.USER_ROI rois)
        {
            return GroupSDK.MAG_SetUserROIs(m_intChannelIndex, ref rois);
        }

        public bool SetUserROIsEx(GroupSDK.RECT_ROIS rois, uint intROINum)
        {
            return GroupSDK.MAG_SetUserROIsEx(m_intChannelIndex, ref rois, intROINum);
        }

        public bool SetIrregularROIs(GroupSDK.IRREGULAR_ROIS rois, uint intROINum)
        {
            return GroupSDK.MAG_SetIrregularROIs(m_intChannelIndex, ref rois, intROINum);
        }

        public bool SetROIReportCallBack(GroupSDK.DelegateROIReport pCallBack, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetROIReportCallBack(m_intChannelIndex, pCallBack, pUserData);
        }

        public bool SetIrregularROIReportCallBack(GroupSDK.DelegateROIReport pCallBack, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetIrregularROIReportCallBack(m_intChannelIndex, pCallBack, pUserData);
        }

        public bool SetIrregularROIReportExCallBack(GroupSDK.DelegateIrregularROIReport pCallBack, IntPtr pUserData)
        {
            if (pCallBack == null)
            {
                return false;
            }

            m_IrregularCallbackUserLayer = pCallBack;

            if (m_IrregularCallbackBottomLayer == null)
            {
                m_IrregularCallbackBottomLayer = new GroupSDK.DelegateIrregularROIReportBottomLayer(IrregularROIReportCallback);
            }

            return GroupSDK.MAG_SetIrregularROIReportExCallBack(m_intChannelIndex, m_IrregularCallbackBottomLayer, pUserData);

        }

        public void IrregularROIReportCallback(uint hDevice, IntPtr pReports, uint intROINum, IntPtr pUserData)
        {

            if (m_IrrReports == null)
            {
                m_IrrReports = new GroupSDK.IRREGULAR_ROI_REPORT[10];
            }

            if (intROINum > m_IrrReports.Length)
            {
                m_IrrReports = new GroupSDK.IRREGULAR_ROI_REPORT[intROINum];
            }

            for (int i = 0; i < intROINum; i++)
            {
                m_IrrReports[i] = (GroupSDK.IRREGULAR_ROI_REPORT)Marshal.PtrToStructure(pReports, typeof(GroupSDK.IRREGULAR_ROI_REPORT));

                pReports = pReports + Marshal.SizeOf(typeof(GroupSDK.IRREGULAR_ROI_REPORT));
            }

            m_IrregularCallbackUserLayer.Invoke(hDevice, m_IrrReports, intROINum, pUserData);
        }

        public bool IsProcessingImage()
        {
            return GroupSDK.MAG_IsProcessingImage(m_intChannelIndex);
        }

	    public bool StartProcessImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData)
        {
            return GroupSDK.MAG_StartProcessImage(m_intChannelIndex, ref param, funcFrame, dwStreamType, pUserData);
        }

	    public bool StartProcessPulseImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData)
        {
            return GroupSDK.MAG_StartProcessPulseImage(m_intChannelIndex, ref param, funcFrame, dwStreamType, pUserData);
        }

	    public bool TransferPulseImage()
        {
            return GroupSDK.MAG_TransferPulseImage(m_intChannelIndex);
        }

	    public void StopProcessImage()
        {
            if (GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                GroupSDK.MAG_StopProcessImage(m_intChannelIndex);
            }
        }

	    public void SetColorPalette(GroupSDK.COLOR_PALETTE ColorPaletteIndex)
        {
            GroupSDK.MAG_SetColorPalette(m_intChannelIndex, ColorPaletteIndex);
        }

	    public bool SetSubsectionEnlargePara(int intX1, int intX2, byte byteY1, byte byteY2)
        {
            return GroupSDK.MAG_SetSubsectionEnlargePara(m_intChannelIndex, intX1, intX2, byteY1, byteY2);
        }

        public bool GetApproximateGray2TemperatureLUT(ref int pLut, uint intBufferSize)
        {
            return GroupSDK.MAG_GetApproximateGray2TemperatureLUT(m_intChannelIndex, ref pLut, intBufferSize);
        }

        public void SetIsothermalPara(int intLowerLimit, int intUpperLimit)
        {
            GroupSDK.MAG_SetIsothermalPara(m_intChannelIndex, intLowerLimit, intUpperLimit);
        }

        public void SetIsothermalParaEx(int intLowerLimit, int intUpperLimit, byte R, byte G, byte B)
        {
            GroupSDK.MAG_SetIsothermalParaEx(m_intChannelIndex, intLowerLimit, intUpperLimit, R, G, B);
        }

        public void SetEnhancedROI(uint intEnhancedRatio, uint x0, uint y0, uint x1, uint y1)
        {
            GroupSDK.MAG_SetEnhancedROI(m_intChannelIndex, intEnhancedRatio, x0, y0, x1, y1);
        }

        public void SetAutoEnlargePara(uint dwAutoEnlargeRange, int intBrightOffset, int intContrastOffset)
        {
            GroupSDK.MAG_SetAutoEnlargePara(m_intChannelIndex, dwAutoEnlargeRange, intBrightOffset, intContrastOffset);
        }

	    public void SetEXLevel(GroupSDK.EX ex, int intCenterX, int intCenterY)
        {
            GroupSDK.MAG_SetEXLevel(m_intChannelIndex, ex, intCenterX, intCenterY);
        }

	    public GroupSDK.EX GetEXLevel()
        {
            return GroupSDK.MAG_GetEXLevel(m_intChannelIndex);
        }

	    public void SetDetailEnhancement(int intDDE, int isQuickDDE)
        {
            GroupSDK.MAG_SetDetailEnhancement(m_intChannelIndex, intDDE, isQuickDDE);
        }

	    public bool SetVideoContrast(int intContrastOffset)
        {
            return GroupSDK.MAG_SetVideoContrast(m_intChannelIndex, intContrastOffset);
        }

	    public bool SetVideoBrightness(int intBrightnessOffset)
        {
            return GroupSDK.MAG_SetVideoBrightness(m_intChannelIndex, intBrightnessOffset);
        }

	    public void GetFixPara(ref GroupSDK.FIX_PARAM param)
        {
            GroupSDK.MAG_GetFixPara(m_intChannelIndex, ref param);
        }

        public float SetFixPara(ref GroupSDK.FIX_PARAM param, bool isEnableCameraCorrect)
        {
            return GroupSDK.MAG_SetFixPara(m_intChannelIndex, ref param, isEnableCameraCorrect);
        }

	    public int FixTemperature(int intT, float fEmissivity, uint dwPosX, uint dwPosY)
        {
            return GroupSDK.MAG_FixTemperature(m_intChannelIndex, intT, fEmissivity, dwPosX, dwPosY);
        }

	    public bool GetOutputBMPdata(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputBMPdata(m_intChannelIndex, ref pData, ref pInfo);
        }

        public bool GetOutputBMPdataRGB24(IntPtr pRGBBuffer, uint intBufferSize, bool bOrderBGR)
        {
            return GroupSDK.MAG_GetOutputBMPdataRGB24(m_intChannelIndex, pRGBBuffer, intBufferSize, bOrderBGR);
        }
 
        public bool GetOutputBMPdataRGB24byte(byte[] cRGBBuffer, bool bOrderBGR)
        {
            if (m_RGBBuffer == IntPtr.Zero)
            {
                m_RGBBuffer = Marshal.AllocHGlobal(cRGBBuffer.Length);
            }
            bool status = GroupSDK.MAG_GetOutputBMPdataRGB24(m_intChannelIndex, m_RGBBuffer, (uint)cRGBBuffer.Length, bOrderBGR);
            Marshal.Copy(m_RGBBuffer, cRGBBuffer, 0, cRGBBuffer.Length);
            return status;
        }
     
        public Bitmap GetOutputBMPBitmap()
        {
            byte[] cRGBBuffer = new byte[m_CamInfo.intVideoWidth * m_CamInfo.intVideoHeight * 3];
            bool status = GetOutputBMPdataRGB24byte(cRGBBuffer, true);

            Bitmap bmp = new Bitmap(m_CamInfo.intVideoWidth, m_CamInfo.intVideoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            if (cRGBBuffer != null)
            {
                //位图矩形
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //以可读写的方式将图像数据锁定
                System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                //把处理后的图像数组复制回图像
                System.Runtime.InteropServices.Marshal.Copy(cRGBBuffer, 0, bmpdata.Scan0, cRGBBuffer.Length);
                //解锁位图像素
                bmp.UnlockBits(bmpdata);
            }
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }
        public bool GetOutputColorBardata(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputColorBardata(m_intChannelIndex, ref pData, ref pInfo);
        }

	    public bool GetOutputVideoData(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputVideoData(m_intChannelIndex, ref pData, ref pInfo);
        }

	    public uint GetDevIPAddress()
        {
            return m_intCameraIPAddr;
        }

        public GroupSDK.TEMP_STATE GetFrameStatisticalData()
        {
            int size = (int)Marshal.SizeOf(typeof(GroupSDK.TEMP_STATE));

            IntPtr ptr = GroupSDK.MAG_GetFrameStatisticalData(m_intChannelIndex);

            GroupSDK.TEMP_STATE state = (GroupSDK.TEMP_STATE)Marshal.PtrToStructure(ptr, typeof(GroupSDK.TEMP_STATE));

            return state;
        }

        public bool GetTemperatureData(int[] data, uint intBufferSize, int isEnableExtCorrect)
        {
            return GroupSDK.MAG_GetTemperatureData(m_intChannelIndex, data, intBufferSize, isEnableExtCorrect);
        }

        public bool GetTemperatureDataRaw(int[] data, uint intBufferSize, int isEnableExtCorrect)
        {
            return GroupSDK.MAG_GetTemperatureData_Raw(m_intChannelIndex, data, intBufferSize, isEnableExtCorrect);
        }

        public int GetTemperatureProbe(uint dwPosX, uint dwPosY, uint intSize)
        {
            return GroupSDK.MAG_GetTemperatureProbe(m_intChannelIndex, dwPosX, dwPosY, intSize);
        }

        public int GetLineTemperatureInfo(int[] buffer, uint intBufferSizeByte, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]int[] info, uint x0, uint y0, uint x1, uint y1)
        {
            return GroupSDK.MAG_GetLineTemperatureInfo(m_intChannelIndex, buffer, intBufferSizeByte, info, x0, y0, x1, y1);
        }

        public int GetLineTemperatureInfo2(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetLineTemperatureInfo2(m_intChannelIndex, x0, y0, x1, y1, info);
        }

        public bool GetRectTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetRectTemperatureInfo(m_intChannelIndex, x0, y0, x1, y1, info);
        }

        public bool GetEllipseTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetEllipseTemperatureInfo(m_intChannelIndex, x0, y0, x1, y1, info);
        }

	    public bool GetRgnTemperatureInfo(uint[] pos, uint intPosNumber, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetRgnTemperatureInfo(m_intChannelIndex, pos, intPosNumber, info);
        }

        public bool UseTemperatureMask(int isUse)
        {
            return GroupSDK.MAG_UseTemperatureMask(m_intChannelIndex, isUse);
        }

	    public bool IsUsingTemperatureMask()
        {
            return GroupSDK.MAG_IsUsingTemperatureMask(m_intChannelIndex);
        }

        public bool SaveBMP(uint dwIndex, [MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveBMP(m_intChannelIndex, dwIndex, sFileName);
        }

        public bool SaveMGT([MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveMGT(m_intChannelIndex, sFileName);
        }

        public bool SaveDDT([MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveDDT(m_intChannelIndex, sFileName);
        }

        public int SaveDDT2Buffer(byte[] pBuffer, uint intBufferSize)
        {
            return GroupSDK.MAG_SaveDDT2Buffer(m_intChannelIndex, pBuffer, intBufferSize);
        }

        public bool LoadDDT(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {             
            if (GroupSDK.MAG_IsLinked(m_intChannelIndex))
            {
                return false;
            }

            if (!GroupSDK.MAG_IsInitialized(m_intChannelIndex) && !GroupSDK.MAG_Initialize(m_intChannelIndex, m_hWndMsg))
            {
                return false;
            }

            if (!GroupSDK.MAG_LoadDDT(m_intChannelIndex, ref param, sFileName, funcFrame, pUserData)){
                return false;
            }

            GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
            GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
            return true;
              

        }

        public bool LoadBufferedDDT(GroupSDK.OUTPUT_PARAM param, IntPtr pBuffer, uint size, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {
            if (!GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                if (!GroupSDK.MAG_LoadBufferedDDT(m_intChannelIndex, ref param, pBuffer, size, funcFrame, pUserData))
                {
                    return false;
                }

                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
                return true;
            }

            return true;
        }

        public bool SetAsyncCompressCallBack(GroupSDK.DelegateDDTCompressComplete funcDDTCompressComplete, int intQuality)
        {
            return GroupSDK.MAG_SetAsyncCompressCallBack(m_intChannelIndex, funcDDTCompressComplete, intQuality);
        }

        public bool GrabAndAsyncCompressDDT(IntPtr pUserData)
        {
            return GroupSDK.MAG_GrabAndAsyncCompressDDT(m_intChannelIndex, pUserData);
        }

        public bool LocalStorageAviStart([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod)
        {
            m_bIsRecordingLocalAvi |= GroupSDK.MAG_LocalStorageAviStart(m_intChannelIndex, sFileName, samplePeriod);
            return m_bIsRecordingLocalAvi;
        }

        public void LocalStorageAviStop()
        {
            GroupSDK.MAG_LocalStorageAviStop(m_intChannelIndex);
            m_bIsRecordingLocalAvi = false;
        }

        public bool IsLocalAviRecording()
        {
            return m_bIsRecordingLocalAvi;
        }

        public bool LocalStorageMgsRecord([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod)
        {
            m_bIsRecordingLocalMgs |= GroupSDK.MAG_LocalStorageMgsRecord(m_intChannelIndex, sFileName, samplePeriod);
            return m_bIsRecordingLocalMgs;
        }

        public int LocalStorageMgsPlay([MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {
            int totalFrames = GroupSDK.MAG_LocalStorageMgsPlay(m_intChannelIndex, sFileName, funcFrame, pUserData);
            if (totalFrames > 0)
            {
                m_bIsPlayingLocalMgs = true;
            }

            if (m_bIsPlayingLocalMgs)
            {
                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
            }

            return totalFrames;
        }

        public bool LocalStorageMgsSeekFrame(uint hFrame)
        {
            return GroupSDK.MAG_LocalStorageMgsSeekFrame(m_intChannelIndex, hFrame);
        }

        public bool LocalStorageMgsPopFrame()
        {
            return GroupSDK.MAG_LocalStorageMgsPopFrame(m_intChannelIndex);
        }

        public void LocalStorageMgsStop()
        {
            GroupSDK.MAG_LocalStorageMgsStop(m_intChannelIndex);
            m_bIsRecordingLocalMgs = false;
            m_bIsPlayingLocalMgs = false;
        }

        public bool IsLocalMgsRecording()
        {
            return m_bIsRecordingLocalMgs;
        }

        public bool IsLocalMgsPlaying()
        {
            return m_bIsPlayingLocalMgs;
        }

        public bool SDStorageMGT()
        {
            return GroupSDK.MAG_SDStorageMGT(m_intChannelIndex);
        }

        public bool SDStorageBMP()
        {
            return GroupSDK.MAG_SDStorageBMP(m_intChannelIndex);
        }

        public bool SDStorageMGSStart()
        {
            m_bIsRecordingMgs |= GroupSDK.MAG_SDStorageMGSStart(m_intChannelIndex);
            return m_bIsRecordingMgs;
        }

        public bool SDStorageMGSStop()
        {
            bool bReturn = GroupSDK.MAG_SDStorageMGSStop(m_intChannelIndex);

            if (bReturn)
            {
                m_bIsRecordingMgs = false;
            }

            return bReturn;
        }

        public bool SDStorageAviStart()
        {
            m_bIsRecordingAvi |= GroupSDK.MAG_SDStorageAviStart(m_intChannelIndex);
            return m_bIsRecordingAvi;
        }

	    public bool SDStorageAviStop()
        {
            bool bReturn = GroupSDK.MAG_SDStorageAviStop(m_intChannelIndex);

            if (bReturn)
            {
                m_bIsRecordingAvi = false;
            }

            return bReturn;
        }

        public bool SDCardStorage(uint hDevice, GroupSDK.SDStorageFileType filetype, uint para)
        {
            return GroupSDK.MAG_SDCardStorage(m_intChannelIndex, filetype, para);
        }

        public void Lock()
        {
            GroupSDK.MAG_LockFrame(m_intChannelIndex);
        }

	    public void Unlock()
        {
            GroupSDK.MAG_UnLockFrame(m_intChannelIndex);
        }

        public int EstimateUnderArmTempFromForeheadRect(uint x0, uint y0, uint x1, uint y1)
        {
            return GroupSDK.MAG_EstimateUnderArmTempFromForeheadRect(m_intChannelIndex, x0, y0, y0, y1);
        }

        public bool ConvertIrCorr2VisCorr(int intIrX, int intIrY, float fDistance, ref int intVisX, ref int intVisY)
        {
            return GroupSDK.MAG_ConvertIrCorr2VisCorr(m_intChannelIndex, intIrX, intIrY, fDistance, ref intVisX, ref intVisY);
        }

        public bool ConvertVisCorr2IrCorr(int intVisX, int intVisY, float fDistance, ref int intIrX, ref int intIrY)
        {
            return GroupSDK.MAG_ConvertVisCorr2IrCorr(m_intChannelIndex, intVisX, intVisY, fDistance, ref intIrX, ref intIrY);
        }

        public bool SaveFIR([MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveFIR(m_intChannelIndex, sFileName);
        }

        public bool LoadFIR(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {             
            if (GroupSDK.MAG_IsLinked(m_intChannelIndex))
            {
                return false;
            }

            if (!GroupSDK.MAG_IsInitialized(m_intChannelIndex) && !GroupSDK.MAG_Initialize(m_intChannelIndex, m_hWndMsg))
            {
                return false;
            }

            if (!GroupSDK.MAG_LoadFIR(m_intChannelIndex, ref param, sFileName, funcFrame, pUserData)){
                return false;
            }

            GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
            GroupSDK.MAG_GetCamInfoEx(m_intChannelIndex, ref m_CamInfoEx, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO_EX)));
            return true;
        }

    }
}
