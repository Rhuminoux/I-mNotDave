using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RopeSegment
{
    public int index;
    public Vector2 posNow;
    public Vector2 posOld;

    public RopeSegment(int index, Vector2 pos){
        this.index = index;
        this.posNow = pos;
        this.posOld = pos;
    }

    public void UpdatePosNow(Vector2 pos){
        this.posNow = pos;
    }
}

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector2[] segments;
    public int nbSegments = 20;

    public float catchDistance = 1f;

    private Transform tie1;
    private Transform tie2;
    /*private Player1Contoller player1;
    private Player2Contoller player2;*/

    public float maxDistance = 20f;
    public float extraDistance = 5f;
    public float pullForce = 15f;
    public float pullDuration = 0.5f;
    private float endPullTime;

    private AudioManager audioManager;

    private void Awake() {
        /*player1 = GameObject.FindObjectOfType<Player1Contoller>();
        player2 = GameObject.FindObjectOfType<Player2Contoller>();
        tie1 = player1.transform;
        tie2 = player2.transform;*/
        endPullTime = Time.time;
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = nbSegments;
        InitSegments();
    }

    private void InitSegments(){
        float gap = 1f / nbSegments;
        Vector2 diff = (Vector2)tie2.position - (Vector2)tie1.position;
        segments = new Vector2[nbSegments];

        for (int i = 0; i<nbSegments; i++){
            if (i == 0){
                segments[i] = (Vector2)tie1.position;
            }
            else if (i == nbSegments - 1){
                segments[i] = (Vector2)tie2.position;
            }
            else {
                Vector2 pos = (Vector2)tie1.position + (diff * gap * i);
                segments[i] = pos;
            }
        }
    }

    private Vector2 ComputeNewSegment(Vector2 prev, Vector2 current, Vector2 next){
        Vector2 newpos = (prev + next) / 2f;
        return ((newpos + current) / 2f);
    }

    private void UpdateSegments(){
        for (int i = 0; i<nbSegments; i++){
            if (i == 0){
                segments[i] = (Vector2)tie1.position;
            }
            else if (i == nbSegments - 1){
                segments[i] = (Vector2)tie2.position;
            }
            else{
                segments[i] = ComputeNewSegment(segments[i-1], segments[i], segments[i+1]);
            }
            
        }
    }

    private void Draw(){
        Vector3[] positions = new Vector3[nbSegments];
        int i = 0;
        foreach(Vector2 seg in segments){
            positions[i] = (Vector3)seg;
            i++;
        }
        lineRenderer.SetPositions(positions);
    }

    /*
    private void PullBack(){
        if (!player1.canMove || !player2.canMove)
            return;
        if (Vector2.Distance((Vector2)tie1.position, (Vector2)tie2.position) < maxDistance)
            return;
        if ((player1.alive && player2.alive) || Vector2.Distance((Vector2)tie1.position, (Vector2)tie2.position) >= maxDistance + extraDistance){
            player1.canMove = false;
            player2.canMove = false;
            Vector2 direction = ((Vector2)tie2.position - (Vector2)tie1.position).normalized;
            audioManager.Play("Rope");
            audioManager.Play("PullBack");
            player1.rb.velocity = direction * pullForce;
            player2.rb.velocity = -direction * pullForce;
            player1.animator.SetBool("Pulled", true);
            player2.animator.SetBool("Pulled", true);
            endPullTime = Time.time + pullDuration;
        }
        else if (player1.alive && !player2.alive){
            player2.canMove = false;
            Vector2 direction = ((Vector2)tie2.position - (Vector2)tie1.position).normalized;
            audioManager.Play("Drag");
            player2.rb.velocity = -direction * pullForce;
            endPullTime = Time.time + pullDuration;
        }
        else if (!player1.alive && player2.alive){
            player1.canMove = false;
            Vector2 direction = ((Vector2)tie2.position - (Vector2)tie1.position).normalized;
            audioManager.Play("Drag");
            player1.rb.velocity = direction * pullForce;
            endPullTime = Time.time + pullDuration;
        }
    }

    private void EndPullBack(){
        if (endPullTime > Time.time)
            return;
        if (!player1.canMove){
            player1.rb.velocity = new Vector2(0, 0);
            player1.canMove = true;
            player1.animator.SetBool("Pulled", false);
        }
        if (!player2.canMove){
            player2.rb.velocity = new Vector2(0, 0);
            player2.canMove = true;
            player2.animator.SetBool("Pulled", false);
        }
    }*/

    private void FixedUpdate() {
        UpdateSegments();
        Draw();
        //PullBack();
        //EndPullBack();
    }

    public bool CanCatch(Vector2 bug){
        int i = 0;
        foreach(Vector2 seg in segments){
            if (i > 0 && i < nbSegments - 1)
            {
                if (Vector2.Distance(bug, seg) < catchDistance)
                    return true;
            }
            i++;
        }
        return false;
    }
}
