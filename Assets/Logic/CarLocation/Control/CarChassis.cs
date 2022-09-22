using System;
using UnityEngine;

namespace Assets.Logic.CarLocation
{
    public class CarChassis
    {
        private const int MIN_WHEEL_BASE = 1;
        private const int MAX_WHEEL_BASE = 10;
        private const int MAX_STEERING_ANGLE = 30;

        private const int MIN_EFFECTIVE_STEERING_PERCENTAGE = 15;

        private float speed = 0;
        private float steeringAngle = 0;
        private readonly float wheelBase = 0;

        public float SteeringAngle
        {
            get => steeringAngle;
            set
            {
                if (Math.Abs(value) <= MAX_STEERING_ANGLE)
                    steeringAngle = value;
            }
        }
        public float Speed 
        { 
            get => speed; 
            set => speed = value;
        }
        public float WheelBase { get => wheelBase; }
        
        public float UnderSteeredAngle => SteeringAngle * (MIN_EFFECTIVE_STEERING_PERCENTAGE
                                                            + (100 
                                                               - MIN_EFFECTIVE_STEERING_PERCENTAGE
                                                               - (Math.Abs(Speed) 
                                                                  / CarPowerTrain.MaxSpeed 
                                                                  * (100 - MIN_EFFECTIVE_STEERING_PERCENTAGE)))) / 100;

        public float RotationAngle
        {
            get
            {
                float displacement = Speed * Time.deltaTime;

                float X = (float) (wheelBase + Math.Cos(UnderSteeredAngle * Math.PI / 180) * displacement);
                float Y = (float) (Math.Sin(UnderSteeredAngle * Math.PI / 180) * displacement);

                return (float) (Math.Atan2(Y, X) / Math.PI * 180);
            }
        }

        public CarChassis(float wheelBase)
        {
            if (wheelBase < MIN_WHEEL_BASE || wheelBase > MAX_WHEEL_BASE)
                throw new Exception($"The car's WheelBase {nameof(wheelBase)} must be between {MIN_WHEEL_BASE} and {MAX_WHEEL_BASE}");
            this.wheelBase = wheelBase;
        }
    }
}
