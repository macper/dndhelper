using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace DnDHelper
{
    [Serializable]
    public class Helper : INotifyPropertyChanged
    {
        private DateTime _currentTime = new DateTime(1300, 5, 13);
        public DateTime CurrentTime { get { return _currentTime; } set { _currentTime = value; OnPropertyChanged("CurrentTime"); if (CurrentTimeChanged != null) { CurrentTimeChanged(this, null); } } }
        public List<CharacterGroup> Groups;

        public string Notes { get; set; }
        public int XP { get; set; }
        public int Version { get; set; }
        public Calendar Calendar { get; set; }
        public event EventHandler CurrentTimeChanged;

        // TODO: wywalić bo przeniesione do Rules

        public List<Item> Items;
        public List<Effect> Effects;

        public Helper()
        {
            Groups = new List<CharacterGroup>();
            Calendar = new Calendar();

            // TODO : wywalić
            Items = new List<Item>();
            Effects = new List<Effect>();
        }

        public void SaveState()
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Helper));
            foreach (CharacterGroup chGr in Groups)
            {
                foreach (Character ch in chGr.Members)
                {
                    ch.Serialize();
                }
            }
            using (var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Helper.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        public static Helper LoadState()
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Helper));
            Helper helper;
            using (var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Helper.xml", FileMode.Open))
            {
                helper = (Helper)xmlSerializer.Deserialize(fs);
            }
            foreach (CharacterGroup chGr in helper.Groups)
            {
                foreach (Character ch in chGr.Members)
                {
                    ch.Deserialize();
                }
            }
            return helper;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool GetVersionFromServer()
        {
            FTPVersionServer.VersionServer server = GetVersionServer();
            var versions = server.GetAllAppVersions();
            var list = new List<int>();
            foreach (string vs in versions)
            {
                try
                {
                    list.Add(int.Parse(vs));
                }
                catch
                {
                }
            }
            list.Sort();
            list.Reverse();
            int latestVersion = list[0];
            if (latestVersion > Version)
            {
                server.GetFile("Helper.xml", latestVersion.ToString(), "Helper.xml");
                return true;
            }
            return false;
        }

        public void PutVersionOnServer()
        {
            FTPVersionServer.VersionServer server = GetVersionServer();
            Version++;
            SaveState();
            server.PutFile("Helper.xml", Version.ToString());
        }

        private FTPVersionServer.VersionServer GetVersionServer()
        {
            System.Collections.Specialized.NameValueCollection appSett = System.Configuration.ConfigurationManager.AppSettings;
            FTPVersionServer.VersionServer server = new FTPVersionServer.VersionServer(appSett["FTPServer"], appSett["FTPUser"], appSett["FTPPassword"], appSett["FTPAppDir"]);
            return server;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
