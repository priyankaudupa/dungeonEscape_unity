using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject notEnoughGem;
    private int currentSelelectedItem;
    private int currentItemCost;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
             _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
       // Debug.Log("selected item");
       switch(item)
       {
        case 0:
            UIManager.Instance.UpdateShopSelection(70);
            currentSelelectedItem =0;
            currentItemCost = 250;
            break;
        case 1:
            UIManager.Instance.UpdateShopSelection(-29);
            currentSelelectedItem =1;
            currentItemCost = 400;
            break;
        case 2:
            UIManager.Instance.UpdateShopSelection(-126);
            currentSelelectedItem =2;
            currentItemCost = 200;
            break;
       }
    }

    public void BuyItem()
    {
        if(_player.diamonds >= currentItemCost)
        {
            if(currentSelelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= currentItemCost;
           // Debug.Log("purchased Item :" + currentSelelectedItem);
         //   Debug.Log("Reamining gem :" + _player.diamonds);
        }
        else{
           // Debug.Log("you do not have enough diamond");
            notEnoughGem.SetActive(true);
        }
    }

    public void cancelButton()
    {
        notEnoughGem.SetActive(false);
    }
}
