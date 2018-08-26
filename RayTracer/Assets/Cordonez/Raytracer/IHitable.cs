namespace Cordonez.Raytracer
{
	public interface IHitable
	{
		bool Hit(MyRay _ray, float _tmin, float _tmax, HitInfo _hitInfo);
	}
}