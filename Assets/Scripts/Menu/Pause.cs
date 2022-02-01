using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : Menu
{
  // Attempt to avoid collisions with input maps. 
  // Escape (go back) and Pause could be mapped to the same button.
  public bool canPause
  {
    get
    {
      return (Navigator.count >= 0) && !Navigator.containsPage(this);
    }
  }
  
  public bool canUnpause
  {
    get
    {
      return (Navigator.count > 0) && Navigator.lastPage == this;
    }
  }
  
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
