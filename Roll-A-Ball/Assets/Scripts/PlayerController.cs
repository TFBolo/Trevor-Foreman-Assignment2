using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject pauseScreen;
    public Material playerColor;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        if (SceneChange.oversizedBall)
        {
            transform.position = new Vector3(0, 1, 0);
            transform.localScale = new Vector3(2, 2, 2);
        }

        speed = SceneChange.speed;

        MaterialColorChange();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 16)
        {
            winTextObject.SetActive(true);
            StartCoroutine(GoToEnd());
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        
    }

    public void OnPause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else if (isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
        isPaused = !isPaused;
    }
    public void ReturnMenu()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MaterialColorChange()
    {
        if (SceneChange.colorChoice == 0)
        {
            playerColor.SetColor("_Color", Color.gray);
        }
        else if (SceneChange.colorChoice == 1)
        {
            playerColor.SetColor("_Color", Color.green);
        }
        else if (SceneChange.colorChoice == 2)
        {
            playerColor.SetColor("_Color", Color.blue);
        }
        else if (SceneChange.colorChoice == 3)
        {
            playerColor.SetColor("_Color", Color.red);
        }
    }

    IEnumerator GoToEnd()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
