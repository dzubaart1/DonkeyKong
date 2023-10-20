namespace DefaultNamespace.Barrels
{
    public class CommonBarrel : BaseBarrel
    {
        private const float SPEED = 10f;
        
        public override float GetSpeed()
        {
            return SPEED;
        }
    }
}