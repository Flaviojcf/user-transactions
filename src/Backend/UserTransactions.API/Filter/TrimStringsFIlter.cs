using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.API.Filter
{
    [ExcludeFromCodeCoverage]
    public class TrimStringsActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg != null)
                {
                    TrimAllStrings(arg);
                }
            }
        }

        private static void TrimAllStrings(object obj)
        {
            var properties = obj.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    prop.SetValue(obj, value.Trim());
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
