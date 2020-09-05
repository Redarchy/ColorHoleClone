using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *All the purpose of this class is to generate a 2D hole using path structures of PolygonCollider2D.
 *Then assigns this 2D hole to a 3D mesh to make it involved in game mechanics.
 */



public class HoleCreator : MonoBehaviour
{
    public PolygonCollider2D hole2DCol; //2D Hole Collider
    public PolygonCollider2D ground2DCol; //2D Ground Collider
    public MeshCollider generatedMeshCol; //Generated Mesh Collider
    Mesh generatedMesh;

    public float initialScale = 1f; //scale of the created hole. in-game, it can be changed dynamically.

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;

        if (transform.hasChanged)
        {
            transform.hasChanged = false;

            //updates 2d hole's position and scale
            hole2DCol.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCol.transform.localScale = transform.localScale * initialScale;

            //applies the hole's path on 2d ground to create the hole on the ground
            Create2DHole();

            //since 2D objects cannot collide with 3D's, we generate an appropriate version of our 2d ground collider to 3d.
            Create3DMeshCol();
        }
    }
    
    private void Create2DHole()
    {
        Vector2[] vertices = hole2DCol.GetPath(0); //gets the 2d hole's path

        for(int i = 0; i < vertices.Length; i++)
        {
            //Debug.Log($" {i} Before : {vertices[i].x} ; {vertices[i].y}");

            vertices[i] = hole2DCol.transform.TransformPoint(vertices[i]); //transforms 2d hole's path elements from local to world coordinates

            //Debug.Log($" {i} After : {vertices[i].x} ; {vertices[i].y}");
        }

        ground2DCol.pathCount = 2; //creates a new path on 2d ground to make there a hole
        ground2DCol.SetPath(1, vertices); //sets the hole to recently created path
    }

    private void Create3DMeshCol()
    {
        //removes the old one
        if(generatedMesh != null)
        {
            Destroy(generatedMesh);
        }

        generatedMesh = ground2DCol.CreateMesh(true, true); //creates a new one
        generatedMeshCol.sharedMesh = generatedMesh; //assigns the variable to mesh collider component
    }

    
}