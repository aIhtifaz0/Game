using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesPoison : MonoBehaviour
{
    public GameObject par;
    public GameObject parTarget;
    // Start is called before the first frame update

    public void crot()
    {
        Instantiate(par, parTarget.transform.position, par.transform.rotation);
    }
}

