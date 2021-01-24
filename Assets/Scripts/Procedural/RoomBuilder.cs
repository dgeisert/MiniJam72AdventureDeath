using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public Material[] tileMaterials;
    Vector2 shift = new Vector2(4.5f, 2);
    Vector2 size = new Vector2(10, 10);
    // Start is called before the first frame update
    void Start()
    {
        Material mat = tileMaterials.Random1();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.layer = 8;
        cube.GetComponent<MeshRenderer>().enabled = false;
        cube.transform.SetParent(transform);
        cube.transform.localPosition = new Vector3((size.x / 2 - shift.x) * 5 - 5, -3f, (size.y / 2 - shift.y) * 5);
        cube.transform.localScale = new Vector3(size.x * 5, 1, size.y * 5);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 pos = new Vector3((i - shift.x) * 5, 0, (j - shift.y) * 5);
                GameObject go = GameObject.Instantiate(
                    floorTiles.Random1(),
                    pos,
                    Quaternion.identity,
                    transform
                );
                go.GetComponent<MeshRenderer>().material = mat;
                if (j == 0)
                {
                    go = GameObject.Instantiate(
                        wallTiles.Random1(),
                        pos - transform.right * 5,
                        Quaternion.identity,
                        transform
                    );
                    go.transform.localEulerAngles =
                        new Vector3(0, 180, 0);
                    go.GetComponent<MeshRenderer>().material = mat;
                }
                if (j == 9)
                {
                    go = GameObject.Instantiate(
                        wallTiles.Random1(),
                        pos + transform.forward * 5 - transform.right * 5,
                        Quaternion.identity,
                        transform
                    );
                    go.transform.localEulerAngles =
                        new Vector3(0, 180, 0);
                    go.GetComponent<MeshRenderer>().material = mat;
                }
                if (i == 9)
                {
                    go = GameObject.Instantiate(
                        wallTiles.Random1(),
                        pos + transform.forward * 5,
                        Quaternion.identity,
                        transform
                    );
                    go.transform.localEulerAngles =
                        new Vector3(0, -90, 0);
                    go.GetComponent<MeshRenderer>().material = mat;
                }
                if (i == 0)
                {
                    go = GameObject.Instantiate(
                        wallTiles.Random1(),
                        pos - transform.right * 5,
                        Quaternion.identity,
                        transform
                    );
                    go.transform.localEulerAngles =
                        new Vector3(0, 90, 0);
                    go.GetComponent<MeshRenderer>().material = mat;
                }
            }
        }
    }
}