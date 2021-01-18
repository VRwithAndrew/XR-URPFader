using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportAnchorWithFade : TeleportationAnchor
{
    public ScreenFade screenFade = null;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (teleportTrigger == TeleportTrigger.OnSelectEntered)
            StartCoroutine(FadeSequence(base.OnSelectEntered, interactor));
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        if (teleportTrigger == TeleportTrigger.OnSelectExited)
            StartCoroutine(FadeSequence(base.OnSelectExited, interactor));
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        if (teleportTrigger == TeleportTrigger.OnActivate)
            StartCoroutine(FadeSequence(base.OnActivate, interactor));
    }

    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        if (teleportTrigger == TeleportTrigger.OnDeactivate)
            StartCoroutine(FadeSequence(base.OnDeactivate, interactor));
    }

    private IEnumerator FadeSequence(UnityAction<XRBaseInteractor> actionEvent, XRBaseInteractor interactor)
    {
        // Fade to black
        float duration = screenFade.FadeIn();

        // Wait, then do the teleport stuff
        yield return new WaitForSeconds(duration);
        actionEvent.Invoke(interactor);

        // Fade to clear
        screenFade.FadeOut();
    }

    private void OnValidate()
    {
        // Uhhh, let's get the screen fader like this
        if (!screenFade)
            screenFade = FindObjectOfType<ScreenFade>();
    }
}

