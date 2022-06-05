using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScreens : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    private void Awake()
    {  
    }

    public virtual void OnOpen()
    {
    }

    public virtual void OnClosed()
    {
    }
}
