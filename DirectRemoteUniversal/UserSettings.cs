using Model;
using System;
using System.Collections.Generic;
using Windows.Storage;

namespace DirectRemoteUniversal
{
    public class UserSettings
    {
        private const string _SETTINGS_FILE = "settings.txt";

        public IList<DirecTvBox> Boxes { get; set; }

        public DirecTvBox LastUsedBox { get; set; }

        public async void SaveSettings()
        {
            var settingsFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(_SETTINGS_FILE, CreationCollisionOption.ReplaceExisting);

            var settingsText = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            await FileIO.WriteTextAsync(settingsFile, settingsText);
        }

        public async void LoadSettings()
        {
            var settingsFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(_SETTINGS_FILE, CreationCollisionOption.OpenIfExists);

            var settingsText = await FileIO.ReadTextAsync(settingsFile);
            var settings = Newtonsoft.Json.JsonConvert.DeserializeObject<UserSettings>(settingsText);
        }
    }
}
