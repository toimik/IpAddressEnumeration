namespace Toimik.IpAddressEnumeration.Tests;

using Xunit;

public class StaggeredV4IpAddressEnumeratorTest : V4IpAddressEnumeratorTest
{
    [Theory]
    [InlineData("191.255.255.255", "63.255.255.255", "223.255.255.255", false)]
    [InlineData("128.0.0.0", "64.0.0.0", /* "192.0.0.0", Not returned because it is reserved */ "32.0.0.0", true)]
    public override void EndToEnd(
        string firstIpAddress,
        string secondIpAddress,
        string thirdIpAddress,
        bool isForward)
    {
        base.EndToEnd(
            firstIpAddress,
            secondIpAddress,
            thirdIpAddress,
            isForward);
    }

    [Theory]

    // 0.0.0.0 to 0.255.255.255
    [InlineData("0.0.0.0", null, false)]
    [InlineData("0.0.0.1", null, false)]
    [InlineData("0.255.255.254", "191.127.255.254", false)]
    [InlineData("0.255.255.255", "191.127.255.255", false)]
    [InlineData("1.0.0.0", "1.0.0.0", false)]

    // 10.0.0.0 to 10.255.255.255
    [InlineData("9.255.255.255", "9.255.255.255", false)]
    [InlineData("10.0.0.0", "114.0.0.0", false)]
    [InlineData("10.0.0.1", "114.0.0.1", false)]
    [InlineData("10.255.255.254", "114.255.255.254", false)]
    [InlineData("10.255.255.255", "114.255.255.255", false)]
    [InlineData("9.0.0.0", "9.0.0.0", false)]

    // 127.0.0.0 to 127.255.255.255
    [InlineData("126.255.255.255", "126.255.255.255", false)]
    [InlineData("127.0.0.0", "191.0.0.0", false)]
    [InlineData("127.0.0.1", "191.0.0.1", false)]
    [InlineData("127.255.255.254", "191.255.255.254", false)]
    [InlineData("127.255.255.255", "191.255.255.255", false)]
    [InlineData("128.0.0.0", "128.0.0.0", false)]

    // 100.64.0.0 to 100.127.255.255
    [InlineData("100.63.255.255", "100.63.255.255", false)]
    [InlineData("100.64.0.0", "164.64.0.0", false)]
    [InlineData("100.64.0.1", "164.64.0.1", false)]
    [InlineData("100.127.255.254", "164.127.255.254", false)]
    [InlineData("100.127.255.255", "164.127.255.255", false)]
    [InlineData("100.128.0.0", "100.128.0.0", false)]

    // 169.254.0.0 to 169.254.255.255
    [InlineData("169.253.255.255", "169.253.255.255", false)]
    [InlineData("169.254.0.0", "41.254.0.0", false)]
    [InlineData("169.254.0.1", "41.254.0.1", false)]
    [InlineData("169.254.255.254", "41.254.255.254", false)]
    [InlineData("169.254.255.255", "41.254.255.255", false)]
    [InlineData("169.255.0.0", "169.255.0.0", false)]

    // 172.16.0.0 to 172.31.255.255
    [InlineData("172.15.255.255", "172.15.255.255", false)]
    [InlineData("172.16.0.0", "44.16.0.0", false)]
    [InlineData("172.16.0.1", "44.16.0.1", false)]
    [InlineData("172.31.255.254", "44.31.255.254", false)]
    [InlineData("172.31.255.255", "44.31.255.255", false)]
    [InlineData("172.32.0.0", "172.32.0.0", false)]

    // 192.0.0.0 to 192.0.0.255
    [InlineData("191.255.255.255", "191.255.255.255", false)]
    [InlineData("192.0.0.0", "64.0.0.0", false)]
    [InlineData("192.0.0.1", "64.0.0.1", false)]
    [InlineData("192.0.0.254", "64.0.0.254", false)]
    [InlineData("192.0.0.255", "64.0.0.255", false)]
    [InlineData("192.0.1.0", "192.0.1.0", false)]

