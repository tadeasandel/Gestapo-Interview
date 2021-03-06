﻿using UnityEngine;

[CreateAssetMenu(fileName = "Button Answer", menuName = "Button Answers/Create New Button Answers", order = 0)]
public class ButtonAnswer : ScriptableObject
{
  public string buttonText;
  public GameObject buttonPrefab;
  [Header("Size")]
  public float width;
  public float height;
  public Vector3 buttonLocation;
  public int levelIndexToTransfer;
}

