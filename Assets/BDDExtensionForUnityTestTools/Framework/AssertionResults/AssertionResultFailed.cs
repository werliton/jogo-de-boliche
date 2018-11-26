//-----------------------------------------------------------------------
// <copyright file="AssertionResultFailed.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// An object of <see cref="AssertionResultFailed"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method has failed. 
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
    /// An object of <see cref="AssertionResultFailed"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method has failed. 
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IAssertionResult" />
    public class AssertionResultFailed : IAssertionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionResultFailed"/> class.
        /// </summary>
        /// <param name="text">The text that describes the reason why the execution of the Step Method has failed.</param>
        public AssertionResultFailed(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text that describes the reason why the execution of the Step Method has failed.
        /// </value>
        internal string Text { get; set; }
    }
}
