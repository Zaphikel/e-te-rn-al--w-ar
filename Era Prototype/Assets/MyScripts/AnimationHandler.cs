using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour
{

    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    Animator Anim;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Anim.SetFloat("Movement", playerLocomotion.moveDirection.normalized.magnitude);
        Anim.SetBool("HasGun", playerManager.hasGun);
        
    }
}
