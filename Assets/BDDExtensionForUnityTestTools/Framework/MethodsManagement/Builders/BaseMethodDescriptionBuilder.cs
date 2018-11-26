//-----------------------------------------------------------------------
// <copyright file="BaseMethodDescriptionBuilder.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// The builder of a <see cref="BaseMethodDescription"/> object/
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// The builder of a <see cref="BaseMethodDescription"/> object.
    /// </summary>
    public class BaseMethodDescriptionBuilder
    {
        /// <summary>
        /// Builds the <see cref="BaseMethodDescription"/> object for the method described by the <paramref name="methodInfo"/> parameter.
        /// </summary>
        /// <typeparam name="T">One of the following types: <see cref="GivenBaseAttribute"/>, <see cref="WhenBaseAttribute"/>, <see cref="ThenBaseAttribute"/>.</typeparam>
        /// <param name="component">The component containing the method.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> object.</param>
        /// <returns>The <see cref="BaseMethodDescription"/> object for the method described by the <paramref name="methodInfo"/> parameter.</returns>
        public virtual BaseMethodDescription Build<T>(Component component, MethodInfo methodInfo) where T : IGivenWhenThenDeclaration
        {
            BaseMethodDescription result = new BaseMethodDescription();
            result.ComponentObject = component;
            result.Method = methodInfo;
            result.StepType = typeof(T);
            object[] attributes = methodInfo.GetCustomAttributes(typeof(T), true);
            IGivenWhenThenDeclaration gwtBaseAttribute = (IGivenWhenThenDeclaration)attributes[0];
            result.Text = gwtBaseAttribute.GetStepScenarioText();
            result.ExecutionOrder = gwtBaseAttribute.GetExecutionOrder();
            return result;
        }
    }
}
