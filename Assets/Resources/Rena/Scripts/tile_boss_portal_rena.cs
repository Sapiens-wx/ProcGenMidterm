using System.Collections;
using UnityEngine;

public class tile_boss_portal_rena : Tile
{
    public bool isActivated = false;
    private Animator ani;
    public Tile bullet;
    private int level = 1;

    [Header("Phase 1")]
    public int bulletCountL1 = 16;
    public float bulletSpeedL1 = 3f;
    public float bulletLifetimeL1 = 3f;
    public int bulletHealthL1 = 1;
    public int roundsL1 = 8;
    public float shootIntervalL1 = 0.5f;
    public float groupIntervalL1 = 2f;
    public int healthToL2 = 14;
    public float devAngleL1 = 30f;
    private int triggerCountL1 = 0;

    [Header("Phase 2")]
    public int bulletCountL2 = 8;
    public float bulletSpeedL2 = 3f;
    public float bulletLifetimeL2 = 3f;
    public int bulletHealthL2 = 2;
    public int roundsL2 = 5;
    public float shootIntervalL2 = 0.2f;
    public float groupIntervalL2 = 2f;
    public int healthToL3 = 6;
    public float devAngleL2 = 30f;
    private int triggerCountL2 = 0;
    
    [Header("Phase 3")]
    public int bulletCountL3 = 8;
    public float bulletSpeedL3 = 3f;
    public float bulletLifetimeL3 = 3f;
    public int bulletHealthL3 = 2;
    public int roundsL3 = 3;
    public float shootIntervalL3 = 0.2f;
    public float groupIntervalL3 = 2f;
    public float devAngleL3 = 30f;
    private int triggerCountL3 = 0;
    public float timeBeforeSplit = 2.5f;
    

    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.enabled = false;
    }

    public void activate()
    {
        isActivated = true;
        ani.enabled = true;
        StartCoroutine(StartFight());
    }

    private IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1f);
        while (isActivated)
        {
            yield return StartCoroutine(DoLevel(level));
        }
    }

    private IEnumerator DoLevel(int l)
    {
        HealthCheck();
        switch (l)
        {
            case 1:
                yield return StartCoroutine(Level1()); // 测试
                break;
            case 2:
                yield return StartCoroutine(Level2());
                break;
            case 3:
                yield return StartCoroutine(Level3());
                break;
        }
    }

    private IEnumerator Level1()
    {
        for (int r = 0; r < roundsL1; r++)
        {
            float angleStep = 360f / bulletCountL1;
            for (int i = 0; i < bulletCountL1; i++)
            {
                float angle = angleStep * i + devAngleL1 * triggerCountL3;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Tile newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<tile_bullet_rena>().Shoot(dir, bulletSpeedL1, bulletLifetimeL1, bulletHealthL1);
            }
            yield return new WaitForSeconds(shootIntervalL1);
        }

        triggerCountL1 ++;
        yield return new WaitForSeconds(groupIntervalL1);
    }

    private IEnumerator Level2()
    {
        for (int r = 0; r < roundsL2; r++)
        {
            float angleStep = 360f / bulletCountL2;
            for (int i = 0; i < bulletCountL2; i++)
            {
                float angle = angleStep * i + devAngleL2 * triggerCountL2;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Tile newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<tile_bullet_rena>().Shoot(dir, bulletSpeedL2, bulletLifetimeL2, bulletHealthL2);
            }
            triggerCountL2 ++;
            yield return new WaitForSeconds(shootIntervalL2);
        }
        yield return new WaitForSeconds(groupIntervalL2);
    }
    
    private IEnumerator Level3()
    {
        for (int r = 0; r < roundsL3; r++)
        {
            float angleStep = 360f / bulletCountL3;
            for (int i = 0; i < bulletCountL3; i++)
            {
                float angle = angleStep * i + devAngleL3 * triggerCountL3;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Tile newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<tile_bullet_rena>().ShootSplit(dir, bulletSpeedL3, bulletLifetimeL3, bulletHealthL3, timeBeforeSplit,bulletCountL3);
            }
            triggerCountL3 ++;
            yield return new WaitForSeconds(shootIntervalL3);
        }
        yield return new WaitForSeconds(groupIntervalL3);
    }

    private void HealthCheck()
    {
        if (health <= healthToL3)
        {
            level = 3;
        }
        else if (health <= healthToL2)
        {
            level = 2;
        }
        else
        {
            level = 1;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Tile otherTile = other.gameObject.GetComponent<Tile>();
        if (otherTile != null)
        {
            if (otherTile.hasTag(TileTags.Weapon))
            {
                takeDamage(this, 1);
            }
        }
    }
}