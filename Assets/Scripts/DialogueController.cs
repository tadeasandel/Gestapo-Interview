using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
  [SerializeField] level[] levels;
  [SerializeField] TextMeshProUGUI textMeshProText;

  [SerializeField] int currentIndex;

  private void Start()
  {
    LoadLevelByIndex(0);
  }

  public void LoadLevelByIndex(int index)
  {
    DeleteCurrentLevel();
    currentIndex = index;
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
      ProcessInputFields(index);
      print("This level has text Answer");
    }
    else
    {
      print("No question assigned");
    }
  }

  public void SumbitAnswer()
  {
    List<string> answers = new List<string>();
    foreach (Transform currentChild in transform)
    {
      answers.Add(GetText(currentChild));
    }
    if (levels[currentIndex].question.doesOrderMatter)
    {
      ProcessAnswersInOrder(answers);
    }
    else
    {
      ProcessAnswersWithoutOrder(answers);
    }
  }

  private void ProcessAnswersWithoutOrder(List<string> answers)
  {
    List<string> correctAnswers = new List<string>();
    List<string> alrdyAnswered = new List<string>();
    foreach (string item in levels[currentIndex].question.correctAnswers)
    {
      correctAnswers.Add(item);
    }
    foreach (string answer in answers)
    {
      if (!correctAnswers.Contains(answer))
      {
        FailedAnswer();
        return;
      }
      if (alrdyAnswered.Contains(answer))
      {
        FailedAnswer();
        return;
      }
      print(answer);
      alrdyAnswered.Add(answer);
    }
    LoadLevelByIndex(currentIndex + 1);
  }

  private void ProcessAnswersInOrder(List<string> answers)
  {
    string[] correctAnswers = levels[currentIndex].question.correctAnswers;
    for (int i = 0; i < correctAnswers.Length; i++)
    {
      if (correctAnswers[i] != answers[i])
      {
        FailedAnswer();
        return;
      }
    }
    LoadLevelByIndex(currentIndex + 1);
  }

  public void FailedAnswer(string failMessage = "wrong duh!")
  {

  }

  public string GetText(Transform child)
  {
    string text = "";
    TMP_Dropdown currentDropDown = child.GetComponent<TMP_Dropdown>();
    TMP_InputField currentInputField = child.GetComponent<TMP_InputField>();
    if (currentDropDown != null)
    {
      text = currentDropDown.transform.GetChild(0).GetComponent<TMP_Text>().text;
    }
    else if (currentInputField != null)
    {
      text = currentInputField.text;
      print(currentInputField.text);
    }
    return text;
  }

  private void ProcessInputFields(int index)
  {
    foreach (InputFieldAnswer inputField in levels[index].inputFields)
    {
      GameObject temporaryInputField = Instantiate(inputField.inputFieldPrefab, transform);
      RectTransform inputFieldTransform = temporaryInputField.GetComponent<RectTransform>();
      if (inputFieldTransform != null)
      {
        inputFieldTransform.position = inputField.inputFieldLocation;
        inputFieldTransform.localScale = new Vector3(inputField.width, inputField.height, 1);
      }
    }
  }

  private void ProcessDropDowns(int index)
  {
    foreach (DropDownAnswer dropDown in levels[index].dropDownAnswers)
    {
      GameObject temporaryDropDown = Instantiate(dropDown.dropDownPrefab, transform);
      RectTransform dropDownTransform = temporaryDropDown.GetComponent<RectTransform>();
      if (dropDownTransform != null)
      {
        dropDownTransform.position = dropDown.dropDownLocation;
        dropDownTransform.localScale = new Vector3(dropDown.width, dropDown.height, 1);
      }

      TMP_Dropdown dropdownElement = temporaryDropDown.GetComponent<TMP_Dropdown>();
      if (dropdownElement != null)
      {
        dropdownElement.options.Clear();
        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < dropDown.answerTexts.Length; i++)
        {
          TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
          optionData.text = dropDown.answerTexts[i];
          dropdownElement.options.Add(optionData);
        }
      }
    }
  }

  private void ProcessButtons(int index)
  {
    foreach (ButtonAnswer button in levels[index].buttonAnswers)
    {
      GameObject temporaryButton = Instantiate(button.buttonPrefab, transform);
      RectTransform buttonTransform = temporaryButton.GetComponent<RectTransform>();
      if (buttonTransform != null)
      {
        buttonTransform.position = button.buttonLocation;
        buttonTransform.localScale = new Vector3(button.width, button.height, 1);
      }

      TMP_Text buttonText = temporaryButton.GetComponentInChildren<TMP_Text>();
      if (buttonText != null)
      {
        buttonText.text = button.buttonText;
      }

      temporaryButton.GetComponent<ButtonController>().SetLevelToTransferIndex(button.levelIndexToTransfer);
    }
  }

  public void DeleteCurrentLevel()
  {
    foreach (Transform childTransform in transform)
    {
      Destroy(childTransform.gameObject);
    }
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
  public InputFieldAnswer[] inputFields;
}

[System.Serializable]
public class Question
{
  public string question;
  public bool doesOrderMatter;
  public string[] correctAnswers;
}