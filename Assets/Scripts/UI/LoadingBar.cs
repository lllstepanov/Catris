using System;
using UnityEngine;
using UnityEngine.UI;

namespace CatrisLoading 
{
    internal class LoadingBar : MonoBehaviour
    {
		/// <summary>
		/// Loading completed delegate object field. 
		/// </summary>
		internal static event Action OnLoadingCompleted;

		/// <summary>
		/// Progress bar field.
		/// </summary>
		[SerializeField]
		private Image bar;

		/// <summary>
		/// Speed of the filling.
		/// </summary>
		[SerializeField]
		private float speed = 0.01f;

		/// <summary>
		/// Current amount of filling of the loading bar.
		/// </summary>
		private float currentFill = 0;

		/// <summary>
		/// Stop flag. True - when filling is done.
		/// </summary>
		private bool stop = false;

		private void Update()
		{
			// IF stop field value is false - continue filling. If false - stops the filling. 
			if (stop) return;
			
			///Call Fill method.
			Fill();
		}

		/// <summary>
		/// Filling the loading bar by time. Stops when fill amount is greater than 1.
		/// </summary>
		private void Fill()
		{
			// Checking if the currentFill amount is less the 1. If it is than continue filling
			if (currentFill < 1)
			{
				// Update current fill amount. Speed + Time from last frame.
				currentFill += speed * Time.deltaTime;

				// Update progressing var is calculated fill amount.
				bar.fillAmount = currentFill;
			}
			else
			{
				// Filling is completed.
				stop = true;

				// Activete delegate
				OnLoadingCompleted?.Invoke();
			}
		}
	}
}
