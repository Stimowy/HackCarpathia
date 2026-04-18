using UnityEngine;

public class openDoor : MonoBehaviour
{
    [Header("drzwi ustawienia:")]
    [SerializeField] private Animation animationDrzwi;

    [Header("klip animacje:")]
    [SerializeField] private AnimationClip openClip;
    [SerializeField] private AnimationClip closeClip;

    private void Start()
    {
        if (openClip != null) animationDrzwi.AddClip(openClip, openClip.name);
        if (closeClip != null) animationDrzwi.AddClip(closeClip, closeClip.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        animationDrzwi.Play(openClip.name);
    }

    private void OnTriggerExit(Collider other)
    {
        animationDrzwi.Play(closeClip.name);
    }
}
