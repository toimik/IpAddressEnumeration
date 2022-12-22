namespace Toimik.IpAddressEnumeration.Tests;

using Xunit;

public class SequentialV4IpAddressEnumeratorTest : V4IpAddressEnumeratorTest
{
    [Theory]
    [InlineData("223.255.255.255", "223.255.255.254", "223.255.255.253", false)]
    [InlineData("1.0.0.0", "1.0.0.1", "1.0.0.2", true)]
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
    [InlineData("0.255.255.254", null, false)]
    [InlineData("0.255.255.255", null, false)]
    [InlineData("1.0.0.0", "1.0.0.0", false)]

    // 10.0.0.0 to 10.255.255.255
    [InlineData("9.255.255.255", "9.255.255.255", false)]
    [InlineData("10.0.0.0", "9.255.255.255", false)]
    [InlineData("10.0.0.1", "9.255.255.255", false)]
    [InlineData("10.255.255.254", "9.255.255.255", false)]
    [InlineData("10.255.255.255", "9.255.255.255", false)]
    [InlineData("9.0.0.0", "9.0.0.0", false)]

    // 127.0.0.0 to 127.255.255.255
    [InlineData("126.255.255.255", "126.255.255.255", false)]
    [InlineData("127.0.0.0", "126.255.255.255", false)]
    [InlineData("127.0.0.1", "126.255.255.255", false)]
    [InlineData("127.255.255.254", "126.255.255.255", false)]
    [InlineData("127.255.255.255", "126.255.255.255", false)]
    [InlineData("128.0.0.0", "128.0.0.0", false)]

    // 100.64.0.0 to 100.127.255.255
    [InlineData("100.63.255.255", "100.63.255.255", false)]
    [InlineData("100.64.0.0", "100.63.255.255", false)]
    [InlineData("100.64.0.1", "100.63.255.255", false)]
    [InlineData("100.127.255.254", "100.63.255.255", false)]
    [InlineData("100.127.255.255", "100.63.255.255", false)]
    [InlineData("100.128.0.0", "100.128.0.0", false)]

    // 169.254.0.0 to 169.254.255.255
    [InlineData("169.253.255.255", "169.253.255.255", false)]
    [InlineData("169.254.0.0", "169.253.255.255", false)]
    [InlineData("169.254.0.1", "169.253.255.255", false)]
    [InlineData("169.254.255.254", "169.253.255.255", false)]
    [InlineData("169.254.255.255", "169.253.255.255", false)]
    [InlineData("169.255.0.0", "169.255.0.0", false)]

    // 172.16.0.0 to 172.31.255.255
    [InlineData("172.15.255.255", "172.15.255.255", false)]
    [InlineData("172.16.0.0", "172.15.255.255", false)]
    [InlineData("172.16.0.1", "172.15.255.255", false)]
    [InlineData("172.31.255.254", "172.15.255.255", false)]
    [InlineData("172.31.255.255", "172.15.255.255", false)]
    [InlineData("172.32.0.0", "172.32.0.0", false)]

    // 192.0.0.0 to 192.0.0.255
    [InlineData("191.255.255.255", "191.255.255.255", false)]
    [InlineData("192.0.0.0", "191.255.255.255", false)]
    [InlineData("192.0.0.1", "191.255.255.255", false)]
    [InlineData("192.0.0.254", "191.255.255.255", false)]
    [InlineData("192.0.0.255", "191.255.255.255", false)]
    [InlineData("192.0.1.0", "192.0.1.0", false)]

    // 192.0.2.0 to 192.0.2.255
    [InlineData("192.0.1.255", "192.0.1.255", false)]
    [InlineData("192.0.2.0", "192.0.1.255", false)]
    [InlineData("192.0.2.1", "192.0.1.255", false)]
    [InlineData("192.0.2.254", "192.0.1.255", false)]
    [InlineData("192.0.2.255", "192.0.1.255", false)]
    [InlineData("192.0.3.0", "192.0.3.0", false)]

    // 192.88.99.0 to 192.88.99.255
    [InlineData("192.88.98.255", "192.88.98.255", false)]
    [InlineData("192.88.99.0", "192.88.98.255", false)]
    [InlineData("192.88.99.1", "192.88.98.255", false)]
    [InlineData("192.88.99.254", "192.88.98.255", false)]
    [InlineData("192.88.99.255", "192.88.98.255", false)]
    [InlineData("192.88.100.0", "192.88.100.0", false)]

    // 192.168.0.0 to 192.168.255.255
    [InlineData("192.167.255.255", "192.167.255.255", false)]
    [InlineData("192.168.0.0", "192.167.255.255", false)]
    [InlineData("192.168.0.1", "192.167.255.255", false)]
    [InlineData("192.168.255.254", "192.167.255.255", false)]
    [InlineData("192.168.255.255", "192.167.255.255", false)]
    [InlineData("192.169.0.0", "192.169.0.0", false)]

