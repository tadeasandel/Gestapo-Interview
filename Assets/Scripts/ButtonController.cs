using UnityEngine;

public class ButtonController : MonoBehaviour
{
  int levelToTransferIndex;

  public void OnButtonClicked()
  {
    FindObjectOfType<DialogueController>().LoadLevelByIndex(levelToTransferIndex);
  }

  public void SetLevelToTransferIndex(int index)
  {
    print(index);
    levelToTransferIndex = index;
  }
}