using System;

namespace DGP.Genshin.Service
{
    public class Setting
    {
        public const string ShowUnreleasedData = "ShowUnreleasedCharacter";
        public const string PresentTravelerElementType = "PresentTravelerElementType";
        public const string AppTheme = "AppTheme";
        public static T EnumConverter<T>(object n)
        {
            return (T)Enum.Parse(typeof(T), n.ToString());
        }
    }
}
