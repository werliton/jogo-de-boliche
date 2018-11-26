//-----------------------------------------------------------------------
// <copyright file="CreationOfGameObjectBDDStaticFourthScenario.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This BDD Component is part of the example of how to use a Static Component for building a scenario.
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
using HudDimension.BDDExtensionForUnityTestTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This BDD Component is part of the example of how to use a Static Component for building a scenario.
/// </summary>
public class CreationOfGameObjectBDDStaticFourthScenario : StaticBDDComponent
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
    /// The expected warning text.
    /// </summary>
    private string expectedWarningText = "Warning! No Object to delete!";

    /// <summary>
    /// The warning text field gameObject.
    /// </summary>
    [SerializeField]
    private GameObject warningTextObject;

    /// <summary>
    /// Gets or sets warning text field gameObject.
    /// </summary>
    /// <value>
    /// The warning text field gameObject.
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
    /// Gets or sets the expected warning text.
    /// </summary>
    /// <value>
    /// The expected warning text.
    /// </value>
    public string ExpectedWarningText
    {
        get
        {
            return this.expectedWarningText;
        }

        set
        {
            this.expectedWarningText = value;
        }
    }

    /// <summary>
    /// This method checks when the software is waiting for input.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Given(1, "the software is just started and it is waiting for an input", Delay = 1000)]
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
    /// This method performs the press action of the "Delete" button.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [When(1, "I press the button \"Delete\"")]
    public IAssertionResult PressTheButtonDelete()
    {
        IAssertionResult result = new AssertionResultSuccessful();
        GameObject buttonDelete = GameObject.FindWithTag(ButtonDeleteTag);
        Button button = buttonDelete.GetComponent<Button>();
        button.onClick.Invoke();
        return result;
    }

    /// <summary>
    /// This method checks if the warning text field is present and visible the scene, showing the text stored inside the ExpectedWarningText property.
    /// </summary>
    /// <returns>One of the three <see cref="IAssertionResult"/> implementations: <see cref="AssertionResultSuccessful"/>, <see cref="AssertionResultFailed"/>, <see cref="AssertionResultRetry"/>.</returns>
    [Then(1, "the warning message \"Warning! No Object to delete!\" has to appear on the scene", Delay = 1000)]
    public IAssertionResult WarningInTheScene()
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
            if (text.text.Equals(this.ExpectedWarningText))
            {
                result = new AssertionResultSuccessful();
            }
            else
            {
                result = new AssertionResultFailed("The warning message has not the expected text: \"" + text.text + "\" instead of \"" + this.ExpectedWarningText);
            }
        }

        return result;
    }
}