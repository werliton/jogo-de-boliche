//-----------------------------------------------------------------------
// <copyright file="MethodsManagementUtilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// A collection of utilities for managing the BDD Methods.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// A collection of utilities for managing the BDD Methods.
    /// </summary>
    public class MethodsManagementUtilities
    {
        /// <summary>
        /// Builds a list of <see cref="FullMethodDescription"/> objects given a list of <see cref="BaseMethodDescription"/> objects.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="methodsDescriptionsList">The methods descriptions list.</param>
        /// <param name="fullMethodDescriptionBuilder">The full method description builder.</param>
        /// <returns>A list of <see cref="FullMethodDescription"/> objects.</returns>
        public List<FullMethodDescription> LoadFullMethodsDescriptions<T>(List<BaseMethodDescription> methodsDescriptionsList, FullMethodDescriptionBuilder fullMethodDescriptionBuilder) where T : IGivenWhenThenDeclaration
        {
            List<FullMethodDescription> fullMethodsDescriptions = new List<FullMethodDescription>();

            for (int index = 0; index < methodsDescriptionsList.Count; index++)
            {
                BaseMethodDescription baseMethodDescription = methodsDescriptionsList[index];
                List<FullMethodDescription> fullmethodDescriptionsList = fullMethodDescriptionBuilder.BuildFromBaseMethodDescription(baseMethodDescription, (uint)index + 1);
                fullMethodsDescriptions.AddRange(fullmethodDescriptionsList);
            }

            return fullMethodsDescriptions;
        }

        /// <summary>
        ///  Builds a list of <see cref="FullMethodDescription"/> objects given a list of <see cref="MethodDescription"/> objects.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="methodsDescriptionsList">The methods descriptions list.</param>
        /// <param name="fullMethodDescriptionBuilder">The full method description builder.</param>
        /// <returns>A list of <see cref="FullMethodDescription"/> objects.</returns>
        public List<FullMethodDescription> LoadFullMethodsDescriptions<T>(List<MethodDescription> methodsDescriptionsList, FullMethodDescriptionBuilder fullMethodDescriptionBuilder) where T : IGivenWhenThenDeclaration
        {
            List<FullMethodDescription> fullMethodsDescriptions = new List<FullMethodDescription>();

            for (int index = 0; index < methodsDescriptionsList.Count; index++)
            {
                MethodDescription methodDescription = methodsDescriptionsList[index];
                List<FullMethodDescription> fullmethodDescriptionsList = fullMethodDescriptionBuilder.Build(methodDescription, (uint)index + 1);
                fullMethodsDescriptions.AddRange(fullmethodDescriptionsList);
            }

            return fullMethodsDescriptions;
        }

        /// <summary>
        /// Builds a list of <see cref="FullMethodDescription"/> objects given a list of method full names.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="dynamicBDDComponents">The dynamic BDD components.</param>
        /// <param name="methodsLoader">The methods loader.</param>
        /// <param name="methodDescriptionBuilder">The method description builder.</param>
        /// <param name="methodParametersLoader">The method parameters loader.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="chosenMethodsParametersIndexes">The chosen methods parameters indexes.</param>
        /// <returns>A list of <see cref="FullMethodDescription"/> objects.</returns>
        public List<MethodDescription> LoadMethodsDescriptionsFromChosenMethods<T>(Component[] dynamicBDDComponents, MethodsLoader methodsLoader, MethodDescriptionBuilder methodDescriptionBuilder, MethodParametersLoader methodParametersLoader, string[] chosenMethods, string[] chosenMethodsParametersIndexes) where T : IGivenWhenThenDeclaration
        {
            List<MethodDescription> methodsDescriptions = new List<MethodDescription>();
            List<BaseMethodDescription> baseMethodsDescriptions = methodsLoader.LoadOrderedStepMethods<T>(dynamicBDDComponents, chosenMethods);
            for (int index = 0; index < baseMethodsDescriptions.Count; index++)
            {
                BaseMethodDescription baseMethodDescription = baseMethodsDescriptions[index];
                string parametersIndex = chosenMethodsParametersIndexes[index];
                MethodDescription methodDescription = methodDescriptionBuilder.Build(methodParametersLoader, baseMethodDescription, parametersIndex);
                methodsDescriptions.Add(methodDescription);
            }

            return methodsDescriptions;
        }

        /// <summary>
        /// Gets the parameters index string for the passed method name.
        /// </summary>
        /// <param name="methodFullName">Full name of the method.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="chosenMethodsParametersIndexes">The chosen methods parameters indexes.</param>
        /// <returns>The parameters index string.</returns>
        public string GetParametersIndexForMethod(string methodFullName, string[] chosenMethods, string[] chosenMethodsParametersIndexes)
        {
            string result = null;
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                if (methodFullName.Equals(chosenMethods[index]))
                {
                    result = chosenMethodsParametersIndexes[index];
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether if the scenario is static.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>
        ///   <c>true</c> if there is a Static Component; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStaticBDDScenario(Component[] components)
        {
            bool result = false;
            foreach (Component component in components)
            {
                if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the full id of a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The full id of a method.</returns>
        public string GetFullId(FullMethodDescription method)
        {
            if (method == null)
            {
                return string.Empty;
            }

            string mainFullId = this.GetMainFullId(method.MainMethod);
            return mainFullId + method.Id;
        }

        /// <summary>
        /// Gets the main full id of a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The main full id of a method.</returns>
        public string GetMainFullId(FullMethodDescription method)
        {
            if (method == null || method.MainMethod == null)
            {
                return string.Empty;
            }

            return this.GetMainFullId(method.MainMethod) + method.Id + "_";
        }

        /// <summary>
        /// Gets the ordered list of <see cref="BaseMethodDescription"/> objects given a list of method full names.
        /// </summary>
        /// <param name="baseMethodsDescriptions">The base methods descriptions.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <returns>The ordered list of <see cref="BaseMethodDescription"/> objects.</returns>
        private List<BaseMethodDescription> GetOrderedListByMethodsNames(List<BaseMethodDescription> baseMethodsDescriptions, string[] chosenMethods)
        {
            List<BaseMethodDescription> result = new List<BaseMethodDescription>();
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                foreach (BaseMethodDescription method in baseMethodsDescriptions)
                {
                    if (chosenMethods[index].Equals(method.GetFullName()))
                    {
                        result.Add(method);
                    }
                }
            }

            return result;
        }
    }
}
