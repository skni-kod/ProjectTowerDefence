using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [HideInInspector]
    protected float size = 1f;

    // Aktualna wartość procentowa reprezentowana przez pasek
    [HideInInspector]
    protected float currentPercentValue = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie koloru paska w zależności od jego zastosowania
        GameObject parentObject = transform.parent.gameObject;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (parentObject.layer == LayerMask.NameToLayer("Enemies")) sprite.color = Color.red;
        else if (parentObject.layer == LayerMask.NameToLayer("Towers")) sprite.color = Color.yellow;
        else sprite.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        // Ustawienie sprite'a zawsze przodem do kamery
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

        // Ustawienie odpowiedniej długości paska
        transform.localScale = new Vector3(size * currentPercentValue / 10, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// Ustawienie wartości paska
    /// </summary>
    /// <param name="value">Wartość do ustawienia z zakresu [0-100]</param>
    public void SetValue(float value)
    {
        if (value < 0) currentPercentValue = 0f;
        else if (value > 100) currentPercentValue = 100f;
        else currentPercentValue = value;
    }

    /// <summary>
    /// Inicjalizacja właściwości paska
    /// </summary>
    /// <param name="height">Wysokość nad obiektem</param>
    /// <param name="size">Wielkość paska</param>
    public void Initialize(float height = 1.5f, float size = 1)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        this.size = size;
        transform.localScale *= size;
    }
}
