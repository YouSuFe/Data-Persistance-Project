using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int bestScore = 0;
    public string playerName;
    public string currentPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string name;
    }

    public void SaveScore(int highscore)
    {
        SaveData data = new SaveData();
        data.bestScore = highscore;
        data.name = currentPlayer;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void ResetScore()
    {
        SaveData data = new SaveData();
        data.bestScore = 0;
        data.name = "";

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.name;
            bestScore = data.bestScore; 
        }
    }
}
