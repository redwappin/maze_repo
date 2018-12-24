
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Menu : MonoBehaviour {

    // Use this for initialization
    int NumberOfRow=5;
    public TextMeshProUGUI Rows;
	void Start () {
        NumberOfRow = PlayerPrefs.GetInt("Rows");

	}
	
	// Update is called once per frame
	void Update () {
        UpdateNumberOfRows();

    }

    public void DecrementNumberOfRow()
    {
        if (NumberOfRow > 5)
        {
            NumberOfRow--;
        }
        PlayerPrefs.SetInt("Rows", NumberOfRow);

    }

    public void IncrementNumberOfRow()
    {
        if (NumberOfRow < 15)
        {
            NumberOfRow++;
        }
        PlayerPrefs.SetInt("Rows", NumberOfRow);
    }

    public void StartGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Maze");
    }

    public void UpdateNumberOfRows()
    {
        this.Rows.text = NumberOfRow.ToString();
    }
}
