﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    public class MatrixGraph : Graph
    {
        Edge[][] edgesTab;

        public MatrixGraph(int verticesCount, bool directed = false): base(verticesCount, directed)
        {
            edgesTab = new Edge[verticesCount][];
            for(int i=0; i<verticesCount; i++)
            {
                edgesTab[i] = new Edge[verticesCount];
            }            
        }


        public override bool AddEdge(Edge e)
        {
            return AddEdge(e.From, e.To, e.Weight);
        }

        public override bool AddEdge(int from, int to, double weight = 0)
        {
            Edge e = new Edge(from, to, weight);
            if (edgesTab[e.From][e.To] != null)
            {
                return false;
            }
            else
            {
                edgesTab[e.From][e.To] = new Edge(e);
                return true;
            }
        }

        public override bool DeleteEdge(Edge e, bool checkWeight=false)
        {
            Edge edge = edgesTab[e.From][e.To];
            if (e!=null)
            {
                if(edge.Weight == e.Weight)
                {
                    edgesTab[e.From][e.To] = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override bool DeleteEdge(int from, int to)
        {
            edgesTab[from][to] = null;
            return true;
        }

        public override Edge GetEdge(int from, int to)
        {
            return edgesTab[from][to];
        }

        public override IEnumerable<Edge> GetEdgesFrom(int vertexID)
        {
            Edge[] edges = edgesTab[vertexID];
            return edges.Where(x => x != null);
        }

        public override IEnumerable<Edge> GetEdgesTo(int vertexID)
        {
            List<Edge> edges = new List<Edge>();
            for(int i=0; i<verticesCount; i++)
            {
                Edge e = edgesTab[i][vertexID];
                if (e!=null)
                {
                    edges.Add(e);
                }
            }
            return edges;
        }

        public override Graph IsolatedGraph(bool directed)
        {
            return new MatrixGraph(this.verticesCount, directed);
        }

        public override Graph NewGraphOfSameType(int verticesCount, bool directed = false)
        {
            return new MatrixGraph(verticesCount, directed);
        }

        public override Graph ReversedGraph()
        {
            MatrixGraph reversedGraph = new MatrixGraph(verticesCount, directed);
            for(int i=0; i<verticesCount; i++)
            {
                for(int j=0; j<verticesCount; j++)
                {
                    reversedGraph.edgesTab[j][i] = new Edge(edgesTab[i][j]);
                }
            }
            return reversedGraph;
        }
    }
}