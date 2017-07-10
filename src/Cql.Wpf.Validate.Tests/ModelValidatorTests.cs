using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.ViewModel;
using System.Collections.Generic;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class ModelValidatorTests
    {


        [TestMethod]
        public void Model()
        {
            ValidationSet validationSet = new ValidationSet();
            Config.ViewConfig viewConfig = new Config.ViewConfig();
            List<Config.FieldConfig> viewConfigFields = new List<Config.FieldConfig>();
            viewConfigFields.Add(new Config.FieldConfig().);
            viewConfig.Fields = viewConfigFields;
            validationSet.ViewConfig = new ViewConfigViewModel(viewConfig);
            validationSet.Model = null;
            Assert.IsTrue(ValidationContext.Validate(validationSet).IsValid);
        }
    }
}
