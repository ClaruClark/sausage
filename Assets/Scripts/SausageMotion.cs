using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageMotion : MonoBehaviour
{
    [SerializeField] float Intensivity = 1f;
    [SerializeField] float Mass = 1f;
    [SerializeField] float stiffness = 1f;
    [SerializeField] float damping = 0.7f;

    private Mesh original, clone;
    private MeshRenderer rend;
    private SausageVertex[] sv;
    private Vector3[] vertexArray;
    private void Start()
    {
        original = GetComponent<MeshFilter>().sharedMesh;
        clone = Instantiate(original);
        GetComponent<MeshFilter>().sharedMesh = clone;
        rend = GetComponent<MeshRenderer>();
        sv = new SausageVertex[clone.vertices.Length];
        for (int i = 0; i < clone.vertices.Length; i++)
        {
            sv[i] = new SausageVertex(i, transform.TransformPoint(clone.vertices[i]));
        }
    }

    

    public void FixedUpdate()
    {
        vertexArray = original.vertices;
        for(int i = 0; i < sv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[sv[i].id]);
            float intensity = (1 - (rend.bounds.max.y - target.y) / rend.bounds.size.y) * Intensivity;
            sv[i].Shake(target, Mass, stiffness, damping);
            target = transform.InverseTransformPoint(sv[i].pos);
            vertexArray[sv[i].id] = Vector3.Lerp(vertexArray[sv[i].id], target, intensity);
        }
        clone.vertices = vertexArray;
    }

    public class SausageVertex
    {
        public int id;
        public Vector3 pos;
        public Vector3 velocity, force;
        public SausageVertex( int _id, Vector3 _pos)
        {
            id = _id;
            pos = _pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - pos) * s;
            velocity = (velocity + force / m) * d;
            pos += velocity;
            if((velocity + force + force/m).magnitude < 0.001f)
            {
                pos = target;
            }
        }
    }
}
