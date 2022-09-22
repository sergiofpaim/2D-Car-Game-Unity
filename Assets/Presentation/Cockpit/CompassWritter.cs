using Assets.Logic.CarLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassWritter : MonoBehaviour
{
    [SerializeField] public Text compassText;

    private const int ZERO_DEGREE_POSITION = 48;
    private const int DISPLAY_SIZE = 25;
    private const int CHARS_PER_360 = 48;
    private const string DIRECTIONS = "N • • ¦ • • E • • ¦ • • S • • ¦ • • W • • ¦ • • N • • ¦ • • E • • ¦ • • S";
    private float directionToRender = -1;

    private CarWritter car;
    public string CarName;

    private void Start()
    {
        car = GameObject.Find(CarName).gameObject.GetComponent<CarWritter>();
    }

    void Update()
    {
        if (directionToRender != car.Location.DirectionInDegrees + 0.25)
        {
            directionToRender = car.Location.DirectionInDegrees;
            compassText.text = DIRECTIONS.Substring(ZERO_DEGREE_POSITION
                                                   - (int)Math.Ceiling(directionToRender
                                                                       * CHARS_PER_360
                                                                       / 360),
                                                   DISPLAY_SIZE);
        }
    }
}
