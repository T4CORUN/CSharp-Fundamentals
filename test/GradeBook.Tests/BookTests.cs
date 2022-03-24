using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void TestComputeStatisticsCalc()
        {
            var book = new InMemoryBook(null);
            
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            var stats = book.GetStatistics();

            Assert.Equal(77.3, stats.MinValue);
            Assert.Equal(85.6, Math.Round(stats.AverageValue,1));
            Assert.Equal(90.5, stats.MaxValue);
            Assert.Equal('B', stats.LetterGrade);
        }

        [Fact]
        public void TestComputeStatisticsEmpty()
        {
            var book = new InMemoryBook(null);

            var stats = book.GetStatistics();

            Assert.Equal(0.0, stats.MinValue);
            Assert.Equal(0.0, stats.AverageValue);
            Assert.Equal(0.0, stats.MaxValue);
        }

        //[Fact]
        public void AddInvalidGrade()
        {   
            var book = new InMemoryBook(null);

            book.AddGrade(-5.0);
            book.AddGrade(108.0);

            var stats = book.GetStatistics();

            Assert.Equal(0.0, stats.MinValue);
            Assert.Equal(0.0, stats.AverageValue);
            Assert.Equal(0.0, stats.MaxValue);        
        }
    }
}