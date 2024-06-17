using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

namespace RoadCrossing
{
    public class CanvasGameOverManager : MonoBehaviour
    {
        public Button buttonRestart;
        public Button buttonMainMenu;
        public Button buttonRanking;

        public Text claimingStatusTxt;
        public Text globalRanking;

        public string Address { get; private set; }

        void Start()
        {
            buttonRestart.gameObject.SetActive(true);
            buttonRestart.interactable = true;
            buttonMainMenu.gameObject.SetActive(true);
            buttonMainMenu.interactable = false;
            buttonRanking.gameObject.SetActive(true);
            buttonRanking.interactable = true;
            claimingStatusTxt.text = "";
            GetRank();
        }

        public async void SubmitScore()
        {
            buttonRestart.interactable = false;
            buttonMainMenu.interactable = false;
            buttonRanking.interactable = false;
            claimingStatusTxt.text = "Claiming!";
            globalRanking.text = "Global Ranking: ...";
            Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
            var contract = ThirdwebManager.Instance.SDK.GetContract(
                    "0x39C5083b315Ea951896e8479D83E0A61F9f1Baf6",
                    "[{\"type\":\"event\",\"name\":\"ScoreAddedd\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"_scores\",\"inputs\":[{\"type\":\"address\",\"name\":\"\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getRank\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"rank\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"submitScore\",\"inputs\":[{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"}]"
                );

            GameObject gameController = GameObject.Find("GameController");
            if (gameController != null)
            {
                RCGGameController rcgGameController = gameController.GetComponent<RCGGameController>();
                if (rcgGameController != null)
                {
                    await contract.Write("submitScore", (int)rcgGameController.score);
                    buttonRestart.interactable = true;
                    buttonMainMenu.interactable = true;
                    GetRank();
                }
                else
                {
                    Debug.LogError("rcgGameController Error");
                }
            }
            else
            {
                Debug.LogError("gameController Error");
            }
        }

        internal async void GetRank()
        {
            buttonRestart.interactable = false;
            buttonMainMenu.interactable = false;
            buttonRanking.interactable = false;
            globalRanking.text = "Global Ranking: ...";
            Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
            var contract = ThirdwebManager.Instance.SDK.GetContract(
                "0x39C5083b315Ea951896e8479D83E0A61F9f1Baf6",
                "[{\"type\":\"event\",\"name\":\"ScoreAddedd\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"_scores\",\"inputs\":[{\"type\":\"address\",\"name\":\"\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getRank\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"rank\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"submitScore\",\"inputs\":[{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"}]"
                );
            var rank = await contract.Read<int>("getRank", Address);
            Debug.Log($"Rank for address {Address} is {rank}");
            globalRanking.text = "Global Ranking: " + rank.ToString();
            claimingStatusTxt.text = "Claimed!";
            claimingStatusTxt.text = "";
            buttonRestart.interactable = true;
            buttonMainMenu.interactable = true;
            buttonRanking.interactable = true;
        }
    }
}


