using UnityEngine.EventSystems;

public class MainMenu : Menu
{
  public override void Start()
  {
    base.Start();
    EventSystem.current.SetSelectedGameObject(firstSelected);
  }
}