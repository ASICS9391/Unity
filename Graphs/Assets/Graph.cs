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
        //Vector3 position;
        //position.y = 0f;
        //position.z = 0f;


        points = new Transform[resolution * resolution];
        //for (int i = 0, z = 0; i < points.Length; z++)
        //{
        //    position.z = (z + 0.5f) * step - 1f;

        //    for (int x = 0; x < resolution; x++, i++)
        //    {
        //        Transform point = Instantiate(pointPrefab);

        //        position.x = (x + 0.5f) * step - 1f;
                
        //        point.localPosition = position;
        //        point.localScale = scale;
        //        point.SetParent(transform, false);

        //        points[i] = point;
        //    }
        //}

        for(int i = 0; i<points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
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
            Sine2DFunction, 
            RippleFunction, 
            Cylinder
        };

        GraphFunction f = functions[(int)function];

        float step = 2f / resolution;

        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }

        }
		//for(int i = 0; i < points.Length; i++)
  //      {
  //          Transform point = points[i];
  //          Vector3 position = point.localPosition;


  //          position.y = f(position.x, position.z, t);
  //          point.localPosition = position;
  //      }

	}

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;

        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.z = z;

        return p;
    }     

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;

        p.x = x;
        p.z = z;


        float y = Mathf.Sin(PI * (x + t));

        y += Mathf.Sin(2f*PI * (x + t)) / 2f;
        
        y *= 2f / 3f;

        p.y = y;

        return p;
    }

    static Vector3 SquareWaveFunction(float x, float z, float t)
    {
        Vector3 p;

        p.x = x;
        p.z = z;


        float y = Mathf.Sin((PI/2 )* (x + t));
        int maxIterations = 35;

        for(int k = 2; k < maxIterations; k++)
        {
            y += (Mathf.Sin((PI/2) * ((2f * k) - 1f) * (x + t)) / ((2f * k) - 1f));
        }

        y *= (4f / PI);

        p.y = y;

        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;

        p.x = x;
        p.z = z;

        float _PI = PI;
        float y = Mathf.Sin(_PI * (x + t));
        y += Mathf.Sin(_PI * (z + t));
        y *= y * 0.5f;

        p.y = y;
        return p;
    }

    static Vector3 RippleFunction(float x, float z, float t)
    {
        Vector3 p;

        p.x = x;
        p.z = z;


        float d = Mathf.Sqrt((x * x) + (z * z));
        float y = Mathf.Sin(PI * (4f * d -t));

        y /= 1f + 10F * d;

        p.y = y;

        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;

        float r = 1f + Mathf.Sin(PI * (6f * u + 2f * v +t)) * 0.2f;

        p.x = r*Mathf.Sin(PI * u);
        p.y = v;
        p.z = r*Mathf.Cos(PI * u);


        return p;
    }

}
