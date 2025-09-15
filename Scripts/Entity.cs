using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponentInChildren<Animator>();
    }
    
    public void SetVelocity(float xv, float zv)
    {
        rb.linearVelocity = new Vector3(xv, 0, zv);
    }
    public void ZeroVelocity() => SetVelocity(0, 0);

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}
