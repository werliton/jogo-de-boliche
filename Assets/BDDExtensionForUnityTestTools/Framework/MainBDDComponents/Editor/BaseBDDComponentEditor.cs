//-----------------------------------------------------------------------
// <copyright file="BaseBDDComponentEditor.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the custom inspector for the BDD Components.
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
using UnityEditor;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the custom inspector for the BDD Components.
    /// </summary>
    /// <seealso cref="UnityEditor.Editor" />
    [CustomEditor(typeof(BaseBDDComponent), true)]
    public class BaseBDDComponentEditor : Editor
    {
        /// <summary>
        /// The path of the BDD Component image.
        /// </summary>
        private string texturePath = null;

        /// <summary>
        /// The unity interface.
        /// </summary>
        private IUnityInterfaceWrapper unityInterface = new UnityInterfaceWrapper();

        /// <summary>
        /// Gets or sets the path of the BDD Component image.
        /// </summary>
        /// <value>
        /// The path of the BDD Component image.
        /// </value>
        internal string MainTexturePath
        {
            get
            {
                return this.texturePath;
            }

            set
            {
                this.texturePath = value;
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        public void Awake()
        {
            BaseBDDComponent script = (BaseBDDComponent)target;
            script.Checking = true;
        }

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            BDDExtensionRunner bddExtensionRunner = ((Component)target).gameObject.GetComponent<BDDExtensionRunner>();
            string openComponentButtonTextureFullPath = Utilities.GetAssetFullPath(bddExtensionRunner, this.MainTexturePath);

            BaseBDDComponent script = (BaseBDDComponent)target;
            if (EditorApplication.isCompiling)
            {
                script.Checking = true;
            }

            BDDExtensionRunner runner = script.gameObject.GetComponent<BDDExtensionRunner>();
            if (runner != null)
            {
                this.unityInterface.EditorGUILayoutBeginHorizontal();
                Texture2D texture = this.unityInterface.AssetDatabaseLoadAssetAtPath(openComponentButtonTextureFullPath, typeof(Texture2D));
                GUILayoutOption[] options = new GUILayoutOption[1] { this.unityInterface.GUILayoutHeight(70) };
                GUIContent label = new GUIContent(texture);
                EditorGUILayout.LabelField(label, options);
                this.unityInterface.EditorGUILayoutEndHorizontal();
                ComponentsChecker checkForErrors = new ComponentsChecker();
                script.Errors = checkForErrors.Check(script);

                if (script.Errors.Count > 0)
                {
                    BaseBDDComponentEditorBusinessLogic businessLogic = new BaseBDDComponentEditorBusinessLogic(script);
                    businessLogic.Errors(script.Errors, this.unityInterface);
                }
                else
                {
                    this.DrawDefaultInspector();
                }

                script.Checking = false;
            }
            else
            {
                this.DrawDefaultInspector();
            }
        }
    }
}
