using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : Menu
{
  public override void FindFields()
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
}
