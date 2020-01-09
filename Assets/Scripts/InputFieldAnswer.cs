using UnityEngine;

[CreateAssetMenu(fileName = "InputField", menuName = "InputField Answers/Create New InputField Answer", order = 0)]
public class InputFieldAnswer : ScriptableObject
{
  public GameObject inputFieldPrefab;
  [Header("Size")]
  public float width;
  public float height;
  public Vector3 inputFieldLocation;
}