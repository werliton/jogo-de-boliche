//-----------------------------------------------------------------------
// <copyright file="IAssertionResult.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This interface is used as return value of a Step Method.
// Implementations: <seealso cref="AssertionResultSuccessful"/>, <seealso cref="AssertionResultRetry"/>, <see cref="AssertionResultFailed"/>
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
    /// This interface is used as return value of a Step Method.
    /// Implementations: <seealso cref="AssertionResultSuccessful"/>, <seealso cref="AssertionResultRetry"/>, <see cref="AssertionResultFailed"/>
    /// </summary>
    public interface IAssertionResult
    {
    }
}
