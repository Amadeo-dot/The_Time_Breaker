using UnityEngine;
using System.Collections.Generic;

public class RewindObject : MonoBehaviour
{
    
    private bool isRewinding = false;
    private Rigidbody rb;
    private List<TransformData> history = new List<TransformData>();
    
    [SerializeField] private float recordTime = 5f;

    void Record()
    {
        if(history.Count > recordTime / Time.fixedDeltaTime)
        {
            history.RemoveAt(history.Count - 1);
        }

        history.Insert (0, new TransformData(transform.position, transform.rotation));
    }

    void Rewind()
    {
        if(history.Count > 0)
        {
            TransformData data = history[0];
            transform.position = data.position;
            transform.rotation = data.rotation;
            history.RemoveAt(0);
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        if(rb != null)
            rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        if(rb != null)
            rb.isKinematic = false;
    }

    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ManejoTiempo.Register(this);
    }

    void OnDestroy()
    {
        ManejoTiempo.Unregister(this);
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }
}
