using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RequiredValidatorTests
    {
        [TestMethod]
        public void Required_Validate_NonemptyString()
        {
            RequiredValidator validator = new RequiredValidator();
            Assert.IsTrue(validator.Validate("notempty").IsValid);
        }

        [TestMethod]
        public void Required_Validate_EmptyString()
        {
            RequiredValidator validator = new RequiredValidator();
            Assert.IsFalse(validator.Validate("").IsValid);
        }

        [TestMethod]
        public void Required_Validate_BlankString()
        {
            RequiredValidator validator = new RequiredValidator();
            Assert.IsFalse(validator.Validate("    ").IsValid);
        }

        [TestMethod]
        public void Required_Validate_NonString()
        {
            RequiredValidator validator = new RequiredValidator();
            Assert.IsTrue(validator.Validate(253).IsValid);
        }

        [TestMethod]
        public void Required_Validate_Null()
        {
            RequiredValidator validator = new RequiredValidator();
            Assert.IsFalse(validator.Validate(null).IsValid);
        }
    }
}
