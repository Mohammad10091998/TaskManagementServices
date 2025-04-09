using System.ComponentModel;
using System.Reflection;

namespace TaskManagementServices.Enums
{
    public enum StatusEnum
    {
        [Description("Pending")]
        Pending = 0,

        [Description("In Progress")]
        InProgress = 1,

        [Description("Completed")]
        Completed = 2
    }
    public static class EnumDescriptionExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
