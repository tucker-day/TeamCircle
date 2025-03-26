using UnityEngine;
using UnityEngine.EventSystems;

public class UIAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private static UIAnimationController currentlyHoveredButton;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetFloat("SpeedMultiplier", 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        UIAnimationController[] allButtons = FindObjectsOfType<UIAnimationController>();
        foreach (var button in allButtons)
        {
            if (button != this)
            {
                button.StopIdleAnimation();
            }
        }

        
        animator.SetBool("isHovered", true);
        currentlyHoveredButton = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        animator.SetBool("isHovered", false);
        
        
        currentlyHoveredButton = null;
        ResumeAllIdleAnimations();
    }

    private void StopIdleAnimation()
    {
        animator.SetFloat("SpeedMultiplier", 0f); 
    }

    private void ResumeIdleAnimation()
    {
        animator.SetFloat("SpeedMultiplier", 1f); 
    }

    private static void ResumeAllIdleAnimations()
    {
        UIAnimationController[] allButtons = FindObjectsOfType<UIAnimationController>();
        foreach (var button in allButtons)
        {
            button.ResumeIdleAnimation();
        }
    }
}
