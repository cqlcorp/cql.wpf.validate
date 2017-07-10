using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Tests
{
    [TestClass]
    public class DynamicDateRangeValidatorTests
    {
        #region TodayAndNow
        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_BetweenRange()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Today.AddMilliseconds(1)).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_AboveMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsFalse(validator.Validate(DateTime.Now.AddMilliseconds(1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_BelowMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsFalse(validator.Validate(DateTime.Today.AddMilliseconds(-1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(-1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_AtMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Now).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_AtMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Today).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndNow_Null()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region TodayAndUtcNow
        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_BetweenRange()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Today.AddMilliseconds(1)).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_AboveMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsFalse(validator.Validate(DateTime.Now.AddMilliseconds(1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_BelowMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsFalse(validator.Validate(DateTime.Today.AddMilliseconds(-1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(-1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_AtMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Now).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_AtMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Today).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateTodayAndUtcNow_Null()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.Today, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region UtcTodayAndNow
        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_BetweenRange()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Today.AddMilliseconds(1)).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_AboveMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsFalse(validator.Validate(DateTime.Now.AddMilliseconds(1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_BelowMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsFalse(validator.Validate(DateTime.Today.AddMilliseconds(-1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(-1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_AtMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Now).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_AtMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(DateTime.Today).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndNow_Null()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.Now);
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion

        #region UtcTodayAndUtcNow
        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_BetweenRange()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Today.AddMilliseconds(1)).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_AboveMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsFalse(validator.Validate(DateTime.Now.AddMilliseconds(1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.AddMilliseconds(1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_BelowMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsFalse(validator.Validate(DateTime.Today.AddMilliseconds(-1)).IsValid);
            Assert.IsFalse(validator.Validate(DateTime.UtcNow.Date.AddMilliseconds(-1)).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_AtMax()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Now).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_AtMin()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(DateTime.Today).IsValid);
            Assert.IsTrue(validator.Validate(DateTime.UtcNow.Date).IsValid);
        }

        [TestMethod]
        public void DynamicDateRange_ValidateUtcTodayAndUtcNow_Null()
        {
            DynamicDateRangeValidator validator = new DynamicDateRangeValidator(Config.DynamicDate.UtcToday, Config.DynamicDate.UtcNow);
            Assert.IsTrue(validator.Validate(null).IsValid);
        }
        #endregion
    }
}
