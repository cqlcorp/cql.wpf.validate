using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Config;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class JsonFileConfigProviderTests
    {
        #region LoadViewConfigAsync
        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileExistsOnLocalConfigExists()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("config.json");
            var viewConfig = await provider.LoadViewConfigAsync("TestFieldName");
            Assert.IsTrue(viewConfig.Name.ToString().Equals("TestFieldName"));
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileExistsOnLocalConfigDoesNotExist()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("config.json");
            var viewConfig = await provider.LoadViewConfigAsync("ExistentialCrisis");
            Assert.IsTrue(viewConfig == null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileDoesNotExistOnLocal()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("irrationalConfig.json");
            var viewConfig = await provider.LoadViewConfigAsync("TestFieldName");
            Assert.IsTrue(viewConfig == null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileExistsOnRemoteConfigExists()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("https://raw.githubusercontent.com/cqlcorp/cql.wpf.validate/unitTests/src/Cql.Wpf.Validate.Tests/config.json");
            var viewConfig = await provider.LoadViewConfigAsync("TestFieldName");
            Assert.IsTrue(viewConfig.Name.ToString().Equals("TestFieldName"));
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileExistsOnRemoteConfigDoesNotExist()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("https://raw.githubusercontent.com/cqlcorp/cql.wpf.validate/unitTests/src/Cql.Wpf.Validate.Tests/config.json");
            var viewConfig = await provider.LoadViewConfigAsync("ExistentialCrisis");
            Assert.IsTrue(viewConfig == null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadViewConfigAsync_FileDoesNotExistOnRemote()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("http://notareallink.com/config.json");
            var viewConfig = await provider.LoadViewConfigAsync("TestFieldName");
            Assert.IsTrue(viewConfig == null);
        }
        #endregion

        #region LoadConfigAsync
        [TestMethod]
        public async Task JsonFileConfigProvider_LoadConfigAsync_FileExistsOnLocal()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("config.json");
            var config = await provider.LoadConfigAsync();
            Assert.IsTrue(config != null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadConfigAsync_FileDoesNotExistOnLocal()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("irrationalConfig.json");
            var config = await provider.LoadConfigAsync();
            Assert.IsTrue(config == null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadConfigAsync_FileExistsOnRemote()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("https://raw.githubusercontent.com/cqlcorp/cql.wpf.validate/unitTests/src/Cql.Wpf.Validate.Tests/config.json");
            var config = await provider.LoadConfigAsync();
            Assert.IsTrue(config != null);
        }

        [TestMethod]
        public async Task JsonFileConfigProvider_LoadConfigAsync_FileDoesNotExistOnRemote()
        {
            JsonFileConfigProvider provider = new JsonFileConfigProvider("http://notareallink.com/config.json");
            var config = await provider.LoadConfigAsync();
            Assert.IsTrue(config == null);
        }
        #endregion
    }
}
