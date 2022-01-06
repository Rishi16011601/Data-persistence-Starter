using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text Username_field;
    public Text BestScoreText;
    //public string t1;
    //public GameObject button1;
    //public string textValue;

    /*
    public void ReadName(string s)
    {
        t1 = button1.GetComponent<Text>().text;
        Debug.Log(t1);
    }
    */

    public void StartNew()
    {
        //Manager.Instance.PlayerName = Username_field.text.ToString();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Manager.Instance.SaveDetails(Username_field.text.ToString());

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    
    public void SaveOnStart()
    {
        Manager.Instance.SaveDetails(Username_field.text.ToString());
    }
    

    /*
    public void LoadOnStart()
    {
        //MainManager.Instance.LoadDetails();
        //ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
    */
}
