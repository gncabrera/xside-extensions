﻿/*
  	Copyright 2017 Gustavo Cabrera
    Licensed under the Apache License, Version 2.0 (the "License");
 	you may not use this file except in compliance with the License.
 	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 */
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        /// <summary>
        /// Method that returns all the duplicates (distinct) in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="source">The source collection to detect for duplicates</param>
        /// <param name="distinct">Specify <b>true</b> to only return distinct elements.</param>
        /// <returns>A distinct list of duplicates found in the source collection.</returns>
        /// <remarks>This is an extension method to IEnumerable&lt;T&gt;</remarks>
        public static IEnumerable<T> Duplicates<T>
                 (this IEnumerable<T> source, bool distinct = true)
        {
            Ensure.That(source, "source").IsNotNull();

            // select the elements that are repeated
            IEnumerable<T> result = source.GroupBy(a => a).SelectMany(a => a.Skip(1));

            // distinct?
            if (distinct == true)
            {
                // deferred execution helps us here
                result = result.Distinct();
            }

            return result;
        }
    }
}
