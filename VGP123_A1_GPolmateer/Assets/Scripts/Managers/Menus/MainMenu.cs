using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.MainMenu;
        playButton.onClick.AddListener(() => GameManager.Instance.LoadScene("MainLevel"));
        settingsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Settings));
        quitButton.onClick.AddListener(QuitGame);
    }


}