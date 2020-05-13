﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarBase : MonoBehaviour {

    public GameObject m_fogOfWarPlane;
    private GameObject m_base;
    public LayerMask m_fogLayer;
    public float m_radius = 50f;
    private float m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;

    // Use this for initialization
    void Start () {
        Initialize ();
        if(GameObject.Find("PlayerBase") == null)
        {
           return;
        }
        else
        {
            m_base = GameObject.Find("PlayerBase");
        }
    }

    // Update is called once per frame
    void Update () {
        var Troops = GameObject.FindGameObjectsWithTag ("Unit");
        if (Troops.Length > 0) {
            for(int j=0;j<Troops.Length;j++){
            Ray _r = new Ray (transform.position, Troops[j].transform.position - transform.position);
            RaycastHit _hit;
            if (Physics.Raycast (_r, out _hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide)) {
                for (int i = 0; i < m_vertices.Length; i++) {
                    Vector3 v = m_fogOfWarPlane.transform.TransformPoint (m_vertices[i]);
                    float dist = Vector3.SqrMagnitude (v - _hit.point);
                    if (dist < m_radiusSqr) {
                        float alpha = Mathf.Min (m_colors[i].a, dist / m_radiusSqr);
                        m_colors[i].a = alpha;
                    }
                }
                UpdateColor ();
             }
            }
            //return;
        }
        else
        {
        Ray r = new Ray (transform.position, m_base.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast (r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide)) {
            for (int i = 0; i < m_vertices.Length; i++) {
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint (m_vertices[i]);
                float dist = Vector3.SqrMagnitude (v - hit.point);
                if (dist < m_radiusSqr) {
                    float alpha = Mathf.Min (m_colors[i].a, dist / m_radiusSqr);
                    m_colors[i].a = alpha;
                }
            }
            UpdateColor ();
        }
        }
    }
    void Initialize () {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter> ().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++) {
            m_colors[i] = Color.black;
        }
        UpdateColor ();
    }

    void UpdateColor () {
        m_mesh.colors = m_colors;
    }
}