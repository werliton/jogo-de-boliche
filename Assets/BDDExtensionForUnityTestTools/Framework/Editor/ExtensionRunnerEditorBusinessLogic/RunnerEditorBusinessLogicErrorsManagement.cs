//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicErrorsManagement.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic of the errors management.
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

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic of the errors management.
    /// </summary>
    public class RunnerEditorBusinessLogicErrorsManagement
    {
        /// <summary>
        /// The filename of the openComponentButtonTexture.
        /// </summary>
        private string openComponentButtonTextureFileName = @"openComponentButton.png";

        /// <summary>
        /// The filename of the errorTexture.
        /// </summary>
        private string errorTextureFileName = @"exclamation_red.png";

        /// <summary>
        /// Gets or sets the filename of the errorTexture.
        /// </summary>
        /// <value>
        /// The filename of the errorTexture.
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
        /// Gets or sets the filename of the openComponentButtonTexture.
        /// </summary>
        /// <value>
        /// The name of the filename of the openComponentButtonTexture.
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
        /// <param name="bddExtensionRunner">The BDD extension runner.</param>
        public void Errors(List<UnityTestBDDError> errors, IUnityInterfaceWrapper unityInterface, BDDExtensionRunner bddExtensionRunner)
        {
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
                if (error.ShowRedExclamationMark)
                {
                    Texture2D errorTexture = unityInterface.AssetDatabaseLoadAssetAtPath(errorTextureFullPath, typeof(Texture2D));
                    GUILayoutOption[] errorTextureOptions = new GUILayoutOption[2] { unityInterface.GUILayoutWidth(24), unityInterface.GUILayoutHeight(24) };
                    unityInterface.EditorGUILayoutLabelField(errorTexture, errorTextureOptions);
                }

                float labelWidth = currentViewWidth - 100;
                unityInterface.EditorGUILayoutLabelField(error.Message, labelWidth);
                
                Texture2D openComponentButtonTexture = unityInterface.AssetDatabaseLoadAssetAtPath(openComponentButtonTextureFullPath, typeof(Texture2D));
                GUILayoutOption[] options = new GUILayoutOption[2] { unityInterface.GUILayoutWidth(24), unityInterface.GUILayoutHeight(24) };
                if (error.ShowButton)
                {
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
                    }
                }

                unityInterface.EditorGUILayoutEndHorizontal();
            }
        }
    }
}
