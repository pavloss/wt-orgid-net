using System;
using Newtonsoft.Json.Linq;

namespace WindingTreeOrganizationsNet.Helper
{
    public static class ExtensionMethods
    {
        public static bool IsValidJson(this string s)
        {
            try
            {
                JObject.Parse(s);
                return true;
            }
            catch (Exception e)
            {
                //log exception
                return false;
            }
        }
    }
}
