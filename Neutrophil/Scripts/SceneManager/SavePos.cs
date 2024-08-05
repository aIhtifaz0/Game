using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SavePos", menuName = "ScriptableObjects/SavePos")]
public class SavePos : ScriptableObject
{
    // Start is called before the first frame update

    public Vector3 startpoint;

    public void Awake(){
        GameObject player = GameObject.FindWithTag("Player");
        startpoint = player.transform.position;
    }
}
