using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour {
    
    public Transform prefab;
    public int gridResolution = 20;

    private List<Transformation> _transformations;

    Transform[] grid;

    Matrix4x4 transformationMatrix;

    void Awake () {
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
        
        _transformations = new List<Transformation>();
    }
    
    Transform CreateGridPoint (int x, int y, int z) {
        Transform point = Instantiate<Transform>(prefab);
        point.transform.SetParent(transform);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );
        return point;
    }
    
    Vector3 GetCoordinates (int x, int y, int z) {
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }
    
    void Update()
    {
        UpdateTransformation();
        
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }
    
    Vector3 TransformPoint (int x, int y, int z) {
        Vector3 coordinates = GetCoordinates(x, y, z);
        return transformationMatrix.MultiplyPoint(coordinates);
    }

    void UpdateTransformation()
    {
        GetComponents<Transformation>(_transformations);
        if (_transformations.Count > 0)
        {
            transformationMatrix = _transformations[0].Matrix;
            for (int i = 1; i < _transformations.Count; i++)
            {
                transformationMatrix = _transformations[i].Matrix * transformationMatrix;
            }
        }
    }
}
