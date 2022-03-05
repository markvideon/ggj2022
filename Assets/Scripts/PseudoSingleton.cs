using UnityEngine;

// Singletons in Unity are awkward.
public class PseudoSingleton<T> : MonoBehaviour
{
  private static PseudoSingleton<T> _instance;

  private void Awake()
  {
    if (_instance == null)
    {
      _instance = this;
    }
    else
    {
      Destroy(this);
    }
  }
}