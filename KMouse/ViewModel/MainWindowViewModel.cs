using KMouse.Controls;
using Newtonsoft.Json;

namespace KMouse.ViewModel;

public class MainWindowViewModel : BaseViewModel
{
    private int _sensValue;
    private Hotkey _hotkey;

    public int SensValue
    {
        get => _sensValue;
        set
        {
            _sensValue = value;
            OnPropertyChanged();
            Save();
        }
    }

    public Hotkey Hotkey
    {
        get => _hotkey;
        set
        {
            _hotkey = value;
            OnPropertyChanged();
            Save();
        }
    }

    private void Save()
    {
        ConfigurationHelper.Save(_sensValue, _hotkey);
    }

    public void Init()
    {
        var settings = App.Configuration;
        if (settings["Sens"] != null)
        {
            SensValue = int.Parse(settings["Sens"]);
        }

        if (settings["Hotkey"] != null)
        {
            Hotkey = JsonConvert.DeserializeObject<Hotkey>(settings["Hotkey"]);
        }
    }
}