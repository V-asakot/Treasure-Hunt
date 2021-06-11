using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{

   
   
    private PlayerStats _stats;

    public PlayerStats Stats { set {_stats = value;SaveStats(); } get {if(_stats == null) LoadStats();return _stats; } }

    public void LoadStats()
    {
        string _filePath = Application.persistentDataPath;
        try
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);

            }
            string f = _filePath + "/Stats.set";
            if (!File.Exists(f))
            {
                SaveStats();

            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(f, FileMode.Open);
            _stats= (PlayerStats)bf.Deserialize(file);
            file.Close();
        }
        catch (IOException ex)
        {

            File.Delete(_filePath + "/Stats.set");
            SaveStats();
        }
    }
    public void SaveStats()
    {
        string _filePath = Application.persistentDataPath;
        try
        {

            if (_stats == null) _stats = new PlayerStats();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_filePath + "/Stats.set", FileMode.OpenOrCreate);
            bf.Serialize(file, _stats);
            file.Close();

        }
        catch (IOException ex)
        {


            File.Delete(_filePath + "/Stats.set");


        }


    }

 
}
