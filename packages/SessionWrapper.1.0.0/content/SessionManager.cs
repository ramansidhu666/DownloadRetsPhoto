using System.Reflection;
using System.Web;

namespace StateManagement
{
    public static class SessionManager
    {
        #region session variables
        //Add your session variables like Session_UserID, this name will be considered as session key.
        //to set value - 
        //SessionManager.Session_UserID = 120;
        //it will automatically store the value in session
        //to get value
        //int? existinguserid = SessionManager.Session_UserID;
        //it will fetch value from session

        public static int? Session_UserID
        {
            get
            {
                return GetFromSession(MethodBase.GetCurrentMethod());
            }
            set
            {
                SetInSession(MethodBase.GetCurrentMethod(), value);
            }
        }
        #endregion

        #region private functions
        private static void SetInSession(MethodBase propertyName, dynamic Val)
        {
            Set(propertyName.Name.Replace("set_", ""), Val);
        }

        private static dynamic GetFromSession(MethodBase propertyName, dynamic DefaultVal = null)
        {
            return Get(propertyName.Name.Replace("get_", ""), DefaultVal);
        }
        #endregion

        #region Public function
        public static bool Set(string key, dynamic value)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                return false;
            }

            current.Session.Add(key, value);
            return true;
        }

        public static dynamic Get(string key, dynamic defaultValue = null)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                return defaultValue;
            }

            var valueFromSession = current.Session[key];
            if (valueFromSession != null)
            {
                return valueFromSession;
            }
            return defaultValue;
        }

        public static bool Remove(string key)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                return false;
            }
            current.Session.Remove(key);
            return true;
        }

        public static bool ClearSession()
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                return false;
            }

            current.Session.Clear();
            current.Session.Abandon();

            return true;
        }
        #endregion
    }
}
