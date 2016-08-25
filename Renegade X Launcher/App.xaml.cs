﻿using System.Windows;

namespace LauncherTwo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void StartupApp(object sender, StartupEventArgs e)
        {
            foreach (string a in e.Args)
            {
                if (a.StartsWith("--patch-result="))
                {
                    string code = a.Substring("--patch-result=".Length);
                    if (code != "0")
                    {
                        MessageBox.Show(string.Format("Failed to update the launcher (code {0}).\n\nPlease close any applications related to Renegade-X and try again.", code), "Patch failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (a.StartsWith("--firstInstall"))
                {
                    Installer x = new Installer();
                    x.Show();
                    x.FirstInstall();
                }
            }

            if (LauncherTwo.Properties.Settings.Default.UpgradeRequired)
            {
                LauncherTwo.Properties.Settings.Default.Upgrade();
                LauncherTwo.Properties.Settings.Default.UpgradeRequired = false;
                LauncherTwo.Properties.Settings.Default.Save();
            }

            /* Commented out untill I found a better way to intergrate it in the installation
            if (!GameInstallation.IsRootPathPlausible())
            {
                var result = MessageBox.Show("The game path seems to be incorrect. Please ensure that the launcher is placed in the correct location. If you proceed, files in the following location might be affected:\n\n" + GameInstallation.GetRootPath() + "\n\nAre you sure want to proceed?", "Invalid game path", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result != MessageBoxResult.Yes)
                {
                    Shutdown();
                    return;
                }
            }
            */
            //If no args are present, normally start the launcher
            if(e.Args.Length == 0)
            {
                new MainWindow().Show();
            }
            
        }
    }
}
