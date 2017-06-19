using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cql.Wpf.Validate.Config
{
	/// <summary>
	/// Provides <see cref="ViewConfig" />'s by reading in a JSON file.
	/// The file can be located on a local (or network) file system, or at a HTTP address.
	/// </summary>
    public class JsonFileConfigProvider : IConfigProvider
    {
        private readonly string _fileLocation;
        private ValidationConfig _validationConfig;

        public JsonFileConfigProvider(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

		/// <summary>
		/// Gets a specific <see cref="ViewConfig"/> in the file.
		/// </summary>
		/// <returns>Null if not found.</returns>
        public async Task<ViewConfig> LoadViewConfigAsync(string name)
        {
            var loaded = await EnsureLoaded();
            if (loaded)
            {
                return CloneViewConfig(_validationConfig.ViewConfigs.FirstOrDefault(s => s.Name == name));
            }
            return null;
        }

	    /// <summary>
		/// Gets the entire <see cref="ValidationConfig"/>.
		/// </summary>
		/// <returns>Null if not found.</returns>
        public async Task<ValidationConfig> LoadConfigAsync()
        {
            var loaded = await EnsureLoaded();
            if (loaded)
            {
                return _validationConfig;
            }
            return null;
        }

        async Task<bool> EnsureLoaded()
        {
	        if (_validationConfig == null)
            {
	            string fileContents;
	            if (_fileLocation.StartsWith("http"))
                    fileContents = await LoadFromNetworkAsync();
                else
                    fileContents = LoadFromFileSystem();

                if (!string.IsNullOrWhiteSpace(fileContents))
                {
                    _validationConfig = JsonConvert.DeserializeObject<ValidationConfig>(fileContents);
                    return true;
                }
                return false;
            }
            return true;
        }

        private string LoadFromFileSystem()
        {
            if (File.Exists(_fileLocation))
            {
                var contentAsString = File.ReadAllText(_fileLocation);
                return contentAsString;
            }
            return null;
        }

        private async Task<string> LoadFromNetworkAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_fileLocation);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();
                return contentAsString;
            }
            return null;
		}

	    private ViewConfig CloneViewConfig(ViewConfig config)
	    {
		    if (config == null) return null;
		    var json = JsonConvert.SerializeObject(config);
		    return JsonConvert.DeserializeObject<ViewConfig>(json);
	    }
	}
}