using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 10f)]
    public float size = 1f;

    //usunac
    // Aktualna wartość procentowa reprezentowana przez pasek
    protected float currentPercentValue = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie koloru paska w zależności od jego zastosowania
        GameObject parentObject = transform.parent.gameObject;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (parentObject.layer == LayerMask.NameToLayer("Enemies")) sprite.color = Color.red;
        else if (parentObject.tag.Equals("Nexus")) sprite.color = Color.cyan;
        else if (parentObject.layer == LayerMask.NameToLayer("Towers") || parentObject.layer == LayerMask.NameToLayer("Building")) sprite.color = Color.yellow;
        else sprite.color = Color.black;

        // Dostosowanie wielkości
        transform.localScale = new Vector3(size * 10, size, size);
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
}
