using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Config : MonoBehaviour
{
    UIManager UI;

    public static float masterAudio;
    
    public static float default_masterAudio = 1;

    

    void Start() {
        UI = GetComponent<UIManager>();
        ApplyConfig();
        Debug.Log(path);
        path = Path.Combine(Application.persistentDataPath, "Config.cfg");
    }

    public string path;
    string config;
    //Create a new config file and write default values
    void CreateConfig() {
        try {
            path = Path.Combine(Application.persistentDataPath, "Config.cfg");

            using(FileStream fileStream = File.Create(path)) {
            
            }

            string configData = $"[masterAudio:{default_masterAudio}]";

            File.WriteAllText(path, configData);
        }
        catch(Exception e) {
            Debug.LogError($"Failed to create config file: {e.Message}");
        }
    }

    //Overwrite the config file with current values
    public void WriteConfig() {
        path = Path.Combine(Application.persistentDataPath, "Config.cfg");
        Debug.Log("Wrote config");

        try {
            if(!File.Exists(path)) {
                CreateConfig();
            }

            string configData = $"[masterAudio:{masterAudio}]";

            File.WriteAllText(path, configData);
        }
        catch(Exception e) {
            Debug.LogError($"Failed to write to config: {e.Message}");
        }
    }

    //Read the config file
    public void ReadConfig() {
        path = Path.Combine(Application.persistentDataPath, "Config.cfg");

        if(!File.Exists(path)) {
            Debug.Log("No config save file found, creating one");
            CreateConfig();
        }
        
        config = File.ReadAllText(path);
    }

    public void ApplyConfig()
    {
        ReadConfig();

        if(!string.IsNullOrEmpty(config)) {
            string[] sections = config.Split(new[] {"[", "]"}, StringSplitOptions.RemoveEmptyEntries);

            foreach(string section in sections) {
                string[] setting = section.Split(":");
                if(setting.Length == 2) {
                    string name = setting[0];
                    string value = setting[1];

                    switch(name) {
                        case "masterAudio":
                            masterAudio = NewValue(value, default_masterAudio);
                            break;
                        default:
                            Debug.LogWarning($"Unknown config key: {name}");
                            break;
                    }
                }
            }

            UI.UpdateSliders(masterAudio);
        }

        else {
            Debug.LogError("Config file is invalid, using default values");
            UseDefaultValues();
        }
    }

    //Tries to get the saved value for sensitivity or master audio etc.
    float NewValue(string value, float defaultValue) {
        if(float.TryParse(value, out float parsedValue)) {
            return parsedValue;
        }
        else {
            Debug.LogError("Failed to parse value: " + value + " using default value");
            return defaultValue;
        }
    }

    void UseDefaultValues() {
        Debug.Log("Set values to default");
        masterAudio = default_masterAudio;
    }
}
