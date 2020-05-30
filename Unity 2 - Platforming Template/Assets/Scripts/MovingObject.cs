using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float XSpeed;
    public float ySpeed;
    public float duration;

    private Vector3 StartPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = gameObject.transform.position;
        InvokeRepeating("StartAndEndMove", 0f, duration * 3);
    }

    public IEnumerator Move(float XSpeed, float ySpeed, float duration)
    {
        for (int i = 0; i < duration * 250; i++)
        {
            yield return new WaitForSeconds(1 / 250 - Time.deltaTime);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + XSpeed / 250, gameObject.transform.position.y + ySpeed / 250, gameObject.transform.position.z);
        }
    }

    public IEnumerator MoveToEndToStart()
    {
        StartCoroutine(Move(XSpeed, ySpeed, duration));
        yield return new WaitForSecondsRealtime(duration);
        StartCoroutine(Move((-1) * XSpeed, (-1) * ySpeed, duration));
        yield return new WaitForSecondsRealtime(duration);
    }

    public void StartAndEndMove()
    {
        StartCoroutine(MoveToEndToStart());
        gameObject.transform.position = StartPos;
    }
}
