using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PipesController : MonoBehaviour
{
    public List<GameObject> activePipes; 
    public List<GameObject> unactivePipes;
    public Transform pipesInitTransform;
    private Vector3 pipesInitPosition;
    public int speed = 5;
    [SerializeField] private int maxNumberOfPipes = 3;
    [SerializeField] private float maxHeight = 3;
    [SerializeField] private float minHeight = 3;
    [SerializeField] private float generationRate = 5f;
    [SerializeField] private float lastGenerationTime;
    private float newHeight;
    void Start()
    {
        pipesInitPosition = pipesInitTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveActivePipes();
        GeneratePipes();
    }

    private void GeneratePipes()
    {
        var unactivePipesCount = unactivePipes.Count -1;

        if (!PlayerController.isAlive || activePipes.Count -1 >= maxNumberOfPipes || unactivePipesCount < 1 || Time.time < generationRate + lastGenerationTime)
        {
            return;
        }
        lastGenerationTime = Time.time;
        activePipes.Add(unactivePipes[unactivePipesCount]);
        activePipes[activePipes.Count - 1].transform.position = pipesInitPosition;
        if (pipesInitPosition.y <= minHeight)
        {
            newHeight = UnityEngine.Random.Range(1, 2);
        }
        else if (pipesInitPosition.y >= maxHeight)
        {
            newHeight = UnityEngine.Random.Range(-2, -1);
        }
        else
        {
            newHeight = UnityEngine.Random.Range(-2, 2);
        }
        pipesInitPosition.y += newHeight;
        pipesInitPosition.y = Mathf.Clamp(pipesInitPosition.y,minHeight, maxHeight);
        activePipes[activePipes.Count - 1].gameObject.SetActive(true);
        unactivePipes.RemoveAt(unactivePipesCount);
    }

    private void MoveActivePipes()
    {
        if (PlayerController.isAlive && activePipes.Count > 0)
        {
            for (int i = activePipes.Count -1; i >=  0; i--)
            {
                activePipes[i].transform.position += Vector3.left * Time.deltaTime * speed;
                if (activePipes[i].transform.position.x <= -10)
                {
                    unactivePipes.Add(activePipes[i]);
                    unactivePipes[unactivePipes.Count - 1].gameObject.SetActive(false);
                    activePipes.RemoveAt(i);
                }
            }
        }
    }
}
