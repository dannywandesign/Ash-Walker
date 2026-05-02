using UnityEngine;

public class ScrollCollectible : MonoBehaviour
{
    [Header("Unique ID")]
    public string UniqueID;

    void Start()
    {
        if (PlayerPrefs.GetInt(UniqueID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(UniqueID, 1);
            PlayerPrefs.Save();

            Debug.Log("Collected Scroll: " + UniqueID);

            Destroy(gameObject);
        }
    }
}