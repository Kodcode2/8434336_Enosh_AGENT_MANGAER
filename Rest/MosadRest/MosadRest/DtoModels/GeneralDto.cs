namespace MosadRest.DtoModels
{

    public class locationDto
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool IsLocationValid() =>
    x > 0 && x < 1001 && y > 0 && y < 1001;

    }
    public class DirectionDto
    {
        public string direction { get; set; }
        public Dictionary<string, (int x, int y)?> NumDirection = new()
            {
                { "nw",(-1,1) },
                { "n",(0,1) },
                { "ne",(1,1) },
                { " w",(-1,0) },
                { "e",(0,1) },
                { "sw",(1,-1) },
                { "s",(0,-1) },
                { "se",(-1,-1) }
            };

    }
    public class ResIdDto
    {
        public int Id { get; set; }
    }
}

