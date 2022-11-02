

using System;
using System.Reflection;

namespace TrackingManagement.Utils
{
    public class EntityUtils
    {
        public static T updateRecord<T, V>(T Base, V Update)
        {
            foreach(PropertyInfo prop in Base.GetType().GetProperties())
            {
                var updateProp = Update.GetType().GetProperty(prop.Name);
                if (updateProp != null)
                {
                    var value = updateProp.GetValue(Update, null);
                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                    prop.SetValue(Base, safeValue, null);
                }
            }
            return Base;
        }
    }
}
