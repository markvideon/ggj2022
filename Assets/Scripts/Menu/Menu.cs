using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  [HideInInspector] public PlayerMovement player;
  [HideInInspector] public GameObject child;

  [SerializeField] protected GameObject firstSelected;

  public virtual void FindFields()
  {
    player = FindObjectOfType<PlayerMovement>();
  }
  
  public virtual void Start()
  {
    child = this.transform.GetChild(0).gameObject;
  }

  public virtual void ShowMenu()
  {
    EventSystem.current.SetSelectedGameObject(firstSelected);

    Assert.IsNotNull(child);
    child?.SetActive(true);
  }

  public virtual void HideMenu()
  {
    child?.SetActive(false);
  }
}