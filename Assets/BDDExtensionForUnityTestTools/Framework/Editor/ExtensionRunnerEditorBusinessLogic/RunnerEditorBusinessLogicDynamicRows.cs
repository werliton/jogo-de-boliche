//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicDynamicRows.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic for drawing the rows inside the BDDExtensionRunner inspector for a Dynamic Scenario.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic for drawing the rows inside the BDDExtensionRunner inspector for a Dynamic Scenario.
    /// </summary>
    public class RunnerEditorBusinessLogicDynamicRows
    {
        /// <summary>
        /// Draws the dynamic rows.
        /// </summary>
        /// <typeparam name="T">The type of the Step Methods.</typeparam>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="methodsLoader">The methods loader.</param>
        /// <param name="methodDescriptionBuilder">The method description builder.</param>
        /// <param name="parametersLoader">The parameters loader.</param>
        /// <param name="bddComponents">The BDD components.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <param name="foldouts">The foldouts.</param>
        /// <param name="serializedObjects">The serialized objects.</param>
        /// <param name="target">The target.</param>
        /// <param name="methodsUtilities">The methods utilities.</param>
        /// <param name="dynamicRowsElements">The dynamic rows elements.</param>
        /// <param name="lockParametersRows">If set to <c>true</c> [lock parameters rows].</param>
        /// <param name="rebuild">If set to <c>true</c> [rebuild].</param>
        /// <param name="updatedChosenMethodsList">The updated chosen methods list.</param>
        /// <param name="updatedFoldouts">The updated foldouts.</param>
        /// <param name="dirtyStatus">If set to <c>true</c> [dirty status].</param>
        /// <param name="undoText">The undo text.</param>
        /// <returns>True if a rebuild of the parameters index is requested.</returns>
        public bool DrawDynamicRows<T>(
            IUnityInterfaceWrapper unityInterface,
            MethodsLoader methodsLoader,
            MethodDescriptionBuilder methodDescriptionBuilder,
            MethodParametersLoader parametersLoader,
            Component[] bddComponents,
            ChosenMethods chosenMethods,
            bool[] foldouts,
            Dictionary<Type, ISerializedObjectWrapper> serializedObjects,
            UnityEngine.Object target,
            RunnerEditorBusinessLogicMethodsUtilities methodsUtilities,
            RunnerEditorBusinessLogicDynamicRowsElements dynamicRowsElements,
            bool lockParametersRows,
            bool rebuild,
            out ChosenMethods updatedChosenMethodsList,
            out bool[] updatedFoldouts,
            out bool dirtyStatus,
            out string undoText) where T : IGivenWhenThenDeclaration
        {
            updatedChosenMethodsList = (ChosenMethods)chosenMethods.Clone();
            updatedFoldouts = new bool[foldouts.Length];
            Array.Copy(foldouts, updatedFoldouts, foldouts.Length);
            undoText = string.Empty;
            dirtyStatus = false;

            List<BaseMethodDescription> methodsList = methodsLoader.LoadStepMethods<T>(bddComponents);
            string[] methodsNames = methodsUtilities.GetMethodsNames(methodsList);

            FullMethodDescriptionBuilder fullMethodDescriptionBuilder = new FullMethodDescriptionBuilder();
            for (int index = 0; index < chosenMethods.ChosenMethodsNames.Length; index++)
            {
                MethodDescription methodDescription = methodsUtilities.GetMethodDescription(methodDescriptionBuilder, parametersLoader, chosenMethods, methodsList, index);

                methodsNames = methodsUtilities.CheckMissedMethod(chosenMethods, methodsNames, index, methodDescription);

                List<FullMethodDescription> fullMethodDescriptionsList = fullMethodDescriptionBuilder.Build(methodDescription, (uint)index + 1);

                unityInterface.EditorGUILayoutBeginHorizontal();
                dynamicRowsElements.DrawFoldoutSymbol(unityInterface, updatedFoldouts, index, fullMethodDescriptionsList);

                dynamicRowsElements.DrawLabel<T>(unityInterface, index);
                float textSize = (unityInterface.EditorGUIUtilityCurrentViewWidth() - RunnerEditorBusinessLogicData.LabelWidthAbsolute - RunnerEditorBusinessLogicData.ButtonsWidthAbsolute) * RunnerEditorBusinessLogicData.TextWidthPercent;
                dynamicRowsElements.DrawDescription(unityInterface, chosenMethods.ChosenMethodsNames[index], methodDescription, textSize);

                string newChosenMethod = dynamicRowsElements.DrawComboBox(unityInterface, chosenMethods.ChosenMethodsNames[index], methodsNames);
                rebuild = methodsUtilities.UpdateDataIfNewMethodIsChosen(newChosenMethod, updatedChosenMethodsList, updatedFoldouts, index, rebuild, out undoText);
                dirtyStatus = dirtyStatus || dynamicRowsElements.DrawAddRowButton(unityInterface, index, updatedChosenMethodsList, target, undoText, out updatedChosenMethodsList, out undoText);
                dirtyStatus = dirtyStatus || dynamicRowsElements.DrawRemoveRowButton(unityInterface, index, updatedChosenMethodsList, target, undoText, out updatedChosenMethodsList, out undoText);
                if (dirtyStatus)
                {
                    break;
                }

                dynamicRowsElements.DrawCogButton(unityInterface, methodDescription, (BDDExtensionRunner) target);
                unityInterface.EditorGUILayoutEndHorizontal();

                dynamicRowsElements.DrawParametersRows(unityInterface, foldouts[index], fullMethodDescriptionsList, serializedObjects, lockParametersRows);
            }

            return rebuild;
        }
    }
}