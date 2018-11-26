//-----------------------------------------------------------------------
// <copyright file="AssertionResultSuccessful.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// An object of <see cref="AssertionResultSuccessful"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method execution is successful.
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
namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// An object of <see cref="AssertionResultSuccessful"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method execution is successful.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IAssertionResult" />
    public class AssertionResultSuccessful : IAssertionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionResultSuccessful"/> class.
        /// </summary>
        public AssertionResultSuccessful()
        {
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        internal string Text
        {
            get
            {
                return "OK";
            }
        }
    }
}
