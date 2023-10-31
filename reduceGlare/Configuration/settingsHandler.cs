using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using reduceGlare.Configuration;

namespace reduceGlare.Configuration
{
    internal class settingsHandler : PersistentSingleton<settingsHandler>
    {
        [UIValue("modEnable")]
        public bool modEnable
        {
            get => settings.Instance.modEnable;
            set
            {
                settings.Instance.modEnable = value;
            }
        }
        [UIValue("playersPlace")]
        public bool playersPlace
        {
            get => settings.Instance.playersPlace;
            set
            {
                settings.Instance.playersPlace = value;
            }
        }
        [UIValue("reduceGlare")]
        public bool reduceGlare
        {
            get => settings.Instance.reduceGlare;
            set
            {
                settings.Instance.reduceGlare = value;
            }
        }
    }
}
