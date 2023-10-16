using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph
    {
        int GetVertexIndex(Vertex ver);
        void AddVertex(Vertex ver);
        int GetEdgeIndex(Edge edg);
        bool IsEdgeInGraph(Edge edg);
        void AddEdge(Edge edg);  
        void RemoveEdge(Edge edg);
        void RemoveVertex(Vertex ver);

    }
    public class Graph : IGraph
    {
        public List<Vertex> vertices {  get; set; }
        public List<Edge> edges { get; set; }
        protected static int num = 0;
        public int id { get; set; }

        public Graph() 
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            id = Graph.num;
            Graph.num++;
        }

        public int GetVertexIndex(Vertex ver)
        {
            int index = 0;
            foreach(var v in vertices)
            {
                if(v == ver)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void AddVertex(Vertex ver)
        {
            if(GetVertexIndex(ver) == -1)
            {
                vertices.Add(ver);
            }
        }

        public int GetEdgeIndex(Edge edg)
        {
            int index = 0;
            foreach (var e in edges)
            {
                if (e == edg)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public bool IsEdgeInGraph(Edge edg)
        {
            foreach (var e in edges)
            {
                if ((e.v1 == edg.v1 || e.v1 == edg.v2) && (e.v2 == edg.v1 || e.v2 == edg.v2))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddEdge(Edge edg)
        {
            if (GetEdgeIndex(edg) == -1)
            {
                if (!IsEdgeInGraph(edg))
                {
                    edges.Add(edg);
                    AddVertex(edg.v1);
                    AddVertex(edg.v2);
                    edg.v1.AddAdjacent(edg.v2);
                    edg.v2.AddAdjacent(edg.v1);
                }
            }
        }

        public void RemoveEdge(Edge edg)
        {
            int index = GetEdgeIndex(edg);
            if (index != -1)
            {
                edges.RemoveAt(index);
            }
        }

        public void RemoveVertex(Vertex ver)
        {
            int index = GetVertexIndex(ver);
            if (index != -1)
            {
                foreach (var e in edges)
                {
                    if (e.v1 == ver || e.v2 == ver)
                    {
                        RemoveEdge(e);
                    }
                }
                vertices.RemoveAt(index);
            }
        }

        public override string ToString()
        {
            return "graph";
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach(var ver in vertices)
            {
                hash += ver.GetHashCode();
            }
            foreach(var edg in edges)
            {
                hash += edg.GetHashCode();
            }
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || typeof(Graph) != obj.GetType()) return false;
            Graph objGraph = (Graph)obj;
            if (id == objGraph.id) return true; else return false;
        }
    }

    public class DirectedGraph : IGraph
    {
        public List<Vertex> vertices { get; set; }
        public List<Edge> edges { get; set; }
        protected static int num = 0;
        public int id { get; set; }

        public DirectedGraph()
        {
            vertices = new List<Vertex> ();
            edges = new List<Edge> ();
            id = DirectedGraph.num;
            DirectedGraph.num++;
        }

        public int GetVertexIndex(Vertex ver)
        {
            int index = 0;
            foreach (var v in vertices)
            {
                if (v == ver)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void AddVertex(Vertex ver)
        {
            if (GetVertexIndex(ver) == -1)
            {
                vertices.Add(ver);
            }
        }

        public int GetEdgeIndex(Edge edg)
        {
            int index = 0;
            foreach (var e in edges)
            {
                if (e == edg)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public bool IsEdgeInGraph(Edge edg)
        {
            foreach (var e in edges)
            {
                if (e.v1 == edg.v1 && e.v2 == edg.v2)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddEdge(Edge edg)
        {
            if (GetEdgeIndex(edg) == -1)
            {
                if (!IsEdgeInGraph(edg))
                {
                    edges.Add(edg);
                    AddVertex(edg.v1);
                    AddVertex(edg.v2);
                    edg.v1.AddAdjacent(edg.v2);
                }
            }
        }

        public void RemoveEdge(Edge edg)
        {
            int index = GetEdgeIndex(edg);
            if (index != -1)
            {
                edges.RemoveAt(index);
            }
        }

        public void RemoveVertex(Vertex ver)
        {
            int index = GetVertexIndex(ver);
            if (index != -1)
            {
                foreach (var e in edges)
                {
                    if (e.v1 == ver || e.v2 == ver)
                    {
                        RemoveEdge(e);
                    }
                }
                vertices.RemoveAt(index);
            }
        }

        public override string ToString()
        {
            return "graph";
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var ver in vertices)
            {
                hash += ver.GetHashCode();
            }
            foreach (var edg in edges)
            {
                hash += edg.GetHashCode();
            }
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || typeof(Graph) != obj.GetType()) return false;
            Graph objGraph = (Graph)obj;
            if (id == objGraph.id) return true; else return false;
        }
    }
}
