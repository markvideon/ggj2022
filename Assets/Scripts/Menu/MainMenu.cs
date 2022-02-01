using System;
using UnityEngine.EventSystems;

public class MainMenu : Menu
{
  public override void Start()
  {
    base.Start();
    EventSystem.current.SetSelectedGameObject(firstSelected);
    
    Navigator.PushMenu(this);
  }

  public void OnDestroy()
  {
    Navigator.clear();
  }
}