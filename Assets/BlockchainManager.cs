using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

public class BlockchainManager : MonoBehaviour
{
    public Button buttonNFTTokenGate;
    public Button buttonStartGame;
    public Button buttonShop;
    public Button buttonBlockchain;

    public Text description;
    public string Address { get; private set; }

    // Start is called before the first frame update
    private void ResetAllButton()
    {
        buttonNFTTokenGate.gameObject.SetActive(false);
        buttonStartGame.gameObject.SetActive(false);
        buttonShop.gameObject.SetActive(false);
        buttonBlockchain.gameObject.SetActive(false);

        buttonNFTTokenGate.interactable = true;
        buttonStartGame.interactable = true;
        buttonShop.interactable = true;
        buttonBlockchain.interactable = true;

        description.text = "Please help the poor little frog find ETH Base coins";
    }

    public async void Login()
    {
        ResetAllButton();
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Debug.Log(Address);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x4e4d3095E9B031fce665121122cc3B7A4963a94e");
        List<NFT> nftList = await contract.ERC721.GetOwned(Address);
        if (nftList.Count == 0)
        {
            description.text = "Claim 1 Token Gate NFT for Playing";
            buttonNFTTokenGate.gameObject.SetActive(true);
            buttonStartGame.gameObject.SetActive(false);
            buttonShop.gameObject.SetActive(false);
            buttonBlockchain.gameObject.SetActive(false);
        }
        else
        {
            description.text = "Please help the poor little frog find ETH Base coins";
            buttonNFTTokenGate.gameObject.SetActive(false);
            buttonStartGame.gameObject.SetActive(true);
            buttonShop.gameObject.SetActive(true);
            buttonBlockchain.gameObject.SetActive(true);
        }
    }

    public async void ClaimNFTPass()
    {
        buttonNFTTokenGate.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x4e4d3095E9B031fce665121122cc3B7A4963a94e");
        var result = await contract.ERC721.ClaimTo(Address, 1);
        buttonNFTTokenGate.gameObject.SetActive(false);
        buttonStartGame.gameObject.SetActive(true);
        buttonShop.gameObject.SetActive(true);
        buttonBlockchain.gameObject.SetActive(true);
        description.text = "Please help the poor little frog find ETH Base coins";
    }
}
