namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class Metal : MyMaterial
	{
		private readonly Vector3 m_albedo;
		private readonly float m_fuzziness;

		public Metal(Vector3 _albedo, float _fuzziness)
		{
			m_albedo = _albedo;
			m_fuzziness = _fuzziness;
		}

		public override bool Scatter(MyRay _ray, HitInfo _hitInfo, out Vector3 _attenuation, out MyRay _scattered)
		{
			Vector3 reflected = Reflect(_ray.Direction.normalized, _hitInfo.Normal);
			_scattered = new MyRay(_hitInfo.HitPoint, reflected + m_fuzziness * _8_Metal.RandomPointInUnitSphere());
			_attenuation = m_albedo;
			return Vector3.Dot(_scattered.Direction, _hitInfo.Normal) > 0;
		}

		private Vector3 Reflect(Vector3 _v, Vector3 _n)
		{
			return _v - 2 * Vector3.Dot(_v, _n) * _n;
		}
	}
}