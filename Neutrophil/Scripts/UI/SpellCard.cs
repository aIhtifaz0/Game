using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCard : MonoBehaviour
{


    private Image cooldownImage;
    public float cooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        cooldownImage = transform.GetChild(0).GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCooldown();
        }
    }


    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        float time = 0;
        while (time < cooldownTime)
        {
            time += Time.deltaTime;
            cooldownImage.fillAmount = time / cooldownTime;
            //Reverse the fill amount
            cooldownImage.fillAmount = 1 - time / cooldownTime;
            yield return null;
        }
    }
}
