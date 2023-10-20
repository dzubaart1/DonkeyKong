using DefaultNamespace.Barrels;

namespace DefaultNamespace
{
    public class CommonBarrelFactory : BaseFactory
    {
        public override BaseBarrel SpawnBarrel()
        {
            var go = Instantiate(BarrelPrefab);
            var res = go.AddComponent<CommonBarrel>();
            return res;
        }
    }
}