    // 198.18.0.0 to 198.19.255.255
    [InlineData("198.17.255.255", "198.17.255.255", false)]
    [InlineData("198.18.0.0", "198.17.255.255", false)]
    [InlineData("198.18.0.1", "198.17.255.255", false)]
    [InlineData("198.19.255.254", "198.17.255.255", false)]
    [InlineData("198.19.255.255", "198.17.255.255", false)]
    [InlineData("198.20.0.0", "198.20.0.0", false)]

    // 198.51.100.0 to 198.51.100.255
    [InlineData("198.51.99.255", "198.51.99.255", false)]
    [InlineData("198.51.100.0", "198.51.99.255", false)]
    [InlineData("198.51.100.1", "198.51.99.255", false)]
    [InlineData("198.51.100.254", "198.51.99.255", false)]
    [InlineData("198.51.100.255", "198.51.99.255", false)]
    [InlineData("198.51.101.0", "198.51.101.0", false)]

    // 203.0.113.0 to 203.0.113.255
    [InlineData("203.0.112.255", "203.0.112.255", false)]
    [InlineData("203.0.113.0", "203.0.112.255", false)]
    [InlineData("203.0.113.1", "203.0.112.255", false)]
    [InlineData("203.0.113.254", "203.0.112.255", false)]
    [InlineData("203.0.113.255", "203.0.112.255", false)]
    [InlineData("203.0.114.0", "203.0.114.0", false)]

    // 224.0.0.0 to 239.255.255.255
    [InlineData("223.255.255.255", "223.255.255.255", false)]
    [InlineData("224.0.0.0", "223.255.255.255", false)]
    [InlineData("224.0.0.1", "223.255.255.255", false)]
    [InlineData("239.255.255.254", "223.255.255.255", false)]

    // [InlineData("239.255.255.255", "223.255.255.255", false)] See below
    // [InlineData("240.0.0.0", "240.0.0.0", false)] See below

    // 240.0.0.0 to 255.255.255.254
    [InlineData("239.255.255.255", "223.255.255.255", false)]
    [InlineData("240.0.0.0", "223.255.255.255", false)]
    [InlineData("240.0.0.1", "223.255.255.255", false)]
    [InlineData("255.255.255.253", "223.255.255.255", false)]
    [InlineData("255.255.255.254", "223.255.255.255", false)]

    // [InlineData("255.255.255.255", "223.255.255.255", false)] See below

    // 255.255.255.255
    [InlineData("255.255.255.255", "223.255.255.255", false)]

    // 0.0.0.0 to 0.255.255.255
    [InlineData("0.0.0.0", "1.0.0.0", true)]
    [InlineData("0.0.0.1", "1.0.0.0", true)]
    [InlineData("0.255.255.254", "1.0.0.0", true)]
    [InlineData("0.255.255.255", "1.0.0.0", true)]
    [InlineData("1.0.0.0", "1.0.0.0", true)]

    // 10.0.0.0 to 10.255.255.255
    [InlineData("9.255.255.255", "9.255.255.255", true)]
    [InlineData("10.0.0.0", "11.0.0.0", true)]
    [InlineData("10.0.0.1", "11.0.0.0", true)]
    [InlineData("10.255.255.254", "11.0.0.0", true)]
    [InlineData("10.255.255.255", "11.0.0.0", true)]
    [InlineData("11.0.0.0", "11.0.0.0", true)]

    // 127.0.0.0 to 127.255.255.255
    [InlineData("126.255.255.255", "126.255.255.255", true)]
    [InlineData("127.0.0.0", "128.0.0.0", true)]
    [InlineData("127.0.0.1", "128.0.0.0", true)]
    [InlineData("127.255.255.254", "128.0.0.0", true)]
    [InlineData("127.255.255.255", "128.0.0.0", true)]
    [InlineData("128.0.0.0", "128.0.0.0", true)]

    // 100.64.0.0 to 100.127.255.255
    [InlineData("100.63.255.255", "100.63.255.255", true)]
    [InlineData("100.64.0.0", "100.128.0.0", true)]
    [InlineData("100.64.0.1", "100.128.0.0", true)]
    [InlineData("100.127.255.254", "100.128.0.0", true)]
    [InlineData("100.127.255.255", "100.128.0.0", true)]
    [InlineData("100.128.0.0", "100.128.0.0", true)]

    // 169.254.0.0 to 169.254.255.255
    [InlineData("169.253.255.255", "169.253.255.255", true)]
    [InlineData("169.254.0.0", "169.255.0.0", true)]
    [InlineData("169.254.0.1", "169.255.0.0", true)]
    [InlineData("169.254.255.254", "169.255.0.0", true)]
    [InlineData("169.254.255.255", "169.255.0.0", true)]
    [InlineData("169.255.0.0", "169.255.0.0", true)]

