using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Dijkstra
    {
        protected class DijkstraElement
        {
            public double distance { get; set; }
            public Vertex vertex { get; set; }
            public Vertex? predecessor { get; set; }
            public List<Vertex> shortestpath { get; set; }

            public DijkstraElement(Vertex _vertex)
            {
                distance = double.PositiveInfinity;
                vertex = _vertex;
                shortestpath = new List<Vertex>();
                predecessor = null;
            }
        }

        protected List<DijkstraElement> elements { get; set; }
        protected List<Vertex> Q { get; set; }
        protected WeightedDirectedGraph graph { get; set; }
        protected Vertex startvertex { get; set; }

        public Dijkstra(WeightedDirectedGraph _graph, Vertex _startvertex)
        {
            graph = _graph;
            Q = new List<Vertex>();
            elements = new List<DijkstraElement>();
            startvertex = _startvertex;
            Init();
            DijkstraCalculation();
            foreach(var dje in elements)
            {
                dje.shortestpath = ShortestPath(dje.vertex);
            }
        }

        private void Init()
        {
            foreach(var element in graph.vertices)
            {
                DijkstraElement dje = new DijkstraElement(element);
                if(element == startvertex)
                {
                    dje.distance = 0;
                }
                elements.Add(dje);
                Q.Add(element);
            }
        }

        private void DijkstraCalculation()
        {
            while(Q.Count > 0)
            {
                int index = IndexOfVertexWithSmallestDistanceInQ();
                if(index == -1)
                {
                    break;
                }
                Vertex u = Q[index];
                Q.Remove(u);
                foreach(var v in u.adjacents)
                {
                    if(IsInQ(v))
                    {
                        DistanceUpdate(u, v);
                    }
                }
            }
        }

        private int IndexOfVertexWithSmallestDistanceInQ()
        {
            var dist = double.PositiveInfinity;
            var index = -1;
            var count = 0;
            foreach(var u in Q)
            {
                var i = IndexOfVertexInElements(u);
                if (elements[i].distance < dist)
                {
                    index = count;
                    dist = elements[i].distance;
                }
                count++;
            }
            return index;
        }

        private int IndexOfVertexInElements(Vertex u)
        {
            var index = 0;
            foreach(var dje in elements)
            {
                if(dje.vertex == u)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        private bool IsInQ(Vertex v)
        {
            foreach(var element in Q)
            {
                if (element == v) return true;
            }
            return false;
        }

        private void DistanceUpdate(Vertex u,  Vertex v)
        {
            var djeu = elements[IndexOfVertexInElements(u)];
            var djev = elements[IndexOfVertexInElements(v)];
            var alt = djeu.distance + WeightingOfEdge(u, v);
            if(alt < djev.distance && alt != null)
            {
                djev.distance = (double)alt;
                djev.predecessor = u;
            }
        }

        private double? WeightingOfEdge(Vertex u, Vertex v)
        {
            foreach(var edg in graph.edges)
            {
                if(edg.v1 == u && edg.v2 == v)
                {
                    return edg.weight;
                }
            }
            return null;
        }

        private List<Vertex> ShortestPath(Vertex v)
        {
            var path = new List<Vertex>();
            path.Add(v);
            var u = v;
            var index = IndexOfVertexInElements(u);
            while (elements[index].predecessor is not null)
            {
                u = elements[index].predecessor;
                if (u is not null)
                {
                    path.Insert(index, u);
                    index = IndexOfVertexInElements(u);
                }
            }
            return path;
        }

        public List<Tuple<Vertex, double, Vertex?, List<Vertex>>> GetElements()
        {
            List<Tuple<Vertex, double, Vertex?, List<Vertex>>> ele = new List<Tuple<Vertex, double, Vertex?, List<Vertex>>>();
            foreach(var element in  elements)
            {
                ele.Add(new Tuple<Vertex, double, Vertex?, List<Vertex>>(element.vertex, element.distance, element.predecessor, element.shortestpath));
            }
            return ele;
        }

        public Tuple<Vertex, double, Vertex?, List<Vertex>>? GetElement(Vertex endvertex)
        {
            foreach(var element in  elements)
            {
                if(element.vertex == endvertex)
                {
                    return new Tuple<Vertex, double, Vertex?, List<Vertex>>(element.vertex, element.distance, element.predecessor, element.shortestpath);
                }
            }
            return null;
        }
    }
}
