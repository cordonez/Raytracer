namespace Cordonez.Raytracer
{
	using System.Collections.Generic;

	public static class ListExtensions
	{
		public static bool Hit<THitable>(this List<THitable> _list, MyRay _ray, float _tmin, float _tmax, HitInfo _hitInfo) where THitable : IHitable
		{
			bool hitAnything = false;
			float closestHit = _tmax;
			HitInfo tempHitInfo = new HitInfo();
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].Hit(_ray, _tmin, closestHit, tempHitInfo))
				{
					hitAnything = true;
					closestHit = tempHitInfo.T;
					_hitInfo.T = closestHit;
					_hitInfo.HitPoint = tempHitInfo.HitPoint;
					_hitInfo.Normal = tempHitInfo.Normal;
					_hitInfo.Material = tempHitInfo.Material;
				}
			}

			return hitAnything;
		}
	}
}