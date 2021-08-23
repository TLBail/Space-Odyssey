namespace Script.bullet_and_projectile
{
    public interface Shootable
    {
        public CAMP getCamp();
        public void takeDamage(int damage);
    }
}