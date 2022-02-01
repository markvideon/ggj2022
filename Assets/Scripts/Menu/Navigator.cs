using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
  public static PlayerInput input;

  public static Menu lastPage
  {
    get
    {
      if (count == 0) return null;
      return _navigationStack[count - 1];
    }
  }

  public static bool containsPage(Menu thisPage)
  {
    return _navigationStack.Contains(thisPage);
  }

  public static void clear()
  {
    _navigationStack.Clear();
  }
  
  public static int count
  {
    get { return _navigationStack.Count;  }
  }
  
  protected static List<Menu> _navigationStack = new List<Menu>();
  private static bool _hasSetSceneCallback = false;
  
  public virtual void FindFields()
  {
    input = FindObjectOfType<PlayerInput>();
  }
  
  private void Start()
  {
    if (!_hasSetSceneCallback)
    {
      _hasSetSceneCallback = true;
      SceneManager.sceneUnloaded += ((_) => _navigationStack.Clear());
    }
    
    SceneManager.sceneLoaded += (arg0, scene) =>
    {
      FindFields();
    };
  }
  // Pseudo-navigation stack of 1
  public static void PushMenu(Menu nextMenu)
  {
    if (_navigationStack.Count == 0)
    {
      if (input != null && input.enabled) input.SwitchCurrentActionMap("UI");
    }
    
    if (_navigationStack.Count > 0)
    {
      var presentMenu = _navigationStack[_navigationStack.Count - 1];
      presentMenu.HideMenu();
    }
    
    _navigationStack.Add(nextMenu);
    nextMenu.ShowMenu();
  }

  public static void PopMenu()
  {
    if (_navigationStack.Count > 0)
    {
      var lastMenu = _navigationStack[_navigationStack.Count - 1];
      lastMenu.HideMenu();
      _navigationStack.Remove(lastMenu);

      if (_navigationStack.Count > 0)
      {
        // New last menu
        var nextLastMenu = _navigationStack[_navigationStack.Count - 1];
        nextLastMenu.ShowMenu();
      }
      else
      {
        if (input != null && input.enabled) input.SwitchCurrentActionMap("Gameplay");
      }
    }
    else
    {
      // Debug.Log("Warning: attempted to pop a menu that wasn't active.");
    }
  }
}