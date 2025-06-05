using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ArmAnime()
    {
        //animator.SetTrigger("Move");
        animator.SetBool("Move", true);
    }
}
