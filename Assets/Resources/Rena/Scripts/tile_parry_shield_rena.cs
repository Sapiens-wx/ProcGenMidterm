
using System.Collections;
using UnityEngine;

public class tile_parry_shield : Tile
{
    public GameObject shield;
    public float duration = 0.05f;
    private void Start()
    {
        shield.SetActive(false);
    }

    public override void useAsItem(Tile tileUsingUs)
    {
        StartCoroutine(ActivateShield());
    }

    private IEnumerator ActivateShield()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(duration);
        shield.SetActive(false);
    }
    
    
}
