using Microsoft.Win32;
using System;

namespace DGP.Snap.Framework.Shell.Setting
{
    /// <summary>
    /// 在注册表中读写设置
    /// </summary>
    public class Setting : IDisposable
    {
        public string ApplicationName { get; private set; }
        private readonly RegistryKey setting;
        private readonly RegistryKey currentUser;
        private readonly RegistryKey software;
        public Setting(string applicationName)
        {
            ApplicationName = applicationName;
            currentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            software = currentUser.OpenSubKey("SOFTWARE", true);
            setting = software.CreateSubKey(ApplicationName, true);
        }
        /// <summary>
        /// 在注册表 <see cref="RegistryHive.CurrentUser"/> 键值下读写设置
        /// </summary>
        /// <param name="key">值的名称</param>
        /// <returns></returns>
        public object this[string key]
        {
            get => setting.GetValue(key);
            set => setting.SetValue(key, value);
        }

        public void Dispose()
        {
            setting.Dispose();
            software.Dispose();
            currentUser.Dispose();
        }
    }
}
