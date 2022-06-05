using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private List<GUIScreens> prebakedList = new List<GUIScreens>(); 

    private Dictionary<string, GUIScreens> cashedUIDict = new Dictionary<string, GUIScreens>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        BakeDict();
    }

    private void BakeDict()
    {
        foreach (var menu in prebakedList)
        {
            RegisterSelf(menu);
        }
    }

    public static void RegisterSelf(GUIScreens screen)
    {
        if (Instance.cashedUIDict.ContainsValue(screen))
        {
            return;
        }

        Instance.cashedUIDict.Add(screen.GetType().Name, screen);
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

    [ContextMenu("FindAllMenus")]
    private void FindAllMenus()
    {
        prebakedList = new List<GUIScreens>(10);
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            GUIScreens[] menus = canvas.GetComponentsInChildren<GUIScreens>(true);
            foreach (GUIScreens mn in menus)
            {
                if (!cashedUIDict.ContainsValue(mn))
                {
                    prebakedList.Add( mn);
                }              
            }
        }

        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }

}
