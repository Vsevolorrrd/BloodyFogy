using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TestForLesson : MonoBehaviour
{
    [SerializeField] Vector2 start;
    [SerializeField] Vector2 end;
    public Transform obj;
    Vector2 ruxa;
    Vector2 boba;
    Vector2 rima;
    public float Speed = 2f;
    private void Start()
    {
        obj.transform.position = start;

        Vector2 soli = end - start;
             float Ro = Mathf.Sqrt(soli.x * soli.x + soli.y * soli.y);

        ruxa = new Vector2(soli.x / Ro, soli.y / Ro);
    }
    // Update is called once per frame
    void Update()
    {
        obj.position +=(Vector3)(ruxa * Speed * Time.deltaTime);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine (start, end);
    }
}
