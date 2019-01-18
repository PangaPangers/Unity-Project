using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] compToDisable;

    Camera sceneCam;

    void Start()
    {
        
        if (!isLocalPlayer)
        {
            for(int i = 0; i< compToDisable.Length; i++)
            {
                compToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCam = Camera.main;
            if (sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }

    }

    void OnDisable()
    {
        
        if (sceneCam != null)
        {
            sceneCam.gameObject.SetActive(true);
        }
    }

}
