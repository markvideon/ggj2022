using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemManager : PseudoSingleton<EventSystemManager>
{
    private static EventSystemManager _instance;
    private EventSystem _eventSystemComponent;
    
    void Awake()
    {
        if (_instance == null)
        {
             _instance = this;
             _eventSystemComponent = GetComponent<EventSystem>();
             SceneManager.sceneLoaded += (arg0, mode) => CleanUp();
        }
        else
        {
            Destroy(this);
        }
    }

    void CleanUp()
    {
        // Should only be one event system in the scene at a time.
        var eventSystems = FindObjectsOfType<EventSystem>();
        
        foreach (var eventSystem in eventSystems)
        {
            if (eventSystem != _eventSystemComponent) Destroy(eventSystem.gameObject);
        }
    }
}
