using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenPlayerFeedbackForm()
	{
		#if !UNITY_EDITOR
		openWindow("https://docs.google.com/forms/d/e/1FAIpQLScwQDBHrVtTYs1TSRmxCMhIDjP8cz9FRLt8WkJx_JpeYVQ3Vw/viewform?usp=sf_link");
		#endif
		Debug.Log("Player Feedback Form has been opened");
	}

	public void OpenInstagram()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.instagram.com/thejubbzone/");
		#endif
		Debug.Log("The Jubb Zone Insta has been opened");
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}