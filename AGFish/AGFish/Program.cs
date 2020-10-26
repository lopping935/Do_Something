using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGFish
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
         public static object obj = new object();
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        /// <summary>
        /// 未捕获异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MiniDumpUtil.TryWriteMiniDump(MiniDumpType.MiniDumpNormal);
        }
    }

    /****************************************************************************
     * @ 抓取Dump
     ****************************************************************************/
    public class MiniDumpUtil
    {
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("DbgHelp.dll")]
        private static extern bool MiniDumpWriteDump(IntPtr hProcess, int processId, IntPtr fileHandle, MiniDumpType dumpType, ref MiniDumpExceptionInfo excepInfo, IntPtr userInfo, IntPtr extInfo);

        [DllImport("DbgHelp.dll")]
        private static extern bool MiniDumpWriteDump(IntPtr hProcess, int processId, IntPtr fileHandle, MiniDumpType dumpType, IntPtr excepParam, IntPtr userInfo, IntPtr extInfo);

        public static bool TryWriteMiniDump(MiniDumpType dmpType)
        {
            var name = string.Format("{0:yyyyMMddHHmmss}.dmp", DateTime.Now);
            return TryWriteMiniDump(name, dmpType);
        }

        public static bool TryWriteMiniDump(string dmpFileName, MiniDumpType dmpType)
        {
            using (var stream = new FileStream(dmpFileName, FileMode.OpenOrCreate))
            {
                var process = Process.GetCurrentProcess();
                var exceptionInfo = new MiniDumpExceptionInfo
                {
                    ThreadId = GetCurrentThreadId(),
                    ExceptionPointers = Marshal.GetExceptionPointers(),
                    ClientPointers = true
                };
                return stream.SafeFileHandle != null && (exceptionInfo.ExceptionPointers == IntPtr.Zero ? MiniDumpWriteDump(process.Handle, process.Id, stream.SafeFileHandle.DangerousGetHandle(), dmpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) : MiniDumpWriteDump(process.Handle, process.Id, stream.SafeFileHandle.DangerousGetHandle(), dmpType, ref exceptionInfo, IntPtr.Zero, IntPtr.Zero));
            }
        }
    }

    [Flags]
    public enum MiniDumpType
    {
        MiniDumpNormal = 0x00000000,
        MiniDumpWithDataSegs = 0x00000001,
        MiniDumpWithFullMemory = 0x00000002,
        MiniDumpWithHandleData = 0x00000004,
        MiniDumpFilterMemory = 0x00000008,
        MiniDumpScanMemory = 0x00000010,
        MiniDumpWithUnloadedModules = 0x00000020,
        MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
        MiniDumpFilterModulePaths = 0x00000080,
        MiniDumpWithProcessThreadData = 0x00000100,
        MiniDumpWithPrivateReadWriteMemory = 0x00000200,
        MiniDumpWithoutOptionalData = 0x00000400,
        MiniDumpWithFullMemoryInfo = 0x00000800,
        MiniDumpWithThreadInfo = 0x00001000,
        MiniDumpWithCodeSegs = 0x00002000,
        MiniDumpWithoutAuxiliaryState = 0x00004000,
        MiniDumpWithFullAuxiliaryState = 0x00008000,
        MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
        MiniDumpIgnoreInaccessibleMemory = 0x00020000,
        MiniDumpWithTokenInformation = 0x00040000,
        MiniDumpWithModuleHeaders = 0x00080000,
        MiniDumpFilterTriage = 0x00100000,
        MiniDumpValidTypeFlags = 0x001fffff
    }

    public struct MiniDumpExceptionInfo
    {
        public int ThreadId;
        public IntPtr ExceptionPointers;
        public bool ClientPointers;
    }

}
