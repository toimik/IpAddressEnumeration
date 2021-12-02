/*
 * Copyright 2021 nurhafiz@hotmail.sg
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Toimik.IpAddressEnumeration
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Represents an enumerator of public IPv4 addresses that adopts a staggered approach.
    /// </summary>
    public class StaggeredV4IpAddressEnumerator : IpAddressEnumerator
    {
        public StaggeredV4IpAddressEnumerator()
            : base()
        {
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Enumerating addresses in a non-sequential fashion is useful when accessing a lot of them
        /// within quick succession. e.g. when performing reverse dns.
        /// <para>
        /// This reduces the likelihood of overloading servers belonging to any one entity
        /// considering that addresses are assigned in block sequences.
        /// </para>
        /// <para>
        /// A simple, yet effective methodological process is adopted. Not only does it ensure a
        /// reasonably fair distribution, it also enables the operation to be resumable.
        /// </para>
        /// <para>
        /// First, a base-2 32-bit counter is initialized to zero (
        /// <c>00000000000000000000000000000000</c> ). Each enumeration follows these steps:
        /// <list type="number">
        /// <item>
        /// <description>
        /// Increment the counter by one ( <c>00000000000000000000000000000001</c> )
        /// </description>
        /// </item>
        /// <item>
        /// <description>Reverse it ( <c>10000000000000000000000000000000</c> )</description>
        /// </item>
        /// <item>
        /// <description>
        /// Split into equal octets ( <c>10000000</c>, <c>00000000</c>, <c>00000000</c>,
        /// <c>00000000</c> )
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Convert each octet to decimal ( <c>128</c>, <c>0</c>, <c>0</c>, <c>0</c> )
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// If the concatenated decimals represent an IP address that does not fall within any
        /// reserved block, that IP address is returned. Otherwise, repeat the steps. When all the
        /// addresses are exhausted, a <c>null</c> is returned.
        /// </description>
        /// </item>
        /// </list>
        /// The above is for when <paramref name="isForward"/> is <c>true</c>. Decrement the counter
        /// for when <paramref name="isForward"/> is <c>false</c>.
        /// </para>
        /// </remarks>
        public override IEnumerable<IPAddress> Enumerate(
            bool isForward = true,
            IPAddress initialIpAddress = null,
            CancellationToken cancellationToken = default)
        {
            initialIpAddress ??= CreateInitialIpAddress(isForward);
            var bits = CreateBits(initialIpAddress.ToString());

            // Loop until all addresses are enumerated
            do
            {
                bool? isReserved = false;
                int octet1;
                int octet2;
                int octet3;
                int octet4;

                // Loop until a public address is encountered
                do
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    octet1 = CreateOctet(bits, 31);
                    octet2 = CreateOctet(bits, 23);
                    octet3 = CreateOctet(bits, 15);
                    octet4 = CreateOctet(bits, 7);
                    var isPublic = false;
                    switch (octet1)
                    {
                        case 0:
                        case 10:
                        case 127:
                            if (octet2 >= 0 && octet2 <= 255
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 0.0.0.0 to 0.255.255.255

                                // 10.0.0.0 to 10.255.255.255

                                // 127.0.0.0 to 127.255.255.255
                                if (octet1 == 0
                                    && octet2 == 0
                                    && octet3 == 0
                                    && octet4 <= 1
                                    && !isForward)
                                {
                                    isReserved = null;
                                    break;
                                }

                                isReserved = true;
                            }

                            break;

                        case 100:
                            if (octet2 >= 64 && octet2 <= 127
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 100.64.0.0 to 100.127.255.255
                                isReserved = true;
                            }

                            break;

                        case 169:
                            if (octet2 == 254
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 169.254.0.0 to 169.254.255.255
                                isReserved = true;
                            }

                            break;

                        case 172:
                            if (octet2 >= 16 && octet2 <= 31
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 172.16.0.0 to 172.31.255.255
                                isReserved = true;
                            }

                            break;

                        case 192:
                            switch (octet2)
                            {
                                case 0:
                                    switch (octet3)
                                    {
                                        case 0:
                                            if (octet4 >= 0 && octet4 <= 255)
                                            {
                                                // 192.0.0.0 to 192.0.0.255
                                                isReserved = true;
                                            }

                                            break;

                                        case 2:
                                            if (octet4 >= 0 && octet4 <= 255)
                                            {
                                                // 192.0.2.0 to 192.0.2.255
                                                isReserved = true;
                                            }

                                            break;
                                    }

                                    break;

                                case 88:
                                    if (octet3 == 99
                                        && octet4 >= 0 && octet4 <= 255)
                                    {
                                        // 192.88.99.0 to 192.88.99.255
                                        isReserved = true;
                                    }

                                    break;

                                case 168:
                                    if (octet3 >= 0 && octet3 <= 255
                                        && octet4 >= 0 && octet4 <= 255)
                                    {
                                        // 192.168.0.0 to 192.168.255.255
                                        isReserved = true;
                                    }

                                    break;
                            }

                            break;

                        case 198:
                            if (octet2 >= 18 && octet2 <= 19
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 198.18.0.0 to 198.19.255.255
                                isReserved = true;
                            }
                            else if (octet2 == 51
                                && octet3 == 100
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 198.51.100.0 to 198.51.100.255
                                isReserved = true;
                            }

                            break;

                        case 203:
                            if (octet2 == 0
                                && octet3 == 113
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                // 203.0.113.0 to 203.0.113.255
                                isReserved = true;
                            }

                            break;

                        default:
                            if (octet1 >= 224 && octet1 <= 255
                                && octet2 >= 0 && octet2 <= 255
                                && octet3 >= 0 && octet3 <= 255
                                && octet4 >= 0 && octet4 <= 255)
                            {
                                if (octet1 == 255
                                    && octet2 == 255
                                    && octet3 == 255
                                    && octet4 == 255
                                    && isForward)
                                {
                                    // 255.255.255.255
                                    isReserved = null;
                                    break;
                                }

                                // 224.0.0.0 to 239.255.255.255

                                // 240.0.0.0 to 255.255.255.254
                                isReserved = true;
                            }
                            else
                            {
                                isPublic = true;
                            }

                            break;
                    }

                    if (isReserved == null
                        || isPublic)
                    {
                        break;
                    }

                    if (isForward)
                    {
                        bits = Increment(bits);
                    }
                    else
                    {
                        bits = Decrement(bits);
                    }
                }
                while (isReserved.Value);

                if (isReserved == null)
                {
                    break;
                }

                var ipAddress = IPAddress.Parse($"{octet1}.{octet2}.{octet3}.{octet4}");
                if (isForward)
                {
                    bits = Increment(bits);
                }
                else
                {
                    bits = Decrement(bits);
                }

                yield return ipAddress;
            }
            while (true);
        }

        protected static IList<int> CreateBits(string ipAddress)
        {
            var tokens = ipAddress.Split('.');
            var length = tokens.Length;
            if (length != 4)
            {
                throw new FormatException("Invalid IPV4 address format.");
            }

            var bits = new List<int>(32);
            for (int i = length - 1; i >= 0; i--)
            {
                var token = tokens[i];
                var text = Convert.ToString(int.Parse(token), 2).PadLeft(Octet, '0');
                for (int j = text.Length - 1; j >= 0; j--)
                {
                    var character = text[j];
                    bits.Add(int.Parse(character.ToString()));
                }
            }

            return bits;
        }

        protected static int CreateOctet(IList<int> bits, int startIndex)
        {
            var builder = new StringBuilder();
            var endIndex = startIndex - Octet;
            for (int i = startIndex; i > endIndex; i--)
            {
                var bit = bits[i];
                builder.Append(bit);
            }

            var number = Convert.ToInt32(builder.ToString(), 2);
            return number;
        }

        private static IPAddress CreateInitialIpAddress(bool isForward)
        {
            var ipAddress = isForward
                ? "0.0.0.0"
                : "255.255.255.255";
            return IPAddress.Parse(ipAddress);
        }
    }
}