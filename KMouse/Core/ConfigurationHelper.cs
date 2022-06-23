using System.Dynamic;
using System.IO;
using KMouse.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KMouse;

public static class ConfigurationHelper
{
    public static void Save(int sensetivity, Hotkey hotkey)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var json = File.ReadAllText(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);
        config.Sens = sensetivity;
        config.Hotkey = JsonConvert.SerializeObject(hotkey);
        var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);
        File.WriteAllText(appSettingsPath, newJson);
    }
}