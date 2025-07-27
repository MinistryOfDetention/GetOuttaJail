using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    // Arrays of head sprite. 0 = facing down, 1 = facing right, 2 = facing up.
    // To face left, flip the facing right sprite.
    public Sprite[] sqaureHeadSpritesMale;
    public Sprite[] sqaureHeadSpritesFemale;
    public Sprite[] circleHeadSpritesMale;
    public Sprite[] circleHeadSpritesFemale;
    public Sprite[] TriangleHeadSpritesMale;
    public Sprite[] TriangleHeadSpritesFemale;

    public SpriteRenderer headRenderer;

    public String headType;
    public String sex;

    void Start()
    {
        // Ensure the headRenderer is set
        if (headRenderer == null)
        {
            headRenderer = GetComponent<SpriteRenderer>();
            if (headRenderer == null)
            {
                Debug.LogError("HeadManager: SpriteRenderer not found on the GameObject.");
            }
        }
    }

    public void SetFacingLeft()
    {
        switch (headType+sex)
        {
            case "SquareMale":
                headRenderer.sprite = sqaureHeadSpritesMale[1];
                break;
            case "SquareFemale":
                headRenderer.sprite = sqaureHeadSpritesFemale[1];
                break;
            case "CircleMale":
                headRenderer.sprite = circleHeadSpritesMale[1];
                break;
            case "CircleFemale":
                headRenderer.sprite = circleHeadSpritesFemale[1];
                break;
            case "TriangleMale":
                headRenderer.sprite = TriangleHeadSpritesMale[1];
                break;
            case "TriangleFemale":
                headRenderer.sprite = TriangleHeadSpritesFemale[1];
                break;
        }
    }

    public void SetFacingRight()
    {
        switch (headType+sex)
        {
            case "SquareMale":
                headRenderer.sprite = sqaureHeadSpritesMale[1];
                break;
            case "SquareFemale":
                headRenderer.sprite = sqaureHeadSpritesFemale[1];
                break;
            case "CircleMale":
                headRenderer.sprite = circleHeadSpritesMale[1];
                break;
            case "CircleFemale":
                headRenderer.sprite = circleHeadSpritesFemale[1];
                break;
            case "TriangleMale":
                headRenderer.sprite = TriangleHeadSpritesMale[1];
                break;
            case "TriangleFemale":
                headRenderer.sprite = TriangleHeadSpritesFemale[1];
                break;
        }

    }

    public void SetFacingUp()
    {
        switch (headType+sex)
        {
            case "SquareMale":
                headRenderer.sprite = sqaureHeadSpritesMale[2];
                break;
            case "SquareFemale":
                headRenderer.sprite = sqaureHeadSpritesFemale[2];
                break;
            case "CircleMale":
                headRenderer.sprite = circleHeadSpritesMale[2];
                break;
            case "CircleFemale":
                headRenderer.sprite = circleHeadSpritesFemale[2];
                break;
            case "TriangleMale":
                headRenderer.sprite = TriangleHeadSpritesMale[2];
                break;
            case "TriangleFemale":
                headRenderer.sprite = TriangleHeadSpritesFemale[2];
                break;
        }
    }

    public void SetFacingDown()
    {
        switch (headType + sex)
        {
            case "SquareMale":
                headRenderer.sprite = sqaureHeadSpritesMale[0];
                break;
            case "SquareFemale":
                headRenderer.sprite = sqaureHeadSpritesFemale[0];
                break;
            case "CircleMale":
                headRenderer.sprite = circleHeadSpritesMale[0];
                break;
            case "CircleFemale":
                headRenderer.sprite = circleHeadSpritesFemale[0];
                break;
            case "TriangleMale":
                headRenderer.sprite = TriangleHeadSpritesMale[0];
                break;
            case "TriangleFemale":
                headRenderer.sprite = TriangleHeadSpritesFemale[0];
                break;
        }
    }

}
