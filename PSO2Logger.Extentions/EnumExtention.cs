using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSO2Logger.Extentions {
    public static class EnumExtention {

#nullable enable annotations
        public static string? GetDescription<T>(this T value) where T : Enum {
            var description = typeof(T).GetField(value.ToString())
                                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                       .Cast<DescriptionAttribute>()
                                       .FirstOrDefault()
                                       ?.Description;
            return description ?? null;
        }

        public static IEnumerable<T> Enumerate<T>(this T _) where T : Enum => Enum.GetValues(typeof(T)).Cast<T>();
        public static IEnumerable<string?> EnumerateDescription<T>(this T value) where T : Enum {
            return typeof(T).GetField(value.ToString())
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .Cast<DescriptionAttribute>()
                            .Select(x => x?.Description);
        }
#nullable restore
    }
}
