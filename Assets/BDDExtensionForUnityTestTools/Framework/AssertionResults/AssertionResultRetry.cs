//-----------------------------------------------------------------------
// <copyright file="AssertionResultRetry.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
//  An object of <see cref="AssertionResultRetry"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method has to be executed again.
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
    ///  An object of <see cref="AssertionResultRetry"/> is returned by a Step Method. It communicates to the <seealso cref="BDDExtensionRunner"/> that the Step Method has to be executed again.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IAssertionResult" />
    public class AssertionResultRetry : IAssertionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionResultRetry"/> class.
        /// </summary>
        /// <param name="text">The text that describes the reason why the execution of the Step Method has failed after the timeout for the Step Method is reached.</param>
        public AssertionResultRetry(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text that describes the reason why the execution of the Step Method has failed after the timeout for the Step Method is reached.
        /// </value>
        public string Text { get; set; }
    }
}
