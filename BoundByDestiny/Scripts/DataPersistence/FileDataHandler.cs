using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private readonly string dataDirPath;
    private readonly string dataFileName;
    private readonly bool useEncryption;
    private readonly string encryptionCodeWord = "word";
    private readonly string backupExtension = ".bak";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath.Replace('\\', '/');
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileId, bool allowRestoreFromBackup = true)
    {
        if (profileId == null)
        {
            Debug.LogError("Profile ID is null.");
            return null;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName).Replace('\\', '/');
        GameData loadedData = null;
        try
        {
            if (File.Exists(fullPath))
            {
                string dataToLoad = File.ReadAllText(fullPath);

                if (useEncryption)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            else
            {
                Debug.LogWarning("File not found at path: " + fullPath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occurred when trying to load file at path: " + fullPath + "\n" + ex);
            if (allowRestoreFromBackup)
            {
                Debug.LogWarning("Attempting to roll back...");
                bool rollbackSuccess = AttemptRollback(fullPath);
                if (rollbackSuccess)
                {
                    Debug.LogWarning("Rollback successful. Trying to load again...");
                    loadedData = Load(profileId, false);
                }
            }
            else
            {
                Debug.LogError("Backup restore failed. Error message: " + ex.Message);
            }
        }

        return loadedData;
    }

    public void Save(GameData data, string profileId)
    {
        if (profileId == null)
        {
            Debug.LogError("Profile ID is null.");
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName).Replace('\\', '/');
        string backupFilePath = fullPath + backupExtension;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
                dataToStore = EncryptDecrypt(dataToStore);

            File.WriteAllText(fullPath, dataToStore);

            GameData verifiedGameData = Load(profileId);
            if (verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("Save file could not be verified and backup could not be created.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occurred when trying to save data to file: " + fullPath + "\n" + ex);
        }
    }

    public void Delete(string profileId)
    {
        if (profileId == null)
        {
            Debug.LogError("Profile ID is null.");
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName).Replace('\\', '/');
        try
        {
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to delete profile data for profileId: " + profileId + " at path: " + fullPath + "\n" + ex);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        try
        {
            foreach (var dirInfo in new DirectoryInfo(dataDirPath).EnumerateDirectories())
            {
                string profileId = dirInfo.Name;
                string fullPath = Path.Combine(dataDirPath, profileId, dataFileName).Replace('\\', '/');

                if (File.Exists(fullPath))
                {
                    GameData profileData = Load(profileId);
                    if (profileData != null)
                    {
                        profileDictionary.Add(profileId, profileData);
                    }
                    else
                    {
                        Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
                    }
                }
                else
                {
                    Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + profileId);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred while loading all profiles: " + ex);
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;
        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();

        foreach (var pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            if (gameData == null)
                continue;

            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRollback(string fullPath)
    {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            if (File.Exists
(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Successfully rolled back to backup file at: " + backupFilePath);
            }
            else
            {
                throw new FileNotFoundException("Backup file not found at path: " + backupFilePath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occurred when trying to roll back to backup file at: " + backupFilePath + "\n" + ex);
        }

        return success;
    }
}
