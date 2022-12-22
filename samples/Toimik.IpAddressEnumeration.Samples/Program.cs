namespace Toimik.IpAddressEnumeration.Samples;

using System;
using System.Net;

public class Program
{
    public static void Main()
    {
        // Creates an enumerator of public IPv4 addresses that adopts a sequential approach
        var enumerator = new SequentialV4IpAddressEnumerator();

        /* Creates an enumerator of public IPv4 addresses that adopts a staggered approach */
        /* var enumerator = new StaggeredV4IpAddressEnumerator(); */

        var isForward = true;
        var initialIpAddress = IPAddress.Parse("1.2.3.4");
        var ipAddresses = enumerator.Enumerate(isForward, initialIpAddress);
        foreach (IPAddress ipAddress in ipAddresses)
        {
            Console.WriteLine(ipAddress);
        }
    }
}