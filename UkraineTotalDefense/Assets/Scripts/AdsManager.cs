using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    private AdsInitializer adsInitializer;
    private InterstitialAd interstitialAd;
    
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        adsInitializer = GetComponent<AdsInitializer>();
        interstitialAd = GetComponent<InterstitialAd>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.PauseGame();
        adsInitializer.InitializeAds();
    }

    public void LoadAndShowAd()
    {
        interstitialAd.LoadAd();
        interstitialAd.ShowAd();
    }
}
