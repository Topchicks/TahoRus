using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxoNavicon.Model
{
    internal class Translate
    {
        public Dictionary<string, string> translitDict = new Dictionary<string, string>
        {
            {"Россия", "Russia"},
            #region Полные названия
            //Сокращения на русском
            {"город", "сity"},
            {"село", "country"},
            {"поселок (селского типа)", "settlement"},
            {"поселок городского типа", "settlement"},
            {"рабочий поселок", "settlement"},
            {"курортный поселок", "settlement"},
            {"поселковый совет", "village council"},
            {"поселок при станции", "settlement"},
            {"железно дорожная станция", "train station"},
            {"станция", "station"},
            {"станица", "l.c.v."},
            {"хутор.", "farm"},
            {"местечко", "place"},
            {"волость", "rural municipality"},
            {"сельсовет", "village council"},

            {"дачный поселковый совет", "holiday village council"},
            {"населенный пункт", "populated area"},
            {"улус", "ulus"},
            {"слобода", "large village"},
            {"деревня", "village"},
            {"область", "region"},
            {"кишлак", "kishlak"},
            {"район", "district"},
            {"край", "territory"},
            {"Республика", "Republic"},
            {"проспект", "Avenue"},
            {"переулок", "lane"},
            {"бульвар", "boulevard"},
            {"улица", "street"},
            {"проезд", "passway"},
            {"шоссе", "highway"},
            {"строение", "building"},
            {"площадь", "square"},
            {"квартал", "block"},
            {"офис", "office"},
            {"набережная", "embankment"},
            {"разъезд", "passing point"},
            {"заезд", "stopover"},
            {"тупик", "blind alley"},
            {"микрорайон", "micro-district"},
            {"автономный округ", "autonomous area"},
            {"площадка", "landin"},
            {"военный городок", "military town"},
            {"аул", "aul"},
            {"канал", "channel"},
            {"территория", "territory"},
            {"аллея", "alley"},
            {"дачный поселок", "village settlement"},
            {"совхоз", "farm"},
            {"садовое товарищество", "garden association"},
            {"пансионат", "pension"},
#endregion
            #region Сокращения
            //Сокращения на русском
            {"г.", "сity"},
            {"с.", "co."},
            {"п.", "sett."},
            {"пгт.", "sett."},
            {"рп.", "sett."},
            {"кп.", "sett."},
            {"пс.", "vil.c-l."},
            {"п.ст", "sett."},
            {"ж/д ст", "tr.st-n"},
            {"ст.", "st-n"},
            {"ст-ца", "l.c.v."},
            {"х.", "f."},
            {"м.", "pl."},
            {"вл.", "r.m."},
            {"сс.", "vil.c-l"},
            
            {"дп.", "h.v.c."},
            {"нп.", "p.a."},
            {"у.", "u."},
            {"сл.", "l.vil."},
            {"д.", "vil."},
            {"обл.", "reg."},
            {"к.", "k."},
            {"р-н", "dist."},
            {"кр.", "ter."},
            {"Респ.", "Rep."},
            {"пр-т", "ave."},
            {"пер.", "l."},
            {"б-р", "blvd."},
            {"ул.", "st."},
            {"пр-д", "p.w."},
            {"ш.", "hwy."},
            {"стр.", "bdg"},
            {"пл.", "sq."},
            {"кв-л", "bl."},
            {"оф.", "of."},
            {"наб.", "emb."},
            {"р-д", "p.p"},
            {"з-д", "st-ov."},
            {"туп.", "b.a."},
            {"мкр.", "m.-dist."},
            {"АО", "aut.area"},
            {"п-ка", "land."},
            {"в.г.", "m.t"},
            {"а.", "a."},
            {"к-л", "c-l"},
            {"тер.", "ter."},
            {"ал.", "al."},
            {"д.п.", "vil.sett"},
            {"с-з", "f."},
            {"панс.", "p-n"},
            #endregion
            

            // Сложные большие буквы
            {"Я", "Ya"},{"Ш", "Sh"}, {"Щ", "Sch"}, {"Ч", "Ch"}, 
            {"Ъ", ""}, {"Ы", "Y"}, {"Ь", ""},
            {"Ю", "Yu"}, {"Х", "Kh"},
            {"Ый", "Yy"},{"Ий", "Iy"},{"Ье", "Ye"},

            // Простые большие буквы
            {"А", "A"}, {"Б", "B"}, {"В", "V"},  {"Г", "G"}, {"Д", "D"},
            {"Е", "E"}, {"Ё", "E"}, {"Ж", "Zh"}, {"З", "Z"}, {"И", "I"},
            {"Й", "J"}, {"К", "K"}, {"Л", "L"},  {"М", "M"}, {"Н", "N"},
            {"О", "O"}, {"П", "P"}, {"Р", "R"},  {"С", "S"}, {"Т", "T"},
            {"У", "U"}, {"Ф", "F"}, {"Ц", "C"}, {"Э", "E"},
            
            // Сложные буквы
            {"я", "ya"}, {"ш", "sh"}, {"щ", "sch"}, {"ч", "ch"},  
            {"ъ", ""},{"ы", "y"},{"ь", ""},
            {"ю", "yu"},{"х", "kh"},
            {"ый", "yy"},{"ий", "iy"},{"ье", "ye"},
            
            // Простые буквы
            {"а", "a"}, {"б", "b"}, {"в", "v"},  {"г", "g"}, {"д", "d"},
            {"е", "e"}, {"ё", "e"}, {"ж", "zh"}, {"з", "z"}, {"и", "i"},
            {"й", "j"}, {"к", "k"}, {"л", "l"},  {"м", "m"}, {"н", "n"},
            {"о", "o"}, {"п", "p"}, {"р", "r"},  {"с", "s"}, {"т", "t"},
            {"у", "u"}, {"ф", "f"}, {"ц", "c"},  {"э", "e"},
            
            // оставляем пробел
            {" ", " "} 
        };

        public string Transliterate(string text)
        {
            // Сначала попробуем заменить сокращения
            foreach (var kvp in translitDict)
            {
                if (text.Contains(kvp.Key))
                {
                    text = text.Replace(kvp.Key, kvp.Value);
                }
            }

            // Затем транслитерируем оставшиеся символы
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (translitDict.ContainsKey(c.ToString()))
                {
                    sb.Append(translitDict[c.ToString()]);
                    Console.WriteLine(c.ToString());
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
