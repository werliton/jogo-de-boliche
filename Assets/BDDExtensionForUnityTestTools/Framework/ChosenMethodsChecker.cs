//-----------------------------------------------------------------------
// <copyright file="ChosenMethodsChecker.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the whole collection of the Step Methods errors check.
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
    /// This class contains the whole collection of the Step Methods errors check.
    /// </summary>
    public class ChosenMethodsChecker
    {
        /// <summary>
        /// Checks all the Step Methods.
        /// </summary>
        /// <param name="givenChosenMethods">The given chosen methods.</param>
        /// <param name="givenParametersIndexes">The given parameters indexes.</param>
        /// <param name="whenChosenMethods">The when chosen methods.</param>
        /// <param name="whenParametersIndexes">The when parameters indexes.</param>
        /// <param name="thenChosenMethods">The then chosen methods.</param>
        /// <param name="thenParametersIndexes">The then parameters indexes.</param>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> Check(string[] givenChosenMethods, string[] givenParametersIndexes, string[] whenChosenMethods, string[] whenParametersIndexes, string[] thenChosenMethods, string[] thenParametersIndexes, Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            List<UnityTestBDDError> partialResult = this.CheckForBlankMethods<GivenBaseAttribute>(givenChosenMethods);
            result.AddRange(partialResult);

            partialResult = this.CheckForBlankMethods<WhenBaseAttribute>(whenChosenMethods);
            result.AddRange(partialResult);

            partialResult = this.CheckForBlankMethods<ThenBaseAttribute>(thenChosenMethods);
            result.AddRange(partialResult);

            partialResult = this.CheckForMethodNotFound<GivenBaseAttribute>(givenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForMethodNotFound<WhenBaseAttribute>(whenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForMethodNotFound<ThenBaseAttribute>(thenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingParametersIndex<GivenBaseAttribute>(givenChosenMethods, givenParametersIndexes, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingParametersIndex<WhenBaseAttribute>(whenChosenMethods, whenParametersIndexes, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingParametersIndex<ThenBaseAttribute>(thenChosenMethods, thenParametersIndexes, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForComponentNotFound<GivenBaseAttribute>(givenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForComponentNotFound<WhenBaseAttribute>(whenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForComponentNotFound<ThenBaseAttribute>(thenChosenMethods, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingPVS<GivenBaseAttribute>(givenChosenMethods, givenParametersIndexes, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingPVS<WhenBaseAttribute>(whenChosenMethods, whenParametersIndexes, components);
            result.AddRange(partialResult);

            partialResult = this.CheckForNotMatchingPVS<ThenBaseAttribute>(thenChosenMethods, thenParametersIndexes, components);
            result.AddRange(partialResult);

            return result;
        }

        /// <summary>
        /// Checks for blank methods.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForBlankMethods<T>(string[] chosenMethods) where T : IGivenWhenThenDeclaration
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                if (chosenMethods[index].Equals(string.Empty))
                {
                    IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "Incomplete settings detected on " + genericComponentInteface.GetStepName() + " methods at position " + (index + 1);
                    error.StepType = typeof(T);
                    error.Index = index;
                    error.LockRunnerInspectorOnErrors = false;
                    error.ShowButton = false;
                    error.LockBuildParameters = false;
                    error.LockParametersRows = false;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks for method not found inside the BDD Component.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForMethodNotFound<T>(string[] chosenMethods, Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            for (int index = 0; index < chosenMethods.Length; index++)
            {
                if (this.IsMethodNotFound(chosenMethods[index], components))
                {
                    IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "Method " + chosenMethods[index] + " not found on " + genericComponentInteface.GetStepName() + " methods at position " + (index + 1);
                    error.StepType = typeof(T);
                    error.Index = index;
                    error.LockRunnerInspectorOnErrors = false;
                    error.ShowButton = false;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = false;

                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the parameters index for not matching parameters.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="parametersIndexes">The parameters indexes.</param>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForNotMatchingParametersIndex<T>(string[] chosenMethods, string[] parametersIndexes, Component[] components) where T : IGivenWhenThenDeclaration
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            ParametersIndexUtilities parametersIndexUtilities = new ParametersIndexUtilities();
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                string[] parametersIndexList = parametersIndexUtilities.GetParametersIndexList(parametersIndexes[index]);
                foreach (string parametersIndex in parametersIndexList)
                {
                    string parameterType = parametersIndexUtilities.GetParameterType(parametersIndex);
                    string parameterName = parametersIndexUtilities.GetParameterName(parametersIndex);
                    string methodFullName = parametersIndexUtilities.GetMethodFullName(parametersIndex);
                    Component component = this.GetComponent(methodFullName, components);
                    MethodInfo methodInfo = this.GetMethodInfo(methodFullName, component);
                    List<UnityTestBDDError> partialResult = this.CheckForNotMatchingParametersIndex<T>(component, methodInfo, parameterType, parameterName, index);
                    result.AddRange(partialResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the parameters index for not matching parameters..
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="component">The component.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="chosenMethodIndex">Index of the chosen method.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForNotMatchingParametersIndex<T>(Component component, MethodInfo methodInfo, string parameterType, string parameterName, int chosenMethodIndex) where T : IGivenWhenThenDeclaration
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            bool isParameterFound = false;
            bool isParameterTypeMatching = false;
            ParameterInfo[] parameters = methodInfo.GetParameters();

            Type currentParameterType = null;
            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.Name.Equals(parameterName))
                {
                    isParameterFound = true;
                    currentParameterType = parameter.ParameterType;
                    if (currentParameterType.FullName.Equals(parameterType))
                    {
                        isParameterTypeMatching = true;
                    }
                }
            }

            if (!isParameterFound)
            {
                IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "The parameter " + component.GetType().Name + "." + methodInfo.Name + "." + parameterName + " is not found in " + genericComponentInteface.GetStepName() + " methods at position " + (chosenMethodIndex + 1);
                error.Component = component;
                error.MethodMethodInfo = methodInfo;
                error.StepType = typeof(T);
                error.Index = chosenMethodIndex;
                error.LockRunnerInspectorOnErrors = false;
                error.ShowButton = true;
                error.LockBuildParameters = true;
                error.LockParametersRows = true;
                result.Add(error);
            }
            else if (!isParameterTypeMatching)
            {
                IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "The parameter " + component.GetType().Name + "." + methodInfo.Name + "." + parameterName + " has a wrong type in " + genericComponentInteface.GetStepName() + " methods at position " + (chosenMethodIndex + 1) + ".\n Previous type: " + parameterType + "\n Current type " + currentParameterType.FullName;
                error.Component = component;
                error.MethodMethodInfo = methodInfo;
                error.StepType = typeof(T);
                error.Index = chosenMethodIndex;
                error.LockRunnerInspectorOnErrors = false;
                error.ShowButton = true;
                error.LockBuildParameters = true;
                error.LockParametersRows = true;

                result.Add(error);
            }

            return result;
        }

        /// <summary>
        /// Checks for BDD Component not found.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForComponentNotFound<T>(string[] chosenMethods, Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                if (this.GetComponent(chosenMethods[index], components) == null && chosenMethods[index] != null && !chosenMethods[index].Equals(string.Empty))
                {
                    IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The component for the method " + chosenMethods[index] + " is not found  in " + genericComponentInteface.GetStepName() + " methods at position " + (index + 1);
                    error.StepType = typeof(T);
                    error.Index = index;
                    error.LockRunnerInspectorOnErrors = false;
                    error.ShowButton = false;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;

                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks for not matching ParametersValuesStorage fields.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="parametersIndexes">The parameters indexes.</param>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckForNotMatchingPVS<T>(string[] chosenMethods, string[] parametersIndexes, Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            ParametersIndexUtilities parametersIndexUtilities = new ParametersIndexUtilities();
            for (int index = 0; index < chosenMethods.Length; index++)
            {
                Component component = this.GetComponent(chosenMethods[index], components);
                string[] parametersIndexList = parametersIndexUtilities.GetParametersIndexList(parametersIndexes[index]);
                foreach (string parameterIndex in parametersIndexList)
                {
                    string arrayPVSName = parametersIndexUtilities.GetParameterValueStorageName(parameterIndex);
                    FieldInfo arrayPVS = component.GetType().GetField(arrayPVSName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (arrayPVS == null)
                    {
                        IGivenWhenThenDeclaration genericComponentInteface = (IGivenWhenThenDeclaration)Activator.CreateInstance(typeof(T), string.Empty);
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The ParametersValuesStorage field " + arrayPVSName + " for the parameter " + parametersIndexUtilities.GetParameterFullName(parameterIndex) + " is not found in " + genericComponentInteface.GetStepName() + " methods at position " + (index + 1);
                        error.Component = component;
                        error.MethodMethodInfo = this.GetMethodInfo(chosenMethods[index], component);
                        error.StepType = typeof(T);
                        error.Index = index;
                        error.LockRunnerInspectorOnErrors = false;
                        error.ShowButton = true;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;

                        result.Add(error);
                    }
                    else
                    {
                        Array array = arrayPVS.GetValue(component) as Array;
                        if (array == null || array.Length == 0)
                        {
                            UnityTestBDDError error = new UnityTestBDDError();
                            error.Message = "The component " + component.GetType().Name + " seems to have been reset, so some parameter values are lost. Please, undo the reset operation or rebuild the settings to confirm the reset.";
                            error.Component = component;
                            error.MethodMethodInfo = this.GetMethodInfo(chosenMethods[index], component);
                            error.StepType = typeof(T);
                            error.Index = index;
                            error.LockRunnerInspectorOnErrors = false;
                            error.ShowButton = true;
                            error.LockBuildParameters = true;
                            error.LockParametersRows = true;

                            result.Add(error);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the MethodInfo given the full name of the method.
        /// </summary>
        /// <param name="methodFullName">Full name of the method.</param>
        /// <param name="component">The component.</param>
        /// <returns>The MethodInfo given the full name of the method.</returns>
        private MethodInfo GetMethodInfo(string methodFullName, Component component)
        {
            if (!methodFullName.Equals(string.Empty))
            {
                string methodName = methodFullName.Split('.')[1];
                return component.GetType().GetMethod(methodName);
            }

            return null;
        }

        /// <summary>
        /// Gets the component containing the given method.
        /// </summary>
        /// <param name="methodFullName">Full name of the method.</param>
        /// <param name="components">The components.</param>
        /// <returns>The component containing the given method.</returns>
        private Component GetComponent(string methodFullName, Component[] components)
        {
            if (!methodFullName.Equals(string.Empty))
            {
                string componentName = methodFullName.Split('.')[0];
                foreach (Component component in components)
                {
                    if (component.GetType().Name.Equals(componentName))
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether the method is found inside the BDD Component given the method full name.
        /// </summary>
        /// <param name="methodFullName">Full name of the method.</param>
        /// <param name="components">The components.</param>
        /// <returns>
        ///   <c>true</c> if the method is not found; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMethodNotFound(string methodFullName, Component[] components)
        {
            bool result = true;
            if (methodFullName.Equals(string.Empty))
            {
                result = false;
            }
            else
            {
                string componentName = methodFullName.Split('.')[0];
                string methodName = methodFullName.Split('.')[1];
                foreach (Component component in components)
                {
                    if (component.GetType().Name.Equals(componentName))
                    {
                        if (component.GetType().GetMethod(methodName) != null)
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return result;
        }
    }
}