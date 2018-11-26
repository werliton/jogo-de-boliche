//-----------------------------------------------------------------------
// <copyright file="CreationOfGameObjectBDD.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This BDD Component is part of the example of how to use a Dynamic Component for building a scenario.
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
using System.Linq;
using HudDimension.BDDExtensionForUnityTestTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This BDD Component is part of the example of how to use a Dynamic Component for building a scenario.
/// </summary>
public class CreationOfGameObjectBDD : DynamicBDDComponent
{
    /// <summary>
    /// The tag for the button "Create".
    /// </summary>
    private const string ButtonCreateTag = "BUTTON CREATE";

    /// <summary>
    /// The tag for the button "Delete".
    /// </summary>
    private const string ButtonDeleteTag = "BUTTON DELETE";

    /// <summary>
    /// The tag for the cube.
    /// </summary>
    private const string CubeTag = "CUBE";

    /// <summary>
    /// The name of the game object to create.
    /// </summary>
    private const string CubeName = "object for test";

    /// <summary>
    /// This array is going to contain all the instances of objects with the CubeTag tag.
    /// </summary>
    private GameObject[] listOfCubesInTheScene = null;

    /// <summary>
    /// The gameObject of the warning text field.
    /// </summary>
    [SerializeField]
    private GameObject warningTextObject;

    /// <summary>
    /// Gets or sets the gameObject of the warning text field.
    /// </summary>
    /// <value>
    /// The gameObject of the warning text field.
    /// </value>
    public GameObject WarningTextObject
    {
        get
        {
            return this.warningTextObject;
        }

        set
        {
            this.warningTextObject = value;
        }
    }

    /// <summary>
    /// This method checks when the software is waiting for input.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Given("the software is just started and it is waiting for an input")]
    public IAssertionResult StartedAndWaitingForInput()
    {
        IAssertionResult result = null;
        GameObject cube = GameObject.FindWithTag(CubeTag);
        GameObject buttonCreate = GameObject.FindWithTag(ButtonCreateTag);
        if (buttonCreate == null)
        {
            result = new AssertionResultRetry("Button Create not found");
        }
        else if (cube != null)
        {
            result = new AssertionResultFailed("There is an unexpected cube in the scene.");
        }
        else if (buttonCreate != null && buttonCreate.activeSelf)
        {
            result = new AssertionResultSuccessful();
        }

        return result;
    }

    /// <summary>
    /// This method performs the press action of the "Create" button.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [When("I press the button \"Create\"")]
    public IAssertionResult PressTheButtonCreate()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        GameObject buttonCreate = GameObject.FindWithTag(ButtonCreateTag);
        Button button = buttonCreate.GetComponent<Button>();
        button.onClick.Invoke();
        return result;
    }

    /// <summary>
    /// This method checks if an object named "object for test" is on the scene.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then("an object named \"object for test\" has to appear on the scene")]
    public IAssertionResult TheNewObjectAppears()
    {
        IAssertionResult result = null;
        GameObject cube = GameObject.FindWithTag(CubeTag);
        if (cube == null || !cube.name.Equals(CubeName))
        {
            result = new AssertionResultRetry("\"" + CubeName + "\" not found");
        }
        else if (cube != null && cube.activeSelf)
        {
            result = new AssertionResultSuccessful();
        }

        return result;
    }

    /// <summary>
    /// This method prepares the scene for the test using the methods used in a previous test.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Given("there is a cube in the scene called \"object for test\"")]
    [CallBefore(1, "StartedAndWaitingForInput")]
    [CallBefore(2, "PressTheButtonCreate")]
    [CallBefore(3, "TheNewObjectAppears")]
    public IAssertionResult ThereIsACubeInTheScene()
    {
        return new AssertionResultSuccessful();
    }

    /// <summary>
    /// This method performs the press action of the "Delete" button.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [When("I press the button \"Delete\"")]
    public IAssertionResult PressTheButtonDelete()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        GameObject buttonDelete = GameObject.FindWithTag(ButtonDeleteTag);
        Button button = buttonDelete.GetComponent<Button>();
        button.onClick.Invoke();
        return result;
    }

    /// <summary>
    /// This method checks if an object called "object for test" is present in the scene.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then("the software has to destroy the object named \"object for test\"")]
    public IAssertionResult TheCubeDisappears()
    {
        IAssertionResult result = null;
        GameObject cube = GameObject.FindWithTag(CubeTag);
        if (cube != null && cube.activeSelf)
        {
            result = new AssertionResultRetry("\"object for test\" found and Active");
        }
        else if (cube != null)
        {
            result = new AssertionResultRetry("\"object for test\" found inactive");
        }
        else
        {
            result = new AssertionResultSuccessful();
        }

        return result;
    }

    /// <summary>
    /// This method stores all the of cube objects in the scene.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [GenericBDDMethod]
    public IAssertionResult StoreTheListOfCubesInTheScene()
    {
        this.listOfCubesInTheScene = GameObject.FindGameObjectsWithTag(CubeTag);
        return new AssertionResultSuccessful();
    }

    /// <summary>
    /// This method prepares the environment for the test and stores all the cubes present in the scene.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Given("There is a cube in the scene called \"object for test\"")]
    [CallBefore(1, "StartedAndWaitingForInput")]
    [CallBefore(2, "PressTheButtonCreate")]
    [CallBefore(3, "TheNewObjectAppears")]
    [CallBefore(4, "StoreTheListOfCubesInTheScene")]
    public IAssertionResult ThereIsACubeInTheSceneAndStoreItForAFollowingUse()
    {
        return new AssertionResultSuccessful();
    }

    /// <summary>
    /// This method checks if there is only one cube in the scene.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then("nothing is going to change in the scene", Delay = 1000)]
    public IAssertionResult OnlyACubeInTheScene()
    {
        IAssertionResult result = null;
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(CubeTag);
        if (this.listOfCubesInTheScene.SequenceEqual(cubes) == false)
        {
            result = new AssertionResultFailed("The objects in the scene are changed!");
        }
        else
        {
            result = new AssertionResultSuccessful();
        }

        return result;
    }

    /// <summary>
    /// This method checks if the warning text field is present and visible the scene, showing the text passed by the <paramref name="expectedWarningText"/> parameter.
    /// </summary>
    /// <param name="expectedWarningText">The expected warning text.</param>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then("the warning message \"%expectedWarningText%\" has to appear on the scene", Delay = 1000)]
    public IAssertionResult WarningInTheScene(string expectedWarningText)
    {
        IAssertionResult result = null;

        if (this.WarningTextObject == null)
        {
            result = new AssertionResultRetry("No warning on the scene");
        }
        else if (!this.WarningTextObject.activeSelf)
        {
            result = new AssertionResultRetry("No visible warning on the scene");
        }
        else
        {
            Text text = this.WarningTextObject.GetComponent<Text>();
            if (text.text.Equals(expectedWarningText))
            {
                result = new AssertionResultSuccessful();
            }
            else
            {
                result = new AssertionResultFailed("The warning message has not the expected text: \"" + text.text + "\" instead of \"" + expectedWarningText);
            }
        }

        return result;
    }
}