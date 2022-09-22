using Assets.Logic.CarLocation;
using System;
using UnityEngine;

public class SteeringWheelWritter : MonoBehaviour
{
    private float steeringToRender = 0;

    private const int MAX_GAP = 1;

    private CarWritter car;
    public string CarName;

    private void Start()
    {
        car = GameObject.Find(CarName).gameObject.GetComponent<CarWritter>();
    }

    void Update()
    {
        if (Math.Abs(steeringToRender - car.Location.Chassis.SteeringAngle) > MAX_GAP)
            transform.rotation = RotationFrom(car.Location);
    }

    private Quaternion RotationFrom(CarLocation location)
    {
        steeringToRender = location.Chassis.SteeringAngle;

        return Quaternion.Euler(0, 0, location.SteeringAngle);
    }
}
