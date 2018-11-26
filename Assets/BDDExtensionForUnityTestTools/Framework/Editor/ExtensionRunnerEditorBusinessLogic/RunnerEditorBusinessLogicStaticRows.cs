//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicStaticRows.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic for drawing the rows for a Static scenario.
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
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic for drawing the rows for a Static scenario.
    /// </summary>
    public class RunnerEditorBusinessLogicStaticRows
    {
        /// <summary>
        /// Draws the static rows.
        /// </summary>
        /// <typeparam name="T">The Step Methods type.</typeparam>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="stepMethodsLoader">The step methods loader.</param>
        /// <param name="bddComponents">The BDD components.</param>
        /// <param name="labelWidthAbsolute">The label width absolute.</param>
        /// <param name="buttonsWidthAbsolute">The buttons width absolute.</param>
        public void DrawStaticRows<T>(IUnityInterfaceWrapper unityInterface, MethodsLoader stepMethodsLoader, Component[] bddComponents, float labelWidthAbsolute, float buttonsWidthAbsolute) where T : IGivenWhenThenDeclaration
        {
            List<BaseMethodDescription> methodsList = stepMethodsLoader.LoadStepMethods<T>(bddComponents);
            for (int index = 0; index < methodsList.Count; index++)
            {
                unityInterface.EditorGUILayoutBeginHorizontal();
                float rowWidth = unityInterface.EditorGUIUtilityCurrentViewWidth() - labelWidthAbsolute - buttonsWidthAbsolute;
                float textSize = rowWidth - 20;
                string label = StepMethodUtilities.GetStepMethodName<T>();
                if (index > 0)
                {
                    label = "and";
                }

                unityInterface.EditorGUILayoutLabelField(label, labelWidthAbsolute);
                string description = string.Empty;

                description = methodsList[index].Text;
                unityInterface.EditorGUILayoutLabelField(description, textSize);
                this.DrawCogButton(unityInterface, methodsList[index]);
                unityInterface.EditorGUILayoutEndHorizontal();
            }
        }

        /// <summary>
        /// Draws the cog button.
        /// </summary>
        /// <param name="unityInterface">The unity interface.</param>
        /// <param name="methodDescription">The method description.</param>
        internal void DrawCogButton(IUnityInterfaceWrapper unityInterface, BaseMethodDescription methodDescription)
        {
            string texture = @"Assets\UnityTestToolsBDDExtension\Resources\Sprites\cog.png";
            Texture2D inputTexture = unityInterface.AssetDatabaseLoadAssetAtPath(texture, typeof(Texture2D));
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
    }
}
