namespace EDExplorer.Deployment
{
    using DW.ELA.Interfaces;
    using System;
    using System.Deployment.Application;
    using System.IO;

    public class DataPathManager : IPathManager
    {
        public string SettingsDirectory => ApplicationDeployment.IsNetworkDeployed ? AppDataDirectory : LocalDirectory;

        public string LogDirectory => ApplicationDeployment.IsNetworkDeployed ? Path.Combine(AppDataDirectory, "Log") : Path.Combine(LocalDirectory, "Log");

        private string AppDataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EDExplorer");

        private string LocalDirectory => Path.GetDirectoryName(new Uri(typeof(EDExplorer).Assembly.CodeBase).LocalPath);
    }
}
