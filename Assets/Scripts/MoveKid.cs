using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKid : MonoBehaviour {

    public float Speed { get; set; } = 1;

    public bool RunningAway { get; private set; } = false;
    public bool Moving { get; private set; } = true;

    private static List<MoveKid> allMoveKids = new List<MoveKid>();

    private KidDamage kidDamage;
    private KidSounds kidSounds;

    [SerializeField]
    private Renderer standingLegs = null, runningLegs = null;

    [SerializeField]
    private Renderer standingArm = null, runningArm = null;

    public static List<MoveKid> AllMoveKidsCopy() {
        return new List<MoveKid>(allMoveKids);
    }

    public void GetHit() {
        if (!kidDamage.HasHitHouse) {
            if (!RunningAway && !EndGame.Ended)
                WaveManager.AddDefeatedKid();
            Moving = true;
            RunningAway = true;
            kidSounds.PlayHurtSound();
        }
    }

    public void RunAway() {
        Moving = true;
        RunningAway = true;
    }

    public void Stop() {
        Moving = false;
    }

    private void Update() {
        if (Moving)
            transform.position += Vector3.left * (RunningAway ? -Speed * 5 : Speed) * Time.deltaTime;

        SetLegs();
        SetArm();

        transform.localScale = new Vector3(
            RunningAway ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private void SetLegs() {
        if (Moving != runningLegs.enabled) {
            runningLegs.enabled = Moving;
            standingLegs.enabled = !Moving;
        }
    }

    private void SetArm() {
        if (Moving != runningArm.enabled) {
            runningArm.enabled = Moving;
            standingArm.enabled = !Moving;
        }
    }

    private void Awake() {
        allMoveKids.Add(this);
        kidDamage = GetComponent<KidDamage>();
        kidSounds = GetComponent<KidSounds>();
    }

    private void OnDestroy() {
        allMoveKids.Remove(this);
    }

}