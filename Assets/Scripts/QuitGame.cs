using UnityEngine;

public class QuitGame : Menu
{
    private Menu _previousMenu;
    public override void FindFields()
    {
        // Do nothing. No need to FindFields.
    }
    
    public void Quit()
    {
        Debug.Log("Simulated quit");
        Application.Quit();
    }
    
    // Pseudo-navigation stack of 1
    public void PushMenu(Menu previousMenu)
    {
        _previousMenu = previousMenu;
        _previousMenu.HideMenu();
        base.ShowMenu();
    }

    public void PopMenu()
    {
        base.HideMenu();
        _previousMenu?.ShowMenu();
        _previousMenu = null;
    }
}
