using System;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
  [SerializeField] level[] levels;
  [SerializeField] TextMeshProUGUI textMeshProText;

  private void Start()
  {
    LoadLevelByIndex(0);
  }

  public void LoadLevelByIndex(int index)
  {
    textMeshProText.text = levels[index].question.question;
    if (levels[index].usingButtonAnswer)
    {
      ProcessButtons(index);
      print("This level has Button Answer");
    }
    else if (levels[index].usingDropDownAnswers)
    {
      ProcessDropDowns(index);
      print("This level has DropDown Answer");
    }
    else if (levels[index].usingTextAnswers)
    {
      ProcessTexts(index);
      print("This level has text Answer");
    }
    else
    {
      print("No question assigned");
    }
  }

  public void EnterAnswer()
  {

  }

  private void ProcessTexts(int index)
  {
    foreach (ButtonAnswer button in levels[index].buttonAnswers)
    {
      GameObject temporaryButton = Instantiate(button.buttonPrefab);
      RectTransform buttonTransform = temporaryButton.GetComponent<RectTransform>();
      if (buttonTransform != null)
      {
        buttonTransform.position = button.buttonLocation;
        buttonTransform.localScale = new Vector3(button.width, button.height, 1);
      }
    }
  }

  private void ProcessDropDowns(int index)
  {
    foreach (DropDownAnswer dropDown in levels[index].dropDownAnswers)
    {
      GameObject temporaryDropDown = Instantiate(dropDown.dropDownPrefab, this.transform);
      RectTransform dropDownTransform = temporaryDropDown.GetComponent<RectTransform>();
      if (dropDownTransform != null)
      {
        dropDownTransform.position = dropDown.dropDownLocation;
        dropDownTransform.localScale = new Vector3(dropDown.width, dropDown.height, 1);
      }

      TMP_Dropdown DropdownElement = temporaryDropDown.GetComponent<TMP_Dropdown>();
      if (DropdownElement != null)
      {
        for (int i = 0; i < DropdownElement.options.Count; i++)
        {
          DropdownElement.options[i].text = dropDown.answerTexts[i];
        }
      }
    }
  }

  private void ProcessButtons(int index)
  {

  }

  public void DeleteCurrentLevel()
  {

  }

}

[System.Serializable]
public class level
{
  [Header("Question")]
  public Question question;

  [Header("Button Answers")]
  public bool usingButtonAnswer;
  public ButtonAnswer[] buttonAnswers;

  [Header("DropDown Answers")]
  public bool usingDropDownAnswers;
  public DropDownAnswer[] dropDownAnswers;

  [Header("Text Answers")]
  public bool usingTextAnswers;
  public TextAnswer[] textAnswers;
}

[System.Serializable]
public class Question
{
  public string question;
  public string[] correctAnswers;
}