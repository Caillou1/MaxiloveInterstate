using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSimulate : MonoBehaviour {

    public int numberPoints;

    public float delta;
    float deltaTimer;

    public float offset;
    public Vector3 offsetDirection;

    LineRenderer my_line;
    Vector3[] positions;
    // Use this for initialization

	void Start ()
    {
        my_line = GetComponent<LineRenderer>();
        my_line.numPositions = numberPoints;
        positions = new Vector3[numberPoints];
        deltaTimer = delta;
        Vector3[] positionsTemp = new Vector3[numberPoints];
        for (int i = 0; i < positions.Length; i++)
        {
            positionsTemp[i] = transform.position;
        }
        positions = positionsTemp;
        my_line.SetPositions(positionsTemp);

    }
	
	// Update is called once per frame
	void Update ()
    {
        deltaTimer += -1 * Time.deltaTime;

        if (deltaTimer < 0)
        {
            for (int i = positions.Length-1; i > 0 ; i--)
            {
                positions[i] = positions[i-1];
            }
            positions[0] = transform.position;
            deltaTimer = delta;
            Vector3[] positionsTemp = new Vector3[numberPoints];
            for (int i = 0; i < positions.Length; i++)
            {
                positionsTemp[i] = positions[i] + offsetDirection * offset * i / positions.Length;
            }
            my_line.SetPositions(positionsTemp);
        }
	}
}
