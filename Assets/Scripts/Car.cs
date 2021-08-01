using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    enum TypePrivod
    {
        front_wheelDrive = 0,
        rear_wheelDrive = 1,
        all_wheelDrive = 2,
    }

    
    [Space(5)]
    [Header("Parametors")]
    [SerializeField] private float maxSpeed = 1f; //макс скорость
    [SerializeField] private float motorTorque = 200; //мощьность двигателя 
    [SerializeField] private TypePrivod typePrivod = TypePrivod.all_wheelDrive; //тип привода

    [Range(1,255)]
    [SerializeField] public byte tankFuel = 40; //Объем бака для топлива
    
    [Space(20)]
    [SerializeField] private Transform centreMass;
    [SerializeField] private WheelCollider[] whewls;
    [SerializeField] private Transform[] wheelMesh;
    

    private Rigidbody rb;
    private bool isMove = true;


    private void Start() 
    {
       rb = GetComponent<Rigidbody>();
       rb.centerOfMass = centreMass.localPosition;
        isMove = true;
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

    //Передний привод
    private void FrontWheelDrive(float speed)
    {
        for (int i = 0; i < 2; i++)
        {
            whewls[i].motorTorque = Mathf.Min(speed * maxSpeed * 100 / Mathf.Max(Mathf.Abs(whewls[i].rpm), 1), maxSpeed);

            if (whewls[i].rpm > maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }

            if (whewls[i].rpm < -maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }
        }
    }
    //Задний привод
    private void RearWheelDrive(float speed)
    {
        Debug.Log(speed +"RearWheel");
        for (int i = 2; i < whewls.Length; i++)
        {
            whewls[i].motorTorque = Mathf.Min(speed * maxSpeed * 100 / Mathf.Max(Mathf.Abs(whewls[i].rpm), 1), maxSpeed);

            if (whewls[i].rpm > maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }

            if (whewls[i].rpm < -maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }
        }
    }
    //Полный привод
    private void AllWheelDrive(float speed)
    {
        for (int i = 0; i < whewls.Length; i++)
        {
            whewls[i].motorTorque = Mathf.Min(speed * maxSpeed * 100 / Mathf.Max(Mathf.Abs(whewls[i].rpm), 1), maxSpeed);

            if (whewls[i].rpm > maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }

            if (whewls[i].rpm < -maxSpeed)
            {
                whewls[i].motorTorque = 100 * -whewls[i].rpm / maxSpeed;
            }
        }
    }

    private void Move()
    {
        if (!isMove) { return; }
        float vert = Input.GetAxisRaw("Vertical");
        
        //if(vert == 0 && GameManager.instance !=null)
        //{
        //    vert=GameManager.instance.Gaz();
        //}

        switch (typePrivod)
        {
            case TypePrivod.front_wheelDrive:
                FrontWheelDrive(vert);
                break;
            case TypePrivod.rear_wheelDrive:
                RearWheelDrive(vert);
                break;
            case TypePrivod.all_wheelDrive:
                AllWheelDrive(vert);
                break;
            default:
                break;
        }

    }

    private void FixedUpdate() 
    {
        RotAndPosMeshWheel();
        Move();
    }

    public void StopCar()
    {

        isMove = false;

        for (int i = 0; i < whewls.Length; i++)
        {
            whewls[i].motorTorque =0;

        }
    }

   
}
