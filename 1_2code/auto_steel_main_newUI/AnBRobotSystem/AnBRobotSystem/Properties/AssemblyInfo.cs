using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("AnBRobotSystem")]
[assembly: AssemblyDescription("大型线喷号贴标机器人信息系统")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("莱钢电子有限公司")]
[assembly: AssemblyProduct("AnBRobotSystem")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("408dc8be-0119-4d34-9901-ffa69d4e3d30")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 

//
// 可以指定所有这些值，也可以使用“内部版本号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0")]
[assembly: AssemblyFileVersion("1.0")]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4App.config", ConfigFileExtension = "config", Watch = true)]
