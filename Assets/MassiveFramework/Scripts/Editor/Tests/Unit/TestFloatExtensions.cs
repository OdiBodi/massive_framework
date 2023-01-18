using FluentAssertions;
using MassiveCore.Framework.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace MassiveFramework.Tests
{
    public class TestFloatExtensions
    {
        [Test]
        public void ToVector2()
        {
            var zeroToVector2 = 0f.ToVector2();
            var zeroNewVector2 = new Vector2(0f, 0f);
            zeroToVector2.Should().Be(zeroNewVector2);
        }

        [Test]
        public void ToVector3()
        {
            var zeroToVector3 = 0f.ToVector3();
            var zeroNewVector3 = new Vector3(0f, 0f, 0f);
            zeroToVector3.Should().Be(zeroNewVector3);
        }

        [Test]
        public void ToVector4()
        {
            var zeroToVector4 = 0f.ToVector4();
            var zeroNewVector4 = new Vector4(0f, 0f, 0f, 0f);
            zeroToVector4.Should().Be(zeroNewVector4);
        }
    }
}
