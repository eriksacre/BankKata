using System;
using BankKata.External;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class ClockTest
    {
        [Test]
        public void GetTodayAsString_Always_Returns_dd_MM_yyyy()
        {
            var clock = new TestableClock();

            var today = clock.GetTodayAsString();
            
            Assert.That(today, Is.EqualTo("10/03/2018"));
        }

        private class TestableClock : Clock
        {
            protected override DateTime Now()
            {
                return new DateTime(2018, 3, 10);
            }
        }
    }
}