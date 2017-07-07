using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RequiredValidatorTests
    {
        private RequiredValidator validator = new RequiredValidator();

        [TestMethod]
        public void Required_Validate_NonemptyString()
        {
            Assert.IsTrue(validator.Validate("notempty").IsValid);
        }

        [TestMethod]
        public void Required_Validate_EmptyString()
        {
            Assert.IsFalse(validator.Validate("").IsValid);
        }

        [TestMethod]
        public void Required_Validate_BlankString()
        {
            Assert.IsFalse(validator.Validate("    ").IsValid);
        }

        [TestMethod]
        public void Required_Validate_NonString()
        {
            Assert.IsTrue(validator.Validate(253).IsValid);
        }

        [TestMethod]
        public void Required_Validate_Null()
        {
            Assert.IsFalse(validator.Validate(null).IsValid);
        }
    }
}
