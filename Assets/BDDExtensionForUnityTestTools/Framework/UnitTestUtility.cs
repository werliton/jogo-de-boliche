//-----------------------------------------------------------------------
// <copyright file="UnitTestUtility.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains a collection of methods used during the unit test.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains a collection of methods used during the unit test.
    /// </summary>
    public static class UnitTestUtility
    {
        /// <summary>
        /// Contains the list of gameObjects created from the last DestroyTemporaryTestGameObjects() call.
        /// </summary>
        private static List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Creates a component inside a new gameObject.
        /// </summary>
        /// <typeparam name="T">The type of the Component.</typeparam>
        /// <returns>The requested <see cref="Component"/>.</returns>
        public static T CreateComponent<T>() where T : MonoBehaviour
        {
            GameObject gameObject = new GameObject();
            T component = gameObject.AddComponent<T>();
            gameObjects.Add(gameObject);
            return component;
        }

        /// <summary>
        /// Creates a game object.
        /// </summary>
        /// <returns>The requested gameObject.</returns>
        public static GameObject CreateGameObject()
        {
            GameObject gameObject = new GameObject();
            gameObjects.Add(gameObject);
            return gameObject;
        }

        /// <summary>
        /// Destroys all the objects stored inside the gameObjects list.
        /// </summary>
        public static void DestroyTemporaryTestGameObjects()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                GameObject.DestroyImmediate(gameObject);
            }

            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Creates a component inside the given gameObject.
        /// </summary>
        /// <typeparam name="T">The type of the Component.</typeparam>
        /// <param name="gameObject">The game object.</param>
        /// <returns>The requested Component.</returns>
        public static T CreateComponent<T>(GameObject gameObject) where T : MonoBehaviour
        {
            T component = gameObject.AddComponent<T>();
            return component;
        }
    }
}