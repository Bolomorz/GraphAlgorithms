namespace Graphs
{
    public class Vertex
    {
        public string name {  get; set; }
        public Position position { get; set; }
        public int id { get; set; }
        private bool isnull;

        public static Vertex NULL = new Vertex(true);
        protected static int num = 0;

        public Vertex()
        {
            name = string.Empty;
            position = new Position();
            id = Vertex.num;
            Vertex.num++;
            isnull = false;
        }

        public Vertex(string _name)
        {
            name = _name;
            position = new Position();
            id = Vertex.num;
            Vertex.num++;
            isnull = false;
        }

        public Vertex(string _name, int _x, int _y)
        {
            name = _name;
            position = new Position(_x, _y);
            id = Vertex.num;
            Vertex.num++;
            isnull = false;
        }

        public Vertex(string _name, Position _position)
        {
            name = _name;
            position = _position;
            id = Vertex.num;
            Vertex.num++;
            isnull = false;
        }

        private Vertex(bool isnull)
        {
            name = string.Empty;
            position = new Position();
            id = -1;
            isnull = true;
        }

        public bool IsNull()
        {
            return isnull;
        }

        public override string ToString()
        {
            return string.Format("Vertex {0}: {1} {2}", id, name, position);
        }

        public override int GetHashCode()
        {
            int hash = 17 + id;
            foreach(char c in name)
            {
                hash *= 17 + c;
            }
            hash += position.GetHashCode();
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || typeof(Vertex) != obj.GetType()) return false;
            Vertex v = (Vertex)obj;
            if (id != v.id) return false; else return true;
        }

        public static bool operator ==(Vertex a, Vertex b)
        {
            if(a.id == b.id) return true; else return false;
        }

        public static bool operator !=(Vertex a, Vertex b)
        {
            if (a.id != b.id) return true; else return false;
        }
    }
}