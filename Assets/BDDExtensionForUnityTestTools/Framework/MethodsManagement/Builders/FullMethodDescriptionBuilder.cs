//-----------------------------------------------------------------------
// <copyright file="FullMethodDescriptionBuilder.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
//  The builder of a <see cref="FullMethodDescription"/> object.
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
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    ///  The builder of a <see cref="FullMethodDescription"/> object.
    /// </summary>
    public class FullMethodDescriptionBuilder
    {
        /// <summary>
        /// Builds the list of <see cref="FullMethodDescription"/> objects based on the information contained into a <see cref="BaseMethodDescription"/> object.
        /// </summary>
        /// <param name="baseMethodDescription">The <see cref="BaseMethodDescription"/> object.</param>
        /// <param name="stepNumber">The index of the Step Method inside a chosenMethod list.</param>
        /// <returns>A <see cref="List"/> of <see cref="FullMethodDescription"/> object. Each element represent one of the element in the call chain defined by the <see cref="CallBefore"/> attributes. The last one is the main method described by the <paramref name="baseMethodDescription"/> parameter.</returns>
        public virtual List<FullMethodDescription> BuildFromBaseMethodDescription(BaseMethodDescription baseMethodDescription, uint stepNumber)
        {
            MethodDescriptionBuilder methodDescriptionBuilder = new MethodDescriptionBuilder();
            MethodParametersLoader methodParametersLoader = new MethodParametersLoader();
            string parametersIndex = string.Empty;
            MethodDescription methodDescription = methodDescriptionBuilder.Build(methodParametersLoader, baseMethodDescription, parametersIndex);
            return this.Build(methodDescription, stepNumber);
        }

        /// <summary>
        /// Builds the list of <see cref="FullMethodDescription"/> objects based on the information contained into a <see cref="MethodDescription"/> object.
        /// </summary>
        /// <param name="methodDescription">The method description.</param>
        /// <param name="stepNumber">The step number.</param>
        /// <returns>A <see cref="List"/> of <see cref="FullMethodDescription"/> object. Each element represent one of the element in the call chain defined by the <see cref="CallBefore"/> attributes. The last one is the main method described by the <paramref name="methodDescription"/> parameter.</returns>
        public virtual List<FullMethodDescription> Build(MethodDescription methodDescription, uint stepNumber)
        {
            List<FullMethodDescription> result = new List<FullMethodDescription>();
            if (methodDescription != null)
            {
                FullMethodDescription mainFullMethodDescription = this.GetFullMethodDescription(methodDescription.ComponentObject, methodDescription.Method, methodDescription.StepType, methodDescription.Text, methodDescription.Parameters, methodDescription.ParametersIndex, methodDescription.ExecutionOrder, 0, 0, stepNumber, string.Empty, null);
                this.AddDelayAndTimeoutToMainFullMethodDescription(mainFullMethodDescription);
                result = this.GetCallBeforeListFullMethodsDescriptions(mainFullMethodDescription, mainFullMethodDescription.ParametersIndex);
                result.Add(mainFullMethodDescription);
            }

            return result;
        }

        /// <summary>
        /// Adds the delay and timeout information to the Main method.
        /// </summary>
        /// <param name="mainFullMethodDescription">The main full method description.</param>
        private void AddDelayAndTimeoutToMainFullMethodDescription(FullMethodDescription mainFullMethodDescription)
        {
            object[] customCallBeforeAttributes = mainFullMethodDescription.Method.GetCustomAttributes(mainFullMethodDescription.StepType, true);
            IGivenWhenThenDeclaration bddBethodBaseAttribute = (IGivenWhenThenDeclaration)customCallBeforeAttributes[0];
            mainFullMethodDescription.Delay = bddBethodBaseAttribute.GetDelay();
            mainFullMethodDescription.TimeOut = bddBethodBaseAttribute.GetTimeout();
        }

        /// <summary>
        /// Gets the call chain defined by the <see cref="CallBefore"/> attributes.
        /// </summary>
        /// <param name="mainMethodDescription">The main method description.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        /// <returns>A <see cref="List"/> of <see cref="FullMethodDescription"/> object. Each element represent one of the element in the call chain defined by the <see cref="CallBefore"/> attributes.</returns>
        private List<FullMethodDescription> GetCallBeforeListFullMethodsDescriptions(FullMethodDescription mainMethodDescription, string parametersIndex)
        {
            List<FullMethodDescription> result = new List<FullMethodDescription>();
            object[] customCallBeforeAttributes = mainMethodDescription.Method.GetCustomAttributes(typeof(CallBefore), true);
            foreach (object callBeforeAttribute in customCallBeforeAttributes)
            {
                CallBefore callBefore = (CallBefore)callBeforeAttribute;
                if (callBefore.Method != null && !callBefore.Method.Equals(string.Empty))
                {
                    FullMethodDescription callBeforeFullMethodDescription = this.GetCallBeforeFullMethodDescription(callBefore, mainMethodDescription, parametersIndex);
                    List<FullMethodDescription> listOfCallBeforeFullMethodDescriptions = this.GetCallBeforeListFullMethodsDescriptions(callBeforeFullMethodDescription, parametersIndex);
                    result.AddRange(listOfCallBeforeFullMethodDescriptions);
                    result.Add(callBeforeFullMethodDescription);
                }
            }

            result.Sort();
            return result;
        }

        /// <summary>
        /// Gets the <see cref="FullMethodDescription"/> for the method declared by the <see cref="CallBefore"/> attribute.
        /// </summary>
        /// <param name="callBefore">The call before.</param>
        /// <param name="mainMethod">The main method.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        /// <returns>The <see cref="FullMethodDescription"/> for the method declared by the <see cref="CallBefore"/> attribute.</returns>
        private FullMethodDescription GetCallBeforeFullMethodDescription(CallBefore callBefore, FullMethodDescription mainMethod, string parametersIndex)
        {
            MethodInfo methodInfo = mainMethod.ComponentObject.GetType().GetMethod(callBefore.Method);
            MethodParametersLoader methodParametersLoader = new MethodParametersLoader();
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            string fullId = methodsManagementUtilities.GetMainFullId(mainMethod) + callBefore.Id;
            MethodParameters methodParameters = methodParametersLoader.LoadMethodParameters(mainMethod.ComponentObject, methodInfo, fullId, parametersIndex);

            FullMethodDescription result = this.GetFullMethodDescription(mainMethod.ComponentObject, methodInfo, mainMethod.StepType, mainMethod.Text, methodParameters, string.Empty, 0, callBefore.Delay, callBefore.Timeout, callBefore.ExecutionOrder, callBefore.Id, mainMethod);
            return result;
        }

        /// <summary>
        /// Gets the full method description.
        /// </summary>
        /// <param name="componentObject">The <see cref="Component"/> object containing the method.</param>
        /// <param name="method">The <see cref="MethodInfo"/> object for the method.</param>
        /// <param name="stepType">The Step Type.</param>
        /// <param name="text">The BDD text for the method. Can be empty. Cannot be null.</param>
        /// <param name="parameters">The <see cref="MethodParameters"/> object.</param>
        /// <param name="parametersIndex">The string indicating the ParametersIndex for the values of the parameters.</param>
        /// <param name="executionOrder">The relative order of the execution of the method in the call chain.</param>
        /// <param name="delay">The value of the delay.</param>
        /// <param name="timeOut">The value of the timeout.</param>
        /// <param name="callBeforeExecutionOrder">The relative order of the execution of the method in the call chain of the CallBefore methods.</param>
        /// <param name="id">The string that can make the parameter identifier unique.</param>
        /// <param name="mainMethod">The <see cref="FullMethodDescription"/> object if the parent method in the call chain.</param>
        /// <returns>THe <see cref="FullMethodDescription"/> object describing the information passed by the parameters.</returns>
        private FullMethodDescription GetFullMethodDescription(Component componentObject, MethodInfo method, Type stepType, string text, MethodParameters parameters, string parametersIndex, uint executionOrder, uint delay, uint timeOut, uint callBeforeExecutionOrder, string id, FullMethodDescription mainMethod)
        {
            FullMethodDescription result = new FullMethodDescription();
            result.ComponentObject = componentObject;
            result.Method = method;
            result.StepType = stepType;
            result.Text = text;
            result.Parameters = parameters;
            result.ParametersIndex = parametersIndex;
            result.ExecutionOrder = executionOrder;
            result.Delay = delay;
            result.TimeOut = timeOut;
            result.SuccessionOrder = callBeforeExecutionOrder;
            result.MainMethod = mainMethod;
            result.Id = id;

            return result;
        }
    }
}
