//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicMethodsUtilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the utilities for managing the BDD Methods.
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
using System.Collections.Generic;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the utilities for managing the BDD Methods.
    /// </summary>
    public class RunnerEditorBusinessLogicMethodsUtilities
    {
        /// <summary>
        /// Checks if there is a missed method and adds it at the end of the combo box list..
        /// </summary>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="methodsNames">The methods names.</param>
        /// <param name="index">The index.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <returns>The updated combo box list.</returns>
        public string[] CheckMissedMethod(ChosenMethods chosenMethods, string[] methodsNames, int index, MethodDescription methodDescription)
        {
            if (methodDescription == null)
            {
                methodsNames = this.AddMissedMethodNameToMethodsNames(chosenMethods.ChosenMethodsNames[index], methodsNames);
            }

            return methodsNames;
        }

        /// <summary>
        /// Gets the method description.
        /// </summary>
        /// <param name="methodDescriptionBuilder">The method description builder.</param>
        /// <param name="parametersLoader">The parameters loader.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="methodsList">The methods list.</param>
        /// <param name="index">The index.</param>
        /// <returns>The method description.</returns>
        public MethodDescription GetMethodDescription(MethodDescriptionBuilder methodDescriptionBuilder, MethodParametersLoader parametersLoader, ChosenMethods chosenMethods, List<BaseMethodDescription> methodsList, int index)
        {
            MethodDescription methodDescription = null;
            if (!chosenMethods.ChosenMethodsNames[index].Equals(string.Empty))
            {
                methodDescription = this.GetMethodDescriptionForTheChosenMethod(methodDescriptionBuilder, parametersLoader, chosenMethods.ChosenMethodsNames[index], chosenMethods.ChosenMethodsParametersIndex[index], methodsList);
            }

            return methodDescription;
        }

        /// <summary>
        /// Detect if the data in the inspector need to be updated.
        /// </summary>
        /// <param name="newChosenMethod">The new chosen method.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="foldouts">The foldouts.</param>
        /// <param name="index">The index.</param>
        /// <param name="rebuild">If set to <c>true</c> [rebuild].</param>
        /// <param name="newUndoText">The new undo text.</param>
        /// <returns>True if the data in the inspector need to be updated, false otherwise.</returns>
        public bool UpdateDataIfNewMethodIsChosen(string newChosenMethod, ChosenMethods chosenMethods, bool[] foldouts, int index, bool rebuild, out string newUndoText)
        {
            newUndoText = string.Empty;
            if (!newChosenMethod.Equals(chosenMethods.ChosenMethodsNames[index]))
            {
                chosenMethods.ChosenMethodsNames[index] = newChosenMethod;
                newUndoText = "Change Step Method";
                foldouts[index] = false;
                rebuild = true;
            }

            return rebuild;
        }

        /// <summary>
        /// Gets the <see cref="MethodDescription"/> object for the chosen method.
        /// </summary>
        /// <param name="methodDescriptionBuilder">The method description builder.</param>
        /// <param name="parametersLoader">The parameters loader.</param>
        /// <param name="chosenMethodName">Name of the chosen method.</param>
        /// <param name="chosenMethodParametersIndex">Index of the chosen method parameters.</param>
        /// <param name="methodsList">The methods list.</param>
        /// <returns>The <see cref="MethodDescription"/> object for the chosen method.</returns>
        public MethodDescription GetMethodDescriptionForTheChosenMethod(MethodDescriptionBuilder methodDescriptionBuilder, MethodParametersLoader parametersLoader, string chosenMethodName, string chosenMethodParametersIndex, List<BaseMethodDescription> methodsList)
        {
            MethodDescription methodDescription = null;
            foreach (BaseMethodDescription baseMethodDescription in methodsList)
            {
                if (baseMethodDescription.GetFullName().Equals(chosenMethodName))
                {
                    methodDescription = methodDescriptionBuilder.Build(parametersLoader, baseMethodDescription, chosenMethodParametersIndex);
                }
            }

            return methodDescription;
        }

        /// <summary>
        /// Gets an array of the full names of the given methods.
        /// </summary>
        /// <param name="methodsList">The methods list.</param>
        /// <returns>The array of the full names of the given methods.</returns>
        public string[] GetMethodsNames(List<BaseMethodDescription> methodsList)
        {
            string[] methodsNames = new string[methodsList.Count];
            int methodsArrayindex = -1;
            foreach (BaseMethodDescription baseMethodDescription in methodsList)
            {
                methodsArrayindex++;
                methodsNames[methodsArrayindex] = baseMethodDescription.GetFullName();
            }

            return methodsNames;
        }

        /// <summary>
        /// Adds the missed method name to the end of the given array.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="methodsNames">The methods names.</param>
        /// <returns>The updated array.</returns>
        internal string[] AddMissedMethodNameToMethodsNames(string methodName, string[] methodsNames)
        {
            string[] newMethodsArray = new string[methodsNames.Length + 1];
            for (int temporaryIndex = 0; temporaryIndex < methodsNames.Length; temporaryIndex++)
            {
                newMethodsArray[temporaryIndex] = methodsNames[temporaryIndex];
            }

            newMethodsArray[newMethodsArray.Length - 1] = methodName;
            return newMethodsArray;
        }
    }
}