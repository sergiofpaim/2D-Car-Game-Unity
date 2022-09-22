using Assets.Logic.CarLocation;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class TachometerMarkWritter : MonoBehaviour
{
    [SerializeField] private Text rpmCounter;
    [SerializeField] private Text gearCounter;

    private const int MAX_GAP = 80;
    private const float DEGREES_PER_RPM = -30;
    private const int INITIAL_ROTATION_ANGLE = 50;

    private float lastRPM = 0;

    private CarWritter car;
    public string CarName;

    private void Start()
    {
        car = GameObject.Find(CarName).gameObject.GetComponent<CarWritter>();
    }

    void Update()
    {
        if (Math.Abs(lastRPM - car.Location.PowerTrain.RPM) >= MAX_GAP)
        {
            rpmCounter.text = Math.Abs(Math.Round(car.Location.PowerTrain.RPM / 1000, 0)).ToString();
            gearCounter.text = car.Location.PowerTrain.CurrentGear;
            transform.rotation = RotationFrom(car.Location);
        }
    }

    private Quaternion RotationFrom(CarLocation location)
    {
        var rpmToRender = INITIAL_ROTATION_ANGLE + (location.PowerTrain.RPM / 1000 * DEGREES_PER_RPM);
        lastRPM = location.PowerTrain.RPM;

        return Quaternion.Euler(0, 0, rpmToRender);
    }
}
