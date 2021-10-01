using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;

public class LSystem : MonoBehaviour
{
    [Serializable]
    public class Rule
    {
        public string input;
        public string output;
    }

    public string axiom;


    public Rule[] rules;

    public float angle = 90;
    public float scaleFactor = 1.3f;
    public int iterations = 1;

    public GameObject basicPartPrefab;

    string instructionString;

    Transform placementTransform;

    Transform originalTransform;
    Bounds partBounds;

    float partLength = 0;

    Stack<Vector3> positionStack;
    Stack<Quaternion> rotationStack;
    Stack<Vector3> scaleStack;

    List<GameObject> generatedParts;

    void Start()
    {
        originalTransform = Instantiate(new GameObject(), transform.position, transform.rotation).transform;

        originalTransform.position = transform.position;
        originalTransform.rotation = transform.rotation;
        originalTransform.localScale = transform.localScale;

        partBounds = basicPartPrefab.GetComponentInChildren<Collider>().bounds;

        partLength = partBounds.center.y * 2;

        Debug.Log(partBounds);
        positionStack = new Stack<Vector3>();
        rotationStack = new Stack<Quaternion>();
        scaleStack = new Stack<Vector3>();

        generatedParts = new List<GameObject>();

        Generate();
    }

    [ContextMenu("Generate")]
    void Generate()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        ClearGeneratedParts();

        GenerateInstructionString();

        Debug.Log("instructionString: " + instructionString);

        Render();

    }


    void GenerateInstructionString()
    {
        if (axiom.Trim().Length == 0)
        {
            return;
        }

        //Note that all the rules have to be applied to the string at the same
        //time, not sequentially (i.e not with a series of Replace operations).
        //Hence the compicated business with the Linked Lists.

        instructionString = axiom;

        LinkedList<char> instructionsList = new LinkedList<char>();
        instructionsList.AddFirst(axiom[0]);

        //  Debug.Log("before: " + instructionsList);

        for (int i = 0; i < iterations; i++)
        {
            //Debug.Log("iteration: " + i + " " + instructionsList);

            LinkedListNode<char> currentNode = instructionsList.First;
            LinkedListNode<char> nextNode;

            while (currentNode != null)
            {
                //                Debug.Log("currentNode: " + currentNode.Value);

                nextNode = currentNode.Next;

                foreach (Rule rule in rules)
                {
                    if (currentNode.Value == rule.input[0])
                    {
                        LinkedListNode<char> addedNode = currentNode;
                        foreach (Char c in rule.output)
                        {
                            addedNode = instructionsList.AddAfter(addedNode, c);
                        }

                        instructionsList.Remove(currentNode);
                        break;
                    }
                }

                currentNode = nextNode;
            }

            instructionString = string.Join("", instructionsList);


        }


    }

    private void Render()
    {
        placementTransform = transform;

        int n = 0;

        foreach (Char c in instructionString)
        {
            // Debug.Log(c);
            switch (c)
            {
                case 'F':
                case 'G':
                    GameObject added = Instantiate(basicPartPrefab, placementTransform.position, placementTransform.rotation);
                    added.name = "Section: " + n.ToString();
                    added.transform.localScale = placementTransform.localScale;
                    generatedParts.Add(added);

                    //added.transform.SetParent(gameObject.transform, false);
                    n += 1;
                    placementTransform.position += added.transform.up * partLength;

                    break;
                case '+':
                    placementTransform.Rotate(new Vector3(-angle, 0, 0));
                    break;
                case '-':
                    placementTransform.Rotate(new Vector3(angle, 0, 0));
                    break;
                case 'y':
                    placementTransform.Rotate(new Vector3(0, angle, 0));
                    break;
                case 'Y':
                    placementTransform.Rotate(new Vector3(0, -angle, 0));
                    break;
                case 'z':
                    placementTransform.Rotate(new Vector3(0, 0, angle));
                    break;
                case 'Z':
                    placementTransform.Rotate(new Vector3(0, 0, -angle));
                    break;

                case '^':
                    placementTransform.localScale = new Vector3(placementTransform.localScale.x * scaleFactor, placementTransform.localScale.y * scaleFactor, placementTransform.localScale.z * scaleFactor);
                    Debug.Log(placementTransform.localScale);
                    break;
                case '!':
                    placementTransform.localScale = new Vector3(placementTransform.localScale.x / scaleFactor, placementTransform.localScale.y / scaleFactor, placementTransform.localScale.z / scaleFactor);
                    break;
                case '[':
                    Vector3 savedPosition = new Vector3(placementTransform.position.x, placementTransform.position.y, placementTransform.position.z);
                    positionStack.Push(savedPosition);

                    Quaternion savedRotation = new Quaternion(placementTransform.rotation.x, placementTransform.rotation.y, placementTransform.rotation.z, placementTransform.rotation.w);
                    rotationStack.Push(savedRotation);

                    Vector3 savedScale = new Vector3(placementTransform.localScale.x, placementTransform.localScale.y, placementTransform.localScale.z);
                    scaleStack.Push(savedScale);
                    break;
                case ']':
                    placementTransform.position = positionStack.Pop();
                    placementTransform.rotation = rotationStack.Pop();
                    placementTransform.localScale = scaleStack.Pop();
                    break;
            }

        }
    }

    void ClearGeneratedParts()
    {
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;
        transform.localScale = originalTransform.localScale;

        for (int i = generatedParts.Count - 1; i >= 0; i--)
        {
            Destroy(generatedParts[i]);
        }

        generatedParts.Clear();
    }
}