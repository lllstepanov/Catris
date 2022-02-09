using UnityEngine;

namespace Catris
{
	/// <summary>
	/// Handles visual representation of the number of merges
	/// </summary>
	internal class ComboCounter : MonoBehaviour
	{
		/// <summary>
		/// TextMesh of the object
		/// </summary>
		private TextMesh textMesh;

		/// <summary>
		/// Flag is responsible for movement
		/// </summary>
		private bool stop = true;

		/// <summary>
		/// How long movement should takes time
		/// </summary>
		[SerializeField]
		private float time;

		/// <summary>
		/// Current time of the movement
		/// </summary>
		private float timerTime;

		/// <summary>
		/// Color alpha of the object
		/// </summary>
		private float alpha;

		/// <summary>
		/// Speed of the movement
		/// </summary>
		[SerializeField]
		private float speed;

		/// <summary>
		/// Target where object should go
		/// </summary>
		private Vector3 target;

		/// <summary>
		/// Step of every move
		/// </summary>
		private float step;

		/// <summary>
		/// Current color of the object 
		/// </summary>
		private Color color;

		private void Awake()
		{
			/// Assigns TextMesh component to variable
			textMesh = GetComponent<TextMesh>();
		}


		/// <summary>
		/// Set up the ComboCounter object 
		/// </summary>
		
		internal void Show(string text, Vector3 position)
		{
			/// Sets stop flag to false which means movement should start
			stop = false;

			/// Update current time
			timerTime = time;

			/// Sets start position of the object
			transform.position = position;
			
			/// Set the target where object should  to move
			target = position + new Vector3(0,0.5f,0);

			/// Sets current alpha of the object
			alpha = 1;

			/// Sets the color of the object
			color = new Color(255, 255, 255, alpha);

			/// Sets the text of the object
			textMesh.text = "x"+text;
		}

		private void Update()
		{
			/// Check if object should move and fade
			if (stop) return;
			
			/// Fading of the object
			Fade();
				
			/// Movement of the object
			Movement();
		}

		/// <summary>
		/// Handles the fading by time of the object
		/// </summary>
		private void Fade()
		{
			/// Checks if the timer times more the 0
			if (timerTime > 0)
			{
				/// Reduce timer time
				timerTime -= speed*Time.deltaTime;
				
				/// Reduce alpha 
				alpha -= speed*Time.deltaTime;

				/// Sets a new color 
				color = new Color(color.r, color.g, color.b, alpha);

				/// Sets a new color to TextMesh
				textMesh.color = color;
			}
			else
			{
				/// Stops the fading
				stop = true;
				
				/// Deactivates the object 
				gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// Moves the object
		/// </summary>
		private void Movement()
		{
			/// Calc the step of movement
			step = speed * Time.deltaTime;
			
			/// Sets a new postion according to step
			transform.position = Vector3.MoveTowards(transform.position, target, step);
		}
	}
}
