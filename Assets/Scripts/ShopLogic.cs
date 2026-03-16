using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    public void BuyDash()
    {
        if (ShardManager.instance.shardCount >= 15)
        {
            ShardManager.instance.RemoveShards(15);
            AbilityManager.instance.canDash = true;
            CloseShop();
        }
    }

    public void BuyGlide()
    {
        if (ShardManager.instance.shardCount >= 10)
        {
            ShardManager.instance.RemoveShards(10);
            AbilityManager.instance.canGlide = true;
            CloseShop();
        }
    }

    public void BuyDoubleJump()
    {
        // UPDATED: Price changed from 25 to 20 shards
        if (ShardManager.instance.shardCount >= 20)
        {
            ShardManager.instance.RemoveShards(20);
            AbilityManager.instance.canDoubleJump = true;
            CloseShop();
        }
    }

    public void GrowUp()
    {
        int randomShards = Random.Range(5, 11);
        for (int i = 0; i < randomShards; i++)
        {
            ShardManager.instance.AddShard();
        }
        AbilityManager.instance.BecomeMature();
        CloseShop();
    }

    private void CloseShop()
    {
        // This finds the GlobalShop script on the parent and toggles it off
        GetComponentInParent<GlobalShop>().ToggleShop();
    }
}