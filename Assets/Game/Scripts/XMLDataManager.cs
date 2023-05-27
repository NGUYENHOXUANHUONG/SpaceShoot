using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class XMLDataManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    public static XMLDataManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SaveData(List<GameData> listScore)
    {
        leaderboard = new Leaderboard { list = listScore };
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.dataPath + "/Scripts/HighScore.xml", FileMode.Create);
        Debug.Log(listScore);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<GameData> LoadData()
    {
        
        if (File.Exists(Application.dataPath + "/Scripts/HighScore.xml"))
        {
            var serializer = new XmlSerializer(typeof(Leaderboard));
            using (FileStream stream = new FileStream(Application.dataPath + "/Scripts/HighScore.xml", FileMode.Open))
            {
                leaderboard = serializer.Deserialize(stream) as Leaderboard;
                stream.Close();
            }

            leaderboard.list.Sort((a, b) => int.Parse(b.Score).CompareTo(int.Parse(a.Score)));

            if (leaderboard.list.Count > 10)
                leaderboard.list = leaderboard.list.GetRange(0, 10);
        }
        else
        {
            Debug.Log("No saved data found.");
        }
        return leaderboard.list;
    }
}
[System.Serializable]
public class GameData
{
    public string Score;
    public GameData() { }
    public GameData(string score)
    {
        Score = score;
    }
}
[System.Serializable]
public class Leaderboard
{
    public List<GameData> list = new List<GameData>();
}





