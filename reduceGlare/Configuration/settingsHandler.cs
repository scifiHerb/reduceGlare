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
        [UIValue("savePlayersPlace")]
        public bool savePlayersPlace
        {
            get => settings.Instance.savePlayersPlace;
            set
            {
                settings.Instance.savePlayersPlace = value;
            }
        }
        [UIValue("onlyReduceGlare")]
        public bool onlyReduceGlare
        {
            get => settings.Instance.onlyReduceGlare;
            set
            {
                settings.Instance.onlyReduceGlare = value;
            }
        }
    }
}
