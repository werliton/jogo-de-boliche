//-----------------------------------------------------------------------
// <copyright file="ExtensionRunnerBusinessLogic.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic of the BDDExtensionRunner component.
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
using UnityTest;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic of the BDDExtensionRunner component.
    /// </summary>
    public class ExtensionRunnerBusinessLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionRunnerBusinessLogic"/> class.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public ExtensionRunnerBusinessLogic(GameObject gameObject)
        {
            this.IntegrationTestGameObject = gameObject;
        }

        /// <summary>
        /// Gets or sets the list of <see cref="FullMethodDescription"/> objects.
        /// </summary>
        /// <value>
        /// The list of <see cref="FullMethodDescription"/> objects.
        /// </value>
        public List<FullMethodDescription> MethodsDescription { get; set; }

        /// <summary>
        /// Gets or sets the start time for the time count of delayed methods executions.
        /// </summary>
        /// <value>
        /// The start time for the time count of delayed methods executions.
        /// </value>
        public DateTime StartDelayTime { get; set; }

        /// <summary>
        /// Gets or sets the start time for the time count of the <see cref="AssertionResultRetry"/> timeout methods executions.
        /// </summary>
        /// <value>
        /// The start time for the time count of the <see cref="AssertionResultRetry"/> timeout methods executions.
        /// </value>
        public DateTime? StartTimoutTime { get; set; }

        /// <summary>
        /// Gets or sets the index of the method to run in the MethodsDescription.
        /// </summary>
        /// <value>
        /// The index of the method to run in the MethodsDescription.
        /// </value>
        public int IndexToRun { get; set; }

        /// <summary>
        /// Gets or sets the Integration Test gameObject.
        /// </summary>
        /// <value>
        /// The Integration Test gameObject.
        /// </value>
        public GameObject IntegrationTestGameObject { get; set; }

        /// <summary>
        /// Gets a value indicating whether there are errors for avoiding the execution of the test.
        /// </summary>
        /// <value>
        ///   <c>true</c> if there are errors; otherwise, <c>false</c>.
        /// </value>
        public bool AreThereErrors { get; internal set; }

        /// <summary>
        /// Gets the list of <see cref="FullMethodDescription"/> objects to run.
        /// </summary>
        /// <param name="allComponents">All components.</param>
        /// <param name="givenMethods">The given methods.</param>
        /// <param name="givenParameters">The given parameters.</param>
        /// <param name="whenMethods">The when methods.</param>
        /// <param name="whenParameters">The when parameters.</param>
        /// <param name="thenMethods">The then methods.</param>
        /// <param name="thenParameters">The then parameters.</param>
        /// <returns>The list of <see cref="FullMethodDescription"/> objects to run.</returns>
        public List<FullMethodDescription> GetAllMethodsDescriptions(
            Component[] allComponents,
            string[] givenMethods,
            string[] givenParameters,
            string[] whenMethods,
            string[] whenParameters,
            string[] thenMethods,
            string[] thenParameters)
        {
            List<FullMethodDescription> result = new List<FullMethodDescription>();
            ComponentsFilter componentsFilter = new ComponentsFilter();
            Component[] components = componentsFilter.Filter(allComponents);
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            if (methodsManagementUtilities.IsStaticBDDScenario(components))
            {
                result.AddRange(this.GetAllStaticFullMethodsDescriptions<GivenBaseAttribute>(components, methodsManagementUtilities));
                result.AddRange(this.GetAllStaticFullMethodsDescriptions<WhenBaseAttribute>(components, methodsManagementUtilities));
                result.AddRange(this.GetAllStaticFullMethodsDescriptions<ThenBaseAttribute>(components, methodsManagementUtilities));
            }
            else
            {
                result.AddRange(this.GetAllDynamicFullMethodsDescriptions<GivenBaseAttribute>(components, methodsManagementUtilities, givenMethods, givenParameters));
                result.AddRange(this.GetAllDynamicFullMethodsDescriptions<WhenBaseAttribute>(components, methodsManagementUtilities, whenMethods, whenParameters));
                result.AddRange(this.GetAllDynamicFullMethodsDescriptions<ThenBaseAttribute>(components, methodsManagementUtilities, thenMethods, thenParameters));
            }

            return result;
        }

        /// <summary>
        /// Gets list of <see cref="FullMethodDescription"/> objects inside a Static Component.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method.</typeparam>
        /// <param name="bddComponents">The BDD components.</param>
        /// <param name="methodsManagementUtilities">The methods management utilities.</param>
        /// <returns>The list of <see cref="FullMethodDescription"/> objects inside a Static Component.</returns>
        public List<FullMethodDescription> GetAllStaticFullMethodsDescriptions<T>(Component[] bddComponents, MethodsManagementUtilities methodsManagementUtilities) where T : IGivenWhenThenDeclaration
        {
            List<FullMethodDescription> result = null;
            BaseMethodDescriptionBuilder baseMethodDescriptionBuilder = new BaseMethodDescriptionBuilder();
            MethodsFilterByExecutionOrder methodsFilterByExecutionOrder = new MethodsFilterByExecutionOrder();
            MethodsLoader bddStepMethodsLoader = new MethodsLoader(baseMethodDescriptionBuilder, methodsFilterByExecutionOrder);
            List<BaseMethodDescription> methodsList = bddStepMethodsLoader.LoadStepMethods<T>(bddComponents);
            FullMethodDescriptionBuilder fullMethodDescriptionBuilder = new FullMethodDescriptionBuilder();
            result = methodsManagementUtilities.LoadFullMethodsDescriptions<T>(methodsList, fullMethodDescriptionBuilder);

            return result;
        }

        /// <summary>
        /// Runs a test cycle.
        /// </summary>
        /// <param name="businessLogic">The business logic.</param>
        /// <param name="methodsDescription">The methods description.</param>
        /// <param name="indexToRun">The index to run.</param>
        /// <returns>The index of the next method to run.</returns>
        public int RunCycle(ExtensionRunnerBusinessLogic businessLogic, List<FullMethodDescription> methodsDescription, int indexToRun)
        {
            int runningIndex = indexToRun;
            if (runningIndex == -1)
            {
                runningIndex++;
                businessLogic.StartDelayTime = businessLogic.DateTimeNow();
            }

            if (runningIndex < methodsDescription.Count)
            {
                bool performed = businessLogic.InvokeMethod(businessLogic, methodsDescription[runningIndex], businessLogic.IntegrationTestGameObject);
                if (performed)
                {
                    runningIndex++;
                    businessLogic.StartDelayTime = businessLogic.DateTimeNow();
                }
            }
            else
            {
                businessLogic.InvokeAssertionSuccessful(businessLogic.IntegrationTestGameObject);
            }

            return runningIndex;
        }

        /// <summary>
        /// Return the DateTime.Now. It is useful when mocking the class.
        /// </summary>
        /// <returns>DateTime.Now value.</returns>
        public virtual DateTime DateTimeNow()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Invokes the <paramref name="methodDescription"/> method.
        /// </summary>
        /// <param name="businessLogic">The business logic.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <param name="gameObject">The game object.</param>
        /// <returns>True if the method is executed or false if the method still has to be executed.</returns>
        public virtual bool InvokeMethod(ExtensionRunnerBusinessLogic businessLogic, FullMethodDescription methodDescription, GameObject gameObject)
        {
            bool performed = false;
            if (businessLogic.DateTimeNow().Subtract(businessLogic.StartDelayTime).TotalMilliseconds >= methodDescription.Delay)
            {
                if (businessLogic.StartTimoutTime == null)
                {
                    businessLogic.StartTimoutTime = businessLogic.DateTimeNow();
                }

                if (methodDescription.Method != null && !methodDescription.Method.Equals(string.Empty))
                {
                    MethodInfo method = methodDescription.Method;
                    Component component = methodDescription.ComponentObject;
                    object[] parameters = businessLogic.GetParametersValues(methodDescription);
                    IAssertionResult executionResult = null;

                    object executionResultObject = method.Invoke(component, parameters);
                    if (executionResultObject == null)
                    {
                        string errorText = "The Step Method return null.";
                        string scenarioText = businessLogic.GetScenarioTextForErrorInSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                        string bddMethodLocation = businessLogic.GetbddMethodLocationForSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                        businessLogic.InvokeAssertionFailed(errorText, scenarioText, bddMethodLocation, gameObject);
                        return true;
                    }

                    if (typeof(AssertionResultSuccessful).IsAssignableFrom(executionResultObject.GetType()) ||
                        typeof(AssertionResultFailed).IsAssignableFrom(executionResultObject.GetType()) ||
                        typeof(AssertionResultRetry).IsAssignableFrom(executionResultObject.GetType()))
                    {
                        executionResult = (IAssertionResult)executionResultObject;
                    }
                    else
                    {
                        string errorText = "The return value of the Step Method is not a valid IAssertionResult implementation.";
                        string scenarioText = businessLogic.GetScenarioTextForErrorInSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                        string bddMethodLocation = businessLogic.GetbddMethodLocationForSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                        businessLogic.InvokeAssertionFailed(errorText, scenarioText, bddMethodLocation, gameObject);
                        return true;
                    }

                    if (executionResult is AssertionResultSuccessful)
                    {
                        performed = true;
                    }
                    else if (executionResult is AssertionResultFailed)
                    {
                        string errorText = ((AssertionResultFailed)executionResult).Text;

                        string scenarioText = businessLogic.GetScenarioTextForErrorInSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                        string bddMethodLocation = businessLogic.GetbddMethodLocationForSpecificMethod(businessLogic.MethodsDescription, methodDescription);

                        businessLogic.InvokeAssertionFailed(errorText, scenarioText, bddMethodLocation, gameObject);
                        performed = true;
                    }
                    else if (executionResult is AssertionResultRetry)
                    {
                        if (businessLogic.DateTimeNow().Subtract(businessLogic.StartTimoutTime ?? DateTime.MaxValue).TotalMilliseconds >= methodDescription.TimeOut)
                        {
                            string errorText = ((AssertionResultRetry)executionResult).Text;

                            string scenarioText = businessLogic.GetScenarioTextForErrorInSpecificMethod(businessLogic.MethodsDescription, methodDescription);
                            string bddMethodLocation = businessLogic.GetbddMethodLocationForSpecificMethod(businessLogic.MethodsDescription, methodDescription);

                            businessLogic.InvokeAssertionFailed(errorText, scenarioText, bddMethodLocation, gameObject);

                            performed = true;
                        }

                        performed = false;
                    }
                }
                else
                {
                    performed = true;
                    businessLogic.StartTimoutTime = null;
                }
            }

            return performed;
        }

        /// <summary>
        /// Gets the parameters values for the method Invoke.
        /// </summary>
        /// <param name="methodDescription">The method description.</param>
        /// <returns>An array containing the ordered collection of the parameters values.</returns>
        public virtual object[] GetParametersValues(FullMethodDescription methodDescription)
        {
            List<object> parameters = new List<object>();
            foreach (MethodParameter parameter in methodDescription.Parameters.Parameters)
            {
                parameters.Add(parameter.Value);
            }

            return parameters.ToArray();
        }

        /// <summary>
        /// Invokes the successful assertion.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public void InvokeAssertionSuccessful(GameObject gameObject)
        {
            AssertionComponent.Create<AssertionSuccessful>(CheckMethod.Start, gameObject, "TestComponent.enabled");
        }

        /// <summary>
        /// Invokes the failed assertion.
        /// </summary>
        /// <param name="errorText">The error text.</param>
        /// <param name="scenarioText">The scenario text.</param>
        /// <param name="bddMethodLocation">The BDD method location.</param>
        /// <param name="gameObject">The game object.</param>
        public virtual void InvokeAssertionFailed(string errorText, string scenarioText, string bddMethodLocation, GameObject gameObject)
        {
            AssertionFailed assertion = AssertionComponent.Create<AssertionFailed>(CheckMethod.Start, gameObject, "TestComponent.enabled");
            assertion.ErrorText = errorText;
            assertion.ScenarioText = scenarioText;
            assertion.BDDMethodLocation = bddMethodLocation;
        }

        /// <summary>
        /// Gets the scenario text indicating the sentence that raises the error.
        /// </summary>
        /// <param name="methods">The methods.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <returns>The scenario text indicating the sentence that raised the error.</returns>
        public virtual string GetScenarioTextForErrorInSpecificMethod(List<FullMethodDescription> methods, FullMethodDescription methodDescription)
        {
            string result = string.Empty;
            Type previousStepType = null;
            bool nextMainMethodHasError = false;
            for (int index = 0; index < methods.Count; index++)
            {
                FullMethodDescription method = methods[index];
                if (method.MainMethod == null)
                {
                    Type currentStepType = method.StepType;
                    string label = this.GetLabel(previousStepType, currentStepType);
                    string partialString = string.Empty;
                    if (method.Equals(methodDescription) || nextMainMethodHasError)
                    {
                        partialString = "\n----------> &&&&& %%%";
                    }
                    else
                    {
                        partialString = "\n            &&&&& %%%";
                    }

                    result += partialString.Replace("&&&&&", label).Replace("%%%", method.GetDecodifiedText());
                    previousStepType = method.StepType;
                    nextMainMethodHasError = false;
                }
                else
                {
                    if (method.Equals(methodDescription))
                    {
                        nextMainMethodHasError = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get the BDD methods chain list indicating which method raised the error.
        /// </summary>
        /// <param name="methods">The methods.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <returns>The BDD methods chain list indicating which method raised the error.</returns>
        public virtual string GetbddMethodLocationForSpecificMethod(List<FullMethodDescription> methods, FullMethodDescription methodDescription)
        {
            string result = string.Empty;
            for (int index = 0; index < methods.Count; index++)
            {
                FullMethodDescription method = methods[index];
                string partialString = string.Empty;
                if (method.Equals(methodDescription))
                {
                    partialString = "\n---------->$ & %%%";
                }
                else
                {
                    partialString = "\n           $ & %%%";
                }

                string indenting = this.GetIndentingForMethod(method);
                string methodText = this.GetMethodText(method);
                string stepTypeText = this.GetStepTypeText(method);
                result += partialString.Replace("$", stepTypeText).Replace("&", indenting).Replace("%%%", methodText);
            }

            return result;
        }

        /// <summary>
        /// Sets the succeedAfterAllAssertionsAreExecuted field of the <see cref="TestComponent"/> to true.
        /// </summary>
        public void SetSucceedOnAssertions()
        {
            TestComponent testComponent = this.IntegrationTestGameObject.GetComponent<TestComponent>();
            testComponent.succeedAfterAllAssertionsAreExecuted = true;
        }

        /// <summary>
        /// Runs the errors check.
        /// </summary>
        /// <param name="allComponents">All components.</param>
        /// <param name="givenMethods">The given methods.</param>
        /// <param name="givenParameters">The given parameters.</param>
        /// <param name="whenMethods">The when methods.</param>
        /// <param name="whenParameters">The when parameters.</param>
        /// <param name="thenMethods">The then methods.</param>
        /// <param name="thenParameters">The then parameters.</param>
        /// <returns>True if there is at least one error, false otherwise.</returns>
        internal bool CheckForErrors(
            Component[] allComponents,
            string[] givenMethods,
            string[] givenParameters,
            string[] whenMethods,
            string[] whenParameters,
            string[] thenMethods,
            string[] thenParameters)
        {
            List<UnityTestBDDError> errors = new List<UnityTestBDDError>();
            ComponentsFilter bddComponentsFilter = new ComponentsFilter();
            Component[] bddComponents = bddComponentsFilter.Filter(allComponents);
            ComponentsChecker checkForComponentsErrors = new ComponentsChecker();
            errors.AddRange(checkForComponentsErrors.Check(bddComponents));

            if (bddComponents.Length > 0)
            {
                bool isStaticScenario = false;
                foreach (Component component in bddComponents)
                {
                    if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
                    {
                        isStaticScenario = true;
                    }
                }

                if (!isStaticScenario)
                {
                    ChosenMethodsChecker checkForChosenMethodsErrors = new ChosenMethodsChecker();
                    errors.AddRange(checkForChosenMethodsErrors.Check(givenMethods, givenParameters, whenMethods, whenParameters, thenMethods, thenParameters, bddComponents));
                }
            }

            if (errors.Count > 0)
            {
                string message = string.Empty;
                foreach (UnityTestBDDError error in errors)
                {
                    message += error.Message + "\n";
                }

                this.InvokeAssertionFailed("Errors detected in configuration. Please, fix them before run the test.\n" + message, null, null, this.IntegrationTestGameObject);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the list of <see cref="FullMethodDescription"/> objects to run for a Dynamic Scenario.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method to filter.</typeparam>
        /// <param name="components">The components.</param>
        /// <param name="methodsManagementUtilities">The methods management utilities.</param>
        /// <param name="methodsFullNamesList">The methods full names list.</param>
        /// <param name="methodsParametersList">The methods parameters list.</param>
        /// <returns>The list of <see cref="FullMethodDescription"/> objects to run for a Dynamic Scenario.</returns>
        private List<FullMethodDescription> GetAllDynamicFullMethodsDescriptions<T>(Component[] components, MethodsManagementUtilities methodsManagementUtilities, string[] methodsFullNamesList, string[] methodsParametersList) where T : IGivenWhenThenDeclaration
        {
            List<FullMethodDescription> result = null;
            BaseMethodDescriptionBuilder baseMethodDescriptionBuilder = new BaseMethodDescriptionBuilder();
            MethodsFilterByMethodsFullNameList methodsFilterByMethodsFullNameList = new MethodsFilterByMethodsFullNameList(methodsFullNamesList);
            MethodsLoader bddStepMethodsLoader = new MethodsLoader(baseMethodDescriptionBuilder, methodsFilterByMethodsFullNameList);

            MethodDescriptionBuilder methodDescriptionBuilder = new MethodDescriptionBuilder();
            MethodParametersLoader methodParametersLoader = new MethodParametersLoader();
            List<MethodDescription> methodsList = methodsManagementUtilities.LoadMethodsDescriptionsFromChosenMethods<T>(components, bddStepMethodsLoader, methodDescriptionBuilder, methodParametersLoader, methodsFullNamesList, methodsParametersList);

            FullMethodDescriptionBuilder fullMethodDescriptionBuilder = new FullMethodDescriptionBuilder();
            result = methodsManagementUtilities.LoadFullMethodsDescriptions<T>(methodsList, fullMethodDescriptionBuilder);

            return result;
        }

        /// <summary>
        /// Gets one of the labels "Given", "When", "Then", "and", according to the sentences sequence.
        /// </summary>
        /// <param name="previousStepType">Type of the previous step.</param>
        /// <param name="currentStepType">Type of the current step.</param>
        /// <returns>One of the labels "Given", "When", "Then", "and", according to the sentences sequence.</returns>
        private string GetLabel(Type previousStepType, Type currentStepType)
        {
            string result = null;
            if (previousStepType == null)
            {
                result = "Given";
            }
            else if (previousStepType.Equals(currentStepType))
            {
                result = "  and";
            }
            else if (previousStepType.Equals(typeof(GivenBaseAttribute)))
            {
                result = " when";
            }
            else
            {
                result = " then";
            }

            return result;
        }

        /// <summary>
        /// Gets the text describing the Type of the BDD Step.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The text describing the Type of the BDD Step.</returns>
        private string GetStepTypeText(FullMethodDescription method)
        {
            string result = null;
            if (method.StepType.Equals(typeof(GivenBaseAttribute)))
            {
                result = "[Given]";
            }
            else if (method.StepType.Equals(typeof(WhenBaseAttribute)))
            {
                result = "[ When]";
            }
            else
            {
                result = "[ Then]";
            }

            return result;
        }

        /// <summary>
        /// Gets the text describing for the method for log operations.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The text describing for the method for log operations.</returns>
        private string GetMethodText(FullMethodDescription method)
        {
            string result = method.GetFullName();
            result += " [Delay= " + method.Delay + " Timeout= " + method.TimeOut + "]";
            return result;
        }

        /// <summary>
        /// Gets the indenting for method for log operations.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The indenting for method for log operations.</returns>
        private string GetIndentingForMethod(FullMethodDescription method)
        {
            string result = string.Empty;
            int numberOfIndents = 0;
            FullMethodDescription mainMethod = method.MainMethod;
            while (mainMethod != null)
            {
                numberOfIndents++;
                mainMethod = mainMethod.MainMethod;
            }

            result = string.Empty.PadRight(numberOfIndents * 3, ' ');
            return result;
        }
    }
}