    // 192.0.2.0 to 192.0.2.255
    [InlineData("192.0.1.255", "192.0.1.255", false)]
    [InlineData("192.0.2.0", "64.0.2.0", false)]
    [InlineData("192.0.2.1", "64.0.2.1", false)]
    [InlineData("192.0.2.254", "64.0.2.254", false)]
    [InlineData("192.0.2.255", "64.0.2.255", false)]
    [InlineData("192.0.3.0", "192.0.3.0", false)]

    // 192.88.99.0 to 192.88.99.255
    [InlineData("192.88.98.255", "192.88.98.255", false)]
    [InlineData("192.88.99.0", "64.88.99.0", false)]
    [InlineData("192.88.99.1", "64.88.99.1", false)]
    [InlineData("192.88.99.254", "64.88.99.254", false)]
    [InlineData("192.88.99.255", "64.88.99.255", false)]
    [InlineData("192.88.100.0", "192.88.100.0", false)]

    // 192.168.0.0 to 192.168.255.255
    [InlineData("192.167.255.255", "192.167.255.255", false)]
    [InlineData("192.168.0.0", "64.168.0.0", false)]
    [InlineData("192.168.0.1", "64.168.0.1", false)]
    [InlineData("192.168.255.254", "64.168.255.254", false)]
    [InlineData("192.168.255.255", "64.168.255.255", false)]
    [InlineData("192.169.0.0", "192.169.0.0", false)]

    // 198.18.0.0 to 198.19.255.255
    [InlineData("198.17.255.255", "198.17.255.255", false)]
    [InlineData("198.18.0.0", "70.18.0.0", false)]
    [InlineData("198.18.0.1", "70.18.0.1", false)]
    [InlineData("198.19.255.254", "70.19.255.254", false)]
    [InlineData("198.19.255.255", "70.19.255.255", false)]
    [InlineData("198.20.0.0", "198.20.0.0", false)]

    // 198.51.100.0 to 198.51.100.255
    [InlineData("198.51.99.255", "198.51.99.255", false)]
    [InlineData("198.51.100.0", "70.51.100.0", false)]
    [InlineData("198.51.100.1", "70.51.100.1", false)]
    [InlineData("198.51.100.254", "70.51.100.254", false)]
    [InlineData("198.51.100.255", "70.51.100.255", false)]
    [InlineData("198.51.101.0", "198.51.101.0", false)]

    // 203.0.113.0 to 203.0.113.255
    [InlineData("203.0.112.255", "203.0.112.255", false)]
    [InlineData("203.0.113.0", "75.0.113.0", false)]
    [InlineData("203.0.113.1", "75.0.113.1", false)]
    [InlineData("203.0.113.254", "75.0.113.254", false)]
    [InlineData("203.0.113.255", "75.0.113.255", false)]
    [InlineData("203.0.114.0", "203.0.114.0", false)]

    // 224.0.0.0 to 239.255.255.255
    [InlineData("223.255.255.255", "223.255.255.255", false)]
    [InlineData("224.0.0.0", "96.0.0.0", false)]
    [InlineData("224.0.0.1", "96.0.0.1", false)]
    [InlineData("239.255.255.254", "111.255.255.254", false)]

    // [InlineData("239.255.255.255", "111.255.255.255", false)] See below

    // [InlineData("240.0.0.0", "112.0.0.0", false)] See below

    // 240.0.0.0 to 255.255.255.254
    [InlineData("239.255.255.255", "111.255.255.255", false)]
    [InlineData("240.0.0.0", "112.0.0.0", false)]
    [InlineData("240.0.0.1", "112.0.0.1", false)]
    [InlineData("255.255.255.253", "191.255.255.253", false)]

    // [InlineData("255.255.255.254", "191.255.255.254", false)] See below

    // [InlineData("255.255.255.255", "", false)] See below

    // 255.255.255.255
    [InlineData("255.255.255.254", "191.255.255.254", false)]
    [InlineData("255.255.255.255", "191.255.255.255", false)]

