using UnityEngine;

public class Entity : MonoBehaviour
{
    //public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }

    protected virtual void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}
