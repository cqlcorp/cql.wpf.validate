using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class LengthValidatorTests
    {
        private string testString1 = "hi";
        private string testString2 = "hello";
        private string testString3 = "goodbye";
        private string zeroString = "";
        private string nullString = null;
        private LengthValidator maxValidator = new LengthValidator(5, true);
        private LengthValidator minValidator = new LengthValidator(5, false);
        private LengthValidator zeroMaxValidator = new LengthValidator(0, true);
        private LengthValidator zeroMinValidator = new LengthValidator(0, false);

        #region Max
        [TestMethod]
        public void Length_ValidateMax_UnderValidLength()
        {
            Assert.IsTrue(maxValidator.Validate(testString1).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_AtValidLength()
        {
            Assert.IsTrue(maxValidator.Validate(testString2).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_OverValidLength()
        {
            Assert.IsFalse(maxValidator.Validate(testString3).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_NullValidLength()
        {
            Assert.IsTrue(maxValidator.Validate(nullString).IsValid);
        }
        #endregion

        #region Min
        [TestMethod]
        public void Length_ValidateMin_UnderValidLength()
        {
            Assert.IsFalse(minValidator.Validate(testString1).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_AtValidLength()
        {
            Assert.IsTrue(minValidator.Validate(testString2).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_OverValidLength()
        {
            Assert.IsTrue(minValidator.Validate(testString3).IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_NullValidLength()
        {
            Assert.IsTrue(minValidator.Validate(nullString).IsValid);
        }
        #endregion

        #region ZeroMax
        [TestMethod]
        public void Length_ValidateZeroMax_AtValidLength()
        {
            Assert.IsTrue(zeroMaxValidator.Validate(zeroString).IsValid);
        }

        [TestMethod]
        public void Length_ValidateZeroMax_OverValidLength()
        {
            Assert.IsFalse(zeroMaxValidator.Validate(testString3).IsValid);
        }
        #endregion

        #region ZeroMin
        [TestMethod]
        public void Length_ValidateZeroMin_AtValidLength()
        {
            Assert.IsTrue(zeroMinValidator.Validate(zeroString).IsValid);
        }

        [TestMethod]
        public void Length_ValidateZeroMin_OverValidLength()
        {
            Assert.IsTrue(zeroMinValidator.Validate(testString3).IsValid);
        }
        #endregion
    }
}
