using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MenuButtonController menuButtonController;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorFunctions animatorFunctions;
    [SerializeField] private int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController != null && animator != null && animatorFunctions != null)
        {
            if (menuButtonController.index == thisIndex)
            {
                animator.SetBool("selected", true);
                if (Input.GetAxis("Submit") == 1)
                {
                    animator.SetBool("pressed", true);
                }
                else if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                    animatorFunctions.disableOnce = true;
                }
            }
            else
            {
                animator.SetBool("selected", false);
            }
        }
        else
        {
            Debug.LogWarning("MenuButtonController, Animator, or AnimatorFunctions is not assigned.");
        }
    }

    // Handle mouse enter event
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menuButtonController != null && animator != null)
        {
            menuButtonController.index = thisIndex;  // Set index to this button's index
            animator.SetBool("selected", true);
        }
    }

    // Handle mouse exit event
    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("selected", false);
        }
    }
}
