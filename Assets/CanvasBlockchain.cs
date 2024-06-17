using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

public class CanvasBlockchain : MonoBehaviour
{
    public Button buttonGold;
    public Button buttonHP;
    public Button buttonx2Gold;
    public Button buttonAccept;

    public Text ClaimingStatusText;

    public string Address { get; private set; }

    // Start is called before the first frame update
    public void OnWalletConnected()
    {
        buttonGold.gameObject.SetActive(true);
        buttonHP.gameObject.SetActive(true);
        buttonx2Gold.gameObject.SetActive(true);
        buttonAccept.gameObject.SetActive(true);

        buttonGold.interactable = true;
        buttonHP.interactable = true;
        buttonx2Gold.interactable = true;
        buttonAccept.interactable = true;

        ClaimingStatusText.text = "";
    }

    public async void ClaimGold()
    {
        ClaimingStatusText.text = "Claiming!";
        buttonGold.gameObject.SetActive(false);
        buttonHP.interactable = false;
        buttonGold.interactable = false;
        buttonx2Gold.interactable = false;
        buttonAccept.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xcb0C7E1CaE71A08006565a6cE645ae3e99FaF97b");
        var result = await contract.ERC20.Claim("1");

        BlockchainShopManager.Instance.gold = 100;

        Debug.Log("Gold claimed");
        ClaimingStatusText.text = "+100 Gold";
        buttonGold.interactable = true;
        buttonHP.interactable = true;
        buttonx2Gold.interactable = true;
        buttonAccept.interactable = true;
    }

    public async void ClaimHP()
    {
        ClaimingStatusText.text = "Claiming!";
        buttonHP.gameObject.SetActive(false);
        buttonHP.interactable = false;
        buttonGold.interactable = false;
        buttonx2Gold.interactable = false;
        buttonAccept.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xcb0C7E1CaE71A08006565a6cE645ae3e99FaF97b");
        var result = await contract.ERC20.Claim("1");

        BlockchainShopManager.Instance.hp = 1;

        Debug.Log("HP claimed");
        ClaimingStatusText.text = "+1 HP";
        buttonGold.interactable = true;
        buttonHP.interactable = true;
        buttonx2Gold.interactable = true;
        buttonAccept.interactable = true;
    }

    public async void Claimx2Gold()
    {
        ClaimingStatusText.text = "Claiming!";
        buttonx2Gold.gameObject.SetActive(false);
        buttonHP.interactable = false;
        buttonGold.interactable = false;
        buttonx2Gold.interactable = false;
        buttonAccept.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xcb0C7E1CaE71A08006565a6cE645ae3e99FaF97b");
        var result = await contract.ERC20.Claim("1");

        BlockchainShopManager.Instance.goldx2 = 2;

        Debug.Log("HP claimed");
        ClaimingStatusText.text = "+1 HP";
        buttonGold.interactable = true;
        buttonHP.interactable = true;
        buttonx2Gold.interactable = true;
        buttonAccept.interactable = true;
    }
}
