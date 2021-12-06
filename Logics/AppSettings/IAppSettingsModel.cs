using Logics.AppSettings.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logics.AppSettings
{
    public interface IAppSettingsModel
    {
        SecuritySettings SecuritySettings { get; set; }
    }
}
