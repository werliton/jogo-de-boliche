//-----------------------------------------------------------------------
// <copyright file="AssertionFailed.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Extension of the <seealso cref="ActionBase"/> class of the UnityTest asset for managing the failure of an Integration Test.
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
    /// Extension of the <seealso cref="ActionBase"/> class of the UnityTest asset for managing the failure of an Integration Test.
    /// </summary>
    /// <seealso cref="UnityTest.ActionBase" />
    internal class AssertionFailed : ActionBase
    {
        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        /// <value>
        /// The reason of the Test failure.
        /// </value>
        public string ErrorText { get; set; }

        /// <summary>
        /// Gets or sets the scenario text.
        /// </summary>
        /// <value>
        /// The description of the failed scenario in the Given-When-Then form.
        /// </value>
        public string ScenarioText { get; set; }

        /// <summary>
        /// Gets or sets the BDD method location.
        /// </summary>
        /// <value>
        /// The description of the methods calls chain with the information of which method has failed.
        /// </value>
        public string BDDMethodLocation { get; set; }

        /// <summary>
        /// This method composes the description of the failure.
        /// </summary>
        /// <returns>The text containing the description of the failure.</returns>
        public override string GetFailureMessage()
        {
            string text = "Test Failed!\nReason: " + this.ErrorText;
            if (this.ScenarioText != null)
            {
                text += "\n For the scenario:\n" + this.ScenarioText;
            }

            if (this.BDDMethodLocation != null)
            {
                text += "\n BDD Method: \n" + this.BDDMethodLocation;
            }

            return text;
        }

        /// <summary>
        /// Compares the specified object value.
        /// </summary>
        /// <param name="objVal">The object value.</param>
        /// <returns>Always false.</returns>
        protected override bool Compare(object objVal)
        {
            return false;
        }
    }
}
