using UnityEngine;

public class InfiniteGrandstand : MonoBehaviour
{
    public GameObject grandstandPrefab;
    public Transform player;
    public int numberOfStands = 6;
    public float grandstandWidth = 15.0f;

    private GameObject[] stands;

    void Start()
    {
        stands = new GameObject[numberOfStands];

        for (int i = 0; i < numberOfStands; i++)
        {
            Vector3 position = new Vector3 (i * grandstandWidth, 3.75f, -3.0f);
            stands[i] = Instantiate(grandstandPrefab, position, Quaternion.identity);
        }
    }

    void Update()
    {
        foreach (GameObject stand in stands)
        {
            float distance = player.position.x - stand.transform.position.x;

            if (distance > grandstandWidth * (numberOfStands / 2))
            {
                float newX = stand.transform.position.x + grandstandWidth * numberOfStands;
                stand.transform.position = new Vector3(newX, stand.transform.position.y, stand.transform.position.z);
            }
            else if (distance < -grandstandWidth * (numberOfStands / 2))
            {
                float newX = stand.transform.position.x - grandstandWidth * numberOfStands;
                stand.transform.position = new Vector3(newX, stand.transform.position.y, stand.transform.position.z);
            }
        }
    }
}
