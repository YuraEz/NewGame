using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialScreen[] allScreens;
    [SerializeField] private GameObject[] linkeds;
    [SerializeField] private Transform MainCamera;

    private void OnEnable()
    {
        foreach (var screen in allScreens)
        {
            screen.gameObject.SetActive(false);
        }

        allScreens[0].gameObject.SetActive(true);
        StartTutorial();
        //EndTutorial();
    }

    public void StartTutorial()
    {
        MainCamera.gameObject.SetActive(true);
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(false);
        }

        //Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void EndTutorial()
    {
        MainCamera.gameObject.SetActive(false);
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(true);
        }

        //Time.timeScale = 1;
    }
}
