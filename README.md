![GitHub Workflow Status](https://img.shields.io/github/workflow/status/toimik/IpAddressEnumeration/CI)
![Code Coverage](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/nurhafiz/2f60942969f1afd5064f7858e47efd8d/raw/IpAddressEnumeration-coverage.json)
![Nuget](https://img.shields.io/nuget/v/Toimik.IpAddressEnumeration)

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
// Creates an enumerator of public IPv4 addresses that adopts a sequential approach
var enumerator = new SequentialV4IpAddressEnumerator();

// Creates an enumerator of public IPv4 addresses that adopts a staggered approach */
// var enumerator = new StaggeredV4IpAddressEnumerator();

var isForward = true;
var initialIpAddress = IPAddress.Parse("1.2.3.4");
var ipAddresses = enumerator.Enumerate(isForward, initialIpAddress);
foreach (IPAddress ipAddress in ipAddresses)
{
    Console.WriteLine(ipAddress);
}
```
&nbsp;
### Output

#### SequentialV4IpAddressEnumerator.cs

```c# 
1.2.3.4
1.2.3.5
1.2.3.6
...
```

#### StaggeredV4IpAddressEnumerator.cs

The output is the decimal representation of the reversed binary representation of the incremented binary.

```c# 
IP address Incremented binary
1.2.3.4    00100000.11000000.01000000.10000000  <- Original
129.2.3.4  00100000.11000000.01000000.10000001  <- Incremented by 1 bit
65.2.3.4   00100000.11000000.01000000.10000010  <- Incremented by another bit
...        ...
```