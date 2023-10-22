using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Space]
    [SerializeField] private float delay;

    private Button button;
    private bool isClicked;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        if (isClicked) return;

        isClicked = true;
        Invoke(nameof(ChangeScene), delay);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
