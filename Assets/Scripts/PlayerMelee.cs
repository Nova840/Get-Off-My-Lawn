using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [SerializeField]
    private CircleCollider2D areaOfEffect = null;

    private Transform carrying = null;
    private ParticleSpawner carryingSpawner = null;

    public string Player { get; set; }

    [SerializeField]
    private GameObject phonographArms = null, regularArms = null;

    private Renderer[] phonographRenderers, regularRenderers;

    private Animator[] regularArmAnimators;

    [SerializeField]
    private AudioClip missSound = null;

    [SerializeField]
    private Transform carryPosition = null;

    [SerializeField]
    private AudioClip[] hitSounds = null;

    private void Awake() {
        phonographRenderers = phonographArms.GetComponentsInChildren<Renderer>();
        regularRenderers = regularArms.GetComponentsInChildren<Renderer>();
        regularArmAnimators = regularArms.GetComponentsInChildren<Animator>();
        if (phonographRenderers.Length != regularRenderers.Length)
            Debug.LogError("Renderer arrays not the same length.");
        else if (regularRenderers.Length == 0)
            Debug.LogError("No renderers in array.");
    }

    private void Update() {
        if (!PauseGame.Paused && !EndGame.Ended && ShouldInteract())
            Swing();
        UpdateCarrying();
    }

    private bool ShouldInteract() {
        if (Application.isMobilePlatform)
            return Input.touches.Where(t => MainCamera.Instance.ScreenToViewportPoint(t.position).x > .5f).Any(t => t.phase == TouchPhase.Began);
        else
            return Input.GetButtonDown(Player + " Interact");
    }

    private void Swing() {

        bool putDownItem = false;
        if (carrying) {//put down
            carrying.transform.Find("Phonograph").gameObject.SetActive(true);
            carrying.GetComponent<BoxCollider2D>().enabled = true;
            carrying = null;
            carryingSpawner = null;
            putDownItem = true;
            return;
        }

        GameObject[] inCircle = Physics2D.OverlapCircleAll(areaOfEffect.transform.position, areaOfEffect.radius * AverageABSParentXY(areaOfEffect.transform)).Select(c => c.gameObject).ToArray();
        GameObject closestItem = inCircle.Where(g => g.CompareTag("Item")).OrderBy(g => Vector2.Distance(g.transform.position, areaOfEffect.transform.position)).FirstOrDefault();

        bool pickedUpItem = false;
        if (closestItem && !carrying && !putDownItem) {//pick up
            RecordPlayer recordPlayer = closestItem.GetComponent<RecordPlayer>();
            recordPlayer.PlayMusic();
            carrying = recordPlayer.transform;
            carrying.GetComponent<BoxCollider2D>().enabled = false;
            carryingSpawner = carrying.GetComponentInChildren<ParticleSpawner>();
            carrying.Find("Phonograph").gameObject.SetActive(false);
            carrying.GetComponent<RecordPlayer>().GetParticleSpawner().enabled = true;
            pickedUpItem = true;
        }

        if (!pickedUpItem && !putDownItem) {
            bool knocked = false;
            foreach (GameObject g in inCircle) {
                if (g.CompareTag("Kat Trigger")) {
                    g.GetComponent<KatTrigger>().Hit();
                    knocked = true;
                }
            }

            foreach (Animator a in regularArmAnimators)
                a.Play("Arm Swing");
            GameObject[] kidsHit = inCircle.Where(c => c.CompareTag("Kid")).ToArray();
            if (!knocked) {
                if (kidsHit.Length == 0)
                    Sound.PlaySound(missSound);
                else
                    Sound.PlaySound(hitSounds[Random.Range(0, hitSounds.Length)]);
            }
            foreach (GameObject kid in kidsHit)
                kid.GetComponent<MoveKid>().GetHit();
        }

    }

    private void UpdateCarrying() {
        if (carrying) {
            carrying.position = carryPosition.position;
            if (transform.localScale.x < 0) {
                carrying.localScale = new Vector3(-Mathf.Abs(carrying.localScale.x), carrying.localScale.y, carrying.localScale.z);
                carryingSpawner.velocity = -Mathf.Abs(carryingSpawner.velocity);
            } else {
                carrying.localScale = new Vector3(Mathf.Abs(carrying.localScale.x), carrying.localScale.y, carrying.localScale.z);
                carryingSpawner.velocity = Mathf.Abs(carryingSpawner.velocity);
            }
            for (int i = 0; i < regularRenderers.Length; i++) {
                if (!phonographRenderers[i].enabled) {
                    phonographRenderers[i].enabled = true;
                    regularRenderers[i].enabled = false;
                }
            }
        } else {
            for (int i = 0; i < regularRenderers.Length; i++) {
                if (!regularRenderers[i].enabled) {
                    regularRenderers[i].enabled = true;
                    phonographRenderers[i].enabled = false;
                }
            }
        }
    }

    private static float AverageABSParentXY(Transform child) {
        Transform parent = child.parent;
        return (Mathf.Abs(parent.localScale.x) + Mathf.Abs(parent.localScale.y)) / 2;
    }

}