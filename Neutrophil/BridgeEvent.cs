using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEvent : MonoBehaviour
{
    public float lifetime;
    public GameObject SmokePar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(SmokePar,transform.position, Quaternion.identity);
        Destroy(gameObject, lifetime);
    }
}
