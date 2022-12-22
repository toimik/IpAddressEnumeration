/*
 * Copyright 2021-2022 nurhafiz@hotmail.sg
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

namespace Toimik.IpAddressEnumeration;

using System.Collections.Generic;
using System.Net;
using System.Threading;

/// <summary>
/// Represents an enumerator of public IP addresses.
/// </summary>
public abstract class IpAddressEnumerator
{
    protected const int Octet = 8;

    protected IpAddressEnumerator()
    {
    }

    /// <summary>
    /// Gets an enumeration of IP addresses that are publicly available.
    /// </summary>
    /// <param name="isForward">
    /// If <c>true</c>, the enumeration goes in a forward fashion. Otherwise, it goes backwards.
    /// </param>
    /// <param name="initialIpAddress">
    /// The IP address whose concatenated binary representation is used as the initial counter
    /// value. If this is within a reserved block, it is set to the jumped value - the nearest
    /// public IP address according to the direction indicated by <paramref name="isForward"/>.
    /// </param>
    /// <param name="cancellationToken">
    /// Token to monitor for cancellation request.
    /// </param>
    /// <returns>
    /// Enumerable of public IP addresses.
    /// </returns>
    public abstract IEnumerable<IPAddress> Enumerate(
        bool isForward = true,
        IPAddress? initialIpAddress = null,
        CancellationToken cancellationToken = default);

    protected virtual IList<int> Decrement(IList<int> bits)
    {
        // Find from the back first occurrence of 1
        int i;
        for (i = bits.Count - 1; i >= 0; i--)
        {
            var bit = bits[i];
            if (bit == 1)
            {
                break;
            }
        }

        // Borrowing from it turns it into zero
        bits[i] = 0;

        // All the bits after that become one
        for (int j = i + 1; j < bits.Count; j++)
        {
            bits[j] = 1;
        }

        return bits;
    }

    protected virtual IList<int> Increment(IList<int> bits)
    {
        int i;
        for (i = bits.Count - 1; i >= 0; i--)
        {
            bits[i] = bits[i] + 1;
            var hasCarryOver = bits[i] > 1;
            if (!hasCarryOver)
            {
                break;
            }

            bits[i] = 0;
        }

        return bits;
    }
}