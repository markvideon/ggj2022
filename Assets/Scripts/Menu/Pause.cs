using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : Menu
{
  private void FindFields()
  {
    base.FindFields();
    player?.RegisterPauseMenu(this);
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

  public void ShowPause()
  {
    base.ShowMenu();
  }

  public void HidePause()
  {
    HideMenu();
  }
}
