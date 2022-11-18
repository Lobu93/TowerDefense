using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public delegate void UpgradeDelegate();
    public UpgradeDelegate upgradeDelegate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpgradeUnit()
    {
        if (upgradeDelegate != null)
        {
            upgradeDelegate();
            print("Upgrade Button pressed! " + gameObject);
        }
        else
        {
            Debug.LogError("All delegates clear...");
        }
    }
}
