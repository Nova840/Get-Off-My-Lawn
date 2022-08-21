using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundIfRendererEnabled : MonoBehaviour {

    [SerializeField]
    private Renderer monitoredRenderer = null;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private bool wasRendererEnabled = false;
    private void Update() {
        if (!RendererEnabled() && wasRendererEnabled)//just disabled
            audioSource.Stop();
         else if (RendererEnabled() && !wasRendererEnabled)//just enabled
            audioSource.Play();

        wasRendererEnabled = RendererEnabled();
    }

    private bool RendererEnabled() {
        return monitoredRenderer && monitoredRenderer.enabled;
    }

}