using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Farba.Extension
{
    public static class EnumExtension
    {
        public static string Description(this System.Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
            {
                return (attributes.First() as DescriptionAttribute)?.Description;
            }

            var ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

        public static IEnumerable<(System.Enum, string)> GetAllValuesAndDescriptions(this System.Enum value)
        {
            var type = value.GetType();
            if (type.IsEnum == false)
            {
                throw new ArgumentException($"{type.Name} must be an enum type");
            }

            return System.Enum.GetValues(type).Cast<System.Enum>().Select(e => (value: e , Description: e.Description()));
        }
    }
}
