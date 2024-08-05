using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        Debug.Log("Changing selected profile ID to: " + newProfileId);
        // update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        Debug.Log("Deleting profile data for profile ID: " + profileId);
        // delete the data for this profile id
        dataHandler.Delete(profileId);
        // initialize the selected profile id
        InitializeSelectedProfileId();
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileId);
        }
    }

    public void NewGame()
    {
        Debug.Log("Starting a new game.");
        this.gameData = new GameData();
        SaveGame();
    }

    public void LoadGame()
    {
        Debug.Log("Loading game.");
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            Debug.LogWarning("Data persistence is disabled.");
            return;
        }

        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull)
        {
            Debug.LogWarning("No data found. Initializing new game data.");
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.LogError("No data was found. A new game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        if (dataPersistenceObjects == null)
        {
            Debug.LogWarning("dataPersistenceObjects not found. Finding all data persistence objects.");
            dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                Debug.Log("Loading data into: " + dataPersistenceObj);
                dataPersistenceObj.LoadData(gameData);
            }
            else
            {
                Debug.LogWarning("Found null IDataPersistence object.");
            }
        }
    }

    public void SaveGame()
    {
        Debug.Log("SaveGame called");
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            Debug.LogWarning("Data persistence is disabled.");
            return;
        }

        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved.");
            return;
        }

        // ensure dataPersistenceObjects is not null
        if (dataPersistenceObjects == null)
        {
            Debug.LogWarning("dataPersistenceObjects null. Finding all data persistence objects.");
            dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.SaveData(gameData);
            }
            else
            {
                Debug.LogWarning("Found null IDataPersistence object during SaveGame.");
            }
        }

        // timestamp the data so we know when it was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    public GameData GetGameData()
    {
        return this.gameData;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting. Saving game.");
        SaveGame();
    }

   private List<IDataPersistence> FindAllDataPersistenceObjects()
{
    MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
    List<IDataPersistence> dataPersistenceObjects = new List<IDataPersistence>();

    foreach (var monoBehaviour in monoBehaviours)
    {
        if (monoBehaviour is IDataPersistence)
        {
            dataPersistenceObjects.Add(monoBehaviour as IDataPersistence);
        }
    }

    return dataPersistenceObjects;
}

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}
