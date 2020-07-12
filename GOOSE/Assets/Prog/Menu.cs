using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public GameObject creditos;

	public GameObject[] boyz;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

	public void Credits()
	{
		if (!creditos.activeSelf)
		{
			creditos.SetActive(true);
			foreach (GameObject boy in boyz)
			{
				boy.SetActive(false);
			}
		}
		else
		{
			creditos.SetActive(false);
			foreach (GameObject boy in boyz)
			{
				boy.SetActive(true);
			}
		}
	}


}
