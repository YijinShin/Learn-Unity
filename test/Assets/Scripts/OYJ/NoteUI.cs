using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{

    float v;
    float h;
    Vector3 move;
    public int speed = 20;
    
    // GameObject hint;
    // Start is called before the first frame update

    HintDatabase hint;
    void Start()
    {
        // hint = GetComponent<HintDatabase>();
        hint = GameObject.Find("HintDB").GetComponent<HintDatabase>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");

        move = new Vector3(h, 0, v);
        transform.position += move * speed * Time.deltaTime;
        transform.LookAt(transform.position + move);
    }
    // Update is called once per frame
    void OnCollisionEnter(Collision collision){
        if(hint.HintDB.ContainsKey(collision.gameObject.name)){
            hint.HintDB[collision.gameObject.name].gameObject.SetActive(true);

            GameObject foundHint= hint.HintDB[collision.gameObject.name];
            hint.HintDBbool[foundHint] = true;
            Debug.Log("IN");
        }
        Debug.Log(collision.gameObject.name);
    }
}
