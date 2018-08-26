namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class MyCamera
	{
		private readonly Vector3 m_lowerLeftCorner = new Vector3(-2f, -1f, -1f);
		private readonly Vector3 m_horizontal = new Vector3(4f, 0f, 0f);
		private readonly Vector3 m_vertical = new Vector3(0f, 2f, 0f);
		private readonly Vector3 m_origin = Vector3.zero;

		public MyRay GetRay(float _u, float _v)
		{
			return new MyRay(m_origin, m_lowerLeftCorner + _u * m_horizontal + _v * m_vertical);
		}
	}
}