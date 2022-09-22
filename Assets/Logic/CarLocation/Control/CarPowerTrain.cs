using System.Collections.Generic;
using UnityEngine;

namespace Assets.Logic.CarLocation
{
    public class CarPowerTrain
    {
        //Engine parameters
        public static readonly int MAX_RPM = 6000;
        public static readonly int IDLE_RPM = 800;
        private const int MIN_RPM = 3500;
        private const float BREAK_FACTOR = 5F;
        private const float IDLE_FACTOR = 0.8F;
        private const float IDLE_PAUSE_IN_MS = 350F;

        //Transmition parameters 
        private const int NEUTRAL_GEAR = 1;
        private const int MAX_GEAR = 6;
        private const float TRANSMITION_RATIO = 0.0005F;
        private const float WHELL_CIRCUMFERENCE_IN_METERS = 2.8F;

        private static readonly List<Gear> gears = new()
        {
            new() { Code = "R", Ratio = -0.75F, RPMVariation = 2000 },
            new() { Code = "N", Ratio = 0.0F, RPMVariation = 0 },
            new() { Code = "1", Ratio = 0.65F, RPMVariation = 2700 },
            new() { Code = "2", Ratio = 1.1F, RPMVariation = 1900 },
            new() { Code = "3", Ratio = 1.5F, RPMVariation = 1200 },
            new() { Code = "4", Ratio = 2F, RPMVariation = 700 },
            new() { Code = "5", Ratio = 2.8F, RPMVariation = 500 }
        };
        
        //For the formula
        public static float MaxSpeed => MAX_RPM
                * gears[MAX_GEAR].Ratio
                * TRANSMITION_RATIO
                * WHELL_CIRCUMFERENCE_IN_METERS;

        private float SpeedFromTransmition => RPM
                * gears[currentGear].Ratio
                * TRANSMITION_RATIO
                * WHELL_CIRCUMFERENCE_IN_METERS;

        private int currentGear = NEUTRAL_GEAR;
        public string CurrentGear => gears[currentGear].Code;
        public float RPM { get; internal set; } = IDLE_RPM;

        private float idlePauseInMs = 0;

        public float Accelerate()
        {
            if (IsInNeutralPause())
                return 0;

            if (currentGear == NEUTRAL_GEAR)
                GearUp();
            else if (currentGear < NEUTRAL_GEAR)
            {
                RPM -= gears[currentGear].RPMVariation * BREAK_FACTOR * Time.deltaTime;
                if (RPM < IDLE_RPM)
                { 
                    PutInNeutralPause();
                    GearUp();
                }
            }
            else
            {
                RPM += gears[currentGear].RPMVariation * Time.deltaTime;
                if (RPM > MAX_RPM)
                    GearUp();
            }
            return SpeedFromTransmition;
        }

        public float Break()
        {
            if (IsInNeutralPause())
                return 0;

            if (currentGear == NEUTRAL_GEAR)
                GearDown();
            else if (currentGear < NEUTRAL_GEAR) 
            {
                RPM += gears[currentGear].RPMVariation * Time.deltaTime;
                if (RPM > MAX_RPM)
                    RPM = MAX_RPM;
            }
            else 
            {
                RPM -= gears[currentGear].RPMVariation * BREAK_FACTOR  * Time.deltaTime;
                if (currentGear > NEUTRAL_GEAR + 1 && RPM < MIN_RPM)
                    GearDown();
                else if (currentGear == NEUTRAL_GEAR + 1 && RPM < IDLE_RPM)
                {
                    GearDown();
                    PutInNeutralPause();
                }
            }
            return SpeedFromTransmition;
        }

        private bool IsInNeutralPause()
        {
            if (idlePauseInMs > 0)
            {
                idlePauseInMs -= Time.deltaTime * 1000;
                return true;
            }
            else 
                return false;
        }

        private void PutInNeutralPause()
        {
            RPM = IDLE_RPM;
            idlePauseInMs = IDLE_PAUSE_IN_MS;
        }

        public float Idle()
        {
            if (IsInNeutralPause())
                return 0;

            if (currentGear < NEUTRAL_GEAR)
            {
                RPM -= gears[currentGear].RPMVariation * IDLE_FACTOR * Time.deltaTime;
                if (RPM <= IDLE_RPM)
                {
                    RPM = IDLE_RPM;
                    GearUp();
                }
            }
            else if (currentGear > NEUTRAL_GEAR)
            {
                RPM -= gears[currentGear].RPMVariation * IDLE_FACTOR * Time.deltaTime;
                if (currentGear > NEUTRAL_GEAR + 1 && RPM < MIN_RPM)
                    GearDown();
                else if (currentGear == NEUTRAL_GEAR + 1 && RPM < IDLE_RPM)
                {
                    GearDown();
                    RPM = IDLE_RPM;
                }
            }
            return SpeedFromTransmition;
        }

        private void GearUp()
        {
            if (currentGear < gears.Count - 1)
            {
                if (currentGear >= NEUTRAL_GEAR)
                {
                    var oldRatio = gears[currentGear].Ratio;
                    currentGear++;
                    RPM = RPM * oldRatio / gears[currentGear].Ratio;
                }
                else if (currentGear < NEUTRAL_GEAR) 
                {
                    currentGear++;                
                }
            }
            else if (RPM > MAX_RPM)
                RPM = MAX_RPM;
        } 

        private void GearDown()
        {
            if (currentGear > 0)
            {
                if (currentGear >= NEUTRAL_GEAR)
                {
                    var oldRatio = gears[currentGear].Ratio;
                    currentGear--;
                    RPM = RPM * oldRatio / gears[currentGear].Ratio;
                }
            }
        }  
    }       
}