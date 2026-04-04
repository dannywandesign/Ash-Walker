using UnityEngine;
using TMPro;

public partial class ShardManager : MonoBehaviour
{
    public static ShardManager instance;

    public int shardCount = 0;
    public int totalShardsInLevel = 0; 
    public TextMeshProUGUI shardText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        if (shardText == null)
            shardText = GameObject.Find("ShardCounterText")?.GetComponent<TextMeshProUGUI>();

        Shard[] allShards = Object.FindObjectsByType<Shard>(FindObjectsSortMode.None);
        totalShardsInLevel = allShards.Length;

        UpdateUI();
    }

    public void AddShard(int amount)
    {
        shardCount += amount;
        UpdateUI();
    }

    public void ResetShards()
    {
        shardCount = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (shardText != null)
        {
            shardText.text = "SHARDS: " + shardCount + "/" + totalShardsInLevel;
        }
    }
}