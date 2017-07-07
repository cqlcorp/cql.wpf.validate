using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RegexValidatorTests
    {
        private RegexValidator validator = new RegexValidator("@");
        private RegexValidator emptyValidator = new RegexValidator("");
        private RegexValidator nullValidator = new RegexValidator(null);


        #region Regex
        [TestMethod]
        public void Regex_ValidateRegex_Valid()
        {
            Assert.IsTrue(validator.Validate("test@email.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateRegex_Invalid()
        {
            Assert.IsFalse(validator.Validate("testemail.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateRegex_Null()
        {
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region EmptyRegex
        [TestMethod]
        public void Regex_ValidateEmptyRegex_NonemptyString()
        {
            Assert.IsTrue(emptyValidator.Validate("test@email.com").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateEmptyRegex_EmptyString()
        {
            Assert.IsTrue(emptyValidator.Validate("").IsValid);
        }

        [TestMethod]
        public void Regex_ValidateEmptyRegex_Null()
        {
            Assert.IsTrue(emptyValidator.Validate(null).IsValid);
        }
        #endregion

        #region NullRegex
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Value cannot be null.")]
        public void Regex_ValidateNullRegex_NonemptyString()
        {
            nullValidator.Validate("test@email.com");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Value cannot be null.")]
        public void Regex_ValidateNullRegex_EmptyString()
        {
            nullValidator.Validate("");
        }

        [TestMethod]
        public void Regex_ValidateNullRegex_Null()
        {
            Assert.IsTrue(nullValidator.Validate(null).IsValid);
        }
        #endregion
    }
}
