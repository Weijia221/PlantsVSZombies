using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUI : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Hide();
    }
    void Hide()
    {
        if (anim != null)
            anim.enabled = false;
    }
    public void Show()
    {
        if (anim != null)
            anim.enabled = true;
    }
}
