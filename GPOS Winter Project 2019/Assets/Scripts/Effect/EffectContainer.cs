using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContainer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("ID", -1);
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Exit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            Destroy(gameObject);
    }

    public void Init(int id, bool isLoop)
    {
        animator.SetInteger("ID", id);
        animator.SetBool("isLoop", isLoop);
    }

    public void Init(int id, bool isLoop, Color color)
    {
        animator.SetInteger("ID", id);
        animator.SetBool("isLoop", isLoop);
        spriteRenderer.color = color;
    }
}
