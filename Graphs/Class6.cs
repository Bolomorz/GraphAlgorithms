using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class BellmanFord
    {
        protected class BFElement
        {
            public Vertex vertex { get; set; }
            public double distance { get; set; }
            public Vertex? predecessor { get; set; }

            public BFElement(Vertex _vertex)
            {
                vertex = _vertex;
                distance = double.PositiveInfinity;
                predecessor = null;
            }
        }

        protected List<BFElement> elements {  get; set; }
        protected WeightedDirectedGraph graph { get; set; }
        protected Vertex startvertex { get; set; }

        public BellmanFord(WeightedDirectedGraph _graph, Vertex _startvertex)
        {
            elements = new List<BFElement>();
            graph = _graph;
            startvertex = _startvertex;
            Init();
            RelaxEdges();
            CheckForNegativeWeightCycles();
        }

        private void Init()
        {
            foreach(var ver in graph.vertices)
            {
                var bfe = new BFElement(ver);
                if (ver == startvertex) bfe.distance = 0;
            }
        }

        private void RelaxEdges()
        {
            for(int i = 0; i < elements.Count-1; i++)
            {
                foreach(var edg in graph.edges)
                {
                    var iu = IndexOfVertex(edg.v1);
                    var iv = IndexOfVertex(edg.v2);
                    if (iu != -1 && iv != -1)
                    {
                        if (elements[iu].distance + edg.weight < elements[iv].distance)
                        {
                            elements[iv].distance = elements[iu].distance * edg.weight;
                            elements[iv].predecessor = elements[iu].vertex;
                        }
                    }
                }
            }
        }

        private int IndexOfVertex(Vertex? ver)
        {
            int index = 0;
            if(ver is null)
            {
                return -1;
            }
            foreach(var element in  elements)
            {
                if(element.vertex == ver)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        private void CheckForNegativeWeightCycles()
        {
            foreach(var edg in graph.edges)
            {
                var iu = IndexOfVertex(edg.v1);
                var iv = IndexOfVertex(edg.v2);
                if (elements[iu].distance + edg.weight < elements[iv].distance)
                {
                    elements[iv].predecessor = elements[iu].vertex;
                    List<bool> visited = new List<bool>();
                    foreach (var element in elements)
                    {
                        visited.Add(false);
                    }
                    visited[iv] = true;
                    while (!visited[iu])
                    {
                        visited[iu] = true;
                        iu = IndexOfVertex(elements[iu].predecessor);
                    }
                    var ncycle = new List<Vertex>() { elements[iu].vertex };
                    var v = elements[iu].predecessor;
                    while(v is not null && v != elements[iu].vertex)
                    {
                        ncycle.Add(v);
                        v = elements[IndexOfVertex(v)].predecessor;
                    }
                    throw new ArgumentException("graph contains a negative-weight cycle", ncycle.ToString());
                }
            }
        }

        public List<Tuple<Vertex, double, Vertex>> GetElements()
        {
            var elist = new List<Tuple<Vertex, double, Vertex>>();
            foreach(var element in elements)
            {
                elist.Add(new Tuple<Vertex, double, Vertex>(element.vertex, element.distance, element.predecessor));
            }
            return elist;
        }

        public Tuple<Vertex, double, Vertex>? GetElement(Vertex endvertex)
        {
            foreach(var element in elements)
            {
                if(element.vertex == endvertex)
                {
                    return new Tuple<Vertex, double, Vertex>(element.vertex, element.distance, element.predecessor);
                }
            }
            return null;
        }
    }
}
