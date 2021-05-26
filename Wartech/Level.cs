namespace Wartech
{
    public class Level
    {
        public Hex[,] Hexes { get; set; }

        public Level()
        {
            Hexes = new Hex[17, 16];
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Hexes[i, j] = new Hex();
                }
            }
            
            Hexes[5, 5].Building = new Building();
        }
    }
}