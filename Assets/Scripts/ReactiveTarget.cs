using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit()
    {
        WanderingAI enemyAI = GetComponent<WanderingAI>();
        if (enemyAI != null)
        {
            enemyAI.ChangeState(EnemyStates.dead);
        }

        Animator enemyAnimator = GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }

        //StartCoroutine(Die());
    }

    private void DeadEvent()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator Die()
    {
        //iTween.RotateAdd(this.gameObject, new Vector3(-75, 0, 0), 1);

        // Enemy falls over and disappears after two seconds
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
