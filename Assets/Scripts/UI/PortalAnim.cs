using UnityEngine;

public class PortalAnim : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn() 
    {
        animator.SetTrigger("FadeToBlack");
    }

    public void FadeOut() 
    {
        animator.SetTrigger("FadeFromBlack");
    }
}
