using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Position
    {
        public int x;
        public int y;
        
        public Position()
        {
            x = 0; y = 0;
        }

        public Position(int _x, int _y)
        {
            x = _x; y= _y;
        }

        public override string ToString()
        {
            return String.Format("({0}|{1})", x, y);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + x ^ y;
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || typeof(Vertex) != obj.GetType()) return false;
            Position p = (Position)obj;
            if (p.x == x && p.y == y) return true; else return false;
        }

        public static bool operator ==(Position a, Position b)
        {
            if (a.x == b.x && a.y == b.y) return true; else return false;
        }

        public static bool operator !=(Position a, Position b)
        {
            if (a.x != b.x && a.y != b.y) return true; else return false;
        }
    }
}
