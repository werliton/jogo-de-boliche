//-----------------------------------------------------------------------
// <copyright file="ComponentsChecker.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the whole collection of the BDD Components errors check.
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
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the whole collection of the BDD Components errors check.
    /// </summary>
    public class ComponentsChecker
    {
        /// <summary>
        /// Checks all the errors for the given components.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> Check(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            List<UnityTestBDDError> partialResult = this.CheckEnoughAttachedComponents(components);
            result.AddRange(partialResult);

            partialResult = this.CheckDuplicateComponents(components);
            result.AddRange(partialResult);

            partialResult = this.CheckDuplicateStaticComponents(components);
            result.AddRange(partialResult);

            foreach (Component component in components)
            {
                partialResult = this.Check(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if there are more than one Static Components attached to the Integration Test.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckDuplicateStaticComponents(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            bool isAlreadyFoundAStaticComponent = false;
            foreach (Component component in components)
            {
                if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()) && isAlreadyFoundAStaticComponent)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "There are more than one Static BDD Component. Only one is allowed";
                    error.Component = null;
                    error.MethodMethodInfo = null;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = false;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;

                    result.Add(error);
                    return result;
                }
                else
                {
                    isAlreadyFoundAStaticComponent = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the specified component.
        /// </summary>
        /// <param name="component">The component to check.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> Check(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            List<UnityTestBDDError> partialResult = this.CheckDuplicateStepMethodsForASingleComponent(component);
            result.AddRange(partialResult);

            partialResult = this.CheckValuesParametersStorage(component);
            result.AddRange(partialResult);

            partialResult = this.CheckBDDMethodReturnValue(component);
            result.AddRange(partialResult);

            partialResult = this.CheckCallBeforeMethods(component);
            result.AddRange(partialResult);

            partialResult = this.CheckCallBeforeOnStepDeclaration(component);
            result.AddRange(partialResult);

            partialResult = this.CheckCallBeforeExecutionOrders(component);
            result.AddRange(partialResult);

            partialResult = this.CheckRecursiveCalls(component);
            result.AddRange(partialResult);

            partialResult = this.CheckParametersUniqueness(component);
            result.AddRange(partialResult);

            partialResult = this.CheckStepMethodsExecutionOrders(component);
            result.AddRange(partialResult);

            partialResult = this.CheckStepMethodsExistence(component);
            result.AddRange(partialResult);

            partialResult = this.CheckStaticComponentsWithParameters(component);
            result.AddRange(partialResult);
            return result;
        }

        /// <summary>
        /// Checks if there are more than one methods with the same name inside a BDD Component.
        /// </summary>
        /// <param name="components">The components to check.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckDuplicateStepMethods(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckDuplicateStepMethodsForASingleComponent(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if a component is attached more than once to an Integration Test.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckDuplicateComponents(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            Dictionary<Type, Component> dictionary = new Dictionary<Type, Component>();
            foreach (Component component in components)
            {
                if (dictionary.ContainsKey(component.GetType()))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The component " + component.GetType().Name + " is duplicated.";
                    error.Component = component;
                    error.MethodMethodInfo = null;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = false;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
                else
                {
                    dictionary.Add(component.GetType(), component);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks errors on the Parameters Values Storage fields definitions.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckValuesParametersStorage(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckValuesParametersStorage(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the return value of a BDD Method.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckBDDMethodReturnValue(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckBDDMethodReturnValue(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the methods declared inside the <see cref="CallBefore"/> attributes.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckCallBeforeMethods(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeMethods(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if the method declared inside a <see cref="CallBefore"/> attribute is a BDD Method.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckCallBeforeOnStepDeclaration(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeOnStepDeclaration(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the values of the ExecutionOrder parameters for a <see cref="CallBefore"/> chain.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckCallBeforeExecutionOrders(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeExecutionOrders(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the values of the ExecutionOrder parameters for Static Components.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStepMethodsExecutionOrders(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            foreach (Component component in components)
            {
                if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
                {
                    List<UnityTestBDDError> partialResult = this.CheckStepMethodsExecutionOrders(component);
                    result.AddRange(partialResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there is at least one BDD Method for each Step Method type.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStepMethodsExistence(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckStepMethodsExistence(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if there is at least one BDD Method for each Step Method type.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStepMethodsExistence(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            bool isGivenMethodFound = false;
            bool isWhenMethodFound = false;
            bool isThenMethodFound = false;
            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                object[] bddMethodBaseAttributes = methodInfo.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true);
                foreach (object rawAttribute in bddMethodBaseAttributes)
                {
                    if (typeof(GivenBaseAttribute).IsAssignableFrom(rawAttribute.GetType()))
                    {
                        isGivenMethodFound = true;
                    }

                    if (typeof(WhenBaseAttribute).IsAssignableFrom(rawAttribute.GetType()))
                    {
                        isWhenMethodFound = true;
                    }

                    if (typeof(ThenBaseAttribute).IsAssignableFrom(rawAttribute.GetType()))
                    {
                        isThenMethodFound = true;
                    }
                }
            }

            if (!isGivenMethodFound)
            {
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "The component " + component.GetType().Name + " has not Given components";
                error.Component = component;
                error.MethodMethodInfo = null;
                error.LockRunnerInspectorOnErrors = true;
                error.ShowButton = true;
                error.ShowRedExclamationMark = true;
                error.StepType = null;
                error.Index = 0;
                error.LockBuildParameters = true;
                error.LockParametersRows = true;

                result.Add(error);
            }

            if (!isWhenMethodFound)
            {
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "The component " + component.GetType().Name + " has not When components";
                error.Component = component;
                error.MethodMethodInfo = null;
                error.LockRunnerInspectorOnErrors = true;
                error.ShowButton = true;
                error.ShowRedExclamationMark = true;
                error.StepType = null;
                error.Index = 0;
                error.LockBuildParameters = true;
                error.LockParametersRows = true;

                result.Add(error);
            }

            if (!isThenMethodFound)
            {
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "The component " + component.GetType().Name + " has not Then components";
                error.Component = component;
                error.MethodMethodInfo = null;
                error.LockRunnerInspectorOnErrors = true;
                error.ShowButton = true;
                error.ShowRedExclamationMark = true;
                error.StepType = null;
                error.Index = 0;
                error.LockBuildParameters = true;
                error.LockParametersRows = true;
                result.Add(error);
            }

            return result;
        }

        /// <summary>
        /// Checks if there are BDD Methods with parameters inside a Static Component.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStaticComponentsWithParameters(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            foreach (Component component in components)
            {
                List<UnityTestBDDError> partialResult = this.CheckStaticComponentsWithParameters(component);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if there are BDD Methods with parameters inside a Static Component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStaticComponentsWithParameters(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
            {
                MethodInfo[] methodsInfo = component.GetType().GetMethods();
                foreach (MethodInfo methodInfo in methodsInfo)
                {
                    List<UnityTestBDDError> partialResult = this.CheckStaticComponentsWithParameters(methodInfo);
                    result.AddRange(partialResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there are BDD Methods with parameters inside a Static Component.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckStaticComponentsWithParameters(MethodInfo method)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (method.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true).Length > 0)
            {
                if (method.GetParameters().Length > 0)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + method.DeclaringType.Name + "." + method.Name + " is not allowed to have parameters";
                    error.Component = null;
                    error.MethodMethodInfo = method;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the full names of the parameters are unique inside a Dynamic Component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckParametersUniqueness(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (this.CheckRecursiveCalls(component).Count > 0)
            {
                // Absolutely I have not to go throw if there is a infinite loop
                return result;
            }

            FullMethodDescriptionBuilder fullMethodDescriptionBuilder = new FullMethodDescriptionBuilder();
            MethodDescriptionBuilder methodDescriptionBuilder = new MethodDescriptionBuilder();
            BaseMethodDescriptionBuilder baseMethodDescriptionBuilder = new BaseMethodDescriptionBuilder();
            MethodParametersLoader methodParametersLoader = new MethodParametersLoader();
            MethodInfo[] methods = component.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true).Length > 0)
                {
                    BaseMethodDescription baseMethodDescription = baseMethodDescriptionBuilder.Build<BDDMethodBaseAttribute>(component, method);
                    MethodDescription methodDescription = methodDescriptionBuilder.Build(methodParametersLoader, baseMethodDescription, string.Empty);
                    List<FullMethodDescription> fullMethodsDescriptionsList = fullMethodDescriptionBuilder.Build(methodDescription, 1);
                    List<UnityTestBDDError> partialResult = this.CheckParametersUniqueness(fullMethodsDescriptionsList);
                    result.AddRange(partialResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the full names of the parameters are unique inside a Dynamic Component.
        /// </summary>
        /// <param name="fullMethodDescriptionList">The full method description list.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckParametersUniqueness(List<FullMethodDescription> fullMethodDescriptionList)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            Dictionary<string, FullMethodDescription> dictionary = new Dictionary<string, FullMethodDescription>();
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            foreach (FullMethodDescription fullMethodDescription in fullMethodDescriptionList)
            {
                if (fullMethodDescription.Parameters.Parameters.Length > 0)
                {
                    string fullId = methodsManagementUtilities.GetMainFullId(fullMethodDescription.MainMethod) + fullMethodDescription.Id;
                    string key = fullMethodDescription.GetFullName() + "." + fullId;
                    if (dictionary.ContainsKey(key))
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The CallBefore chains declared for the method " + fullMethodDescriptionList.Last().Method.Name + " have a non unique identifications for the parameters of the method identified by the key: \"" + key + "\"\n Please, use the Id property in the CallBefore attributes for making them unique.";

                        error.Component = null;
                        error.MethodMethodInfo = fullMethodDescriptionList.Last().Method;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.LockBuildParameters = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }
                    else
                    {
                        dictionary.Add(key, fullMethodDescription);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there are recursive calls inside a <see cref="CallBefore"/> chain.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckRecursiveCalls(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            MethodInfo[] methods = component.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true).Length > 0)
                {
                    // The following statement seems very useless but 
                    // a MethodInfo object returned by the GetMethods method
                    // could be different from the MethodInfo object for the same method returned by the GetMethod(string methodName) method
                    // if there is a hineritance.
                    string methodName = method.Name;
                    MethodInfo callChainMethod = component.GetType().GetMethod(methodName);

                    CallChain callChain = new CallChain();
                    callChain.Method = callChainMethod;
                    callChain.CallingChain = null;
                    callChain.CallBeforeDeclaration = null;
                    List<UnityTestBDDError> partialResult = this.CheckRecursiveCalls(callChainMethod, callChain);
                    result.AddRange(partialResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there is at least one BDD Component attached to the Integration Test.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        public List<UnityTestBDDError> CheckEnoughAttachedComponents(Component[] components)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (components != null && components.Length == 0)
            {
                UnityTestBDDError error = new UnityTestBDDError();
                error.Message = "Please, add your BDD Components and enjoy BDD.";
                error.Component = null;
                error.MethodMethodInfo = null;
                error.LockRunnerInspectorOnErrors = true;
                error.ShowButton = false;
                error.LockBuildParameters = true;
                error.ShowRedExclamationMark = false;
                error.StepType = null;
                error.Index = 0;
                error.LockParametersRows = true;
                result.Add(error);
            }

            return result;
        }

        /// <summary>
        /// Checks the return value of a BDD Method.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckBDDMethodReturnValue(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                List<UnityTestBDDError> partialResult = this.CheckBDDMethodReturnValue(methodInfo);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the return value of a BDD Method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckBDDMethodReturnValue(MethodInfo methodInfo)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (!typeof(IAssertionResult).IsAssignableFrom(methodInfo.ReturnType))
            {
                object[] customAttributes = methodInfo.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true);
                if (customAttributes.Length > 0)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name + " does not return an IAssertionResult value.";
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the methods declared inside the <see cref="CallBefore"/> attributes.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeMethods(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeMethods(methodInfo);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the methods declared inside the <see cref="CallBefore"/> attributes.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeMethods(MethodInfo methodInfo)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            object[] customAttributes = methodInfo.GetCustomAttributes(typeof(CallBefore), true);
            foreach (object obj in customAttributes)
            {
                CallBefore callBefore = obj as CallBefore;
                string method = callBefore.Method;
                if (methodInfo.DeclaringType.GetMethod(method) == null)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "Method " + methodInfo.DeclaringType.Name + "." + method + " not found. It is referenced in a CallBefore attribute for the method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name;
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the method declared inside a <see cref="CallBefore"/> attribute is a BDD Method.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeOnStepDeclaration(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeOnStepDeclaration(methodInfo);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks if the method declared inside a <see cref="CallBefore"/> attribute is a BDD Method..
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeOnStepDeclaration(MethodInfo methodInfo)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            object[] callBeforeAttributes = methodInfo.GetCustomAttributes(typeof(CallBefore), true);
            if (callBeforeAttributes.Length > 0)
            {
                object[] bddMethodBaseAttributes = methodInfo.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true);
                if (bddMethodBaseAttributes.Length == 0)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name + " has a CallBefore attribute but it is not a BDD Method.";
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        ///  Checks the values of the ExecutionOrder parameters for a <see cref="CallBefore"/> chain.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeExecutionOrders(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                List<UnityTestBDDError> partialResult = this.CheckCallBeforeExecutionOrders(methodInfo);
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the values of the ExecutionOrder parameters for Static Components.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckStepMethodsExecutionOrders(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            uint maxGivenExecutionOrderValue = 0;
            Dictionary<uint, Type> givenDictionary = new Dictionary<uint, Type>();

            uint maxWhenExecutionOrderValue = 0;

            Dictionary<uint, Type> whenDictionary = new Dictionary<uint, Type>();

            uint maxThenExecutionOrderValue = 0;

            Dictionary<uint, Type> thenDictionary = new Dictionary<uint, Type>();

            MethodInfo[] methodsInfo = component.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodsInfo)
            {
                object[] customAttributes = methodInfo.GetCustomAttributes(typeof(StaticBDDComponent.Given), true);
                if (customAttributes.Length > 0)
                {
                    StaticBDDComponent.Given givenAttibute = customAttributes[0] as StaticBDDComponent.Given;
                    if (givenAttibute.ExecutionOrder == 0)
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The Given declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a wrong ExecutionOrder value: it must be >0";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }

                    if (maxGivenExecutionOrderValue < givenAttibute.ExecutionOrder)
                    {
                        maxGivenExecutionOrderValue = givenAttibute.ExecutionOrder;
                    }

                    if (givenDictionary.ContainsKey(givenAttibute.ExecutionOrder))
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The Given declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a duplicate ExecutionOrder value: " + givenAttibute.ExecutionOrder + ". Check the others Given methods.";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }
                    else
                    {
                        givenDictionary.Add(givenAttibute.ExecutionOrder, givenAttibute.GetType());
                    }
                }

                customAttributes = methodInfo.GetCustomAttributes(typeof(StaticBDDComponent.When), true);
                if (customAttributes.Length > 0)
                {
                    StaticBDDComponent.When whenAttibute = customAttributes[0] as StaticBDDComponent.When;
                    if (whenAttibute.ExecutionOrder == 0)
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The When declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a wrong ExecutionOrder value: it must be >0";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }

                    if (maxWhenExecutionOrderValue < whenAttibute.ExecutionOrder)
                    {
                        maxWhenExecutionOrderValue = whenAttibute.ExecutionOrder;
                    }

                    if (whenDictionary.ContainsKey(whenAttibute.ExecutionOrder))
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The When declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a duplicate ExecutionOrder value. Check the others When methods.";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }
                    else
                    {
                        whenDictionary.Add(whenAttibute.ExecutionOrder, whenAttibute.GetType());
                    }
                }

                customAttributes = methodInfo.GetCustomAttributes(typeof(StaticBDDComponent.Then), true);
                if (customAttributes.Length > 0)
                {
                    StaticBDDComponent.Then thenAttibute = customAttributes[0] as StaticBDDComponent.Then;
                    if (thenAttibute.ExecutionOrder == 0)
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The Then declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a wrong ExecutionOrder value: it must be >0";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;

                        result.Add(error);
                    }

                    if (maxThenExecutionOrderValue < thenAttibute.ExecutionOrder)
                    {
                        maxThenExecutionOrderValue = thenAttibute.ExecutionOrder;
                    }

                    if (thenDictionary.ContainsKey(thenAttibute.ExecutionOrder))
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The Then declaration for the method " + component.GetType().Name + "." + methodInfo.Name + " has a duplicate ExecutionOrder value. Check the others Then methods.";
                        error.Component = null;
                        error.MethodMethodInfo = methodInfo;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }
                    else
                    {
                        thenDictionary.Add(thenAttibute.ExecutionOrder, thenAttibute.GetType());
                    }
                }
            }

            for (uint givenIndex = 1; givenIndex <= maxGivenExecutionOrderValue; givenIndex++)
            {
                if (!givenDictionary.ContainsKey(givenIndex))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The component " + component.GetType().Name + " has a missing Given.ExecutionOrder: " + givenIndex;
                    error.Component = component;
                    error.MethodMethodInfo = null;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;

                    result.Add(error);
                }
            }

            for (uint whenIndex = 1; whenIndex <= maxWhenExecutionOrderValue; whenIndex++)
            {
                if (!whenDictionary.ContainsKey(whenIndex))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The component " + component.GetType().Name + " has a missing When.ExecutionOrder: " + whenIndex;
                    error.Component = component;
                    error.MethodMethodInfo = null;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;

                    result.Add(error);
                }
            }

            for (uint thenIndex = 1; thenIndex <= maxThenExecutionOrderValue; thenIndex++)
            {
                if (!thenDictionary.ContainsKey(thenIndex))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The component " + component.GetType().Name + " has a missing Then.ExecutionOrder: " + thenIndex;
                    error.Component = component;
                    error.MethodMethodInfo = null;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        ///  Checks the values of the ExecutionOrder parameters for a <see cref="CallBefore"/> chain.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckCallBeforeExecutionOrders(MethodInfo methodInfo)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();

            uint maxExecutionOrderValue = 0;
            Dictionary<uint, CallBefore> dictionary = new Dictionary<uint, CallBefore>();
            object[] customAttributes = methodInfo.GetCustomAttributes(typeof(CallBefore), true);
            foreach (object obj in customAttributes)
            {
                CallBefore callBefore = obj as CallBefore;
                if (callBefore.ExecutionOrder == 0)
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name + " has a wrong value for CallBefore.ExecutionOrder: " + callBefore.ExecutionOrder + "\n It must be >0";
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }

                if (maxExecutionOrderValue < callBefore.ExecutionOrder)
                {
                    maxExecutionOrderValue = callBefore.ExecutionOrder;
                }

                if (dictionary.ContainsKey(callBefore.ExecutionOrder))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name + " has duplicated CallBefore.ExecutionOrder: " + callBefore.ExecutionOrder;
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
                else
                {
                    dictionary.Add(callBefore.ExecutionOrder, callBefore);
                }
            }

            for (uint index = 1; index <= maxExecutionOrderValue; index++)
            {
                if (!dictionary.ContainsKey(index))
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "The method " + methodInfo.DeclaringType.Name + "." + methodInfo.Name + " has a missing CallBefore.ExecutionOrder: " + index;
                    error.Component = null;
                    error.MethodMethodInfo = methodInfo;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there are more than one methods with the same name inside a BDD Component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckDuplicateStepMethodsForASingleComponent(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            Dictionary<string, MethodInfo> dictionary = new Dictionary<string, MethodInfo>();
            MethodInfo[] methods = component.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                List<UnityTestBDDError> partialResult = this.CheckDuplicateStepMethodsForASingleComponentAndSingleBDDDeclarationType(component, dictionary, method, typeof(BDDMethodBaseAttribute));
                result.AddRange(partialResult);
            }

            return result;
        }

        /// <summary>
        /// Checks the type of the duplicate step methods for a single component and single BDD declaration.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="method">The method.</param>
        /// <param name="stepType">Type of the step.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckDuplicateStepMethodsForASingleComponentAndSingleBDDDeclarationType(Component component, Dictionary<string, MethodInfo> dictionary, MethodInfo method, Type stepType)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            object[] attributes = method.GetCustomAttributes(stepType, true);
            if (attributes.Length > 0)
            {
                string key = stepType.Name + "." + method.Name;
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, method);
                }
                else
                {
                    UnityTestBDDError error = new UnityTestBDDError();
                    error.Message = "There are more than one BDD Methods with the name " + component.GetType().Name + "." + method.Name + " You can have only one method with the same name.";
                    error.Component = component;
                    error.MethodMethodInfo = method;
                    error.LockRunnerInspectorOnErrors = true;
                    error.ShowButton = true;
                    error.ShowRedExclamationMark = true;
                    error.StepType = null;
                    error.Index = 0;
                    error.LockBuildParameters = true;
                    error.LockParametersRows = true;
                    result.Add(error);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks errors on the Parameters Values Storage fields definitions.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckValuesParametersStorage(Component component)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            if (typeof(DynamicBDDComponent).IsAssignableFrom(component.GetType()))
            {
                Dictionary<Type, FieldInfo> dictionary = new Dictionary<Type, FieldInfo>();
                FieldInfo[] fieldsInfo = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (FieldInfo fieldInfo in fieldsInfo)
                {
                    object[] valuesArrayStorageList = fieldInfo.GetCustomAttributes(typeof(ParametersValuesStorage), true);
                    if (valuesArrayStorageList.Length > 1)
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The field " + component.GetType().Name + "." + fieldInfo.Name + " has more than one ParametersValuesStorage definitions.";
                        error.Component = component;
                        error.MethodMethodInfo = null;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;

                        result.Add(error);
                    }

                    if (valuesArrayStorageList.Length == 1)
                    {
                        if (!fieldInfo.FieldType.IsArray)
                        {
                            UnityTestBDDError error = new UnityTestBDDError();
                            error.Message = "The field " + component.GetType().Name + "." + fieldInfo.Name + " is not an array.";
                            error.Component = component;
                            error.MethodMethodInfo = null;
                            error.LockRunnerInspectorOnErrors = true;
                            error.ShowButton = true;
                            error.ShowRedExclamationMark = true;
                            error.StepType = null;
                            error.Index = 0;
                            error.LockBuildParameters = true;
                            error.LockParametersRows = true;
                            result.Add(error);
                        }
                        else
                        {
                            if (dictionary.ContainsKey(fieldInfo.FieldType.GetElementType()))
                            {
                                UnityTestBDDError error = new UnityTestBDDError();
                                error.Message = "The component " + component.GetType().Name + " has more than one ParametersValuesStorage for the type " + fieldInfo.FieldType.GetElementType();
                                error.Component = component;
                                error.MethodMethodInfo = null;
                                error.LockRunnerInspectorOnErrors = true;
                                error.ShowButton = true;
                                error.ShowRedExclamationMark = true;
                                error.StepType = null;
                                error.Index = 0;
                                error.LockBuildParameters = true;
                                error.LockParametersRows = true;

                                result.Add(error);
                            }
                            else
                            {
                                dictionary.Add(fieldInfo.FieldType.GetElementType(), fieldInfo);
                            }

                            if (fieldInfo.IsPrivate)
                            {
                                object[] serializedFieldAttributes = fieldInfo.GetCustomAttributes(typeof(SerializeField), true);
                                if (serializedFieldAttributes.Length == 0)
                                {
                                    UnityTestBDDError error = new UnityTestBDDError();
                                    error.Message = "The field " + component.GetType().Name + "." + fieldInfo.Name + " is private but it hasn't the [SerializedField] attribute. The inspector is not going to manage it.";
                                    error.Component = component;
                                    error.MethodMethodInfo = null;
                                    error.LockRunnerInspectorOnErrors = true;
                                    error.ShowButton = true;
                                    error.ShowRedExclamationMark = true;
                                    error.StepType = null;
                                    error.Index = 0;
                                    error.LockBuildParameters = true;
                                    error.LockParametersRows = true;
                                    result.Add(error);
                                }
                            }
                        }
                    }
                }

                MethodInfo[] methods = component.GetType().GetMethods();
                foreach (MethodInfo method in methods)
                {
                    object[] customAttributes = method.GetCustomAttributes(typeof(BDDMethodBaseAttribute), true);
                    if (customAttributes.Length > 0)
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        foreach (ParameterInfo parameter in parameters)
                        {
                            if (!dictionary.ContainsKey(parameter.ParameterType))
                            {
                                UnityTestBDDError error = new UnityTestBDDError();
                                error.Message = "There is not ParametersValuesStorage for the type " + parameter.ParameterType.Name + " for the parameter " + parameter.Name + " for the method " + component.GetType().Name + "." + method.Name;
                                error.Component = component;
                                error.MethodMethodInfo = null;
                                error.LockRunnerInspectorOnErrors = true;
                                error.ShowButton = true;
                                error.ShowRedExclamationMark = true;
                                error.StepType = null;
                                error.Index = 0;
                                error.LockBuildParameters = true;
                                error.LockParametersRows = true;
                                result.Add(error);
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if there are recursive calls inside a <see cref="CallBefore"/> chain.
        /// </summary>
        /// <param name="methodToInspect">The method to inspect.</param>
        /// <param name="callChain">The call chain.</param>
        /// <returns>A list of <see cref="UnityTestBDDError"/> objects. Each element describes an error found. If the list is empty, there are no errors. The list cannot be null.</returns>
        private List<UnityTestBDDError> CheckRecursiveCalls(MethodInfo methodToInspect, CallChain callChain)
        {
            List<UnityTestBDDError> result = new List<UnityTestBDDError>();
            object[] callBeforeObjects = methodToInspect.GetCustomAttributes(typeof(CallBefore), true);
            foreach (object callBeforeObject in callBeforeObjects)
            {
                CallBefore callBefore = callBeforeObject as CallBefore;
                MethodInfo callBeforeMethod = methodToInspect.DeclaringType.GetMethod(callBefore.Method);
                if (callBeforeMethod != null)
                {
                    CallChain newCallChain = new CallChain();
                    newCallChain.Method = methodToInspect;
                    newCallChain.CallBeforeDeclaration = callBefore;
                    newCallChain.CallingChain = callChain;
                    if (callBeforeMethod.Equals(callChain.Root().Method))
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "The method " + callBeforeMethod.Name + " has a recursive call. Recursive calls are not allowed.\n Call chain:\n" + newCallChain.GetCallChainText();
                        error.Component = null;
                        error.MethodMethodInfo = callBeforeMethod;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = true;
                        error.LockBuildParameters = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        result.Add(error);
                    }
                    else
                    {
                        List<UnityTestBDDError> partialResult = this.CheckRecursiveCalls(callBeforeMethod, newCallChain);
                        result.AddRange(partialResult);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This class describes the chain of methods defined by the <see cref="CallBefore"/> attributes.
        /// </summary>
        private class CallChain
        {
            /// <summary>
            /// The current method.
            /// </summary>
            private MethodInfo method;

            /// <summary>
            /// The call before declaration for the current method.
            /// </summary>
            private CallBefore callBeforeDeclaration;

            /// <summary>
            /// The CallChain object that precedes the current method.
            /// </summary>
            private CallChain callingChain;

            /// <summary>
            /// Gets or sets CallChain object that precedes the current method.
            /// </summary>
            /// <value>
            /// The CallChain object that precedes the current method.
            /// </value>
            public CallChain CallingChain
            {
                get
                {
                    return this.callingChain;
                }

                set
                {
                    this.callingChain = value;
                }
            }

            /// <summary>
            /// Gets or sets the call before declaration for the current method.
            /// </summary>
            /// <value>
            /// The call before declaration for the current method.
            /// </value>
            public CallBefore CallBeforeDeclaration
            {
                get
                {
                    return this.callBeforeDeclaration;
                }

                set
                {
                    this.callBeforeDeclaration = value;
                }
            }

            /// <summary>
            /// Gets or sets the current method.
            /// </summary>
            /// <value>
            /// The current method.
            /// </value>
            public MethodInfo Method
            {
                get
                {
                    return this.method;
                }

                set
                {
                    this.method = value;
                }
            }

            /// <summary>
            /// This method return the root CallChain object.
            /// </summary>
            /// <returns>The root CallChain object.</returns>
            internal CallChain Root()
            {
                if (this.CallingChain != null)
                {
                    return this.CallingChain.Root();
                }
                else
                {
                    return this;
                }
            }

            /// <summary>
            /// Gets the text describing the call chain.
            /// </summary>
            /// <returns>The text describing the call chain.</returns>
            internal string GetCallChainText()
            {
                if (this.CallingChain == null)
                {
                    return string.Empty;
                }
                else
                {
                    return this.CallingChain.GetCallChainText() + "\n" + this.Method.Name + " " + this.CallBeforeDeclaration.ToString();
                }
            }
        }
    }
}