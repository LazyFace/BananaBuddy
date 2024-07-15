using UnityEngine;

public class BananaCollector : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        Notify(Actions.BananaPicked);
        gameObject.SetActive(false);
    }
}
