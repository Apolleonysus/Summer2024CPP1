using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : BaseMenu
{
    public Button resumeGame;
    public Button returnToMenu;
    public Button quitGame;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Pause;

        // Button Listeners
        resumeGame.onClick.AddListener(ResumeGame); // Resume game logic
        returnToMenu.onClick.AddListener(() => SceneManager.LoadScene("Title"));
        quitGame.onClick.AddListener(QuitGame);
    }

    // Method to handle resuming the game
    public void ResumeGame()
    {
       
        Time.timeScale = 1;
    }
}
