using System.Collections.Generic;
using System.Linq;

namespace EngineAbstractor
{
    public enum ControlType
    {
        A,
        B
    }

    public enum InputValue
    {
        Foward,
        Backward,
        Right,
        Left
    }

    public class ControlInput
    {
        private readonly Dictionary<(ControlType, InputValue), bool> inputs;
        public IReadOnlyList<InputValue> Inputs(ControlType controlType) => inputs.Where(i => i.Value &&
                                                                                              i.Key.Item1 == controlType)
                                                                                  .Select(i => i.Key.Item2)
                                                                                  .ToList();
        public ControlInput()
        {
            inputs = new()
            {
                { (ControlType.A, InputValue.Foward), false },
                { (ControlType.A, InputValue.Backward), false },
                { (ControlType.A, InputValue.Right), false  },
                { (ControlType.A, InputValue.Left), false },
                { (ControlType.B, InputValue.Foward), false },
                { (ControlType.B, InputValue.Backward), false },
                { (ControlType.B, InputValue.Right), false  },
                { (ControlType.B, InputValue.Left), false }
            };
        }
        public void SetInput(ControlType controlType, InputValue inputValue, bool value)
        {
            inputs[(controlType, inputValue)] = value;
        }
    }
}
