namespace DefaultNamespace.Barrels
{
    public class SpeedBarrel : BaseBarrel
    {
        private const float SPEED = 20f;
        
        public override float GetSpeed()
        {
            return SPEED;
        }
    }
}