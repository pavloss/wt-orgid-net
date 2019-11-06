using System.IO;
using System.Reflection;

namespace WindingTreeOrganizationsNet.Helper
{
    public static class JsonResourceReader
    {
        public static string ReadJsonFromResource(string resourceName)
        {
            var jsonContent = "";
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using StreamReader reader = new StreamReader(stream);
                jsonContent = reader.ReadToEnd(); //Make string equal to full file
            }

            return jsonContent;
        }
    }
}
