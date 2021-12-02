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
    /// Represents an enumerator of public IPv4 addresses that adopts a sequential approach.
    /// </summary>
    public class SequentialV4IpAddressEnumerator : IpAddressEnumerator
    {
        public SequentialV4IpAddressEnumerator()
            : base()
        {
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The IP addresses are returned in numerical sequence.
        /// </remarks>
        public override IEnumerable<IPAddress> Enumerate(
            bool isForward = true,
            IPAddress initialIpAddress = null,
            CancellationToken cancellationToken = default)
        {
            initialIpAddress ??= CreateInitialIpAddress(isForward);
            var bits = CreateBits(initialIpAddress.ToString());
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                var hasJumped = false;
                var octet1 = CreateOctet(bits, 0);
                var octet2 = CreateOctet(bits, 8);
                var octet3 = CreateOctet(bits, 16);
                var octet4 = CreateOctet(bits, 24);
                var isDone = false;
                switch (octet1)
                {
                    case 0:
                        if (octet2 >= 0 && octet2 <= 255
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 0.0.0.0 to 0.255.255.255
                            if (!isForward)
                            {
                                isDone = true;
                                break;
                            }

                            bits = CreateBits("1.0.0.0");
                            hasJumped = true;
                        }

                        break;

                    case 10:
                        if (octet2 >= 0 && octet2 <= 255
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 10.0.0.0 to 10.255.255.255
                            bits = isForward
                                ? CreateBits("11.0.0.0")
                                : CreateBits("9.255.255.255");
                            hasJumped = true;
                        }

                        break;

                    case 100:
                        if (octet2 >= 64 && octet2 <= 127
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 100.64.0.0 to 100.127.255.255
                            bits = isForward
                                ? CreateBits("100.128.0.0")
                                : CreateBits("100.63.255.255");
                            hasJumped = true;
                        }

                        break;

                    case 127:
                        if (octet2 >= 0 && octet2 <= 255
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 127.0.0.0 to 127.255.255.255
                            bits = isForward
                                ? CreateBits("128.0.0.0")
                                : CreateBits("126.255.255.255");
                            hasJumped = true;
                        }

                        break;

                    case 169:
                        if (octet2 == 254
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 169.254.0.0 to 169.254.255.255
                            bits = isForward
                                ? CreateBits("169.255.0.0")
                                : CreateBits("169.253.255.255");
                            hasJumped = true;
                        }

                        break;

                    case 172:
                        if (octet2 >= 16 && octet2 <= 31
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 172.16.0.0 to 172.31.255.255
                            bits = isForward
                                ? CreateBits("172.32.0.0")
                                : CreateBits("172.15.255.255");
                            hasJumped = true;
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
                                            bits = isForward
                                                ? CreateBits("192.0.1.0")
                                                : CreateBits("191.255.255.255");
                                            hasJumped = true;
                                        }

                                        break;

                                    case 2:
                                        if (octet4 >= 0 && octet4 <= 255)
                                        {
                                            // 192.0.2.0 to 192.0.2.255
                                            bits = isForward
                                                ? CreateBits("192.0.3.0")
                                                : CreateBits("192.0.1.255");
                                            hasJumped = true;
                                        }

                                        break;
                                }

                                break;

                            case 88:
                                if (octet3 == 99
                                    && octet4 >= 0 && octet4 <= 255)
                                {
                                    // 192.88.99.0 to 192.88.99.255
                                    bits = isForward
                                        ? CreateBits("192.88.100.0")
                                        : CreateBits("192.88.98.255");
                                    hasJumped = true;
                                }

                                break;

                            case 168:
                                if (octet3 >= 0 && octet3 <= 255
                                    && octet4 >= 0 && octet4 <= 255)
                                {
                                    // 192.168.0.0 to 192.168.255.255
                                    bits = isForward
                                        ? CreateBits("192.169.0.0")
                                        : CreateBits("192.167.255.255");
                                    hasJumped = true;
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
                            bits = isForward
                                ? CreateBits("198.20.0.0")
                                : CreateBits("198.17.255.255");
                            hasJumped = true;
                        }
                        else if (octet2 == 51
                            && octet3 == 100
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 198.51.100.0 to 198.51.100.255
                            bits = isForward
                                ? CreateBits("198.51.101.0")
                                : CreateBits("198.51.99.255");
                            hasJumped = true;
                        }

                        break;

                    case 203:
                        if (octet2 == 0
                            && octet3 == 113
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 203.0.113.0 to 203.0.113.255
                            bits = isForward
                                ? CreateBits("203.0.114.0")
                                : CreateBits("203.0.112.255");
                            hasJumped = true;
                        }

                        break;

                    default:
                        if (octet1 >= 224 && octet1 <= 255
                            && octet2 >= 0 && octet2 <= 255
                            && octet3 >= 0 && octet3 <= 255
                            && octet4 >= 0 && octet4 <= 255)
                        {
                            // 224.0.0.0 to 239.255.255.255

                            // 240.0.0.0 to 255.255.255.255
                            if (isForward)
                            {
                                isDone = true;
                                break;
                            }

                            // NOTE: This jumps twice
                            bits = CreateBits("223.255.255.255");
                            hasJumped = true;
                        }

                        break;
                }

                if (isDone)
                {
                    break;
                }

                if (hasJumped)
                {
                    octet1 = CreateOctet(bits, 0);
                    octet2 = CreateOctet(bits, 8);
                    octet3 = CreateOctet(bits, 16);
                    octet4 = CreateOctet(bits, 24);
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
            for (int i = 0; i < length; i++)
            {
                var token = tokens[i];
                var text = Convert.ToString(int.Parse(token), 2).PadLeft(Octet, '0');
                for (int j = 0; j < text.Length; j++)
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
            var endIndex = startIndex + Octet;
            for (int i = startIndex; i < endIndex; i++)
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
                ? "1.0.0.0"
                : "223.255.255.255";
            return IPAddress.Parse(ipAddress);
        }
    }
}