using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScreens : MonoBehaviour
{
    [SerializeField] protected bool isConst = false;

    public bool IsActive => gameObject.activeSelf;

    protected void Awake()
    {  
    }

    protected virtual void Start()
    {
        if (!isConst)
        {
            UIController.RegisterSelf(this);
        }
    }

    public virtual void OnOpen()
    {
    }

    public virtual void OnClosed()
    {
    }
}
