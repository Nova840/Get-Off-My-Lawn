using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDamage : MonoBehaviour {

    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private Animator paintAnimator = null;

    private MoveKid moveKid;

    public bool HasHitHouse { get; private set; } = false;

    private void Awake() {
        moveKid = GetComponent<MoveKid>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("House")) {
            other.GetComponent<House>().DamageHouse(damage);
            HasHitHouse = true;
            StartCoroutine(PaintAndRun());
        }
    }

    private IEnumerator PaintAndRun() {
        moveKid.Stop();
        paintAnimator.Play("Kid Arm Paint");
        do
            yield return new WaitForSeconds(0);
        while (paintAnimator.GetCurrentAnimatorStateInfo(0).IsName("Kid Arm Paint"));
        moveKid.RunAway();
    }

}