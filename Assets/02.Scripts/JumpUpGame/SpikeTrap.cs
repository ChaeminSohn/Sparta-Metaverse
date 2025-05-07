using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private static readonly int IsSet = Animator.StringToHash("IsSet");

    private bool isSet = false;
    private float timeSinceLastChange = 0f;
    private float changeDelay = 5f;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();    
    }
    void Update()
    {
        if (timeSinceLastChange < changeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
        }
        else
        {
            timeSinceLastChange = 0f;
            ChangeSpikeSet();
        }
    }
    void ChangeSpikeSet()
    {    
       animator.SetBool(IsSet, !isSet);
        isSet = !isSet;
    }
}
