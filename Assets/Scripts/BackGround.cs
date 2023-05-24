using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Sprite[] backgrounds;
    public SpriteRenderer backgroundRenderer;
    private Vector3 maxPoint;
    private Vector3 minPoint;
    void Start()
    {
        // Chọn một background ngẫu nhiên
        int randomIndex = Random.Range(0, backgrounds.Length);
        Sprite randomBackground = backgrounds[randomIndex];

        // Gán background cho renderer
        backgroundRenderer.sprite = randomBackground;

        //Điều chỉnh background vừa với khung camera
        maxPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0.0f));
        minPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        float weightScreen = maxPoint.x - minPoint.x;
        float heightScreen = maxPoint.y - minPoint.y;

        float pixelsPerUnit = this.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector3 sizeOfSprite = new Vector3(this.GetComponent<SpriteRenderer>().sprite.rect.width, this.GetComponent<SpriteRenderer>().sprite.rect.height, 0.0f);

        Vector3 sizeOfBackround = new Vector3(weightScreen / (sizeOfSprite.x / pixelsPerUnit), heightScreen / (sizeOfSprite.x / pixelsPerUnit), 0.0f);

        this.transform.localScale = sizeOfBackround;
    }

    
}
