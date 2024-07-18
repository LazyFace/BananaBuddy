using UnityEngine;

public class BananaCollector : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        Notify(Actions.BananaPicked);
        AudioManager.Instance.PlaySFX("BananaPicked");
        gameObject.SetActive(false);
    }
}
