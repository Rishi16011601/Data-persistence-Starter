using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public TextMeshProUGUI TextPro;

    public string PlayerName;
    public int PlayerScore = 0;
    public string BestPlayer = "";
    public int BestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            LoadDetails();
            BDisplay();
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDetails();
        BDisplay();
        //BestDisplay();
    }

    public void BDisplay()
    {
        TextPro.text = $"Best Score : {BestPlayer} : {BestScore}";
    }

    [System.Serializable]
    class SaveData
    {
        //public string PlayerName;
        public int BestScore;
        public string BestPlayer;
    }

    public void SaveDetails(string name)
    {
        PlayerName = name;
        Debug.Log(PlayerName);
        //SaveData data = new SaveData();
        SaveData bestData = new SaveData();
        //data.PlayerName = PlayerName;
        //data.PlayerScore = PlayerScore;

        //bestData.PlayerName = PlayerName;
        bestData.BestScore = BestScore;
        bestData.BestPlayer = BestPlayer;

        string json = JsonUtility.ToJson(bestData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadDetails()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData bestData = JsonUtility.FromJson<SaveData>(json);

            //PlayerName = bestData.PlayerName;
            BestPlayer = bestData.BestPlayer;
            BestScore = bestData.BestScore;
        }
    }

}
