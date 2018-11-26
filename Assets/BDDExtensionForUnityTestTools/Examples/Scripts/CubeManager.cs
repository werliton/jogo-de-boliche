//-----------------------------------------------------------------------
// <copyright file="CubeManager.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Scrip for managing a cube creation using a "Create" and a "Delete" button.
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
using UnityEngine;

/// <summary>
/// Scrip for managing a cube creation using a "Create" and a "Delete" button.
/// </summary>
public class CubeManager : MonoBehaviour
{
    /// <summary>
    /// The Unity Tag associated to a cube object.
    /// </summary>
    private const string CubeTag = "CUBE";

    /// <summary>
    /// Gets or sets the GameObject for the Warning Message.
    /// </summary>
    /// <value>
    /// The warning message GameObject.
    /// </value>
    private const string WarningTag = "WARNING MESSAGE";

    /// <summary>
    /// The cube prefab.
    /// </summary>
    [SerializeField]
    private Transform cubePrefab;

    /// <summary>
    /// The warning message.
    /// </summary>
    [SerializeField]
    private GameObject warningMessage;

    /// <summary>
    /// Gets or sets the cube prefab.
    /// </summary>
    /// <value>
    /// The cube prefab.
    /// </value>
    public Transform CubePrefab
    {
        get
        {
            return this.cubePrefab;
        }

        set
        {
            this.cubePrefab = value;
        }
    }

    /// <summary>
    /// Gets or sets the warning message.
    /// </summary>
    /// <value>
    /// The warning message.
    /// </value>
    public GameObject WarningMessage
    {
        get
        {
            return this.warningMessage;
        }

        set
        {
            this.warningMessage = value;
        }
    }

    /// <summary>
    /// Called by the button "Create".
    /// </summary>
    public void OnCreate()
    {
        if (GameObject.FindWithTag(CubeTag) == null)
        {
            Transform cube = Instantiate(this.CubePrefab, new Vector3(600, 260, -450), Quaternion.identity) as Transform;
            cube.Rotate(-25, -45, 25, Space.Self);
            cube.parent = gameObject.transform;
            cube.name = "object for test";
            this.WarningMessage.SetActive(false);
        }
    }

    /// <summary>
    /// Called by the button "Delete".
    /// </summary>
    public void OnDelete()
    {
        GameObject cube = GameObject.FindWithTag(CubeTag);
        if (cube == null)
        {
            this.WarningMessage.SetActive(true);
        }

        GameObject.Destroy(cube);
    }
 
    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
    }
}
