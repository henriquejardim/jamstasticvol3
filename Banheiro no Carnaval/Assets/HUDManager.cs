using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour {


    public string timerFormat = "00.000";

    public GameObject panel;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI meters;
    public TextMeshProUGUI beerCount;
    public TextMeshProUGUI beerTimer;
    public Slider pipiBar;

    public void Start() {
        Reset();
    }

    public void Reset() {
        beerTimer.text = string.Empty;
    }

    public void SetTimer(float value) {
        timer.text = Formatter.FloatToTime(value, timerFormat);
    }

    public void SetPipiBar(float value) {
        pipiBar.value = value/100;
    }

    public void SetMeter(float value) {
        meters.text = value.ToString() + " M";
    }

    public void SetBeerCount(int value) {
        beerCount.text = "x " + value;
    }

    public void SetBeerTimer(float value) {
        if (value <= 0f) {
            beerTimer.text = string.Empty;
            return;
        }

        beerTimer.text = Formatter.FloatToTime(value, "00.000");
    }
 

   
}
