using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class LengthValidatorTests
    {
        #region Max
        [TestMethod]
        public void Length_ValidateMax_UnderValidLength()
        {
            LengthValidator maxValidator = new LengthValidator(5, true);
            Assert.IsTrue(maxValidator.Validate("hi").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_AtValidLength()
        {
            LengthValidator maxValidator = new LengthValidator(5, true);
            Assert.IsTrue(maxValidator.Validate("hello").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_OverValidLength()
        {
            LengthValidator maxValidator = new LengthValidator(5, true);
            Assert.IsFalse(maxValidator.Validate("goodbye").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMax_NullValidLength()
        {
            LengthValidator maxValidator = new LengthValidator(5, true);
            Assert.IsTrue(maxValidator.Validate(null).IsValid);
        }
        #endregion

        #region Min
        [TestMethod]
        public void Length_ValidateMin_UnderValidLength()
        {
            LengthValidator minValidator = new LengthValidator(5, false);
            Assert.IsFalse(minValidator.Validate("hi").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_AtValidLength()
        {
            LengthValidator minValidator = new LengthValidator(5, false);
            Assert.IsTrue(minValidator.Validate("hello").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_OverValidLength()
        {
            LengthValidator minValidator = new LengthValidator(5, false);
            Assert.IsTrue(minValidator.Validate("goodbye").IsValid);
        }

        [TestMethod]
        public void Length_ValidateMin_NullValidLength()
        {
            LengthValidator minValidator = new LengthValidator(5, false);
            Assert.IsTrue(minValidator.Validate(null).IsValid);
        }
        #endregion

        #region ZeroMax
        [TestMethod]
        public void Length_ValidateZeroMax_AtValidLength()
        {
            LengthValidator zeroMaxValidator = new LengthValidator(0, true);
            Assert.IsTrue(zeroMaxValidator.Validate("").IsValid);
        }

        [TestMethod]
        public void Length_ValidateZeroMax_OverValidLength()
        {
            LengthValidator zeroMaxValidator = new LengthValidator(0, true);
            Assert.IsFalse(zeroMaxValidator.Validate("goodbye").IsValid);
        }
        #endregion

        #region ZeroMin
        [TestMethod]
        public void Length_ValidateZeroMin_AtValidLength()
        {
            LengthValidator zeroMinValidator = new LengthValidator(0, false);
            Assert.IsTrue(zeroMinValidator.Validate("").IsValid);
        }

        [TestMethod]
        public void Length_ValidateZeroMin_OverValidLength()
        {
            LengthValidator zeroMinValidator = new LengthValidator(0, false);
            Assert.IsTrue(zeroMinValidator.Validate("goodbye").IsValid);
        }
        #endregion
    }
}
