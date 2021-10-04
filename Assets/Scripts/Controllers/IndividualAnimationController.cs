using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IndividualAnimationController : MonoBehaviour
{
    public List<GameObject> Individuals = new List<GameObject>();
    public IndividualDictionary IndividualDictionary = new IndividualDictionary();

    private float timer;

    public Vector2 AnimatonRange;
    private void FixedUpdate()
    {
        timer += 0.05f;
        if(timer > 1)
        {         
            RandomSelector();
        }
    }

    private void RandomSelector()
    {
        GameObject RandomIndividual = Individuals[Random.Range(0, Individuals.Count)];

        if (!IndividualDictionary[RandomIndividual])
            StartCoroutine(RandomJumperDelay(RandomIndividual));

        timer = 0;
    }

    public void JumpingAnimation(GameObject Individual)
    {
        float animationDuration = Random.Range(AnimatonRange.x, AnimatonRange.y);

        IndividualDictionary[Individual] = true;
        Individual.transform.DOLocalMoveY(6f, animationDuration).SetEase(Ease.OutQuad).OnComplete(()=> {
            Individual.transform.DOLocalMoveY(0f, animationDuration).SetEase(Ease.InQuad);
        });

        Individual.transform.DOLocalRotate(new Vector3(-70, 0, 0), animationDuration).SetEase(Ease.OutQuad).OnComplete(() => {
            Individual.transform.DOLocalRotate(new Vector3(70, 0, 0), animationDuration).SetEase(Ease.InQuad).OnComplete(() => {
                Individual.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear).OnComplete(()=> IndividualDictionary[Individual] = false);
            });
        });

        Individual.transform.DOScale(Vector3.one*1.25f, animationDuration).SetEase(Ease.Linear).SetLoops(1,LoopType.Yoyo);
    }

    IEnumerator RandomJumperDelay(GameObject Individual)
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        JumpingAnimation(Individual);
    }
}
[System.Serializable]
public class IndividualDictionary : UnitySerializedDictionary<GameObject, bool> { }