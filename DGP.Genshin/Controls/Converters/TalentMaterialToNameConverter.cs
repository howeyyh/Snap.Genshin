using DGP.Genshin.Data.Talent;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class TalentMaterialToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TalentMaterial material = (TalentMaterial)value;
            return material switch
            {
                TalentMaterial.Ballad => "诗文",
                TalentMaterial.Diligence => "勤劳",
                TalentMaterial.Freedom => "自由",
                TalentMaterial.Gold => "黄金",
                TalentMaterial.Prosperity => "繁荣",
                TalentMaterial.Resistance => "抗争",
                _ => "",
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
