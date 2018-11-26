//-----------------------------------------------------------------------
// <copyright file="IMethodsFilter.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This interface is used for making independent the use of the filters.
// </summary>
// 
// <disclaimer>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </disclaimer>
//
// <author>Alessio Langiu</author>
// <email>alessio.langiu@huddimension.co.uk</email>
//-----------------------------------------------------------------------
using System.Reflection;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This interface is used for making independent the use of the filters.
    /// </summary>
    public interface IMethodsFilter
    {
        /// <summary>
        /// Filters the specified method.
        /// </summary>
        /// <typeparam name="T">It must to be an implementation if the interface <see cref="IGivenWhenThenDeclaration"/>.</typeparam>
        /// <param name="method">The method to test.</param>
        /// <returns>True if the method passes the test, otherwise false.</returns>
        bool Filter<T>(MethodInfo method) where T : IGivenWhenThenDeclaration;
    }
}
