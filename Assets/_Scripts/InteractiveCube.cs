using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveCube : MonoBehaviour
{

    private float timer = 0;
    private bool gazedAt = false;
    private float gazeTime = GazeTimer.GazeTime;
    private float minX = -10f, minY = 0.5f, minZ = -12.5f, maxX = 10.8f, maxY = 3f, maxZ = 10.2f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
            Transform child = transform.GetChild(0);
            Vector3 newScale = new Vector3(timer / gazeTime, child.localScale.y, child.localScale.z);
            Vector3 newPosition = new Vector3(-0.5f + (timer / gazeTime) / 2, child.localPosition.y, child.localPosition.z);
            child.localPosition = newPosition;
            child.localScale = newScale;

            if (timer >= gazeTime)
            {
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0f;
            }
        }
    }

    public void PointerEnter()
    {
        gazedAt = true;
    }

    public void PointerExit()
    {
        gazedAt = false;
    }

    public void PointerDown()
    {
        float newX = Random.Range(minX, maxX);
        if (newX > 0 && newX < 1.5)
            newX = 1.5f;
        if (newX < 0 && newX > -1.5)
            newX = -1.5f;
        float newZ = Random.Range(minZ, maxZ);
        if (newZ > 0 && newZ < 1.5)
            newZ = 1.5f;
        if (newZ < 0 && newZ > -1.5)
            newZ = -1.5f;
        Vector3 newPosition = new Vector3(newX, Random.Range(minY, maxY), newZ);
        transform.position = newPosition;
        Text score = GameObject.Find("ScoreText").GetComponent<Text>();
        GazeTimer.score++;
        score.text = "Score: " + GazeTimer.score.ToString();
    }
}
