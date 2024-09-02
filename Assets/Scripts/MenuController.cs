using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas StartCanvas;
    [SerializeField] private Canvas GameCanvas;
    [SerializeField] private GameController gameController;
    void Start()
    {
        StartCanvas.GetComponent<Canvas>().enabled = true;
        GameCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void StartButtonPressed()
    {
        StartCanvas.GetComponent<Canvas>().enabled = false;
        GameCanvas.GetComponent <Canvas>().enabled = true;
        gameController.StartGame();
    }
    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void ShowEndScreen()
    {
        StartCanvas.enabled = true;
        GameCanvas.enabled = false;
    }
}
