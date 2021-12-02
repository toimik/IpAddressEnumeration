# Toimik.IpAddressEnumeration

.NET 5 C# IP Address enumerators.

## Features

- Enumerate IPv4 addresses in a sequential / staggered fashion
-  More to come ...

## Quick Start

### Installation

#### Package Manager

```command
PM> Install-Package Toimik.IpAddressEnumeration
```

#### .NET CLI

```command
> dotnet add package Toimik.IpAddressEnumeration
```

### Usage

```c# 
using System;
using System.Net;

public class Program
{
    public static void Main()
    {
        // Creates an enumerator of public IPv4 addresses that adopts a sequential approach
        var enumerator = new SequentialV4IpAddressEnumerator();

        /* Creates an enumerator of public IPv4 addresses that adopts a staggered approach */

        // var enumerator = new StaggeredV4IpAddressEnumerator();

        var isForward = true;
        var initialIpAddress = IPAddress.Parse("1.2.3.4");
        var ipAddresses = enumerator.Enumerate(isForward, initialIpAddress);
        foreach (IPAddress ipAddress in ipAddresses)
        {
            Console.WriteLine(ipAddress);
        }
    }
}
```