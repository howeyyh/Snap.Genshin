using DGP.Genshin.Data;
using System;

namespace DGP.Genshin.Service
{
    public class TravelerPresentService
    {
        public void SetPresentTraveler()
        {
            Element element = SettingService.Instance.GetOrDefault(Setting.PresentTravelerElementType, Element.Anemo, n => (Element)Enum.Parse(typeof(Element), n.ToString()));
            Character TravelerSource = CharacterManager.Instance["Traveler" + element];

            CharacterManager.Instance["TravelerPresent"].ImageUri = TravelerSource.ImageUri;
            CharacterManager.Instance["TravelerPresent"].Element = TravelerSource.Element;
        }

        #region 单例
        private static TravelerPresentService instance;
        private static readonly object _lock = new object();
        private TravelerPresentService()
        {

        }
        public static TravelerPresentService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new TravelerPresentService();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
}
