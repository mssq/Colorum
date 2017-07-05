using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void ChangeToIdle() {
        anim.SetBool("Activated", false);
    }
}
