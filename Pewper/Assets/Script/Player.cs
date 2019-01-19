using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    void Awake()
    {
        SetDefaults();
    }


    [ClientRpc]
    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " Now has " + currentHealth + " Health.");
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

   


}
