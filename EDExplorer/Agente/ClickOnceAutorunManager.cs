namespace EDExplorer.Autorun
{
    //    using DW.ELA.Interfaces;
    using Microsoft.Win32;
    using System;
    using System.IO;

    public interface IAutorunManager
    {
        bool AutorunEnabled { get; set; }
    }
    public class ClickOnceAutorunManager : IAutorunManager
    {
        private const string AutorunRegistryKey = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string EDExplorerKey = @"EDExplorer";

        public bool AutorunEnabled
        {
            get
            {
                using (var readHandle = Registry.CurrentUser.OpenSubKey(AutorunRegistryKey, false))
                    return readHandle.GetValue(EDExplorerKey) as string == ExecutablePath;
            }

            set
            {
                using (var writeHandle = Registry.CurrentUser.OpenSubKey(AutorunRegistryKey, true))
                {
                    if (value && !AutorunEnabled)
                        writeHandle.SetValue(EDExplorerKey, ExecutablePath);
                    else if (!value && AutorunEnabled)
                        writeHandle.DeleteValue(EDExplorerKey);
                }
            }
        }

        protected virtual string ExecutablePath
        {
            get
            {
                string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                return Path.Combine(baseDir, @"EDExplorer\EDExplorer.appref-ms");
            }
        }
    }
}
