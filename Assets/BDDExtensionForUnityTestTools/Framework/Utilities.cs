//-----------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
// </copyright>
//
// <summary>
// This class contains some general utilities.
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

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains some general utilities.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Gets the full path of a file inside the BDDExtensionFramework asset.
        /// </summary>
        /// <param name="bddExtensionRunner">The BDD extension runner.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The full path of the file inside the BDDExtensionFramework asset. </returns>
        public static string GetAssetFullPath(BDDExtensionRunner bddExtensionRunner, string fileName)
        {
            string result = string.Empty;
            if (bddExtensionRunner != null)
            {
                MonoScript script = MonoScript.FromMonoBehaviour(bddExtensionRunner);
                string runnerFullPath = AssetDatabase.GetAssetPath(script);
                string runnerPath = runnerFullPath.Substring(0, runnerFullPath.Length - "BDDExtensionRunner.cs".Length - 1);
                result = runnerPath + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar + "Sprites" + System.IO.Path.DirectorySeparatorChar + fileName;
            }

            return result;
        }
    }
}
