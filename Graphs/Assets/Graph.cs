using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;
    Transform[] points;
    const float PI = Mathf.PI;

    [Range(10, 100)]
    public int resolution = 10;

    public GraphFunctionName function;

	// Use this for initialization
	void Start ()
    {
        float step = (2f) / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;


        points = new Transform[resolution * resolution];
        for (int i = 0, z = 0; i < points.Length; z++)
        {
            position.z = (z + 0.5f) * step - 1f;

            for (int x = 0; x < resolution; x++, i++)
            {
                Transform point = Instantiate(pointPrefab);

                position.x = (x + 0.5f) * step - 1f;
                
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);

                points[i] = point;
            }
        }
        


    }

    // Update is called once per frame
    void Update ()
    {
        float t = Time.time;

        GraphFunction[] functions =
        {
            SineFunction,
            MultiSineFunction,
            SquareWaveFunction,
            Sine2DFunction
        };

        GraphFunction f = functions[(int)function];


		for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;


            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }

	}

    static float SineFunction(float x, float z, float t)
    {
        return Mathf.Sin(PI * (x + t));
    }     

    static float MultiSineFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(PI * (x + t));

        y += Mathf.Sin(2f*PI * (x + t)) / 2f;
        
        y *= 2f / 3f; 

        return y;
    }

    static float SquareWaveFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(PI * (x + t));
        int maxIterations = 15;

        for(int k = 2; k < maxIterations; k++)
        {
            y += (Mathf.Sin(PI * ((2f * k) - 1f) * (x + t)) / ((2f * k) - 1f));
        }

        return ((4f / PI) * y);
    }

    static float Sine2DFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(PI * (x + t));
        y += Mathf.Sin(PI * (z + t));
        y *= y * 0.5f;   
        return y;
    }


}
