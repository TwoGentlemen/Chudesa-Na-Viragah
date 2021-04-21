using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private Transform centreMass;
    [SerializeField] private WheelCollider[] whewls;
    [SerializeField] private Transform[] wheelMesh;

    
   private Rigidbody rb;


   private void Start() 
   {
       rb = GetComponent<Rigidbody>();
       rb.centerOfMass = centreMass.localPosition;
   }
    private void RotAndPosMeshWheel()
    {
        Vector3 pos;
        Quaternion rot;
        for (int i = 0; i < whewls.Length; i++)
        {
            whewls[i].GetWorldPose(out pos, out rot);
            wheelMesh[i].position = pos;
            wheelMesh[i].rotation = Quaternion.Euler(0, -90, rot.eulerAngles.x);
        }
    }
    private void Move()
    {
        float vert = Input.GetAxis("Vertical");

        for (int i = 0; i < whewls.Length; i++)
        {
            whewls[i].motorTorque = vert*speed;
        }

    }
   private void Update() 
   {
   
        RotAndPosMeshWheel();
        Move();
   }
}
