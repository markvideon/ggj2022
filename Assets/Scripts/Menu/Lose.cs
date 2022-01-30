using UnityEngine.SceneManagement;

public class Lose : Menu
{
  private void FindFields()
  {
    base.FindFields();
    player?.RegisterLoseMenu(this);
  }
  
  private void Start()
  {
    base.Start();
    
    FindFields();
    
    SceneManager.sceneLoaded += (arg0, scene) =>
    {
      FindFields();
    };
  }

  public void ShowLose()
  {
    base.ShowMenu();
  }

  public void HideLose()
  {
    base.HideMenu();
  }
}