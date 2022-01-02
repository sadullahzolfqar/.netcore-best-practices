using Logics.AppSettings.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logics.AppSettings
{
    public class AppSettingsModel: IAppSettingsModel
    {
        public SecuritySettings SecuritySettings { get; set; }

        public AppSettingsModel()
        {
            SecuritySettings = new SecuritySettings();
        }
    }
}
