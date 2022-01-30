using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : Menu
{
    public override void FindFields()
    {
        // Do nothing. No need to FindFields.
    }
    
    public void Quit()
    {
        Debug.Log("Simulated quit");
        Application.Quit();
    }
}
