
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameClockHistoryVisualiser : MonoBehaviour
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