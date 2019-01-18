using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] compToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCam;

    void Start()
    {
        
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCam = Camera.main;
            if (sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }

        RegisterPlayer();

    }

    void RegisterPlayer()
    {
        string _ID = "Player: " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }


    void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < compToDisable.Length; i++)
        {
            compToDisable[i].enabled = false;
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
