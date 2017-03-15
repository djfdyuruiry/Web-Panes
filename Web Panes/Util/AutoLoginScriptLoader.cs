using System.IO;
using System.Reflection;

namespace WebPanes.Util
{
    public class AutoLoginScriptLoader
    {
        private const string UserNamePlaceholder = "{{username}}";
        private const string PasswordPlaceholder = "{{password}}";

        private readonly static string ScriptResourceFileName = $"{nameof(WebPanes)}.Resources.AutoLoginScript.js";

        private string _script;

        public string LoadScript(string userName, string password)
        {
            if (_script == null)
            {
                LoadScriptFromResourceFile();
            }
                        
            var script = _script.Replace(UserNamePlaceholder, userName);
            script = script.Replace(PasswordPlaceholder, password);

            return script;
        }

        private void LoadScriptFromResourceFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = ScriptResourceFileName;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    _script = reader.ReadToEnd();
                }
            }
        }
    }
}
