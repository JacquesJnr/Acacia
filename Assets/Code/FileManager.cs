using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This is a simple JSON utility save system from - https://github.com/UnityTechnologies/UniteNow20-Persistent-Data/blob/main/FileManager.cs
///
/// This allows for save / load of game data to a file using data from the SaveData class.
/// Modified to allow deletion of save files.
/// </summary>

public static class FileManager
{
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            //Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            Debug.Log($"No Save File Detected with exeption {e}");
            result = "";
            return false;
        }
    }

    public static void DeleteSaveFile(string a_FileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        
        try
        {
            File.Delete(fullPath);
            Debug.Log("Save File Deleted");
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to Delete File {a_FileName} with Exeption {e}");
            throw;
        }
    }
}