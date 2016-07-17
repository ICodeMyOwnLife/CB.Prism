using System.Reflection;


namespace CB.Prism.Interactivity
{
    internal class ReflectionHelper
    {
        #region Implementation
        internal static object GetPropertyValue(object obj, string path)
        {
            var pathParts = path.Split('.');

            foreach (var part in pathParts)
            {
                var prop = obj?.GetType().GetTypeInfo().GetProperty(part);
                obj = prop?.GetValue(obj);
            }

            return obj;
        }
        #endregion
    }
}