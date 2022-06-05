using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private Dictionary<string, GUIScreens> cashedUIDict;

    public static void RegisterSelf(GUIScreens screen)
    {
        if (Instance.cashedUIDict.ContainsValue(screen))
        {
            return;
        }

        Instance.cashedUIDict.Add(screen.name, screen);
    }

    /// <summary>
    /// Открывает менюшку если она уже не открыта
    /// </summary>
    /// <param name="name"></param>
    public static void Open(string name)
    {
        if (!Instance.cashedUIDict.ContainsKey(name))
        {
            return;
        }

        GUIScreens screen = Instance.cashedUIDict[name];

        if (screen.IsActive)
        {
            return;
        }

        screen.gameObject.SetActive(true);
        screen.OnOpen();
    }

    /// <summary>
    /// Открывает менюшку поверх других менюшек
    /// </summary>
    /// <param name="name"></param>
    public static void Select(string name)
    {
        if (!Instance.cashedUIDict.ContainsKey(name))
        {
            return;
        }

        GUIScreens screen = Instance.cashedUIDict[name];

        screen.transform.SetAsLastSibling();
    }

    public static void Close(string name)
    {
        if (!Instance.cashedUIDict.ContainsKey(name))
        {
            return;
        }

        GUIScreens screen = Instance.cashedUIDict[name];

        screen.gameObject.SetActive(false);
        screen.OnClosed();
    }
}
