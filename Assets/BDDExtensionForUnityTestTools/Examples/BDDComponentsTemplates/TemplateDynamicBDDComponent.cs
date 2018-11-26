//-----------------------------------------------------------------------
// <copyright file="TemplateDynamicBDDComponent.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is a template for a Dynamic Component.
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
using HudDimension.BDDExtensionForUnityTestTools;

/// <summary>
/// This is a template for a Dynamic Component.
/// </summary>
public class TemplateDynamicBDDComponent : DynamicBDDComponent
{
    /// <summary>
    /// One Step Method of type Given with a delay of 1 second.
    /// Please rename the method and change, the text and implement the body.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Given("given text", Delay = 1000)]
    public IAssertionResult GivenMethod()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        return result;
    }

    /// <summary>
    /// One Step Method of type When with a delay of 1 second.
    /// Please rename the method and change, the text and implement the body.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [When("when text")]
    public IAssertionResult WhenMethod()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        return result;
    }

    /// <summary>
    /// One Step Method of type Then with a delay of 1 second.
    /// Please rename the method and change, the text and implement the body.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then("then text")]
    public IAssertionResult ThenMethod()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        return result;
    }
}