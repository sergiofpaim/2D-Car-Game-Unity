using Assets.Logic.CarLocation;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class SpeedMarkWritter : MonoBehaviour
{
    [SerializeField] private Text speedCounter;

    private const double MAX_GAP = 0.2;
    private readonly static float MPS_TO_KMH = 3.6F;
    private const float DEGREES_PER_KMH = -1.8F;

    private float lastSpeed = 0;

    private CarWritter car;
    public string CarName;

    private void Start()
    {
        car = GameObject.Find(CarName).gameObject.GetComponent<CarWritter>();
    }

    void Update()
    {
        if (Math.Abs(lastSpeed - car.Location.Chassis.Speed) >= MAX_GAP)
        {
            speedCounter.text = Math.Round(Math.Abs(car.Location.Chassis.Speed * MPS_TO_KMH)).ToString();
            transform.rotation = RotationFrom(car.Location);
        }
    }

    private Quaternion RotationFrom(CarLocation location)
    {
        lastSpeed = location.Chassis.Speed;

        return Quaternion.Euler(0, 0, Math.Abs(location.Chassis.Speed) * MPS_TO_KMH * DEGREES_PER_KMH);
    }
}
