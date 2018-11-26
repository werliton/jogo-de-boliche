//-----------------------------------------------------------------------
// <copyright file="MethodDescriptionBuilder.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
//  The builder of a <see cref="MethodDescription"/> object.
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
    ///  The builder of a <see cref="MethodDescription"/> object.
    /// </summary>
    public class MethodDescriptionBuilder
    {
        /// <summary>
        /// Builds the list of <see cref="MethodDescription"/> objects based on the information contained into a <see cref="BaseMethodDescription"/> object.
        /// </summary>
        /// <param name="methodParametersLoader">The method parameters loader.</param>
        /// <param name="baseMethodDescription">The base method description.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        /// <returns>The built <see cref="MethodDescription"/> object.</returns>
        public virtual MethodDescription Build(MethodParametersLoader methodParametersLoader, BaseMethodDescription baseMethodDescription, string parametersIndex)
        {
            if (baseMethodDescription != null)
            {
                MethodDescription result = new MethodDescription();
                result.ComponentObject = baseMethodDescription.ComponentObject;
                result.Method = baseMethodDescription.Method;
                result.StepType = baseMethodDescription.StepType;
                result.Text = baseMethodDescription.Text;
                result.ExecutionOrder = baseMethodDescription.ExecutionOrder;
                result.ParametersIndex = parametersIndex;
                result.Parameters = methodParametersLoader.LoadMethodParameters(result.ComponentObject, result.Method, string.Empty, result.ParametersIndex);
                return result;
            }

            return null;             
        }
    }
}
