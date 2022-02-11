using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    public event System.Action OnPlayerDeath;

    public Text speedText;

    public Transform crashCar;
    public bool hasCollide = false;

    public Transform centerOfMass;

    public float torque = 1000f;
    public float maxSteer = 20f;
    public float breakTorque = 100f;


    [Header("Wheels forward friction")] //FF = Forward Friction
    public float FFExtremumSlip = 5f;
    public float FFExtremumValue = 8f;
    public float FFAsymptoteSlip = 0.8f;
    public float FFAsymptoteValue = 0.5f;
    public float FFStiffness = 1f;

    [Header("Wheels sideways friction")] //SF = Sideways Friction
    public float SFExtremumSlip = 5f;
    public float SFExtremumValue = 8f;
    public float SFAsymptoteSlip = 0.8f;
    public float SFAsymptoteValue = 0.5f;
    public float SFStiffness = 1f;


    public float Steer;

    public float Throttle { get; set; }

    public bool Break { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    private WheelCollider[] wheelColliders;

    private Rigidbody[] parts;

    private Vector3 lastPosition = Vector3.zero;

    void Start()
    {

        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;

        wheelColliders = GetComponentsInChildren<WheelCollider>();

        WheelFrictionCurve forwardFrictionCurve = wheelColliders[0].forwardFriction;
        forwardFrictionCurve.extremumSlip = FFExtremumSlip;
        forwardFrictionCurve.extremumValue = FFExtremumValue;
        forwardFrictionCurve.asymptoteSlip = FFAsymptoteSlip;
        forwardFrictionCurve.asymptoteValue = FFAsymptoteValue;
        forwardFrictionCurve.stiffness = FFStiffness;

        WheelFrictionCurve sidewaysFrictionCurve = wheelColliders[0].sidewaysFriction;
        sidewaysFrictionCurve.extremumSlip = SFExtremumSlip;
        sidewaysFrictionCurve.extremumValue = SFExtremumValue;
        sidewaysFrictionCurve.asymptoteSlip = SFAsymptoteSlip;
        sidewaysFrictionCurve.asymptoteValue = SFAsymptoteValue;
        sidewaysFrictionCurve.stiffness = SFStiffness;

        foreach (var wheel in wheelColliders)
        {
            wheel.forwardFriction = forwardFrictionCurve;
            wheel.sidewaysFriction = sidewaysFrictionCurve;

        }

    }

    void FixedUpdate()
    {

        speedText.text = " Speed: " + Speed().ToString("f0") + " km/h";

        if(transform.position.y<-15f)
        {
            hasCollide = true;
            OnPlayerDeath();
            Destroy(gameObject);
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        Throttle = 1;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * torque;

            if(Break == true)
            {
                wheel.Break = breakTorque;
            }
            else
            {
                wheel.Break = 0;
            }
            
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Obstacle"))
        {
            if(hasCollide == false)
            {
                hasCollide = true;

                OnPlayerDeath();


                Transform newCarBody = Instantiate(crashCar, transform.position, transform.rotation);
                parts = newCarBody.GetComponentsInChildren<Rigidbody>();
                foreach(Rigidbody part in parts)
                {
                    part.AddForce(-collision.relativeVelocity * ((int)Speed()/2));
                }

                Debug.Log(collision.relativeVelocity);
                Debug.Log(collision.relativeVelocity.magnitude);
                Debug.Log(collision.collider.tag);

                Destroy(gameObject);
                
            }          
        }
    }

    public float Speed()
    {
        if(hasCollide == false)
        {
            return wheelColliders[0].radius * 3.14f * wheelColliders[0].rpm * 0.06f;// 60f / 1000f;
        }
        else
        {
            return 0;
        }
    }

    float motorTorque()
    {
        return torque;
    }
}
