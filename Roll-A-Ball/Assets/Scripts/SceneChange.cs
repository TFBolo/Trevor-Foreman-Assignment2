using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChange : MonoBehaviour
{
    public static bool oversizedBall = false;
    public static float speed = 10;
    public static int colorChoice = 0;

    public TMP_Dropdown colorPicker;
    public Slider speedSlider;
    public TextMeshProUGUI speedText;

    public void Start()
    {
        oversizedBall = false;
        colorChoice = 0;
        speed = 10;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchBallSize()
    {
        oversizedBall = !oversizedBall;
    }

    public void SpeedChange()
    {
        speedText.text = "Ball Speed: " + speedSlider.value;
        speed = speedSlider.value;
    }

    public void ChangeColor()
    {
        colorChoice = colorPicker.value;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
