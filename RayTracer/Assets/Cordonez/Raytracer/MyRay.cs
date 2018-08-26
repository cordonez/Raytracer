namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class MyRay
	{
		public Vector3 Origin;
		public Vector3 Direction;

		public MyRay(Vector3 _origin, Vector3 _direction)
		{
			Origin = _origin;
			Direction = _direction;
		}

		public Vector3 PointAtT(float _t)
		{
			return Origin + _t * Direction;
		}
	}
}