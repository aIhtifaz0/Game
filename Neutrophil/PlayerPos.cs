using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public SavePos savepos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = savepos.startpoint;
    }
}
