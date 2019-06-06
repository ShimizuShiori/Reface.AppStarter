using System;

namespace Reface.AppStarter
{
    public class EnumHelper
    {
        public static bool HasFlag<T>(T value, T flag)
        {
            if (!typeof(T).IsEnum)
                return false;
            int i_value = ConvertToInt32(value);
            int i_flag = ConvertToInt32(flag);
            return (i_value & i_flag) == i_flag;
        }

        public static T RemoveFlag<T>(T value, T flag)
        {
            if (!typeof(T).IsEnum)
                throw new InvalidCastException("T 必须是枚举类型");
            int i_value = ConvertToInt32(value);
            int i_flag = ConvertToInt32(flag);
            int i_result = i_value - (i_value & i_flag);
            return (T)Enum.ToObject(typeof(T), i_result);
        }

        private static int ConvertToInt32<T>(T value)
        {
            return Convert.ToInt32(value);
        }
    }
}
