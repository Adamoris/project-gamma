using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StartMenuButtons: MonoBehaviour
{
    public void Start()
    {
        UnityEngine.Debug.Log("This should bring you into the game");
    }

    public void Option()
    {
        UnityEngine.Debug.Log("This should switch to the options menu");
    }

    public void Exit()
    {
        UnityEngine.Debug.Log("This should exit the game");
        Application.Quit();
    }
}
