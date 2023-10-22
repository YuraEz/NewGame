using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string startScreen;
    [SerializeField] private List<UIScreen> Screens;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScreen(string screenName)
    {
        foreach (UIScreen screen in Screens)
        {
            screen.gameObject.SetActive(screen.ScreenName == screenName);
        }
    }
}
