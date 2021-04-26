using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{

    [Space(5)]
    [SerializeField] private float speed = 1f;

    [Range(10,100)]
    [SerializeField] public int tankFuel = 40; //������������ ����� ���� ��� �������
    [Tooltip("L/sec")]
    [Range(1,10)]
    [SerializeField] public int fuelConsuption = 1; //������ ������� �/���
    
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
            wheelMesh[i].rotation = rot;
        }
    }

    private void Move()
    {
        float vert = Input.GetAxis("Vertical");

        for (int i = 0; i < whewls.Length; i++)
        {
            whewls[i].motorTorque = Mathf.Min(vert*speed*100 / Mathf.Max(Mathf.Abs(whewls[i].rpm),1),speed);
            if(whewls[i].rpm > speed )
            {
                whewls[i].motorTorque = 100*-whewls[i].rpm/speed;
            }

            if (whewls[i].rpm < -speed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / speed;
            }

            Debug.Log("motor = "+whewls[i].motorTorque);
        }

    }

    private void FixedUpdate() 
    {  
        RotAndPosMeshWheel();
        Move();
    }

    public void StopCar()
    {
        rb.isKinematic = true;
        this.enabled = false;
    }

   
}
