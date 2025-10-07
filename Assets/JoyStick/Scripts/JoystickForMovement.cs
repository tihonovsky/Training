using UnityEngine;

public class JoystickForMovement : JoyStickHeandler
{
   public Vector3 ReturnVectorDirection()
   {
      if (_inputVector.x != 0 || _inputVector.y != 0)
      {
         return new Vector3(_inputVector.x, 0, _inputVector.y);
      }
      else
      {
         return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      }
   }
}
