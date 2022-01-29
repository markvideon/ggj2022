using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class SandboxFlowVisualiser : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI textBox;

  private void Start()
  {
    Assert.IsNotNull(textBox);
  }

  public void SetText(string message)
  {
    textBox.text = message;
  }
}
