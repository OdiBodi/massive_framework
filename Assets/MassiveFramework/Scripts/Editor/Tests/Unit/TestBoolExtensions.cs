using FluentAssertions;
using MassiveCore.Framework.Runtime;
using NUnit.Framework;

namespace MassiveFramework.Tests
{
    public class TestBoolExtensions
    {
        [Test]
        public void ToNumberString()
        {
            var one = true.ToNumberString();
            var zero = false.ToNumberString();
            one.Should().Be("1");
            zero.Should().Be("0");
        }
    }
}
