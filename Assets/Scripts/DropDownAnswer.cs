using UnityEngine;

[CreateAssetMenu(fileName = "DropDown Answer", menuName = "DropDown Answers/Create New DropDown Answer", order = 0)]
public class DropDownAnswer : ScriptableObject
{
  public string[] answerTexts;
  public GameObject dropDownPrefab;
  [Header("Size")]
  public float width;
  public float height;
  public Vector3 dropDownLocation;
}