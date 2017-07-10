using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class RangeValidatorTests
    {
        #region Range
        [TestMethod]
        public void Range_ValidateRange_BetweenRange()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsTrue(validator.Validate(4).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsTrue(doubleValidator.Validate(3.9).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 29)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AboveMax()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsFalse(validator.Validate(6).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsFalse(doubleValidator.Validate(5.0).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsFalse(dateValidator.Validate(new DateTime(2017, 5, 1)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_BelowMin()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsFalse(validator.Validate(2).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsFalse(doubleValidator.Validate(3.0).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsFalse(dateValidator.Validate(new DateTime(2017, 4, 27)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AtMax()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsTrue(validator.Validate(5).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsTrue(doubleValidator.Validate(4.8).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 30)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_AtMin()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsTrue(validator.Validate(3).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsTrue(doubleValidator.Validate(3.2).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 28)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateRange_Null()
        {
            RangeValidator<int> validator = new RangeValidator<int>(3, 5);
            Assert.IsTrue(validator.Validate(null).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, 4.8);
            Assert.IsTrue(doubleValidator.Validate(null).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), new DateTime(2017, 4, 30));
            Assert.IsTrue(dateValidator.Validate(null).IsValid);
        }
        #endregion

        #region LowBound
        [TestMethod]
        public void Range_ValidateLowBound_BelowMin()
        {
            RangeValidator<int> lowBoundValidator = new RangeValidator<int>(3, null);
            Assert.IsFalse(lowBoundValidator.Validate(2).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, null);
            Assert.IsFalse(doubleValidator.Validate(3.0).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), null);
            Assert.IsFalse(dateValidator.Validate(new DateTime(2017, 4, 27)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_AtMin()
        {
            RangeValidator<int> lowBoundValidator = new RangeValidator<int>(3, null);
            Assert.IsTrue(lowBoundValidator.Validate(3).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, null);
            Assert.IsTrue(doubleValidator.Validate(3.2).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), null);
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 28)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_AboveMin()
        {
            RangeValidator<int> lowBoundValidator = new RangeValidator<int>(3, null);
            Assert.IsTrue(lowBoundValidator.Validate(4).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, null);
            Assert.IsTrue(doubleValidator.Validate(3.9).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), null);
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 29)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateLowBound_Null()
        {
            RangeValidator<int> lowBoundValidator = new RangeValidator<int>(3, null);
            Assert.IsTrue(lowBoundValidator.Validate(null).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(3.2, null);
            Assert.IsTrue(doubleValidator.Validate(null).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(new DateTime(2017, 4, 28), null);
            Assert.IsTrue(dateValidator.Validate(null).IsValid);
        }
        #endregion

        #region HighBound
        [TestMethod]
        public void Range_ValidateHighBound_BelowMax()
        {
            RangeValidator<int> highBoundValidator = new RangeValidator<int>(null, 5);
            Assert.IsTrue(highBoundValidator.Validate(4).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, 4.8);
            Assert.IsTrue(doubleValidator.Validate(3.9).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, new DateTime(2017, 4, 29));
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 28)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_AtMax()
        {
            RangeValidator<int> highBoundValidator = new RangeValidator<int>(null, 5);
            Assert.IsTrue(highBoundValidator.Validate(5).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, 4.8);
            Assert.IsTrue(doubleValidator.Validate(4.8).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, new DateTime(2017, 4, 29));
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 29)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_AboveMax()
        {
            RangeValidator<int> highBoundValidator = new RangeValidator<int>(null, 5);
            Assert.IsFalse(highBoundValidator.Validate(6).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, 4.8);
            Assert.IsFalse(doubleValidator.Validate(5.0).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, new DateTime(2017, 4, 29));
            Assert.IsFalse(dateValidator.Validate(new DateTime(2017, 4, 30)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateHighBound_Null()
        {
            RangeValidator<int> highBoundValidator = new RangeValidator<int>(null, 5);
            Assert.IsTrue(highBoundValidator.Validate(null).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, 4.8);
            Assert.IsTrue(doubleValidator.Validate(null).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, new DateTime(2017, 4, 29));
            Assert.IsTrue(dateValidator.Validate(null).IsValid);
        }
        #endregion

        #region NullBounds
        [TestMethod]
        public void Range_ValidateNullBounds_NonNull()
        {
            RangeValidator<int> nullBoundsValidator = new RangeValidator<int>(null, null);
            Assert.IsTrue(nullBoundsValidator.Validate(4).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, null);
            Assert.IsTrue(doubleValidator.Validate(4.8).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, null);
            Assert.IsTrue(dateValidator.Validate(new DateTime(2017, 4, 29)).IsValid);
        }

        [TestMethod]
        public void Range_ValidateNullBounds_Null()
        {
            RangeValidator<int> nullBoundsValidator = new RangeValidator<int>(null, null);
            Assert.IsTrue(nullBoundsValidator.Validate(null).IsValid);

            RangeValidator<double> doubleValidator = new RangeValidator<double>(null, null);
            Assert.IsTrue(doubleValidator.Validate(null).IsValid);

            RangeValidator<DateTime> dateValidator = new RangeValidator<DateTime>(null, null);
            Assert.IsTrue(dateValidator.Validate(null).IsValid);
        }
        #endregion
    }
}
