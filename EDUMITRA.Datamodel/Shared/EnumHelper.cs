using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Datamodel.Shared
{
    public class NameValueModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class TypeViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ParentValue { get; set; }
    }
    public class ParentViewModel
    {
        public long Value { get; set; }
        public string Name { get; set; }
        public int ParentValue { get; set; }
    }

    public class ParentModel
    {
        public long Value { get; set; }
        public string Name { get; set; }
        public decimal ParentValue { get; set; }
    }
    public class TetraTypeViewModel
    {
        public string Name { get; set; }
        public long Value { get; set; }
        public decimal Value1 { get; set; }
        public int Value2 { get; set; }
    }
    public class MultiSelectTypeViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
namespace EDUMITRA.Datamodel.Shared
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumHelper
    {
        public static string GetDisplayName<TEnum>(this TEnum value)
        {
            string outString = string.Empty;
            try
            {
                Type enumType = value.GetType();
                if (!enumType.GetTypeInfo().IsEnum)
                    return "";

                string enumValue = Enum.GetName(enumType, value);
                MemberInfo member = enumType.GetMember(enumValue)[0];

                outString = enumValue;

                var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false).ToList();
                if (attrs.Count > 0)
                {
                    outString = ((DisplayAttribute)attrs[0]).Name;

                    if (((DisplayAttribute)attrs[0]).ResourceType != null)
                    {
                        outString = ((DisplayAttribute)attrs[0]).GetName();
                    }
                }
            }
            catch (Exception)
            {
            }
            return outString;
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given int.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
    }
}

