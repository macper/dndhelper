using System;
using System.Collections.Generic;

namespace DnDHelper.Domain
{
    public class AppSettingDefaultValueProvider : IDefaultEntityProvider<AppSetting>
    {
        private readonly Dictionary<string, string> _defaultValues = new Dictionary<string, string>();

        public AppSettingDefaultValueProvider()
        {
            _defaultValues.Add(Const.AppSettings.Time, new DateTime(1300,1,1).ToString());
        }

        public AppSetting GetDefaultValue(string name)
        {
            var appSetting = new AppSetting
                                 {
                                     Name = name,
                                     Value = _defaultValues.ContainsKey(name) ? _defaultValues[name] : string.Empty
                                 };
            return appSetting;
        }
    }
}