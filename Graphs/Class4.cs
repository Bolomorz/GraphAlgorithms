﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

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
    [DataContract(Name = "Graph", IsReference = true)]
    public abstract class AGraph : IGraph
    {
        [DataMember]
        public abstract List<Vertex> vertices { get; set; }
        [DataMember]
        public abstract List<Edge> edges { get; set; }
        [DataMember]
        public abstract int id { get; set; }

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

        public abstract bool IsEdgeInGraph(Edge edg);
        public abstract void AddEdge(Edge edg);
    }

    [DataContract(Name = "Graph", IsReference = true)]
    public class Graph : AGraph
    {
        [DataMember]
        public override List<Vertex> vertices { get; set; }
        [DataMember]
        public override List<Edge> edges { get; set; }
        [DataMember]
        public override int id { get; set; }
        protected static int num = 0;

        public Graph() 
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            id = Graph.num;
            Graph.num++;
        }

        public override bool IsEdgeInGraph(Edge edg)
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

        public override void AddEdge(Edge edg)
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

    [DataContract(Name = "Graph", IsReference = true)]
    public class DirectedGraph : AGraph
    {
        [DataMember]
        public override List<Vertex> vertices { get; set; }
        [DataMember]
        public override List<Edge> edges { get; set; }
        [DataMember]
        public override int id { get; set; }
        protected static int num = 0;

        public DirectedGraph()
        {
            vertices = new List<Vertex> ();
            edges = new List<Edge> ();
            id = DirectedGraph.num;
            DirectedGraph.num++;
        }

        public override bool IsEdgeInGraph(Edge edg)
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

        public override void AddEdge(Edge edg)
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
            if (obj == null || typeof(DirectedGraph) != obj.GetType()) return false;
            DirectedGraph objGraph = (DirectedGraph)obj;
            if (id == objGraph.id) return true; else return false;
        }
    }
}
