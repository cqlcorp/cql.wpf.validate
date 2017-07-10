using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RegexValidatorTests
    {
        #region Regex
        [TestMethod]
        public void Regex_ValidateRegex_Valid()
        {
            RegexValidator validator = new RegexValidator("@");
            Assert.IsTrue(validator.Validate("test@email.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateRegex_Invalid()
        {
            RegexValidator validator = new RegexValidator("@");
            Assert.IsFalse(validator.Validate("testemail.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateRegex_Null()
        {
            RegexValidator validator = new RegexValidator("@");
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region EmptyRegex
        [TestMethod]
        public void Regex_ValidateEmptyRegex_NonemptyString()
        {
            RegexValidator emptyValidator = new RegexValidator("");
            Assert.IsTrue(emptyValidator.Validate("test@email.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateEmptyRegex_EmptyString()
        {
            RegexValidator emptyValidator = new RegexValidator("");
            Assert.IsTrue(emptyValidator.Validate("").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateEmptyRegex_Null()
        {
            RegexValidator emptyValidator = new RegexValidator("");
            Assert.IsTrue(emptyValidator.Validate(null).IsValid);
        }
        #endregion

        #region NullRegex
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Value cannot be null.")]
        public void Regex_ValidateNullRegex_NonemptyString()
        {
            RegexValidator nullValidator = new RegexValidator(null);
            nullValidator.Validate("test@email.com");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Value cannot be null.")]
        public void Regex_ValidateNullRegex_EmptyString()
        {
            RegexValidator nullValidator = new RegexValidator(null);
            nullValidator.Validate("");
        }

        [TestMethod]
        public void Regex_ValidateNullRegex_Null()
        {
            RegexValidator nullValidator = new RegexValidator(null);
            Assert.IsTrue(nullValidator.Validate(null).IsValid);
        }
        #endregion
    }
}
