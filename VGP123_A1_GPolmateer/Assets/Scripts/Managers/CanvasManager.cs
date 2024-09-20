using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button settingsButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;


    void Start()
    {
        if (playButton)
        {
            playButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Level"));

        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));

        }

        if (backButton)
        {
            backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));
        }
    }

    private void SetMenus(GameObject menuToActivate, GameObject menuToDisable)
    {
        if (menuToActivate)
            menuToActivate.SetActive(true);

        if (menuToDisable)
            menuToDisable.SetActive(false);
    }


    void Update()
    {

    }
}