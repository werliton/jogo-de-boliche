//-----------------------------------------------------------------------
// <copyright file="BDDExtensionRunnerEditor.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class is the Custom Editor for the BDDExtensionRunner.
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
using UnityEditor;
using UnityEngine;
using UnityTest;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class is the Custom Editor for the BDDExtensionRunner.
    /// </summary>
    /// <seealso cref="UnityEditor.Editor" />
    [CustomEditor(typeof(BDDExtensionRunner), true)]
    public class BDDExtensionRunnerEditor : Editor
    {
        /// <summary>
        /// The business logic data.
        /// </summary>
        private RunnerEditorBusinessLogicData runnerBusinessLogicData = new RunnerEditorBusinessLogicData();

        /// <summary>
        /// The unity interface wrapper.
        /// </summary>
        private IUnityInterfaceWrapper unityIntefaceWrapper = new UnityInterfaceWrapper();

        /// <summary>
        /// The parameters rebuild  business logic.
        /// </summary>
        private RunnerEditorBusinessLogicParametersRebuild businessLogicParametersRebuild = new RunnerEditorBusinessLogicParametersRebuild();

        /// <summary>
        /// The dynamic rows  business logic.
        /// </summary>
        private RunnerEditorBusinessLogicDynamicRows businessLogicDynamicRows = new RunnerEditorBusinessLogicDynamicRows();

        /// <summary>
        /// The static rows business logic.
        /// </summary>
        private RunnerEditorBusinessLogicStaticRows businessLogicStaticRows = new RunnerEditorBusinessLogicStaticRows();

        /// <summary>
        /// This field is true if the Inspector has to be redrawn.
        /// </summary>
        private bool dirtyStatus = false;

        /// <summary>
        /// Creates the menu for creating a new BDD test.
        /// </summary>
        [MenuItem("Unity Test Tools/BDD Extension Framework/Create BDD Test")]
        public static void CreateNewBDDTest()
        {
            GameObject test = TestComponent.CreateTest();
            test.AddComponent<BDDExtensionRunner>();
            SetSucceedOnAssertions(test);
        }

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            BDDExtensionRunner script = (BDDExtensionRunner)target;
            serializedObject.Update();
            Component[] components = script.gameObject.GetComponents<Component>();
            List<UnityTestBDDError> errors = new List<UnityTestBDDError>();
            ComponentsFilter bddComponentsFilter = new ComponentsFilter();
            Component[] bddComponents = bddComponentsFilter.Filter(components);
            ComponentsChecker checkForComponentsErrors = new ComponentsChecker();
            errors.AddRange(checkForComponentsErrors.Check(bddComponents));

            if (!this.RunnerInspectorIsLockedOnErrors(errors) && bddComponents.Length > 0)
            {
                foreach (Component component in bddComponents)
                {
                    if (((BaseBDDComponent)component).Errors.Count > 0)
                    {
                        UnityTestBDDError error = new UnityTestBDDError();
                        error.Message = "There are some errors in the BDDComponents. Please, check and resolve them before continue.";
                        error.MethodMethodInfo = null;
                        error.Component = null;
                        error.LockRunnerInspectorOnErrors = true;
                        error.ShowButton = false;
                        error.Index = 0;
                        error.LockBuildParameters = true;
                        error.LockParametersRows = true;
                        error.ShowRedExclamationMark = true;
                        error.StepType = null;
                        errors.Add(error);
                        break;
                    }
                }
            }

            if (!this.RunnerInspectorIsLockedOnErrors(errors) && !this.IsStaticScenario(components))
            {
                ChosenMethodsChecker checkForErrors = new ChosenMethodsChecker();
                errors.AddRange(checkForErrors.Check(script.Given, script.GivenParametersIndex, script.When, script.WhenParametersIndex, script.Then, script.ThenParametersIndex, bddComponents));
            }

            RunnerEditorBusinessLogicErrorsManagement runnerEditorBusinessLogicErrorsManagement = new RunnerEditorBusinessLogicErrorsManagement();
            runnerEditorBusinessLogicErrorsManagement.Errors(errors, this.unityIntefaceWrapper, script);

            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            bool isStaticScenario = methodsManagementUtilities.IsStaticBDDScenario(bddComponents);
            SetSucceedOnAssertions(script.gameObject);
            if (!this.RunnerInspectorIsLockedOnErrors(errors))
            {
                this.DrawOptions(this.runnerBusinessLogicData, isStaticScenario, script, this.unityIntefaceWrapper, bddComponents);

                if (!isStaticScenario)
                {
                    if (!this.BuildParametersIsLocked(errors))
                    {
                        bool isParametersRebuildNeeded = this.businessLogicParametersRebuild.IsParametersRebuildNeeded(this.unityIntefaceWrapper, this.runnerBusinessLogicData, bddComponents, bddComponentsFilter);
                        if (isParametersRebuildNeeded)
                        {
                            this.RebuildParameters(script, bddComponents, this.runnerBusinessLogicData);
                            this.runnerBusinessLogicData.BDDObjects = bddComponents;
                            this.runnerBusinessLogicData.SerializedObjects = this.businessLogicParametersRebuild.RebuildSerializedObjectsList(bddComponents, this.runnerBusinessLogicData.SerializedObjects);
                        }
                    }
                }

                if (Event.current.type == EventType.Layout || this.dirtyStatus == false)
                {
                    this.dirtyStatus = false;
                    if (this.runnerBusinessLogicData.SerializedObjects != null)
                    {
                        foreach (ISerializedObjectWrapper so in this.runnerBusinessLogicData.SerializedObjects.Values)
                        {
                            so.Update();
                        }
                    }

                    if (methodsManagementUtilities.IsStaticBDDScenario(bddComponents))
                    {
                        this.BuildStaticScenario(bddComponents);
                    }
                    else
                    {
                        this.BuildDynamicScenario(script, bddComponents, this.LockParametersRows(errors), out this.dirtyStatus);
                    }

                    serializedObject.ApplyModifiedProperties();
                    if (this.runnerBusinessLogicData.SerializedObjects != null)
                    {
                        foreach (ISerializedObjectWrapper so in this.runnerBusinessLogicData.SerializedObjects.Values)
                        {
                            so.ApplyModifiedProperties();
                        }
                    }
                }
                else
                {
                    this.unityIntefaceWrapper.EditorUtilitySetDirty(script);
                }
            }
        }

        /// <summary>
        /// Sets the succeedAfterAllAssertionsAreExecuted field on the <see cref="TestComponent"/>.
        /// </summary>
        /// <param name="testGameObject">The test game object.</param>
        private static void SetSucceedOnAssertions(GameObject testGameObject)
        {
            TestComponent testComponent = testGameObject.GetComponent<TestComponent>();
            testComponent.succeedAfterAllAssertionsAreExecuted = true;
        }

        /// <summary>
        /// Draws the options.
        /// </summary>
        /// <param name="businessLogicData">The business logic data.</param>
        /// <param name="isStaticScenario">If set to <c>true</c> [is static scenario].</param>
        /// <param name="script">The script.</param>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="bddComponents">The BDD components.</param>
        private void DrawOptions(RunnerEditorBusinessLogicData businessLogicData, bool isStaticScenario, BDDExtensionRunner script, IUnityInterfaceWrapper unityInterface, Component[] bddComponents)
        {
            Rect rect = unityInterface.EditorGUILayoutGetControlRect();
            businessLogicData.OptionsFoldout = unityInterface.EditorGUIFoldout(rect, businessLogicData.OptionsFoldout, "Options");
            if (businessLogicData.OptionsFoldout)
            {
                if (!isStaticScenario)
                {
                    this.ForceRebuildParametersButton(script, bddComponents);
                }

                unityInterface.EditorGUILayoutSeparator();
                this.ChooseBetweenUpdateAndFixedUpdate(script, this.unityIntefaceWrapper);
                float width = unityInterface.EditorGUIUtilityCurrentViewWidth();
                int numberOfSeparatorChars = (int)width / 7;
                string text = string.Empty.PadLeft(numberOfSeparatorChars, '_');

                unityInterface.EditorGUILayoutLabelFieldTruncate(text, width);
            }
        }

        /// <summary>
        /// Draws the checkbox for choosing between update and fixed update.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="unityInterface">The unity interface.</param>
        private void ChooseBetweenUpdateAndFixedUpdate(BDDExtensionRunner script, IUnityInterfaceWrapper unityInterface)
        {
            GUIContent label = unityInterface.GUIContent("Run under Fixed Update");
            bool result = GUILayout.Toggle(script.UseFixedUpdate, label, GUILayout.ExpandWidth(false));
            if (result != script.UseFixedUpdate)
            {
                Undo.RecordObject(script, "Change the use of Fixed Update.");
                script.UseFixedUpdate = result;
            }
        }

        /// <summary>
        /// Detects if the parameters rows have to be locked.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>True  if the parameters rows have to be locked.</returns>
        private bool LockParametersRows(List<UnityTestBDDError> errors)
        {
            foreach (UnityTestBDDError error in errors)
            {
                if (error.LockParametersRows)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Draws the button "Rebuild settings.".
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="components">The components.</param>
        private void ForceRebuildParametersButton(BDDExtensionRunner script, Component[] components)
        {
            if (GUILayout.Button("Rebuild settings.", EditorStyles.miniButton, GUILayout.Width(100)))
            {
                GenericMenu menu = new GenericMenu();
                GUIContent optionNotRebuild = new GUIContent("I am not sure. I will try to fix the errors instead.");
                GUIContent optionRebuild = new GUIContent("Rebuild! Every parameter with errors could be resetted and the values could be lost.");
                bool on = false;
                menu.AddItem(
                    optionNotRebuild,
                    on,
                    () =>
                    {
                    });

                menu.AddItem(
                    optionRebuild,
                    on,
                    () =>
                    {
                        RegisterUndoInformation(script, components, "Rebuld settings");
                        this.RebuildParameters(script, components, runnerBusinessLogicData);
                        runnerBusinessLogicData.BDDObjects = components;
                        runnerBusinessLogicData.SerializedObjects = businessLogicParametersRebuild.RebuildSerializedObjectsList(components, runnerBusinessLogicData.SerializedObjects);
                    });

                menu.ShowAsContext();
            }
        }

        /// <summary>
        /// Detects if the parameters rebuild has to be locked.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>True if the parameters rebuild has to be locked.</returns>
        private bool BuildParametersIsLocked(List<UnityTestBDDError> errors)
        {
            foreach (UnityTestBDDError error in errors)
            {
                if (error.LockBuildParameters)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the scenario is Static.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>
        ///   <c>true</c> if the scenario is Static; otherwise, <c>false</c>.
        /// </returns>
        private bool IsStaticScenario(Component[] components)
        {
            if (components == null)
            {
                return false;
            }

            foreach (Component component in components)
            {
                if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the Inspector has to be locked because of the presence of errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>True if the Inspector has to be locked.</returns>
        private bool RunnerInspectorIsLockedOnErrors(List<UnityTestBDDError> errors)
        {
            foreach (UnityTestBDDError error in errors)
            {
                if (error.LockRunnerInspectorOnErrors)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Rebuilds the parameters.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="dynamicBDDComponents">The dynamic BDD components.</param>
        /// <param name="runnerBusinessLogicData">The runner business logic data.</param>
        private void RebuildParameters(BDDExtensionRunner script, Component[] dynamicBDDComponents, RunnerEditorBusinessLogicData runnerBusinessLogicData)
        {
            // Generate the three list of MethodDescription for each step type: Given, When, Then
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();

            BaseMethodDescriptionBuilder methodBuilder = new BaseMethodDescriptionBuilder();
            IMethodsFilter givenMethodFilter = new MethodsFilterByMethodsFullNameList(script.Given);
            MethodsLoader givenMethodsLoader = new MethodsLoader(methodBuilder, givenMethodFilter);

            MethodDescriptionBuilder methodDescriptionBuilder = new MethodDescriptionBuilder();
            FullMethodDescriptionBuilder fullMethodDescriptionBuilder = new FullMethodDescriptionBuilder();
            MethodParametersLoader methodsParametersLoader = new MethodParametersLoader();

            List<MethodDescription> givenMethodsDescriptionList = methodsManagementUtilities.LoadMethodsDescriptionsFromChosenMethods<GivenBaseAttribute>(dynamicBDDComponents, givenMethodsLoader, methodDescriptionBuilder, methodsParametersLoader, script.Given, script.GivenParametersIndex);

            List<FullMethodDescription> givenFullMethodsDescriptionList = methodsManagementUtilities.LoadFullMethodsDescriptions<GivenBaseAttribute>(givenMethodsDescriptionList, fullMethodDescriptionBuilder);

            IMethodsFilter whenMethodFilter = new MethodsFilterByMethodsFullNameList(script.When);
            MethodsLoader whenMethodsLoader = new MethodsLoader(methodBuilder, whenMethodFilter);
            List<MethodDescription> whenMethodsDescriptionList = methodsManagementUtilities.LoadMethodsDescriptionsFromChosenMethods<WhenBaseAttribute>(dynamicBDDComponents, whenMethodsLoader, methodDescriptionBuilder, methodsParametersLoader, script.When, script.WhenParametersIndex);

            List<FullMethodDescription> whenFullMethodsDescriptionList = methodsManagementUtilities.LoadFullMethodsDescriptions<WhenBaseAttribute>(whenMethodsDescriptionList, fullMethodDescriptionBuilder);

            IMethodsFilter thenMethodFilter = new MethodsFilterByMethodsFullNameList(script.Then);
            MethodsLoader thenMethodsLoader = new MethodsLoader(methodBuilder, thenMethodFilter);
            List<MethodDescription> thenMethodsDescriptionList = methodsManagementUtilities.LoadMethodsDescriptionsFromChosenMethods<ThenBaseAttribute>(dynamicBDDComponents, thenMethodsLoader, methodDescriptionBuilder, methodsParametersLoader, script.Then, script.ThenParametersIndex);

            List<FullMethodDescription> thenFullMethodsDescriptionList = methodsManagementUtilities.LoadFullMethodsDescriptions<ThenBaseAttribute>(thenMethodsDescriptionList, fullMethodDescriptionBuilder);

            // Reset the valuesArrayStorages for each component
            ArrayStorageUtilities arrayStorageUtilities = new ArrayStorageUtilities();
            arrayStorageUtilities.ResetAllArrayStorage(dynamicBDDComponents);

            // Rebuild the parameters indexes and locations for each list of MethodDescription
            RunnerEditorBusinessLogicParametersLocationsBuilder parametersLocationsBuilder = new RunnerEditorBusinessLogicParametersLocationsBuilder();

            parametersLocationsBuilder.BuildParametersLocation(givenFullMethodsDescriptionList);
            parametersLocationsBuilder.BuildParametersLocation(whenFullMethodsDescriptionList);
            parametersLocationsBuilder.BuildParametersLocation(thenFullMethodsDescriptionList);

            // Rebuild the parameters Indexes arrays
            script.GivenParametersIndex = parametersLocationsBuilder.RebuildParametersIndexesArrays(givenFullMethodsDescriptionList, script.Given);
            script.WhenParametersIndex = parametersLocationsBuilder.RebuildParametersIndexesArrays(whenFullMethodsDescriptionList, script.When);
            script.ThenParametersIndex = parametersLocationsBuilder.RebuildParametersIndexesArrays(thenFullMethodsDescriptionList, script.Then);
        }

        /// <summary>
        /// Builds the static scenario.
        /// </summary>
        /// <param name="bddComponents">The BDD components.</param>
        private void BuildStaticScenario(Component[] bddComponents)
        {
            BaseMethodDescriptionBuilder baseMethodDescriptionBuilder = new BaseMethodDescriptionBuilder();
            MethodsFilterByExecutionOrder methodsFilterByExecutionOrder = new MethodsFilterByExecutionOrder();
            MethodsLoader bddStepMethodsLoader = new MethodsLoader(baseMethodDescriptionBuilder, methodsFilterByExecutionOrder);

            this.businessLogicStaticRows.DrawStaticRows<GivenBaseAttribute>(this.unityIntefaceWrapper, bddStepMethodsLoader, bddComponents, RunnerEditorBusinessLogicData.LabelWidthAbsolute, RunnerEditorBusinessLogicData.ButtonsWidthAbsolute);
            this.businessLogicStaticRows.DrawStaticRows<WhenBaseAttribute>(this.unityIntefaceWrapper, bddStepMethodsLoader, bddComponents, RunnerEditorBusinessLogicData.LabelWidthAbsolute, RunnerEditorBusinessLogicData.ButtonsWidthAbsolute);
            this.businessLogicStaticRows.DrawStaticRows<ThenBaseAttribute>(this.unityIntefaceWrapper, bddStepMethodsLoader, bddComponents, RunnerEditorBusinessLogicData.LabelWidthAbsolute, RunnerEditorBusinessLogicData.ButtonsWidthAbsolute);
        }

        /// <summary>
        /// Builds the dynamic scenario.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="bddComponents">The BDD components.</param>
        /// <param name="lockParametersRows">If set to <c>true</c> [lock parameters rows].</param>
        /// <param name="dirtyStatus">If set to <c>true</c> [dirty status].</param>
        private void BuildDynamicScenario(BDDExtensionRunner script, Component[] bddComponents, bool lockParametersRows, out bool dirtyStatus)
        {
            bool givenDirtyStatus = false;
            bool whenDirtyStatus = false;
            bool thenDirtyStatus = false;
            string undoText;
            MethodParametersLoader parametersLoader = new MethodParametersLoader();
            RunnerEditorBusinessLogicMethodsUtilities methodsUtilities = new RunnerEditorBusinessLogicMethodsUtilities();
            RunnerEditorBusinessLogicDynamicRowsElements dynamicRowsElements = new RunnerEditorBusinessLogicDynamicRowsElements();
            BaseMethodDescriptionBuilder baseMethodDescriptionBuilder = new BaseMethodDescriptionBuilder();
            MethodDescriptionBuilder methodDescriptionBuilder = new MethodDescriptionBuilder();
            IMethodsFilter methodFilter = new MethodsFilterByStepType();
            MethodsLoader methodsLoader = new MethodsLoader(baseMethodDescriptionBuilder, methodFilter);

            ChosenMethods chosenMethods = new ChosenMethods();

            chosenMethods.ChosenMethodsNames = script.Given;
            chosenMethods.ChosenMethodsParametersIndex = script.GivenParametersIndex;

            this.runnerBusinessLogicData.Rebuild = this.businessLogicDynamicRows.DrawDynamicRows<GivenBaseAttribute>(this.unityIntefaceWrapper, methodsLoader, methodDescriptionBuilder, parametersLoader, bddComponents, chosenMethods, this.runnerBusinessLogicData.GivenFoldouts, this.runnerBusinessLogicData.SerializedObjects, script, methodsUtilities, dynamicRowsElements, lockParametersRows, this.runnerBusinessLogicData.Rebuild, out chosenMethods, out this.runnerBusinessLogicData.GivenFoldouts, out givenDirtyStatus, out undoText);
            this.RegisterUndoInformation(this.target, bddComponents, undoText);
            script.Given = chosenMethods.ChosenMethodsNames;
            script.GivenParametersIndex = chosenMethods.ChosenMethodsParametersIndex;

            chosenMethods.ChosenMethodsNames = script.When;
            chosenMethods.ChosenMethodsParametersIndex = script.WhenParametersIndex;

            this.runnerBusinessLogicData.Rebuild = this.businessLogicDynamicRows.DrawDynamicRows<WhenBaseAttribute>(this.unityIntefaceWrapper, methodsLoader, methodDescriptionBuilder, parametersLoader, bddComponents, chosenMethods, this.runnerBusinessLogicData.WhenFoldouts, this.runnerBusinessLogicData.SerializedObjects, script, methodsUtilities, dynamicRowsElements, lockParametersRows, this.runnerBusinessLogicData.Rebuild, out chosenMethods, out this.runnerBusinessLogicData.WhenFoldouts, out whenDirtyStatus, out undoText);
            this.RegisterUndoInformation(this.target, bddComponents, undoText);

            script.When = chosenMethods.ChosenMethodsNames;
            script.WhenParametersIndex = chosenMethods.ChosenMethodsParametersIndex;

            chosenMethods.ChosenMethodsNames = script.Then;
            chosenMethods.ChosenMethodsParametersIndex = script.ThenParametersIndex;
            this.runnerBusinessLogicData.Rebuild = this.businessLogicDynamicRows.DrawDynamicRows<ThenBaseAttribute>(this.unityIntefaceWrapper, methodsLoader, methodDescriptionBuilder, parametersLoader, bddComponents, chosenMethods, this.runnerBusinessLogicData.ThenFoldouts, this.runnerBusinessLogicData.SerializedObjects, script, methodsUtilities, dynamicRowsElements, lockParametersRows, this.runnerBusinessLogicData.Rebuild, out chosenMethods, out this.runnerBusinessLogicData.ThenFoldouts, out thenDirtyStatus, out undoText);
            this.RegisterUndoInformation(this.target, bddComponents, undoText);

            script.Then = chosenMethods.ChosenMethodsNames;
            script.ThenParametersIndex = chosenMethods.ChosenMethodsParametersIndex;

            dirtyStatus = givenDirtyStatus || whenDirtyStatus || thenDirtyStatus;
        }

        /// <summary>
        /// Registers the undo information.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="bddComponents">The BDD components.</param>
        /// <param name="undoText">The undo text.</param>
        private void RegisterUndoInformation(UnityEngine.Object target, Component[] bddComponents, string undoText)
        {
            List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
            objects.Add(target);
            foreach (Component component in bddComponents)
            {
                UnityEngine.Object unityObject = (UnityEngine.Object)component;
                objects.Add(unityObject);
            }

            Undo.RecordObjects(objects.ToArray(), undoText);
        }
    }
}