using EngineAbstractor;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Logic.CarLocation
{
    public class CarLocation
    {
        private Location location;

        private const float STEERING_INCREMENT_PER_SECOND = 150;
        private const float STEERING_IDLE = 100;
        private float steeringIdleCenter = STEERING_INCREMENT_PER_SECOND / 2000 * Time.deltaTime;

        public CarPowerTrain PowerTrain { get; private set; }
        public CarChassis Chassis { get; internal set; }

        public bool GasPedal { get; private set; } = false;
        public bool BreakPedal { get; private set; } = false;

        public float DirectionInDegrees { get => location.DirectionInDegrees; }

        public readonly ControlType controlType;

        public List<string> LastInputs { get; set; }

        public float SteeringAngle => Chassis.SteeringAngle;

        public CarLocation(string id, float wheelBase, Location firstLocation)
        {
            controlType = id.ToLower() == nameof(ControlType.A).ToLower() 
                                  ? ControlType.A 
                                  : ControlType.B;

            Chassis = new(wheelBase);
            PowerTrain = new();
            location = firstLocation;
        }

        internal Location NextPosition()
        {
            var inputs = CarKeyboardReader.Inputs(controlType);

            if (inputs.Contains(InputValue.Left) &&
                !inputs.Contains(InputValue.Right))
                Chassis.SteeringAngle += STEERING_INCREMENT_PER_SECOND * Time.deltaTime;
            else if (!inputs.Contains(InputValue.Left) &&
                     inputs.Contains(InputValue.Right))
                Chassis.SteeringAngle -= STEERING_INCREMENT_PER_SECOND * Time.deltaTime;
            else
            {
                if (Chassis.SteeringAngle < 0)
                    Chassis.SteeringAngle += STEERING_IDLE * Time.deltaTime;
                else if (Chassis.SteeringAngle > 0)
                    Chassis.SteeringAngle -= STEERING_IDLE * Time.deltaTime;
                if (steeringIdleCenter > Math.Abs(Chassis.SteeringAngle))
                    Chassis.SteeringAngle = 0;
            }

            GasPedal = inputs.Contains(InputValue.Foward);
            BreakPedal = inputs.Contains(InputValue.Backward);

            if (GasPedal && !BreakPedal)
                Chassis.Speed = PowerTrain.Accelerate();
            if (!GasPedal && BreakPedal)
                Chassis.Speed = PowerTrain.Break();

            else if (!GasPedal && !BreakPedal)
            {
                if (Chassis.Speed < 0)
                    Chassis.Speed = PowerTrain.Idle();
                else if (Chassis.Speed >= 0)
                    Chassis.Speed = PowerTrain.Idle();
            }

            float displacement = Chassis.Speed * Time.deltaTime;

            if (displacement == 0)
                return null;

            location.DirectionInDegrees += Chassis.RotationAngle;

            location.Point.X += (float)(Math.Cos(location.DirectionInRadians) * displacement);
            location.Point.Y += (float)(Math.Sin(location.DirectionInRadians) * displacement);

            return location;
        }
    }
}
