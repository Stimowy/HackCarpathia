using UnityEngine;

public class universalActivate : MonoBehaviour
{
    public void pokaz(string TargetTag, Canvas other)
    {
        if (other.CompareTag(TargetTag))
        {
            other.gameObject.SetActive(true);
        }
    }

    public void schowaj(string TargetTag, Canvas other)
    {
        if (other.CompareTag(TargetTag))
        {
            other.gameObject.SetActive(false);
        }
    }

}
