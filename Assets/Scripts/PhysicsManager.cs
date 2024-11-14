using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject marblePrefab;  
    public int numberOfMarbles = 10;  
    public Bowl bowl; 

    private Marble[] marbles;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
       
        marbles = new Marble[numberOfMarbles];
        for (int i = 0; i < numberOfMarbles; i++)
        {
            GameObject marbleObj = Instantiate(marblePrefab, RandomPosition(), Quaternion.identity);
            marbles[i] = marbleObj.GetComponent<Marble>();
        }
    }

    void Update()
    {
        foreach (var marble in marbles)
        {
            bowl.CheckMarbleCollisions(marble);
        }

        for (int i = 0; i < marbles.Length; i++)
        {
            for (int j = i + 1; j < marbles.Length; j++)
            {
                marbles[i].HandleMarbleCollision(marbles[j]);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float radius = Random.Range(1f, 3f);  
        return new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius); 
    }

}