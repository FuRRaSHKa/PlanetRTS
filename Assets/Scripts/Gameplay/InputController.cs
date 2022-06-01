using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public InputController Instance;

    public event Action OnInputUp;
    public event Action OnInputDown;
    public event Action OnInputLeft;
    public event Action OnInputRight;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void Update()
    {
         
    }
}
