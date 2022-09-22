using EngineAbstractor;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
public class CarKeyboardReader : MonoBehaviour
{
    private static readonly ControlInput controlInput = new();

    public static IReadOnlyList<InputValue> Inputs(ControlType controlType) => controlInput.Inputs(controlType);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            controlInput.SetInput(ControlType.A, InputValue.Left, true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            controlInput.SetInput(ControlType.A, InputValue.Left, false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            controlInput.SetInput(ControlType.A, InputValue.Right, true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            controlInput.SetInput(ControlType.A, InputValue.Right, false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            controlInput.SetInput(ControlType.A, InputValue.Backward, true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            controlInput.SetInput(ControlType.A, InputValue.Backward, false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            controlInput.SetInput(ControlType.A, InputValue.Foward, true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            controlInput.SetInput(ControlType.A, InputValue.Foward, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Left, true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Left, false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Right, true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Right, false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Backward, true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Backward, false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Foward, true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            controlInput.SetInput(ControlType.B, InputValue.Foward, false);
        }
    }
}
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
