using System.Runtime.Serialization;
using System.Xml;

namespace Graphs
{
    [DataContract(Name = "Vertex", IsReference = true)]
    public class Vertex
    {
        [DataMember]
        public string name {  get; set; }
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public List<Vertex> adjacents { get; set; }

        protected static int num = 0;

        public Vertex()
        {
            name = string.Empty;
            position = new Position();
            id = Vertex.num;
            Vertex.num++;
            adjacents = new List<Vertex>();
        }

        public Vertex(string _name)
        {
            name = _name;
            position = new Position();
            id = Vertex.num;
            Vertex.num++;
            adjacents = new List<Vertex>();
        }

        public Vertex(string _name, int _x, int _y)
        {
            name = _name;
            position = new Position(_x, _y);
            id = Vertex.num;
            Vertex.num++;
            adjacents = new List<Vertex>();
        }

        public Vertex(string _name, Position _position)
        {
            name = _name;
            position = _position;
            id = Vertex.num;
            Vertex.num++;
            adjacents = new List<Vertex>();
        }

        public int GetAdjacentIndex(Vertex adj)
        {
            int index = 0;
            foreach(Vertex v in adjacents)
            {
                if(v == adj)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void AddAdjacent(Vertex adj)
        {
            if(GetAdjacentIndex(adj) == -1)
            {
                adjacents.Add(adj);
            }
        }

        public void RemoveAdjacent(Vertex adj)
        {
            int index = GetAdjacentIndex(adj);
            if(index != -1)
            {
                adjacents.RemoveAt(index);
            }
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
            if(a.id != b.id) return true; else return false;
        }
    }
}