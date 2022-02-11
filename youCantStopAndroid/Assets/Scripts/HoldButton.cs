using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using UnityEngine.UI;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Car car;

    private bool pointerDown;

    public float steerSide;


    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }


    void Start()
    {
        car = FindObjectOfType<Car>();
        Reset();
    }

    private void Update()
    {
        if (pointerDown)
        {
            if(steerSide != 0)
            {
                car.Steer = steerSide;
            }
            else
            {
                car.Break = true;
            }
        }
        


    }

    private void Reset()
    {

        car.Steer = 0;
        car.Break = false;
        pointerDown = false;

    }

}
