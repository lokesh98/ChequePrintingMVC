using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer
{
    public static class LDAPSettings
    {
        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings.Get(name);

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
        public static string LDAPDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPDomain"];
            }
        }
        public static string LDAPUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPUrl"];
            }
        }
        public static bool IsTesting
        {
            get
            {
                return Setting<bool>("isTesting");
            }
        }
    }
}
