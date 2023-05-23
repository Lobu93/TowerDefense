using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "5279851";
    [SerializeField] string _iOSGameId = "5279850";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    private AdsManager adsManager;

    void Awake()
    {
        // InitializeAds();

        adsManager = GetComponent<AdsManager>();
    }

    public void InitializeAds()
    {
    #if UNITY_IOS
        _gameId = _iOSGameId;
    #elif UNITY_ANDROID
        _gameId = _androidGameId;
    #elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
    #endif
    
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else if (Advertisement.isInitialized)
        {
            adsManager.LoadAndShowAd();
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        GetComponent<AdsManager>().LoadAndShowAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
