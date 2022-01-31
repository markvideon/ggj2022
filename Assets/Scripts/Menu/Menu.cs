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

  [SerializeField] protected GameObject firstSelected;
  
  public virtual void FindFields()
  {
    input = FindObjectOfType<PlayerInput>();
    player = FindObjectOfType<PlayerMovement>();
  }
  
  public virtual void Start()
  {
    child = this.transform.GetChild(0).gameObject;
  }

  public virtual void ShowMenu()
  {
    if (input !=null && input.enabled) input.SwitchCurrentActionMap("UI");
    
    EventSystem.current.SetSelectedGameObject(firstSelected);

    Assert.IsNotNull(child);
    child?.SetActive(true);
  }

  public virtual void HideMenu()
  {
    child?.SetActive(false);
  }
}