    // 0.0.0.0 to 0.255.255.255
    [InlineData("0.0.0.0", "128.0.0.0", true)]
    [InlineData("0.0.0.1", "128.0.0.1", true)]
    [InlineData("0.255.255.254", "128.255.255.254", true)]
    [InlineData("0.255.255.255", "128.255.255.255", true)]
    [InlineData("1.0.0.0", "1.0.0.0", true)]

    // 10.0.0.0 to 10.255.255.255
    [InlineData("9.255.255.255", "9.255.255.255", true)]
    [InlineData("10.0.0.0", "138.0.0.0", true)]
    [InlineData("10.0.0.1", "138.0.0.1", true)]
    [InlineData("10.255.255.254", "138.255.255.254", true)]
    [InlineData("10.255.255.255", "138.255.255.255", true)]
    [InlineData("11.0.0.0", "11.0.0.0", true)]

    // 127.0.0.0 to 127.255.255.255
    [InlineData("126.255.255.255", "126.255.255.255", true)]
    [InlineData("127.0.0.0", "128.128.0.0", true)]
    [InlineData("127.0.0.1", "128.128.0.1", true)]
    [InlineData("127.255.255.254", "128.0.0.1", true)]
    [InlineData("127.255.255.255", null, true)]
    [InlineData("128.0.0.0", "128.0.0.0", true)]

    // 100.64.0.0 to 100.127.255.255
    [InlineData("100.63.255.255", "100.63.255.255", true)]
    [InlineData("100.64.0.0", "20.64.0.0", true)]
    [InlineData("100.64.0.1", "20.64.0.1", true)]
    [InlineData("100.127.255.254", "20.127.255.254", true)]
    [InlineData("100.127.255.255", "20.127.255.255", true)]
    [InlineData("100.128.0.0", "100.128.0.0", true)]

    // 169.254.0.0 to 169.254.255.255
    [InlineData("169.253.255.255", "169.253.255.255", true)]
    [InlineData("169.254.0.0", "105.254.0.0", true)]
    [InlineData("169.254.0.1", "105.254.0.1", true)]
    [InlineData("169.254.255.254", "105.254.255.254", true)]
    [InlineData("169.254.255.255", "105.254.255.255", true)]
    [InlineData("169.255.0.0", "169.255.0.0", true)]

    // 172.16.0.0 to 172.31.255.255
    [InlineData("172.15.255.255", "172.15.255.255", true)]
    [InlineData("172.16.0.0", "108.16.0.0", true)]
    [InlineData("172.16.0.1", "108.16.0.1", true)]
    [InlineData("172.31.255.254", "108.31.255.254", true)]
    [InlineData("172.31.255.255", "108.31.255.255", true)]
    [InlineData("172.32.0.0", "172.32.0.0", true)]

    // 192.0.0.0 to 192.0.0.255
    [InlineData("191.255.255.255", "191.255.255.255", true)]
    [InlineData("192.0.0.0", "32.0.0.0", true)]
    [InlineData("192.0.0.1", "32.0.0.1", true)]
    [InlineData("192.0.0.254", "32.0.0.254", true)]
    [InlineData("192.0.0.255", "32.0.0.255", true)]
    [InlineData("192.0.1.0", "192.0.1.0", true)]

    // 192.0.2.0 to 192.0.2.255
    [InlineData("192.0.1.255", "192.0.1.255", true)]
    [InlineData("192.0.2.0", "32.0.2.0", true)]
    [InlineData("192.0.2.1", "32.0.2.1", true)]
    [InlineData("192.0.2.254", "32.0.2.254", true)]
    [InlineData("192.0.2.255", "32.0.2.255", true)]
    [InlineData("192.0.3.0", "192.0.3.0", true)]

    // 192.88.99.0 to 192.88.99.255
    [InlineData("192.88.98.255", "192.88.98.255", true)]
    [InlineData("192.88.99.0", "32.88.99.0", true)]
    [InlineData("192.88.99.1", "32.88.99.1", true)]
    [InlineData("192.88.99.254", "32.88.99.254", true)]
    [InlineData("192.88.99.255", "32.88.99.255", true)]
    [InlineData("192.88.100.0", "192.88.100.0", true)]