    // 172.16.0.0 to 172.31.255.255
    [InlineData("172.15.255.255", "172.15.255.255", true)]
    [InlineData("172.16.0.0", "172.32.0.0", true)]
    [InlineData("172.16.0.1", "172.32.0.0", true)]
    [InlineData("172.31.255.254", "172.32.0.0", true)]
    [InlineData("172.31.255.255", "172.32.0.0", true)]
    [InlineData("172.32.0.0", "172.32.0.0", true)]

    // 192.0.0.0 to 192.0.0.255
    [InlineData("191.255.255.255", "191.255.255.255", true)]
    [InlineData("192.0.0.0", "192.0.1.0", true)]
    [InlineData("192.0.0.1", "192.0.1.0", true)]
    [InlineData("192.0.0.254", "192.0.1.0", true)]
    [InlineData("192.0.0.255", "192.0.1.0", true)]
    [InlineData("192.0.1.0", "192.0.1.0", true)]

    // 192.0.2.0 to 192.0.2.255
    [InlineData("192.0.1.255", "192.0.1.255", true)]
    [InlineData("192.0.2.0", "192.0.3.0", true)]
    [InlineData("192.0.2.1", "192.0.3.0", true)]
    [InlineData("192.0.2.254", "192.0.3.0", true)]
    [InlineData("192.0.2.255", "192.0.3.0", true)]
    [InlineData("192.0.3.0", "192.0.3.0", true)]

    // 192.88.99.0 to 192.88.99.255
    [InlineData("192.88.98.255", "192.88.98.255", true)]
    [InlineData("192.88.99.0", "192.88.100.0", true)]
    [InlineData("192.88.99.1", "192.88.100.0", true)]
    [InlineData("192.88.99.254", "192.88.100.0", true)]
    [InlineData("192.88.99.255", "192.88.100.0", true)]
    [InlineData("192.88.100.0", "192.88.100.0", true)]

    // 192.168.0.0 to 192.168.255.255
    [InlineData("192.167.255.255", "192.167.255.255", true)]
    [InlineData("192.168.0.0", "192.169.0.0", true)]
    [InlineData("192.168.0.1", "192.169.0.0", true)]
    [InlineData("192.168.255.254", "192.169.0.0", true)]
    [InlineData("192.168.255.255", "192.169.0.0", true)]
    [InlineData("192.169.0.0", "192.169.0.0", true)]

    // 198.18.0.0 to 198.19.255.255
    [InlineData("198.17.255.255", "198.17.255.255", true)]
    [InlineData("198.18.0.0", "198.20.0.0", true)]
    [InlineData("198.18.0.1", "198.20.0.0", true)]
    [InlineData("198.19.255.254", "198.20.0.0", true)]
    [InlineData("198.19.255.255", "198.20.0.0", true)]
    [InlineData("198.20.0.0", "198.20.0.0", true)]

    // 198.51.100.0 to 198.51.100.255
    [InlineData("198.51.99.255", "198.51.99.255", true)]
    [InlineData("198.51.100.0", "198.51.101.0", true)]
    [InlineData("198.51.100.1", "198.51.101.0", true)]
    [InlineData("198.51.100.254", "198.51.101.0", true)]
    [InlineData("198.51.100.255", "198.51.101.0", true)]
    [InlineData("198.51.101.0", "198.51.101.0", true)]

    // 203.0.113.0 to 203.0.113.255
    [InlineData("203.0.112.255", "203.0.112.255", true)]
    [InlineData("203.0.113.0", "203.0.114.0", true)]
    [InlineData("203.0.113.1", "203.0.114.0", true)]
    [InlineData("203.0.113.254", "203.0.114.0", true)]
    [InlineData("203.0.113.255", "203.0.114.0", true)]
    [InlineData("203.0.114.0", "203.0.114.0", true)]

    // 224.0.0.0 to 239.255.255.255
    [InlineData("223.255.255.255", "223.255.255.255", true)]
    [InlineData("224.0.0.0", null, true)]
    [InlineData("224.0.0.1", null, true)]
    [InlineData("239.255.255.254", null, true)]

    // [InlineData("239.255.255.255", null, true)] See below [InlineData("240.0.0.0", null,
    // true)] See below

    // 240.0.0.0 - 255.255.255.254
    [InlineData("239.255.255.255", null, true)]
    [InlineData("240.0.0.0", null, true)]
    [InlineData("240.0.0.1", null, true)]
    [InlineData("255.255.255.253", null, true)]
    [InlineData("255.255.255.254", null, true)]
    [InlineData("255.255.255.255", null, true)]
    public override void InitialIpAddressAtBoundaryValue(
        string initialIpAddress,
        string expectedIpAddress,
        bool isForward)
    {
        base.InitialIpAddressAtBoundaryValue(
             initialIpAddress,
             expectedIpAddress,
             isForward);
    }

    protected override IpAddressEnumerator CreateEnumerator()
    {
        return new SequentialV4IpAddressEnumerator();
    }
}