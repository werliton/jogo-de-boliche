//-----------------------------------------------------------------------
// <copyright file="BaseBDDComponentEditorBusinessLogic.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic for drawing the BDD Component custom inspector.
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
    /// This class contains the business logic for drawing the BDD Component custom inspector.
    /// </summary>
    public class BaseBDDComponentEditorBusinessLogic
    {
        /// <summary>
        /// The component.
        /// </summary>
        private Component component;

        /// <summary>
        /// The filename of the texture for the openComponent button.
        /// </summary>
        private string openComponentButtonTextureFileName = @"openComponentButton.png";

        /// <summary>
        /// The filename of the error texture.
        /// </summary>
        private string errorTextureFileName = @"exclamation_red.png";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBDDComponentEditorBusinessLogic"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public BaseBDDComponentEditorBusinessLogic(Component component)
        {
            this.component = component;
        }

        /// <summary>
        /// Gets or sets the filename of the error texture.
        /// </summary>
        /// <value>
        /// The filename of the error texture.
        /// </value>
        public string ErrorTextureFileName
        {
            get
            {
                return this.errorTextureFileName;
            }

            set
            {
                this.errorTextureFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets filename of the texture for the openComponent button.
        /// </summary>
        /// <value>
        /// The filename of the texture for the openComponent button.
        /// </value>
        public string OpenComponentButtonTextureFileName
        {
            get
            {
                return this.openComponentButtonTextureFileName;
            }

            set
            {
                this.openComponentButtonTextureFileName = value;
            }
        }

        /// <summary>
        /// Draws the given errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="unityInterface">The unity interface.</param>
        public void Errors(List<UnityTestBDDError> errors, IUnityInterfaceWrapper unityInterface)
        {
            BDDExtensionRunner bddExtensionRunner = this.component.gameObject.GetComponent<BDDExtensionRunner>();
            string openComponentButtonTextureFullPath = Utilities.GetAssetFullPath(bddExtensionRunner, this.OpenComponentButtonTextureFileName);
            string errorTextureFullPath = Utilities.GetAssetFullPath(bddExtensionRunner, this.ErrorTextureFileName);
            foreach (UnityTestBDDError error in errors)
            {
                unityInterface.EditorGUILayoutBeginHorizontal();
                unityInterface.EditorGUILayoutSeparator();
                unityInterface.EditorGUILayoutSeparator();
                unityInterface.EditorGUILayoutEndHorizontal();
                unityInterface.EditorGUILayoutBeginHorizontal();
                float currentViewWidth = unityInterface.EditorGUIUtilityCurrentViewWidth();
                Texture2D errorTexture = unityInterface.AssetDatabaseLoadAssetAtPath(errorTextureFullPath, typeof(Texture2D));
                GUILayoutOption[] errorTextureOptions = new GUILayoutOption[2] { unityInterface.GUILayoutWidth(24), unityInterface.GUILayoutHeight(24) };
                unityInterface.EditorGUILayoutLabelField(errorTexture, errorTextureOptions);
                float labelWidth = currentViewWidth - 100;
                unityInterface.EditorGUILayoutLabelField(error.Message, labelWidth);

                Texture2D openComponentButtonTexture = unityInterface.AssetDatabaseLoadAssetAtPath(openComponentButtonTextureFullPath, typeof(Texture2D));
                GUILayoutOption[] options = new GUILayoutOption[2] { unityInterface.GUILayoutWidth(24), unityInterface.GUILayoutHeight(24) };
                if (unityInterface.GUILayoutButton(openComponentButtonTexture, EditorStyles.label, options))
                {
                    if (error.MethodMethodInfo != null)
                    {
                        SourcesManagement.OpenMethodSourceCode(error.MethodMethodInfo, unityInterface);
                    }
                    else if (error.Component != null)
                    {
                        SourcesManagement.OpenSourceCode(error.Component, unityInterface);
                    }
                    else
                    {
                        MethodInfo[] methods = this.component.GetType().GetMethods();
                        foreach (MethodInfo method in methods)
                        {
                            if (method.DeclaringType.Name.Equals(this.component.GetType().Name))
                            {
                                SourcesManagement.OpenSourceCode(method, unityInterface);
                            }
                        }
                    }
                }

                unityInterface.EditorGUILayoutEndHorizontal();
            }
        }
    }
}
