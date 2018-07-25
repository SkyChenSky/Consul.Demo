using System.Configuration;

namespace Consul.Demo.Common
{
    public static class Extension
    {
        /// <summary>
        /// 自定义配置参数
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ValueOfAppSetting(this string inputStr)
        {
            return ConfigurationManager.AppSettings[inputStr];
        }
    }
}
