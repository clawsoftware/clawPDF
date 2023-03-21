using System;
using System.Diagnostics;
using System.Threading;
using SystemWrapper.Diagnostics;
using NUnit.Framework;

namespace SystemWrapper.Tests.Diagnostics
{
    [TestFixture]
    public class StopwatchWrapTests
    {
        [Test]
        public void StopwatchWrap_Initialize_CreatesStopwatchInstance()
        {
            // Act
            var stopwatchWrap = new StopwatchWrap();

            // Assert
            Assert.NotNull(stopwatchWrap.StopwatchInstance);
        }

        [Test]
        public void StopwatchWrap_Initialize_SetsStopwatchInstance()
        {
            // Arrange
            var stopwatch = new Stopwatch();

            // Act
            var stopwatchWrap = new StopwatchWrap(stopwatch);

            // Assert
            Assert.AreEqual(stopwatch, stopwatchWrap.StopwatchInstance);
        }

        [Test]
        public void StopwatchWrap_Properties_ReturnCorrectValues()
        {
            // Arrange
            var stopwatch = new Stopwatch();
            var stopwatchWrap = new StopwatchWrap(stopwatch);

            // Act
            stopwatch.Start();
            stopwatch.Stop();

            // Assert
            Assert.AreEqual(stopwatch.Elapsed, stopwatchWrap.Elapsed);
            Assert.AreEqual(stopwatch.ElapsedMilliseconds, stopwatchWrap.ElapsedMilliseconds);
            Assert.AreEqual(stopwatch.ElapsedTicks, stopwatchWrap.ElapsedTicks);
            Assert.AreEqual(stopwatch.IsRunning, stopwatchWrap.IsRunning);
        }

        [Test]
        public void StopwatchWrap_Stop_Start_IntegrationTest()
        {
            // Arrange
            var stopwatchWrap = new StopwatchWrap();

            // Act
            stopwatchWrap.Start();
            Thread.Sleep(1);
            stopwatchWrap.Stop();

            // Assert
            Assert.AreNotEqual(new TimeSpan(0, 0, 0), stopwatchWrap.Elapsed);
            Assert.AreNotEqual(0, stopwatchWrap.ElapsedMilliseconds);
            Assert.AreNotEqual(0, stopwatchWrap.ElapsedTicks);
            Assert.IsFalse(stopwatchWrap.IsRunning);
        }

        [Test]
        public void StopwatchWrap_Reset_IntegrationTest()
        {
            // Arrange
            var stopwatchWrap = new StopwatchWrap();

            // Act
            stopwatchWrap.Start();
            Thread.Sleep(1);
            stopwatchWrap.Stop();
            stopwatchWrap.Reset();

            // Assert
            Assert.AreEqual(new TimeSpan(0, 0, 0), stopwatchWrap.Elapsed);
            Assert.AreEqual(0, stopwatchWrap.ElapsedMilliseconds);
            Assert.AreEqual(0, stopwatchWrap.ElapsedTicks);
            Assert.IsFalse(stopwatchWrap.IsRunning);
        }

        [Test]
        public void StopwatchWrap_Restart_IntegrationTest()
        {
            // Arrange
            var stopwatchWrap = new StopwatchWrap();

            // Act
            stopwatchWrap.Start();
            Thread.Sleep(1);
            stopwatchWrap.Stop();
            stopwatchWrap.Restart();
            Thread.Sleep(1);

            // Assert
            Assert.AreNotEqual(new TimeSpan(0, 0, 0), stopwatchWrap.Elapsed);
            Assert.AreNotEqual(0, stopwatchWrap.ElapsedMilliseconds);
            Assert.AreNotEqual(0, stopwatchWrap.ElapsedTicks);
            Assert.IsTrue(stopwatchWrap.IsRunning);
        }
    }
}
