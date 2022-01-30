using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  [HideInInspector] public PlayerInput input;
  [HideInInspector] public PlayerMovement player;
  [HideInInspector] public GameObject child;

  [SerializeField] private GameObject firstSelected;
  
  public void FindFields()
  {
    input = FindObjectOfType<PlayerInput>();
    player = FindObjectOfType<PlayerMovement>();
  }
  
  public void Start()
  {
    child = this.transform.GetChild(0).gameObject;
  }

  public void ShowMenu()
  {
    Assert.IsNotNull(input);
    Assert.IsNotNull(child);
    Assert.IsNotNull(player);
    input.SwitchCurrentActionMap("UI");
    
    if (player)
    {
      Debug.Log("Player was found, setting firstSelected.");
      Debug.Log("Setting first selected");
      EventSystem.current.SetSelectedGameObject(firstSelected);
    }
    else
    {
      Debug.Log("Player was not found.");
    }
    
    child.SetActive(true);
  }

  public void HideMenu()
  {
    Assert.IsNotNull(input);
    Assert.IsNotNull(child);
    input.SwitchCurrentActionMap("Gameplay");
    child.SetActive(false);
  }
}