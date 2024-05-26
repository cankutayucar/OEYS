using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OEYS.WEB.Utilities.Extensions
{
    public static class EnumHelper
    {
        static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var member = type.GetMember(enumValue.ToString());
            if (member.Length == 0)
            {
                return null;
            }
            var displayAttribute = member[0].GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? enumValue.ToString();
        }
        public static string GetEnumDisplayNameFromInt<TEnum>(int enumValue) where TEnum : Enum
        {
            var enumMember = (TEnum)Enum.ToObject(typeof(TEnum), enumValue);
            return enumMember.GetDisplayName();
        }

        public static Dictionary<int, string> GetEnumDisplayNames<TEnum>() where TEnum : Enum
        {
            var type = typeof(TEnum);
            var values = Enum.GetValues(type).Cast<TEnum>();
            var displayNames = new Dictionary<int, string>();

            foreach (var value in values)
            {
                int intValue = Convert.ToInt32(value);
                string displayName = value.GetDisplayName();
                displayNames.Add(intValue, displayName);
            }

            return displayNames;
        }
    }
}
