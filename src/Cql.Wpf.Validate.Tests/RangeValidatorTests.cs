using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RangeValidatorTests
    {
        private RangeValidator<int> validator = new RangeValidator<int>(3, 5);
        private RangeValidator<int> lowBoundValidator = new RangeValidator<int>(3, null);
        private RangeValidator<int> highBoundValidator = new RangeValidator<int>(null, 5);
        private RangeValidator<int> nullBoundsValidator = new RangeValidator<int>(null, null);

        #region Range
        [TestMethod]
        public void Range_ValidateRange_BetweenRange()
        {
            Assert.IsTrue(validator.Validate(4).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AboveMax()
        {
            Assert.IsFalse(validator.Validate(6).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_BelowMin()
        {
            Assert.IsFalse(validator.Validate(2).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AtMax()
        {
            Assert.IsTrue(validator.Validate(5).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AtMin()
        {
            Assert.IsTrue(validator.Validate(3).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_Null()
        {
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region LowBound
        [TestMethod]
        public void Range_ValidateLowBound_BelowMin()
        {
            Assert.IsFalse(lowBoundValidator.Validate(2).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_AtMin()
        {
            Assert.IsTrue(lowBoundValidator.Validate(3).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_AboveMin()
        {
            Assert.IsTrue(lowBoundValidator.Validate(4).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_Null()
        {
            Assert.IsTrue(lowBoundValidator.Validate(null).IsValid);
        }
        #endregion

        #region HighBound
        [TestMethod]
        public void Range_ValidateHighBound_BelowMax()
        {
            Assert.IsTrue(highBoundValidator.Validate(4).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_AtMax()
        {
            Assert.IsTrue(highBoundValidator.Validate(5).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_AboveMax()
        {
            Assert.IsFalse(highBoundValidator.Validate(6).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_Null()
        {
            Assert.IsTrue(highBoundValidator.Validate(null).IsValid);
        }
        #endregion

        #region NullBounds
        [TestMethod]
        public void Range_ValidateNullBounds_NonNull()
        {
            Assert.IsTrue(nullBoundsValidator.Validate(4).IsValid);
        }

        [TestMethod]
        public void Range_ValidateNullBounds_Null()
        {
            Assert.IsTrue(nullBoundsValidator.Validate(null).IsValid);
        }
        #endregion
    }
}
