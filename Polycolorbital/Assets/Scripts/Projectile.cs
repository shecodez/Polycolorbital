using UnityEngine;

public class Projectile : MonoBehaviour {

    float speed;
    Rigidbody2D r2d;

    public float Speed { get; set; }

    void Start ()
    {
        speed = 10f;
        
        r2d = GetComponent<Rigidbody2D>();
        Vector2 _direction = r2d.position;
        _direction.Normalize();      
        r2d.velocity = new Vector2(_direction.x * -speed, _direction.y * -speed);
    }

    public void ChangeProjectileColor(Color color)
    {       
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void OnTriggerEnter2D (Collider2D colInfo)
    {
        if (colInfo.tag == "ColorArc")
        {
            // Change Projectile color to ColorArc color
            Color colorArc = colInfo.gameObject.GetComponent<LineRenderer>().startColor;
            Color projectileColor = gameObject.GetComponent<SpriteRenderer>().color;

            if (projectileColor == Color.white) 
                ChangeProjectileColor(colorArc);
            else
                Destroy(gameObject); // Destroy Projectile
            return;
        }
        if (colInfo.tag == "PolyBase")
        {
            Color polygonColor = colInfo.gameObject.GetComponent<MeshRenderer>().material.color;
            Color projectileColor = gameObject.GetComponent<SpriteRenderer>().color;

            if (projectileColor == polygonColor) 
            {
                // if all blocks are destroyed
                GameObject[] blocks = GameObject.FindGameObjectsWithTag("ColorBlock");
                if (blocks.Length == 0)
                {
                    Destroy(colInfo.gameObject); // Destroy Polygon                
                    Debug.Log("Goto next Level");// GameManager.NextLevel;
                }
            }
            Destroy(gameObject); // Destroy Projectile
            return;
        }
        if (colInfo.tag == "ColorBlock")
        {
            Color blockColor = colInfo.gameObject.GetComponent<LineRenderer>().startColor;
            Color projectileColor = gameObject.GetComponent<SpriteRenderer>().color;

            if (blockColor == projectileColor)
            {
                // Remove ColorBlock                
                colInfo.gameObject.SendMessageUpwards("RemoveBlock", colInfo.gameObject);
            }
            if (blockColor != projectileColor)
            {
                // Create colorblock
                int layerIndex = colInfo.transform.parent.GetSiblingIndex();
                int blockIndex = colInfo.gameObject.GetComponent<ColorBlock>().startIndex;
                
                int[] args = { layerIndex, blockIndex };                
                colInfo.gameObject.SendMessageUpwards("CreateBlockArgs", args);
            }
            Destroy(gameObject); // Destroy Projectile
            return;
        }
    }

    void OnBecameInvisible ()
    {
        // Destroy the projectile if it escapes color shield
        Destroy(gameObject);
    }
}
