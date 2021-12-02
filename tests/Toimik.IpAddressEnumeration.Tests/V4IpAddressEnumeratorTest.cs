namespace Toimik.IpAddressEnumeration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Xunit;

    public abstract class V4IpAddressEnumeratorTest
    {
        public virtual void EndToEnd(
            string firstIpAddress,
            string secondIpAddress,
            string thirdIpAddress,
            bool isForward)
        {
            var expectedIpAddresses = new List<string>
            {
                firstIpAddress,
                secondIpAddress,
                thirdIpAddress,
            };

            var enumerator = CreateEnumerator();
            var ipAddresses = enumerator.Enumerate(isForward);

            var index = 0;
            foreach (IPAddress ipAddress in ipAddresses)
            {
                var expectedIpAddress = expectedIpAddresses[index];
                Assert.Equal(expectedIpAddress, ipAddress.ToString());
                index++;
                if (index == expectedIpAddresses.Count)
                {
                    break;
                }
            }

            Assert.Equal(expectedIpAddresses.Count, index);
        }

        public virtual void InitialIpAddressAtBoundaryValue(
            string reversedInitialIpAddress,
            string expectedIpAddress,
            bool isForward)
        {
            var enumerator = CreateEnumerator();
            var ipAddresses = enumerator.Enumerate(isForward, IPAddress.Parse(reversedInitialIpAddress));

            TestWithInitialAddress(expectedIpAddress, ipAddresses);
        }

        [Theory]
        [InlineData("256.0.0.0")]
        [InlineData("0.256.0.0")]
        [InlineData("0.0.256.0")]
        [InlineData("0.0.0.256")]
        [InlineData("a")]
        [InlineData("a.b.c.d")]
        [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
        public void InvalidInitialIpAddress(string initialIpAddress)
        {
            var enumerator = CreateEnumerator();

            Assert.Throws<FormatException>(() => enumerator.Enumerate(initialIpAddress: IPAddress.Parse(initialIpAddress)).ToList());
        }

        protected static void TestWithInitialAddress(string expectedIpAddress, IEnumerable<IPAddress> ipAddresses)
        {
            foreach (IPAddress ipAddress in ipAddresses)
            {
                Assert.Equal(expectedIpAddress, ipAddress.ToString());
                break;
            }
        }

        protected abstract IpAddressEnumerator CreateEnumerator();
    }
}