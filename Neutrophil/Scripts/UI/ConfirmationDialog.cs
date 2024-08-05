using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
    public static ConfirmationDialog Instance { get; private set; }

    public GameObject dialogBox;
    public Button yesButton;
    public Button noButton;

    private Action onConfirm;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        yesButton.onClick.AddListener(HandleConfirm);
        noButton.onClick.AddListener(CloseDialog);
    }

    public void ShowDialog(Action onConfirmAction)
    {
        onConfirm = onConfirmAction;
        dialogBox.SetActive(true);
    }

    void HandleConfirm()
    {
        onConfirm?.Invoke();
        CloseDialog();
    }

    void CloseDialog()
    {
        dialogBox.SetActive(false);
    }
}