using DefaultNamespace.Barrels;

namespace DefaultNamespace
{
    public class SpeedBarrelFactory : BaseFactory
    {
        public override BaseBarrel SpawnBarrel()
        {
            var go = Instantiate(BarrelPrefab);
            var res = go.AddComponent<SpeedBarrel>();
            return res;
        }
    }
}