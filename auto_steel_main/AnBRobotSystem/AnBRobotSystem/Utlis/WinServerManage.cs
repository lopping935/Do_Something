using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Collections;
using System.Diagnostics;

namespace AnBRobotSystem.Utlis
{
    class WinServerManage
    {

        public WinServerManage()
        {
        }

        public WinServerManage(IDictionary p_stateSaver, string p_filepath, string p_serviceName)
        {
            _stateSaver = p_stateSaver;
            _filepath = p_filepath;
            _serviceName = p_serviceName;
        }
        

        private IDictionary _stateSaver;
        public IDictionary stateSaver
        {
            get { return _stateSaver; }
            set { _stateSaver = value; }
        }
        
        private string _filepath;
        public string filepath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        private string _serviceName;
        public string serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
  
        private int _overflag = -1;
        public int overflag
        {
            get { return _overflag; }
            set { _overflag = value; }
        }

        private string _fileVersion;
        public string FileVersion
        {
          get { return _fileVersion; }
          set { _fileVersion = value; }
        }

         #region 
        /// <summary>
        /// 服务是否存在
        /// </summary>
        /// <returns>【true 存在】  【false 不存在】</returns>
        public bool ServiceIsExisted()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName == _serviceName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取服务当前状态 Stopped = 1,StartPending = 2,StopPending = 3,Running = 4,ContinuePending = 5,PausePending = 6,Paused = 7
        /// </summary>
        /// <returns>枚举 ServiceControllerStatus  1-7</returns>
        private ServiceControllerStatus getServiceState()
        {
            System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(_serviceName);
            return service.Status;
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="_stateSaver">安装完成后处于的状态</param>
        /// <param name="_filepath">安装文件路径</param>
        /// <param name="_serviceName">服务名称</param>
        public void InstallService()
        {
            _overflag = 0;
            try
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(_serviceName);
                if (!ServiceIsExisted())
                {
                    //Install Service
                    AssemblyInstaller myAssemblyInstaller = new AssemblyInstaller();
                    myAssemblyInstaller.UseNewContext = true;
                    myAssemblyInstaller.Path = _filepath;
                    myAssemblyInstaller.Install(_stateSaver);
                    myAssemblyInstaller.Commit(_stateSaver);
                    myAssemblyInstaller.Dispose();
                    //--Start Service
                    service.Start();
                    System.Threading.Thread.Sleep(1000);

                }
                else
                {
                    if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running && service.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                    {
                        service.Start();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("installServiceError\n" + ex.Message);
            }
            finally
            {
                _overflag = 1;
            }
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="_filepath">服务路径</param>
        /// <param name="_serviceName">服务名称</param>
        public void UnInstallService()
        {
            _overflag = 0;
            try
            {
                if (ServiceIsExisted())
                {
                    //UnInstall Service
                    AssemblyInstaller myAssemblyInstaller = new AssemblyInstaller();
                    myAssemblyInstaller.UseNewContext = true;
                    myAssemblyInstaller.Path = _filepath;
                    myAssemblyInstaller.Uninstall(null);
                    myAssemblyInstaller.Dispose();
                    
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("unInstallServiceError\n" + ex.Message);
            }
            finally
            {
                _overflag = 1;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="_serviceName">服务名称</param>
        public void StartService()
        {
            _overflag = 0;
            try
            {
                if (ServiceIsExisted())
                {
                    System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(_serviceName);
                    if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running && service.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                    {
                        service.Start();
                        for (int i = 0; i < 60; i++)
                        {
                            service.Refresh();
                            System.Threading.Thread.Sleep(1000);
                            if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                            {
                                break;
                            }
                            if (i == 59)
                            {
                                throw new Exception(_serviceName + " 启动失败");
                            }
                        }
                    }
                }
            }
            catch 
            {
                 
            }
            finally
            {
                _overflag = 1;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="_serviceName">服务名称</param>
        public void StopService( )
        {
            _overflag = 0;
            try
            {
                if (ServiceIsExisted())
                {
                    System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(_serviceName);
                    if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                    {
                        service.Stop();
                        for (int i = 0; i < 60; i++)
                        {
                            service.Refresh();
                            System.Threading.Thread.Sleep(1000);
                            if (service.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                            {
                                break;
                            }
                            if (i == 59)
                            {
                                throw new Exception(_serviceName + "停止失败");
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                _overflag = 1;
            }
        }

        public void ReStartService()
        {
            StopService();
            StartService();
        }

        public string GetAllVersion()
        {
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(_filepath);
            _fileVersion =  string.Format("{0}.{1}.{2}.{3}", myFileVersion.FileMajorPart, myFileVersion.FileMinorPart, myFileVersion.FileBuildPart, myFileVersion.FilePrivatePart);
            return _fileVersion;
        }

        public string GetFileVersion() 
        {
            // Get the file version for the notepad.
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(_filepath);

            // Print the file name and version number.
            _fileVersion =  myFileVersionInfo.FileVersion;
            return _fileVersion;
        }

        public bool ServiceIsExisted(string _serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName == _serviceName)
                {
                    return true;
                }
            }
            return false;
        }

        public ServiceControllerStatus getServiceState(string _serviceName)
        {
            System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(_serviceName);
            return service.Status;
        }
        #endregion 
    }
}
