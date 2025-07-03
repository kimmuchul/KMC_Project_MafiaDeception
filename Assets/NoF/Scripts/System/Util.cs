using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Util : MonoBehaviour
{
    public GameObject TextPanel;
    public Animator animator;
    private bool isShowQuest = false;
    public void ShowMenu()
    {
        bool isActive = TextPanel.activeSelf;
        TextPanel.SetActive(!isActive);
    }

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void ShowQuest()
    {
        isShowQuest = !isShowQuest;
        animator.SetBool("IsShowQuest",isShowQuest);
    }

}
