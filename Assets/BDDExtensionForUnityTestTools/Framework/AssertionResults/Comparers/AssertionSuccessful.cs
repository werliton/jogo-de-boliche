//-----------------------------------------------------------------------
// <copyright file="AssertionSuccessful.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Extension of the <seealso cref="ActionBase"/> class of the UnityTest asset for managing the success of an Integration Test.
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
using UnityTest;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Extension of the <seealso cref="ActionBase"/> class of the UnityTest asset for managing the success of an Integration Test.
    /// </summary>
    /// <seealso cref="UnityTest.ActionBase" />
    public class AssertionSuccessful : ActionBase
    {
        /// <summary>
        /// Gets the failure message.
        /// </summary>
        /// <returns>A string containing a generic message for the successful run of the Integration Test.</returns>
        public override string GetFailureMessage()
        {
            return GetType().Name + ": assertion success.\n";
        }

        /// <summary>
        /// Compares the specified object value.
        /// </summary>
        /// <param name="objVal">The object value.</param>
        /// <returns>Always true.</returns>
        protected override bool Compare(object objVal)
        {
            return true;
        }
    }
}