    // 192.168.0.0 to 192.168.255.255
    [InlineData("192.167.255.255", "192.167.255.255", true)]
    [InlineData("192.168.0.0", "32.168.0.0", true)]
    [InlineData("192.168.0.1", "32.168.0.1", true)]
    [InlineData("192.168.255.254", "32.168.255.254", true)]
    [InlineData("192.168.255.255", "32.168.255.255", true)]
    [InlineData("192.169.0.0", "192.169.0.0", true)]

    // 198.18.0.0 to 198.19.255.255
    [InlineData("198.17.255.255", "198.17.255.255", true)]
    [InlineData("198.18.0.0", "38.18.0.0", true)]
    [InlineData("198.18.0.1", "38.18.0.1", true)]
    [InlineData("198.19.255.254", "38.19.255.254", true)]
    [InlineData("198.19.255.255", "38.19.255.255", true)]
    [InlineData("198.20.0.0", "198.20.0.0", true)]

    // 198.51.100.0 to 198.51.100.255
    [InlineData("198.51.99.255", "198.51.99.255", true)]
    [InlineData("198.51.100.0", "38.51.100.0", true)]
    [InlineData("198.51.100.1", "38.51.100.1", true)]
    [InlineData("198.51.100.254", "38.51.100.254", true)]
    [InlineData("198.51.100.255", "38.51.100.255", true)]
    [InlineData("198.51.101.0", "198.51.101.0", true)]

    // 203.0.113.0 to 203.0.113.255
    [InlineData("203.0.112.255", "203.0.112.255", true)]
    [InlineData("203.0.113.0", "43.0.113.0", true)]
    [InlineData("203.0.113.1", "43.0.113.1", true)]
    [InlineData("203.0.113.254", "43.0.113.254", true)]
    [InlineData("203.0.113.255", "43.0.113.255", true)]
    [InlineData("203.0.114.0", "203.0.114.0", true)]

    // 224.0.0.0 to 239.255.255.255
    [InlineData("223.255.255.255", "223.255.255.255", true)]
    [InlineData("224.0.0.0", "16.0.0.0", true)]
    [InlineData("224.0.0.1", "16.0.0.1", true)]

    // [InlineData("239.255.255.254", "31.255.255.254", true)] See below
    [InlineData("239.255.255.255", "31.255.255.255", true)]

    // [InlineData("240.0.0.0", "8.0.0.0", true)] See below

    // 240.0.0.0 - 255.255.255.254
    [InlineData("239.255.255.254", "31.255.255.254", true)]
    [InlineData("240.0.0.0", "8.0.0.0", true)]
    [InlineData("240.0.0.1", "8.0.0.1", true)]
    [InlineData("255.255.255.253", "128.0.0.3", true)]
    [InlineData("255.255.255.254", "128.0.0.1", true)]
    [InlineData("255.255.255.255", null, true)]
    public override void InitialIpAddressAtBoundaryValue(
        string reversedInitialIpAddress,
        string? expectedIpAddress,
        bool isForward)
    {
        /* Steps to derive the expected IP address
         *
         * If the initial IP address does not fall within any reserved IP range, that is the
         * expected IP address. Otherwise,
         * - a) convert the IP address into its binary representation
         * - b) reverse the string
         * - c) perform a binary arithmetic addition (if forward) / subtraction (if backward) of
         * one of the string in (b)
         * - d) reverse the string
         * - e) convert the string into its decimal IP address equivalent
         * - g) if the IP address does not fall within any reserved IP range, set this to the
         * expected IP address. Otherwise, repeat from step c
         */

        base.InitialIpAddressAtBoundaryValue(
            reversedInitialIpAddress,
            expectedIpAddress,
            isForward);
    }

    protected override IpAddressEnumerator CreateEnumerator()
    {
        return new StaggeredV4IpAddressEnumerator();
    }
}