using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject kat = null;

    [SerializeField]
    private int hits = 5;

    [SerializeField]
    private float time = 2;

    [SerializeField]
    private AudioClip knockSound = null;

    private List<float> hitTimes = new List<float>();

    public void Hit() {
        Sound.PlaySound(knockSound);

        hitTimes.Add(Time.time);
        if (hitTimes.Count > hits)
            hitTimes.RemoveAt(0);
        if (hitTimes.Count != hits)
            return;
        float timeSpan = hitTimes[hits - 1] - hitTimes[0];
        if (timeSpan <= time && !kat.activeSelf)
            kat.SetActive(true);
    }

}