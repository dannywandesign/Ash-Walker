using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    public void PressGrowButton()
    {
        if (AbilityManager.instance != null)
        {
            AbilityManager.instance.GrowPlayer();
        }

        CloseShop();
    }

    private void CloseShop()
    {
        if (GetComponentInParent<GlobalShop>() != null)
        {
            GetComponentInParent<GlobalShop>().ToggleShop();
        }
    }
}