using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}
