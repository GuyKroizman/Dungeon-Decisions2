using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressbar : MonoBehaviour {


    public GameObject m_part;

    private Transform m_componentTransform;
    public float m_timeSeconds;
    private float m_partWidth;
    private float m_canvasScaleFactor;

    /// <summary>
    /// Generator to produce sorted floats in a range. 
    /// That is, each time it will produce a float in the range that is smaller than the previous one
    /// </summary>
    /// <param name="numbersToGenerate">The max floats that this generator will produce</param>
    /// <param name="min">The min (inclusive) value of the first float</param>
    /// <param name="max">The max (inclusive) value of the last float</param>
    /// <returns></returns>
    private IEnumerable<float> RandomDecendingFloatGenerator(int numbersToGenerate, float min, float max)
    {
        var destroyTimes = new List<float>();

        for (int i = 0; i < numbersToGenerate; i++)
        {
            destroyTimes.Add(Random.Range(min, max));
        }
        destroyTimes.Sort();
        destroyTimes.Reverse();

        foreach(var d in destroyTimes)
        {
            yield return d;
        }

    }

    void Start () {

        m_componentTransform = GetComponent<Transform>();
        m_partWidth= m_part.GetComponent<RectTransform>().sizeDelta.x;
        Canvas canvas = GetComponentInParent<Canvas>();
        m_canvasScaleFactor = canvas.scaleFactor;

        const int RowCount = 3;
        const int ColumnCount = 40;

        var enumerator = RandomDecendingFloatGenerator(RowCount * ColumnCount, 0, m_timeSeconds).GetEnumerator();

        for (float i = 0; i < ColumnCount/2; i++)
            for (float j = 0; j < RowCount; j++)
            {
                var cubeRight = CreatePart(i, j);
                enumerator.MoveNext();                
                Destroy(cubeRight, enumerator.Current);
                

                var cubeLeft = CreatePart(-1 * i, j);
                enumerator.MoveNext();                
                Destroy(cubeLeft, enumerator.Current);
                
            }

    }

    /// <summary>
    /// Create a part/particle of the progress bar in the x,z location and return it.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>GameObject</returns>
    private GameObject CreatePart(float x, float z)
    {        
        float h = x * m_partWidth * m_canvasScaleFactor;
        float v = z * m_partWidth * m_canvasScaleFactor;
        return Instantiate(m_part,
            new Vector3(m_componentTransform.position.x + h, m_componentTransform.position.y + v, 0 ),
            Quaternion.identity,
            m_componentTransform);        
    }
	
	
	void Update () {
		
	}
}
