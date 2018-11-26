//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicDynamicRowsElements.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic of each type of row that could be drawn for a Dynamic Scenario.
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
using UnityEditor;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic of each type of row that could be drawn for a Dynamic Scenario.
    /// </summary>
    public class RunnerEditorBusinessLogicDynamicRowsElements
    {
        /// <summary>
        /// The text to show if there is an empty Step Method row.
        /// </summary>
        public const string ChoseMethodFromComboBox = "### Choose a method from the combo box ###";

        /// <summary>
        /// Draws the foldout symbol.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="updatedFoldouts">The updated foldouts.</param>
        /// <param name="index">The index.</param>
        /// <param name="fullMethodDescriptionList">The full method description list.</param>
        public void DrawFoldoutSymbol(IUnityInterfaceWrapper unityInterface, bool[] updatedFoldouts, int index, List<FullMethodDescription> fullMethodDescriptionList)
        {
            bool thereAreParameters = false;
            foreach (FullMethodDescription fullMethodDescription in fullMethodDescriptionList)
            {
                if (fullMethodDescription.Parameters.Parameters.Length > 0)
                {
                    thereAreParameters = true;
                }
            }

            if (thereAreParameters)
            {
                Rect rect = unityInterface.EditorGUILayoutGetControlRect();
                updatedFoldouts[index] = unityInterface.EditorGUIFoldout(rect, updatedFoldouts[index], string.Empty);
            }
            else
            {
                Rect rect = unityInterface.EditorGUILayoutGetControlRect();
                unityInterface.EditorGUIFoldout(rect, false, string.Empty, EditorStyles.label);
            }
        }

        /// <summary>
        /// Draws the right label: "Given", "When", "Then" or "and".
        /// </summary>
        /// <typeparam name="T">The Step Method type.</typeparam>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="index">The index.</param>
        public void DrawLabel<T>(IUnityInterfaceWrapper unityInterface, int index) where T : IGivenWhenThenDeclaration
        {
            string label = StepMethodUtilities.GetStepMethodName<T>();
            if (index > 0)
            {
                label = "and";
            }

            unityInterface.EditorGUILayoutLabelField(label, RunnerEditorBusinessLogicData.LabelWidthAbsolute);
        }

        /// <summary>
        /// Draws the Step Method sentence.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="chosenMethod">The chosen method.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <param name="textSize">Size of the text.</param>
        public void DrawDescription(IUnityInterfaceWrapper unityInterface, string chosenMethod, MethodDescription methodDescription, float textSize)
        {
            string description = string.Empty;
            if (methodDescription != null)
            {
                description = methodDescription.GetDecodifiedText();
            }
            else if (chosenMethod.Equals(string.Empty))
            {
                description = ChoseMethodFromComboBox;
            }
            else
            {
                description = "### The method " + chosenMethod + " is missing ###";
            }

            unityInterface.EditorGUILayoutLabelField(description, textSize);
        }

        /// <summary>
        /// Draws the ComboBox for choosing the Step Method.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="selectedMethod">The selected method.</param>
        /// <param name="methodsArray">The methods array.</param>
        /// <returns>The chosen Step Method.</returns>
        public string DrawComboBox(IUnityInterfaceWrapper unityInterface, string selectedMethod, string[] methodsArray)
        {
            string[] methods = null;
            if (methodsArray.Length == 0)
            {
                methods = new string[1] { string.Empty };
            }
            else if (!methodsArray[0].Equals(string.Empty))
            {
                methods = new string[methodsArray.Length + 1];
                methods[0] = string.Empty;
                Array.Copy(methodsArray, 0, methods, 1, methodsArray.Length);
            }
            else
            {
                methods = new string[methodsArray.Length];
                Array.Copy(methodsArray, methods, methodsArray.Length);
            }

            string result = string.Empty;

            int index = this.GetIndexByValue(selectedMethod, methods);
            index = unityInterface.EditorGUILayoutPopup(index, methods);
            result = methods[index];
            return result;
        }

        /// <summary>
        /// Gets the corresponding index of the selected method.
        /// </summary>
        /// <param name="selectedMethod">The selected method.</param>
        /// <param name="methodsArray">The methods array.</param>
        /// <returns>The corresponding index of the selected method.</returns>
        public int GetIndexByValue(string selectedMethod, string[] methodsArray)
        {
            int result = 0;
            for (int index = 0; index < methodsArray.Length; index++)
            {
                if (methodsArray[index].Equals(selectedMethod))
                {
                    result = index;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Draws the parameters rows.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="fullMethodDescriptionsList">The full method descriptions list.</param>
        /// <param name="serializedObjects">The serialized objects.</param>
        /// <param name="lockParametersRows">If set to <c>true</c> [lock parameters rows].</param>
        public void DrawParametersRows(IUnityInterfaceWrapper unityInterface, bool foldout, List<FullMethodDescription> fullMethodDescriptionsList, Dictionary<Type, ISerializedObjectWrapper> serializedObjects, bool lockParametersRows)
        {
            if (fullMethodDescriptionsList != null && fullMethodDescriptionsList.Count > 0 && foldout/* && !lockParametersRows*/)
            {
                bool theCallBeforeMethodsHaveParameters = false;
                foreach (FullMethodDescription fullMethodDescription in fullMethodDescriptionsList)
                {
                    if (fullMethodDescription.Parameters.Parameters.Length > 0)
                    {
                        if (fullMethodDescription.MainMethod != null)
                        {
                            theCallBeforeMethodsHaveParameters = true;
                        }

                        if (theCallBeforeMethodsHaveParameters)
                        {
                            unityInterface.EditorGUILayoutSeparator();
                            unityInterface.EditorGUILayoutBeginHorizontal();
                            float currentViewWidth = unityInterface.EditorGUIUtilityCurrentViewWidth();
                            string text = this.GetHeaderTextForFullMethodDescription(fullMethodDescription);
                            unityInterface.EditorGUILayoutLabelFieldTruncate(text, currentViewWidth);
                            unityInterface.EditorGUILayoutEndHorizontal();
                        }

                        this.DrawParametersRows(unityInterface, foldout, fullMethodDescription, serializedObjects, lockParametersRows);
                    }
                }

                unityInterface.EditorGUILayoutSeparator();
                unityInterface.EditorGUILayoutSeparator();
            }
        }

        /// <summary>
        /// Draws the parameters rows.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="fullMethodDescription">The full method description.</param>
        /// <param name="serializedObjects">The serialized objects.</param>
        /// <param name="lockParametersRows">If set to <c>true</c> [lock parameters rows].</param>
        public void DrawParametersRows(IUnityInterfaceWrapper unityInterface, bool foldout, FullMethodDescription fullMethodDescription, Dictionary<Type, ISerializedObjectWrapper> serializedObjects, bool lockParametersRows)
        {
            if (fullMethodDescription != null && foldout && !lockParametersRows)
            {
                ISerializedObjectWrapper serializedObject = null;
                serializedObjects.TryGetValue(fullMethodDescription.ComponentObject.GetType(), out serializedObject);

                foreach (MethodParameter parameter in fullMethodDescription.Parameters.Parameters)
                {
                    string parameterName = parameter.ParameterInfoObject.Name;
                    ParameterLocation parameterLocation = parameter.ParameterLocation;

                    // "given.Array.data[0]"
                    string parameterLocationString = parameterLocation.ParameterArrayLocation.ArrayName + ".Array.data[" + parameterLocation.ParameterArrayLocation.ArrayIndex + "]";
                    GUIContent label = unityInterface.GUIContent(parameterName);
                    ISerializedPropertyWrapper property = serializedObject.FindProperty(parameterLocationString);
                    unityInterface.EditorGUILayoutPropertyField(property, label);
                }
            }

            if (fullMethodDescription != null && foldout && lockParametersRows)
            {
                float labelWidth = unityInterface.EditorGUIUtilityCurrentViewWidth();
                unityInterface.EditorGUILayoutLabelField("When there are some errors the parameters are protected to avoid data lost.", labelWidth);
            }
        }

        /// <summary>
        /// Draws the add row button.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="target">The target.</param>
        /// <param name="undoText">The undo text.</param>
        /// <param name="newChosenMethods">The new chosen methods.</param>
        /// <param name="newUndoText">The new undo text.</param>
        /// <returns>Returns true if the inspector needs to be redrawn.</returns>
        public bool DrawAddRowButton(IUnityInterfaceWrapper unityInterface, int currentIndex, ChosenMethods chosenMethods, UnityEngine.Object target, string undoText, out ChosenMethods newChosenMethods, out string newUndoText)
        {
            newUndoText = undoText;
            bool dirty = false;
            newChosenMethods = new ChosenMethods();
            newChosenMethods.ChosenMethodsNames = chosenMethods.ChosenMethodsNames;
            newChosenMethods.ChosenMethodsParametersIndex = chosenMethods.ChosenMethodsParametersIndex;

            if (unityInterface.GUILayoutButton("+", EditorStyles.miniButton, unityInterface.GUILayoutWidth(20)))
            {
                newUndoText = "Add Step Method row";
                string[] newArrayMethodsNames = new string[newChosenMethods.ChosenMethodsNames.Length + 1];
                string[] newArrayMethodsParametersIndex = new string[newChosenMethods.ChosenMethodsParametersIndex.Length + 1];
                int newIndex = 0;
                for (int tempIndex = 0; tempIndex < chosenMethods.ChosenMethodsNames.Length; tempIndex++)
                {
                    if (tempIndex == currentIndex + 1)
                    {
                        newArrayMethodsNames[newIndex] = string.Empty;
                        newArrayMethodsParametersIndex[newIndex] = string.Empty;
                        newIndex++;
                    }

                    newArrayMethodsNames[newIndex] = newChosenMethods.ChosenMethodsNames[tempIndex];
                    newArrayMethodsParametersIndex[newIndex] = newChosenMethods.ChosenMethodsParametersIndex[tempIndex];
                    newIndex++;
                }

                if (newArrayMethodsNames[newArrayMethodsNames.Length - 1] == null)
                {
                    newArrayMethodsNames[newArrayMethodsNames.Length - 1] = string.Empty;
                    newArrayMethodsParametersIndex[newArrayMethodsNames.Length - 1] = string.Empty;
                }

                newChosenMethods.ChosenMethodsNames = newArrayMethodsNames;
                newChosenMethods.ChosenMethodsParametersIndex = newArrayMethodsParametersIndex;
                unityInterface.EditorUtilitySetDirty(target);
                dirty = true;
            }

            return dirty;
        }

        /// <summary>
        /// Draws the remove row button.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="target">The target.</param>
        /// <param name="undoText">The undo text.</param>
        /// <param name="newChosenMethods">The new chosen methods.</param>
        /// <param name="newUndoText">The new undo text.</param>
        /// <returns>Returns true if the inspector needs to be redrawn.</returns>
        public bool DrawRemoveRowButton(IUnityInterfaceWrapper unityInterface, int currentIndex, ChosenMethods chosenMethods, UnityEngine.Object target, string undoText, out ChosenMethods newChosenMethods, out string newUndoText)
        {
            newUndoText = undoText;
            bool dirty = false;
            newChosenMethods = new ChosenMethods();
            newChosenMethods.ChosenMethodsNames = chosenMethods.ChosenMethodsNames;
            newChosenMethods.ChosenMethodsParametersIndex = chosenMethods.ChosenMethodsParametersIndex;
            if (unityInterface.GUILayoutButton("-", EditorStyles.miniButton, unityInterface.GUILayoutWidth(20)))
            {
                if (chosenMethods.ChosenMethodsNames.Length > 1)
                {
                    newUndoText = "Remove Step Method row";
                    string[] newArrayMethodsNames = new string[newChosenMethods.ChosenMethodsNames.Length - 1];
                    string[] newArrayMethodsParametersIndex = new string[newChosenMethods.ChosenMethodsParametersIndex.Length - 1];
                    int newIndex = 0;
                    for (int tempIndex = 0; tempIndex < chosenMethods.ChosenMethodsNames.Length; tempIndex++)
                    {
                        if (tempIndex == currentIndex)
                        {
                            tempIndex++;
                        }

                        if (tempIndex < chosenMethods.ChosenMethodsNames.Length)
                        {
                            newArrayMethodsNames[newIndex] = chosenMethods.ChosenMethodsNames[tempIndex];
                            newArrayMethodsParametersIndex[newIndex] = chosenMethods.ChosenMethodsParametersIndex[tempIndex];
                            newIndex++;
                        }
                    }

                    newChosenMethods.ChosenMethodsNames = newArrayMethodsNames;
                    newChosenMethods.ChosenMethodsParametersIndex = newArrayMethodsParametersIndex;
                    unityInterface.EditorUtilitySetDirty(target);
                    dirty = true;
                }
            }

            return dirty;
        }

        /// <summary>
        /// Draws the cog button.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="methodDescription">The method description.</param>
        /// <param name="bddExtensionRunner">The BDD extension runner.</param>
        internal void DrawCogButton(IUnityInterfaceWrapper unityInterface, MethodDescription methodDescription, BDDExtensionRunner bddExtensionRunner)
        {
            string cogTexture = @"cog.png";
            
            string cogTextureFullPath = Utilities.GetAssetFullPath(bddExtensionRunner, cogTexture);
            Texture2D inputTexture = unityInterface.AssetDatabaseLoadAssetAtPath(cogTextureFullPath, typeof(Texture2D));
            GUILayoutOption[] options = new GUILayoutOption[2] { unityInterface.GUILayoutWidth(16), unityInterface.GUILayoutHeight(16) };

            if (unityInterface.GUILayoutButton(inputTexture, EditorStyles.label, options))
            {
                GenericMenu menu = new GenericMenu();
                GUIContent content = new GUIContent("Open method source");
                bool on = false;
                MethodInfo method = null;
                if (methodDescription != null)
                {
                    on = true;
                    method = methodDescription.Method;
                    menu.AddItem(content, on, () => { SourcesManagement.OpenMethodSourceCode(method, unityInterface); });
                }
                else
                {
                    menu.AddDisabledItem(content);
                }

                menu.ShowAsContext();
            }
        }

        /// <summary>
        /// Gets the header text for the parameters area.
        /// </summary>
        /// <param name="fullMethodDescription">The full method description.</param>
        /// <returns>The header text for the parameters area.</returns>
        private string GetHeaderTextForFullMethodDescription(FullMethodDescription fullMethodDescription)
        {
            string result = "Method ";

            string methodName = fullMethodDescription.Method.Name;
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            string fullId = methodsManagementUtilities.GetMainFullId(fullMethodDescription.MainMethod) + fullMethodDescription.Id;
            result += methodName;
            if (!fullId.Equals(string.Empty))
            {
                result += " Id=" + fullId;
            }

            return result;
        }
    }
}
