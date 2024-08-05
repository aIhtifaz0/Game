using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]

    public GameObject[] dialogueBox;
    public TextMeshProUGUI[] dialogueName;
    public TextMeshProUGUI[] dialogueText;
    public Image[] dialogueImage;
    [Header("Dialogue")]
    public DialogueAssets dialogueAssets;
    public int dialogueOneIndex = 0;
    public int dialogueTwoIndex = 0;

    public bool isDialogueOne;
    public bool isDialogueTwo;

    public Button button1;
    public Button button2;

    bool isOnDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogueOne();
        isDialogueOne = true;
        button1.onClick.AddListener(NextDialogue);
        button2.onClick.AddListener(NextDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnDialogue)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void NextDialogue()
    {
        
        if (isDialogueOne)
        {
            dialogueBox[0].SetActive(true);
            dialogueBox[1].SetActive(false);
            if (dialogueOneIndex >= dialogueAssets.dialogues[0].sentences.Count)
            {
                CloseDialogue();
                dialogueBox[0].SetActive(false);
                return;
            }
            if (dialogueAssets.dialogues[0].sentences[dialogueOneIndex].isSkip)
            {
                isDialogueOne = true;
                isDialogueTwo = false;
            }
            else
            {
                isDialogueOne = false;
                isDialogueTwo = true;
            }
            dialogueOneIndex++;
            SetDialogue();
        }
        else if (isDialogueTwo)
        {
            dialogueBox[1].SetActive(true);
            dialogueBox[0].SetActive(false);
            //CHeck if the dialogue is finished
            if (dialogueTwoIndex >= dialogueAssets.dialogues[1].sentences.Count)
            {
                CloseDialogue();
                dialogueBox[1].SetActive(false);
                return;
            }
            if (dialogueAssets.dialogues[1].sentences[dialogueTwoIndex].isSkip)
            {
                isDialogueTwo = true;
                isDialogueOne = false;
            }
            else
            {
                isDialogueTwo = false;
                isDialogueOne = true;
            }
            dialogueTwoIndex++;
            SetDialogue();
        }
    }

    public void StartDialogueOne()
    {
        if (isDialogueOne)
        {
            dialogueBox[0].SetActive(true);
            dialogueBox[1].SetActive(false);
        }
        else if (isDialogueTwo)
        {
            dialogueBox[1].SetActive(true);
            dialogueBox[0].SetActive(false);
        }
        dialogueName[0].text = dialogueAssets.dialogues[dialogueOneIndex].name;
        dialogueName[1].text = dialogueAssets.dialogues[dialogueTwoIndex].name;
        dialogueText[0].text = dialogueAssets.dialogues[dialogueOneIndex].sentences[0].sentance;
        dialogueText[1].text = dialogueAssets.dialogues[dialogueTwoIndex].sentences[0].sentance;
        dialogueImage[0].sprite = dialogueAssets.dialogues[dialogueOneIndex].sentences[0].image;
        dialogueImage[1].sprite = dialogueAssets.dialogues[dialogueTwoIndex].sentences[0].image;
        isDialogueOne = true;
        isOnDialogue = true;
    }

    private void SetDialogue()
    {
        if(isDialogueOne)
        {
            dialogueName[0].text = dialogueAssets.dialogues[0].name;
            dialogueText[0].text = dialogueAssets.dialogues[0].sentences[dialogueOneIndex].sentance;
            dialogueImage[0].sprite = dialogueAssets.dialogues[0].sentences[dialogueOneIndex].image;
        }
        else if(isDialogueTwo)
        {
            dialogueName[1].text = dialogueAssets.dialogues[1].name;
            dialogueText[1].text = dialogueAssets.dialogues[1].sentences[dialogueTwoIndex].sentance;
            dialogueImage[1].sprite = dialogueAssets.dialogues[1].sentences[dialogueTwoIndex].image;
        }
    }

    private void CloseDialogue()
    {
        Debug.Log("Dialogue Closed");
        dialogueBox[0].SetActive(false);
        dialogueBox[1].SetActive(false);
        isOnDialogue = false;
    }
}
[System.Serializable]
public class Dialogue
{
    public string name;
    public List<Sentences> sentences = new List<Sentences>();

}
[System.Serializable]
public struct Sentences
{
    public Sprite image;
    [TextArea(3, 10)]
    public string sentance;
    public bool isSkip;

}