using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
    private int currentLength = 0;

    public Question.Set set;
    public GameObject block;
    public int length = 20;

    private void Start() {
        this.setUp();

    }

    private void FixedUpdate() {
        if(this.length != this.currentLength) {
            this.setUp();

        }

    }
    
    public void setUp() {
        Utilities.destroyChildren(this.transform);

        Vector3 blockSize = this.block.GetComponent<MeshRenderer>().bounds.size;
    
        for(int i = 0; i < this.length; i++) {
            GameObject block = Instantiate<GameObject>(this.block);
            block.transform.position += new Vector3(0.0f, 0.0f, blockSize.z * i);

            block.transform.SetParent(this.transform);

        }

        this.currentLength = this.length;

        GameObject[] visuals = this.set.instantiate(this.transform);
        float roadSize = blockSize.z * this.length;
        float spacing = roadSize / (visuals.Length + 1);

        for(int i = 0; i < visuals.Length; i++) {
            GameObject visual = visuals[i];
            visual.transform.position = Vector3.zero;

            visual.transform.position += new Vector3(0.0f, 0.0f, (spacing * (i + 1)) - blockSize.z);

        }

    }

}
