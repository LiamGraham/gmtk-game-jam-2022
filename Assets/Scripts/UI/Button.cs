using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter()
    {
        animator.SetBool("selected", true);
        animator.SetBool("pressed", false);
    }
    public void OnPointerExit()
    {
        animator.SetBool("selected", false);
        animator.SetBool("pressed", false);
    }

    public void OnPointerDown()
    {
        animator.SetBool("selected", true);
        animator.SetBool("pressed", true);
    }

    public void PerformAction()
    {
        var action = GetComponent<MenuButtonAction>();
        if (action != null)
        {
            action.PerformAction();
        }
    }
}
