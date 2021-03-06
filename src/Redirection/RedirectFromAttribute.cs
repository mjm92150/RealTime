﻿// <copyright file="RedirectFromAttribute.cs" company="dymanoid">
// Copyright (c) dymanoid. All rights reserved.
// </copyright>

/*
The MIT License (MIT)
Copyright (c) 2015 Sebastian Schöner
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
THE SOFTWARE.
*/

namespace Redirection
{
    using System;

    /// <summary>
    /// Marks a method for redirection. All marked methods are redirected by calling
    /// <see cref="Redirector.PerformRedirections"/> and reverted by <see cref="Redirector.RevertRedirections"/>
    /// </summary>
    ///
    /// <remarks>
    /// NOTE: only the methods belonging to the same assembly that calls Perform/RevertRedirections are redirected.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class RedirectFromAttribute : BaseRedirectAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectFromAttribute"/> class.</summary>
        ///
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="methodType"/> is null.</exception>
        ///
        /// <param name="methodType">The type where the source method is defined.</param>
        /// <param name="methodName">The name of the source method that will be redirected. If null,
        /// the name of the attribute's target method will be used.</param>
        /// <param name="isInstanceMethod">true if the source method is an instance method;
        /// otherwise, false.</param>
        public RedirectFromAttribute(Type methodType, string methodName, bool isInstanceMethod)
            : base(methodType, methodName, isInstanceMethod)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectFromAttribute"/> class with
        /// <see cref="BaseRedirectAttribute.IsInstanceMethod"/> set to true.</summary>
        ///
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="methodType"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="methodName"/> is null or an empty string.</exception>
        ///
        /// <param name="methodType">The type where the source method is defined.</param>
        /// <param name="methodName">The name of the source method that will be redirected.</param>
        public RedirectFromAttribute(Type methodType, string methodName)
            : base(methodType)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException($"The {nameof(methodName)} cannot be null or an empty string");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectFromAttribute"/> class with
        /// empty <see cref="BaseRedirectAttribute.MethodName"/>.
        /// The name of the method this attribute is attached to will be used.</summary>
        ///
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="methodType"/> is null.</exception>
        ///
        /// <param name="methodType">The type where the source method is defined.</param>
        /// <param name="isInstanceMethod">true if the source method is an instance method;
        /// otherwise, false.</param>
        public RedirectFromAttribute(Type methodType, bool isInstanceMethod)
            : base(methodType, isInstanceMethod)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectFromAttribute"/> class with
        /// empty <see cref="BaseRedirectAttribute.MethodName"/> and <see cref="BaseRedirectAttribute.IsInstanceMethod"/>
        /// set to true. The name of the method this attribute is attached to will be used.</summary>
        ///
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="methodType"/> is null.</exception>
        ///
        /// <param name="methodType">The type where the source method is defined.</param>
        public RedirectFromAttribute(Type methodType)
            : base(methodType)
        {
        }
    }
}
