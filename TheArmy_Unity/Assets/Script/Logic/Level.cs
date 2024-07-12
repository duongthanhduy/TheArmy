using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{

    [SerializeField] List<Brick> Nomalbricks;
    [SerializeField] List<Brick> Specialbricks;
    [SerializeField] List<Transform> PosSpawnEnemy;
    public float NomalBrickHealth = 1;
    public float SpecialBrickHealth => NomalBrickHealth *2;

    private void Start()
    {
        for (int i = 0 ; i < Nomalbricks.Count;i++) {
            Nomalbricks[i].SetHealth(NomalBrickHealth);
        }

        for (int i = 0 ; i < Specialbricks.Count;i++) {
            Specialbricks[i].SetHealth(SpecialBrickHealth);
        }
        GameController.Instance.AddBrick(Nomalbricks);
        GameController.Instance.AddBrick(Specialbricks);
    }
    public List<Transform> GetEnemySpawnPos() {
        return PosSpawnEnemy;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < Nomalbricks.Count; i++)
            {
                
               StartCoroutine(DelayDead(i * 0.02f, Nomalbricks[i]));
            }

        }
    }

    private IEnumerator DelayDead(float delay, Brick _brick) {
        yield return new WaitForSeconds(delay);
        _brick.Dead();
    }
#endif
}
