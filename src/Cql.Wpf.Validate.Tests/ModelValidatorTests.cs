using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate;
using System.Collections.Generic;
using Cql.Wpf.Validate.Validators;
using Cql.Wpf.Validate.ViewModel;
using System.Threading.Tasks;
using System.Diagnostics;
using Cql.Wpf.Validate.Config;
using System.IO.Packaging;
using System.Windows;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class ModelValidatorTests
    {
        [AssemblyInitialize]
        public static void InitializeTestAssembly(TestContext ctx)
        {
            var app = new Application();
        }

        #region FieldName
        [TestMethod]
        public async Task Model_ValidateFieldName()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testFieldNameConfig = await ValidationContext.LoadViewConfigAsync("TestFieldName");
            var testFieldName = new TestFieldNameModel { ViewConfig = new ViewConfigViewModel(testFieldNameConfig) };

            Assert.IsTrue(ValidationContext.Validate(testFieldName.ViewConfig, testFieldName).IsValid);
        }
        #endregion

        #region Required
        [TestMethod]
        public async Task Model_ValidateRequired_RequiredFilled()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testValidateRequiredConfig = await ValidationContext.LoadViewConfigAsync("TestValidateRequired");
            var testValidateRequired = new TestValidateRequiredModel { ViewConfig = new ViewConfigViewModel(testValidateRequiredConfig) };
            testValidateRequired.RequiredField = "filled";

            Assert.IsTrue(ValidationContext.Validate(testValidateRequired.ViewConfig, testValidateRequired).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateRequired_RequiredUnfilled()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testValidateRequiredConfig = await ValidationContext.LoadViewConfigAsync("TestValidateRequired");
            var testValidateRequired = new TestValidateRequiredModel { ViewConfig = new ViewConfigViewModel(testValidateRequiredConfig) };

            Assert.IsFalse(ValidationContext.Validate(testValidateRequired.ViewConfig, testValidateRequired).IsValid);
        }
        #endregion

        #region MinLength
        [TestMethod]
        public async Task Model_ValidateMinLength_UnderLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinLength");
            var testMinLength = new TestMinLengthModel { ViewConfig = new ViewConfigViewModel(testMinLengthConfig) };
            testMinLength.MinField = "a";

            Assert.IsFalse(ValidationContext.Validate(testMinLength.ViewConfig, testMinLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinLength_AtLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinLength");
            var testMinLength = new TestMinLengthModel { ViewConfig = new ViewConfigViewModel(testMinLengthConfig) };
            testMinLength.MinField = "ab";

            Assert.IsTrue(ValidationContext.Validate(testMinLength.ViewConfig, testMinLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinLength_OverLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinLength");
            var testMinLength = new TestMinLengthModel { ViewConfig = new ViewConfigViewModel(testMinLengthConfig) };
            testMinLength.MinField = "abc";

            Assert.IsTrue(ValidationContext.Validate(testMinLength.ViewConfig, testMinLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinLength_Null()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinLength");
            var testMinLength = new TestMinLengthModel { ViewConfig = new ViewConfigViewModel(testMinLengthConfig) };
            testMinLength.MinField = null;

            Assert.IsTrue(ValidationContext.Validate(testMinLength.ViewConfig, testMinLength).IsValid);
        }
        #endregion

        #region MaxLength
        [TestMethod]
        public async Task Model_ValidateMaxLength_UnderLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMaxLength");
            var testMaxLength = new TestMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMaxLengthConfig) };
            testMaxLength.MaxField = "a";

            Assert.IsTrue(ValidationContext.Validate(testMaxLength.ViewConfig, testMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMaxLength_AtLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMaxLength");
            var testMaxLength = new TestMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMaxLengthConfig) };
            testMaxLength.MaxField = "ab";

            Assert.IsTrue(ValidationContext.Validate(testMaxLength.ViewConfig, testMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMaxLength_OverLength()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMaxLength");
            var testMaxLength = new TestMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMaxLengthConfig) };
            testMaxLength.MaxField = "abc";

            Assert.IsFalse(ValidationContext.Validate(testMaxLength.ViewConfig, testMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMaxLength_Null()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMaxLength");
            var testMaxLength = new TestMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMaxLengthConfig) };
            testMaxLength.MaxField = null;

            Assert.IsTrue(ValidationContext.Validate(testMaxLength.ViewConfig, testMaxLength).IsValid);
        }
        #endregion

        #region MinMaxLength
        [TestMethod]
        public async Task Model_ValidateMinMaxLength_UnderMin()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = "a";

            Assert.IsFalse(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinMaxLength_AtMin()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = "ab";

            Assert.IsTrue(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinMaxLength_BetweenMinMax()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = "abc";

            Assert.IsTrue(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinMaxLength_AtMax()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = "abcd";

            Assert.IsTrue(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinMaxLength_OverMax()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = "abcde";

            Assert.IsFalse(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMinMaxLength_Null()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMinMaxLengthConfig = await ValidationContext.LoadViewConfigAsync("TestMinMaxLength");
            var testMinMaxLength = new TestMinMaxLengthModel { ViewConfig = new ViewConfigViewModel(testMinMaxLengthConfig) };
            testMinMaxLength.MinMaxField = null;

            Assert.IsTrue(ValidationContext.Validate(testMinMaxLength.ViewConfig, testMinMaxLength).IsValid);
        }
        #endregion

        #region Regex
        [TestMethod]
        public async Task Model_ValidateRegex_InvalidString()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testRegexConfig = await ValidationContext.LoadViewConfigAsync("TestRegex");
            var testRegex = new TestRegexModel { ViewConfig = new ViewConfigViewModel(testRegexConfig) };
            testRegex.RegexField = "notanemail";

            Assert.IsFalse(ValidationContext.Validate(testRegex.ViewConfig, testRegex).IsValid);
            //Trace.Write(ValidationContext.Validate(testRegex.ViewConfig, testRegex).Messages[0].Message.ToString());
        }

        [TestMethod]
        public async Task Model_ValidateRegex_ValidString()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testRegexConfig = await ValidationContext.LoadViewConfigAsync("TestRegex");
            var testRegex = new TestRegexModel { ViewConfig = new ViewConfigViewModel(testRegexConfig) };
            testRegex.RegexField = "test@email.com";

            Assert.IsTrue(ValidationContext.Validate(testRegex.ViewConfig, testRegex).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateRegex_NullString()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testRegexConfig = await ValidationContext.LoadViewConfigAsync("TestRegex");
            var testRegex = new TestRegexModel { ViewConfig = new ViewConfigViewModel(testRegexConfig) };
            testRegex.RegexField = null;

            Assert.IsTrue(ValidationContext.Validate(testRegex.ViewConfig, testRegex).IsValid);
        }
        #endregion

        #region Message
        [TestMethod]
        public async Task Model_ValidateMessage_Invalid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testValidationMessageConfig = await ValidationContext.LoadViewConfigAsync("TestValidationMessage");
            var testValidationMessage = new TestValidationMessageModel { ViewConfig = new ViewConfigViewModel(testValidationMessageConfig) };
            testValidationMessage.ValidationMessageField = "a";

            Assert.IsFalse(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).IsValid);
            Assert.IsTrue(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).Messages.Count == 1);
            Assert.IsTrue(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).Messages[0] != null);
            Assert.IsTrue(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).Messages[0].Message.ToString().Equals("Too short!"));
        }

        [TestMethod]
        public async Task Model_ValidateMessage_Valid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testValidationMessageConfig = await ValidationContext.LoadViewConfigAsync("TestValidationMessage");
            var testValidationMessage = new TestValidationMessageModel { ViewConfig = new ViewConfigViewModel(testValidationMessageConfig) };
            testValidationMessage.ValidationMessageField = "abc";

            Assert.IsTrue(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).IsValid);
            Assert.IsTrue(ValidationContext.Validate(testValidationMessage.ViewConfig, testValidationMessage).Messages.Count == 0);
        }
        #endregion

        #region Nulls
        [TestMethod]
        public async Task Model_ValidateNull_NullModel()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testNullsConfig = await ValidationContext.LoadViewConfigAsync("TestNulls");
            var testNulls = new TestNullsModel { ViewConfig = new ViewConfigViewModel(testNullsConfig) };

            Assert.IsTrue(ValidationContext.Validate(testNulls.ViewConfig, null).IsValid);
        }

        [TestMethod]
        public void Model_ValidateNull_NullConfig()
        {
            var testNulls = new TestNullsModel { ViewConfig = new ViewConfigViewModel(null) };

            Assert.IsTrue(ValidationContext.Validate(null, testNulls).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateNull_NullField()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testNullsConfig = await ValidationContext.LoadViewConfigAsync("TestNulls");
            var testNulls = new TestNullsModel { ViewConfig = new ViewConfigViewModel(testNullsConfig) };
            testNulls.NullField = null;

            Assert.IsTrue(ValidationContext.Validate(testNulls.ViewConfig, testNulls).IsValid);
        }
        #endregion

        #region MultipleFields
        [TestMethod]
        public async Task Model_ValidateMultipleFields_BothValid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "abc";
            testMultipleFields.Field2 = "abc";

            Assert.IsTrue(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstValidSecondInvalid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "abc";
            testMultipleFields.Field2 = "a";

            Assert.IsFalse(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstInvalidSecondValid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "a";
            testMultipleFields.Field2 = "abc";

            Assert.IsFalse(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_BothInvalid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "a";
            testMultipleFields.Field2 = "a";

            Assert.IsFalse(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_BothNull()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = null;
            testMultipleFields.Field2 = null;

            Assert.IsTrue(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstNullSecondValid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = null;
            testMultipleFields.Field2 = "abc";

            Assert.IsTrue(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstValidSecondNull()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "abc";
            testMultipleFields.Field2 = null;

            Assert.IsTrue(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstNullSecondInvalid()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = null;
            testMultipleFields.Field2 = "a";

            Assert.IsFalse(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }

        [TestMethod]
        public async Task Model_ValidateMultipleFields_FirstInvalidSecondNull()
        {
            ValidationContext.Global.ConfigProvider = new JsonFileConfigProvider("config.json");
            var testMultipleFieldsConfig = await ValidationContext.LoadViewConfigAsync("TestMultipleFields");
            var testMultipleFields = new TestMultipleFieldsModel { ViewConfig = new ViewConfigViewModel(testMultipleFieldsConfig) };
            testMultipleFields.Field1 = "a";
            testMultipleFields.Field2 = null;

            Assert.IsFalse(ValidationContext.Validate(testMultipleFields.ViewConfig, testMultipleFields).IsValid);
        }
        #endregion
    }
}
