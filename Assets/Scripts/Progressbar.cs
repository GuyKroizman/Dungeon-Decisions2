using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressbar : MonoBehaviour {


    public GameObject m_part;
    public float m_timeSeconds;

    public int m_rowCount = 3;
    public int m_columnCount = 40;

    private Transform m_componentTransform;
    
    private float m_partWidth;
    
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

    void Start ()
    {

        m_componentTransform = GetComponent<Transform>();

        m_partWidth = GetPartPrefabSize();

        var enumerator = RandomDecendingFloatGenerator(m_rowCount * m_columnCount, 0, m_timeSeconds).GetEnumerator();

        // divide column count by 2 because we need to do half the loops since in each iteration we create two particles.
        // one to the right and one to the left.
        for (float i = 0; i < m_columnCount / 2; i++)
            for (float j = 0; j < m_rowCount; j++)
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
    /// Get the progress bar's particle's size
    /// </summary>
    /// <returns></returns>
    private float GetPartPrefabSize()
    {
        Canvas canvas = GetComponentInParent<Canvas>();

        // to get the particle size we get the x size from the prefab and then multiple it by the canvas scale factor.
        return m_part.GetComponent<RectTransform>().sizeDelta.x * canvas.scaleFactor;
    }

    /// <summary>
    /// Create a part/particle of the progress bar in the x,z location and return it.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>GameObject</returns>
    private GameObject CreatePart(float x, float z)
    {        
        float h = x * m_partWidth;
        float v = z * m_partWidth;
        return Instantiate(m_part,
            new Vector3(m_componentTransform.position.x + h, m_componentTransform.position.y + v, 0 ),
            Quaternion.identity,
            m_componentTransform);        
    }
	
	
	void Update () {
		
	}
}
