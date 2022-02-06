using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Vector3 moveMagnitude;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveMagnitude * Time.deltaTime);
    }
}
