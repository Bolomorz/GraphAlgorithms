using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph
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

    public class WeightlessGraph : Graph
    {
        public new List<WeightlessEdge> edges { get; set; }

        public WeightlessGraph()
        {
            vertices = new List<Vertex>();
            edges = new List<WeightlessEdge>();
            id = WeightlessGraph.num;
            WeightlessGraph.num++;
        }

        public int GetEdgeIndex(WeightlessEdge edg)
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

        public bool IsEdgeInGraph(WeightlessEdge edg)
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

        public void AddEdge(WeightlessEdge edg)
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

        public void RemoveEdge(WeightlessEdge edg)
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
    }

    public class WeightlessDirectedGraph : WeightlessGraph
    {
        public WeightlessDirectedGraph()
        {
            vertices = new List<Vertex>();
            edges = new List<WeightlessEdge>();
            id = WeightlessDirectedGraph.num;
            WeightlessDirectedGraph.num++;
        }

        public new bool IsEdgeInGraph(WeightlessEdge edg)
        {
            foreach(var e in edges)
            {
                if(e.v1 == edg.v1 && e.v2 == edg.v2)
                {
                    return true;
                }
            }
            return false;
        }

        public new void AddEdge(WeightlessEdge edg)
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

    }

    public class WeightedGraph : Graph
    {
        public new List<WeightedEdge> edges { get; set; }

        public WeightedGraph()
        {
            vertices = new List<Vertex> ();
            edges = new List<WeightedEdge> ();
            id = WeightedGraph.num;
            WeightedGraph.num++;
        }

        public int GetEdgeIndex(WeightedEdge edg)
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

        public void RemoveEdge(WeightedEdge edg)
        {
            int index = GetEdgeIndex(edg);
            if (index != -1)
            {
                edges.RemoveAt(index);
            }
        }

        public bool IsEdgeInGraph(WeightedEdge edg)
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

        public void AddEdge(WeightedEdge edg)
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
    }

    public class WeightedDirectedGraph : WeightedGraph
    {
        public WeightedDirectedGraph()
        {
            vertices = new List<Vertex> ();
            edges = new List<WeightedEdge> ();
            id = WeightedDirectedGraph.num;
            WeightedDirectedGraph.num++;
        }

        public new bool IsEdgeInGraph(WeightedEdge edg)
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

        public new void AddEdge(WeightedEdge edg)
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
    }
}
