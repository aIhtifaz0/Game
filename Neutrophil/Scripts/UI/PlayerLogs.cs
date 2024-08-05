using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerLogs : MonoBehaviour
{
    public static PlayerLogs instance;

    public GameObject playerLogPrefab;
    public Transform playerLogParent;

    private int enemyKills;
    private int playerDeaths;
    private int playerDamageTaken;
    private int playerDamageDealt;

    // Start is called before the first frame update
    void Start()
    {
        //Get the data from playerprefs
        enemyKills = PlayerPrefs.GetInt("EnemyKills");
        playerDeaths = PlayerPrefs.GetInt("PlayerDeaths");
        playerDamageTaken = PlayerPrefs.GetInt("PlayerDamageTaken");
        playerDamageDealt = PlayerPrefs.GetInt("PlayerDamageDealt");

        UpdatePlayerLogs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncrementStatAndSave(string key, ref int stat, int amount = 1)
    {
        stat += amount;
        PlayerPrefs.SetInt(key, stat);
        UpdatePlayerLogs();
    }

    public void AddEnemyKill()
    {
        IncrementStatAndSave("EnemyKills", ref enemyKills);
    }

    public void AddPlayerDeath()
    {
        IncrementStatAndSave("PlayerDeaths", ref playerDeaths);
    }

    public void AddPlayerDamageTaken(int damage)
    {
        IncrementStatAndSave("PlayerDamageTaken", ref playerDamageTaken, damage);
    }

    public void AddPlayerDamageDealt(int damage)
    {
        IncrementStatAndSave("PlayerDamageDealt", ref playerDamageDealt, damage);
    }

    public void UpdatePlayerLogs()
    {
        //Destroy all the children
        foreach (Transform child in playerLogParent)
        {
            //skip the first child
            if (child.GetSiblingIndex() == 0)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        CreatePlayerLog("Enemy Kills: ", enemyKills.ToString());
        CreatePlayerLog("Player Deaths: ", playerDeaths.ToString());
        CreatePlayerLog("Player Damage Taken: ", playerDamageTaken.ToString());
        CreatePlayerLog("Player Damage Dealt: ", playerDamageDealt.ToString());
    }   

    private void CreatePlayerLog(string label, string value)
    {
        GameObject newLog = Instantiate(playerLogPrefab, playerLogParent);
        newLog.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = label;
        newLog.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = value;
    }

}
