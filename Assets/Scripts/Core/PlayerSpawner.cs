using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public PlayerSelection[] characterSelection;

	// Use this for initialization
	void Start()
    {
        var equippedWeapon = Game.instance.GetEquippedWeapon();
        foreach (var character in characterSelection)
        {
            if (character.weaponID == equippedWeapon.weaponID)
            {
                Instantiate(character.prefab, transform.position, transform.rotation);

                break;
            }
        }
    }
}

[System.Serializable]
public class PlayerSelection : System.Object
{
    public string weaponID;
    public GameObject prefab;
}
