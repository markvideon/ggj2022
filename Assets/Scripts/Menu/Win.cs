using UnityEngine.SceneManagement;

public class Win : Menu
{
  private void FindFields()
  {
    base.FindFields();
    player?.RegisterWinMenu(this);
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

  public void ShowWin()
  {
    base.ShowMenu();
  }

  public void HideWin()
  {
    base.HideMenu();
  }
}