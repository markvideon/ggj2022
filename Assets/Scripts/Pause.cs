using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
  private PlayerInput input;
  private PlayerMovement player;
  private GameObject child;

  [SerializeField] private GameObject firstSelected;
  
  private void FindFields()
  {
    input = FindObjectOfType<PlayerInput>();
    player = FindObjectOfType<PlayerMovement>();
    
    player?.RegisterPauseMenu(this);
  }
  
  private void Start()
  {
    child = this.transform.GetChild(0).gameObject;

    FindFields();
    
    SceneManager.sceneLoaded += (arg0, scene) =>
    {
      FindFields();
    };
  }

  public void ShowPause()
  {
    Assert.IsNotNull(input);
    Assert.IsNotNull(child);
    input.SwitchCurrentActionMap("UI");
    
    if (player)
    {
      Debug.Log("Player was found, setting firstSelected.");
      EventSystem.current.SetSelectedGameObject(firstSelected);
    }
    else
    {
      Debug.Log("Player was not found.");
    }
    
    child.SetActive(true);
  }

  public void HidePause()
  {
    Assert.IsNotNull(input);
    Assert.IsNotNull(child);
    input.SwitchCurrentActionMap("Gameplay");
    child.SetActive(false);
  }
}
