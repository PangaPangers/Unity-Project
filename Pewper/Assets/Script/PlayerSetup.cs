using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
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

        

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
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

        GameManager.UnRegisterPlayer(transform.name);
    }

}
