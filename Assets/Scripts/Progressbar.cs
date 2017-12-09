using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressbar : MonoBehaviour {


    public GameObject m_part;
    private Transform m_componentTransform;
    
    void Start () {

        m_componentTransform = GetComponent<Transform>();

        List<float> destroyTimes = new List<float>();
        for(int i=0; i < 40; i++)
        {
            destroyTimes.Add(Random.Range(0.0f, 5.0f));
        }
        destroyTimes.Sort();
        destroyTimes.Reverse();
        List<float>.Enumerator it  = destroyTimes.GetEnumerator();

        for (float i = 0; i < 20; i++)
            for (float j = 0; j < 3; j++)
            {
                Destroy(CreatePart(i, j), it.Current);
                it.MoveNext();
                Destroy(CreatePart(-1 * i, j), it.Current);
            }

    }

    private GameObject CreatePart(float x, float z)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        float h = x * 10 * canvas.scaleFactor;
        float v = z * 10 * canvas.scaleFactor;
        return Instantiate(m_part,
            new Vector3(m_componentTransform.position.x + h, m_componentTransform.position.y + v, 0 ),
            Quaternion.identity,
            m_componentTransform);        
    }
	
	
	void Update () {
		
	}
}
