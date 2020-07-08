using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimap;

public class GameManager : MonoBehaviour
{

    [SerializeField] private MinimapIcon playerMinimapIcon;

    // Start is called before the first frame update
    void Start()
    {
        MinimapClass.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            MinimapClass.ShowWindow();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            MinimapClass.HideWindow();
        }
    }
}
