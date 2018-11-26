//-----------------------------------------------------------------------
// <copyright file="MethodsFilterByMethodsFullNameList.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is a methods filter by the existence of the method full name in a list.
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
    /// This is a methods filter by the existence of the method full name in a list.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IMethodsFilter" />
    public class MethodsFilterByMethodsFullNameList : IMethodsFilter
    {
        /// <summary>
        /// The full names methods list.
        /// </summary>
        private string[] methodsFullNamesList = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsFilterByMethodsFullNameList"/> class.
        /// </summary>
        /// <param name="methodsFullNamesList">The methods full names list.</param>
        public MethodsFilterByMethodsFullNameList(string[] methodsFullNamesList)
        {
            this.methodsFullNamesList = methodsFullNamesList;
        }

        /// <summary>
        /// Filters the specified method.
        /// </summary>
        /// <typeparam name="T">It must to be an implementation if the interface <see cref="IGivenWhenThenDeclaration" />.</typeparam>
        /// <param name="method">The method to test.</param>
        /// <returns>
        /// True if the method passes the test, otherwise false.
        /// </returns>
        public bool Filter<T>(MethodInfo method) where T : IGivenWhenThenDeclaration
        {
            foreach (string methodFullName in this.methodsFullNamesList)
            {
                if (methodFullName.Equals(method.DeclaringType.Name + "." + method.Name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
