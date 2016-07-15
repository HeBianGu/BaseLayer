﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using System.Collections.Generic;

namespace HebianGu.ComLibModule.ObjectEx
{
    /// <summary>
    /// Simple IComparable class
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class SimpleComparer<T> : IComparer<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleComparer{T}"/> class.
        /// </summary>
        /// <param name="comparisonFunction">The comparison function.</param>
        public SimpleComparer(Func<T, T, int> comparisonFunction)
        {
            ComparisonFunction = comparisonFunction;
        }

        /// <summary>
        /// Gets or sets the comparison function.
        /// </summary>
        /// <value>The comparison function.</value>
        protected Func<T, T, int> ComparisonFunction { get; set; }

        /// <summary>
        /// Compares the two objects
        /// </summary>
        /// <param name="x">Object 1</param>
        /// <param name="y">Object 2</param>
        /// <returns>0 if they're equal, any other value they are not</returns>
        public int Compare(T x, T y)
        {
            return ComparisonFunction(x, y);
        }
    }
}