using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasPedalWritter : MonoBehaviour
{
    public string CarName;
    private CarWritter car;

    public Image pedal;
    public Sprite idlePedal;
    public Sprite pressedPedal;

    void Start()
    {
        car = GameObject.Find(CarName).gameObject.GetComponent<CarWritter>();
    }

    void Update()
    {
        if (car.Location.GasPedal)
            pedal.sprite = pressedPedal;
        else
            pedal.sprite = idlePedal;
    }
}
