using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	//load scene with index 1
	public void StartGame(){
		SceneManager.LoadScene (1);
	}

	//quit the game
	public void ExitGame(){
		Application.Quit ();
	}

}
