using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Edge
    {
        public Vertex v1 {  get; set; }
        public Vertex v2 { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public int id { get; set; }
        protected bool isnull;

        protected static int num = 0;
        protected static Edge NULL = new Edge(true);

        public Edge() 
        {
            v1 = new Vertex();
            v2 = new Vertex();
            position = new Position();
            name = string.Empty;
            id = Edge.num;
            Edge.num++;
            isnull = false;
        }

        public Edge(string _name)
        {
            v1 = new Vertex();
            v2 = new Vertex();
            position = new Position();
            name = _name;
            id = Edge.num;
            Edge.num++;
            isnull = false;
        }

        public Edge(string _name, Vertex _v1, Vertex _v2, Position _position)
        {
            if (_v1 == _v2) throw new ArgumentException("cannot create edge with same vertices.");
            v1 = _v1;
            v2 = _v2;
            position = _position;
            name = _name;
            id = Edge.num;
            Edge.num++;
            isnull = false;
        }

        public Edge(string _name, Vertex _v1, Vertex _v2, int _x, int _y)
        {
            if (_v1 == _v2) throw new ArgumentException("cannot create edge with same vertices.");
            v1 = _v1;
            v2 = _v2;
            position = new Position(_x, _y);
            name = _name;
            id = Edge.num;
            Edge.num++;
            isnull = false;
        }

        private Edge(bool _isnull)
        {
            v1 = new Vertex();
            v2 = new Vertex();
            position = new Position();
            name = string.Empty;
            id = -1;
            isnull = _isnull;
        }

        public bool IsNull()
        {
            return isnull;
        }

        public override string ToString()
        {
            return string.Format("Edge {0}: {1} {2}", id, name, position);
        }

        public override int GetHashCode()
        {
            int hash = 17 + id;
            foreach (char c in name)
            {
                hash *= 17 + c;
            }
            hash += v1.GetHashCode();
            hash += v2.GetHashCode();
            hash += position.GetHashCode();
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || typeof(Edge) != obj.GetType()) return false;
            Edge e = (Edge)obj;
            if (id != e.id) return false; else return true;
        }

        public static bool operator ==(Edge a, Edge b)
        {
            if (a.id == b.id) return true; else return false;
        }

        public static bool operator !=(Edge a, Edge b)
        {
            if (a.id != b.id) return true; else return false;
        }
    }

    public class WeightedEdge : Edge
    {
        public double weight { get; set; }

        protected static int wnum = 0;

        public WeightedEdge(string _name, Vertex _v1, Vertex _v2, double _weight, Position _position)
        {
            if (_v1 == _v2) throw new ArgumentException("cannot create edge with same vertices.");
            name = _name;
            weight = _weight;
            position = _position;
            v1 = _v1;
            v2 = _v2;
            id = WeightedEdge.wnum;
            WeightedEdge.wnum++;
            isnull = false;
        }

        public WeightedEdge(string _name, Vertex _v1, Vertex _v2, double _weight, int _x, int _y)
        {
            if (_v1 == _v2) throw new ArgumentException("cannot create edge with same vertices.");
            name = _name;
            weight = _weight;
            position = new Position( _x, _y );
            v1 = _v1;
            v2 = _v2;
            id = WeightedEdge.wnum;
            WeightedEdge.wnum++;
            isnull = false;

        }

        public override string ToString()
        {
            return string.Format("WeightedEdge {0}: {1} {2}", id, name, position);
        }

        public override int GetHashCode()
        {
            int hash = 17 + id;
            foreach (char c in name)
            {
                hash *= 17 + c;
            }
            hash += v1.GetHashCode();
            hash += v2.GetHashCode();
            hash += position.GetHashCode();
            hash += (int)weight;
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || typeof(WeightedEdge) != obj.GetType()) return false;
            WeightedEdge we = (WeightedEdge)obj;
            if (id != we.id) return false; else return true;
        }
    }
}
