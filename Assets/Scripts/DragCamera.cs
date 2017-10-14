using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraMovimentForVr
{
	public class DragCamera : MonoBehaviour {
		//run this class only if we are in the editor, not in the mobile phone
		#if UNITY_EDITOR


		bool isDragging = false;

		float startMouseX;
		float startMouseY;

		Camera cam;

		// Use this for initialization
		void Start () {

			//get your camera
			cam = GetComponent<Camera> ();
			
		}
		
		// check if the button is pressed ou lifted
		void Update () {

			//button pressed but not dragging
			if (Input.GetMouseButtonDown (1) && !isDragging) {

				isDragging = true;

				startMouseX = Input.mousePosition.x;
				startMouseY = Input.mousePosition.y;

			//button is not pressed but dragging
			} else if (Input.GetMouseButtonUp (1) && isDragging) {
					
				isDragging = false;

			}
		}

		//take the current position and calculate the distance that we moved the mouse, 
		//then drag the camera to that position and reset the star position onf the mouse
		void LateUpdate() {

			if (isDragging) {
				//current mouse position
				float endMouseX = Input.mousePosition.x;
				float endMouseY = Input.mousePosition.y;

				//diferrence/distance (in screen coordinates)
				float diffX = endMouseX - startMouseX;
				float diffY = endMouseY - startMouseY;

				//new center of the screen
				float newCenterX = Screen.width / 2 + diffX;
				float newCenterY = Screen.height / 2 + diffY;
			
				//get the world cordinate
				Vector3 LookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

				//make the camera look at the LookHerePoint
				transform.LookAt(LookHerePoint);

				//reset the position for the next call
				startMouseX = endMouseX;
				startMouseY = endMouseY;
			};

		}

		#endif
	}
}