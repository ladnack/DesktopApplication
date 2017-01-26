using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour {

    public void ChangeTextSeletedButton(Button button)
    {
        GameObject mainText = GameObject.Find("Text_SelectedButton");
        Text buttonTextComponent = button.GetComponentInChildren<Text>();
        mainText.GetComponent<Text>().text = buttonTextComponent.text;
        
    }








}
