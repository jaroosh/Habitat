﻿// Version 2.1
// ©2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

//#define PROFILING
//#define DEBUG_CONTOUR

using System;
using System.Collections.Generic;
using System.Linq;
using PrimitivesPro.Primitives;
using UnityEngine;

using HashType = System.Int32;

namespace PrimitivesPro.MeshCutting
{
    public class Contour
    {
        public List<Dictionary<HashType, int>> contour; 
        private ArrayDictionary<MidPoint> midPoints;
        private LSHash lsHash;

        public Contour(int trianglesNum)
        {
            AllocateBuffers(trianglesNum);
        }

        public void AllocateBuffers(int trianglesNum)
        {
            if (lsHash == null)
            {
//                MeshUtils.Log("Allocating contour: " + trianglesNum);

                midPoints = new ArrayDictionary<MidPoint>(trianglesNum * 2);
                contour = new List<Dictionary<HashType, int>>();
                lsHash = new LSHash(0.001f, trianglesNum * 2);
            }
            else
            {
                lsHash.Clear();
                foreach (var item in contour)
                {
                    item.Clear();
                }
                contour.Clear();
                
                if (midPoints.Size < trianglesNum*2)
                {
//                    MeshUtils.Log("Re-allocating midPoint: " + trianglesNum);

                    midPoints = new ArrayDictionary<MidPoint>(trianglesNum * 2);
                }
                else
                {
                    midPoints.Clear();
                }
            }
        }

        public int MidPointsCount { get; private set; }

        struct MidPoint
        {
            public HashType id;
            public int vertexId;

#if DEBUG_CONTOUR
            // DEBUG
            public Vector3 position;
#endif

            public HashType idNext;
            public HashType idPrev;
        }

        public void AddTriangle(int triangleID, int id0, int id1, Vector3 v0, Vector3 v1)
        {
#if PROFILING
            MeasureIt.Begin("AddTriangle");
#endif
            // we need to compute position hash to make sure we find all (duplicated) vertices with different edges
            int hash0, hash1;
            lsHash.Hash(v0, v1, out hash0, out hash1);

            // filter out points with similar positions
            if (hash0 == hash1)
            {
#if PROFILING
                MeasureIt.End("AddTriangle");
#endif
                return;
            }

            MidPoint midPoint;
            if (midPoints.TryGetValue(hash0, out midPoint))
            {
                if (midPoint.idNext == HashType.MaxValue && midPoint.idPrev != hash1)
                {
                    midPoint.idNext = hash1;
                }
                else if (midPoint.idPrev == HashType.MaxValue && midPoint.idNext != hash1)
                {
                    midPoint.idPrev = hash1;
                }

                midPoints[hash0] = midPoint;
            }
            else
            {
                midPoints.Add(hash0, new MidPoint{ id = hash0, vertexId = id0, idNext = hash1, idPrev = HashType.MaxValue/*, position = v0*/});
            }

            if (midPoints.TryGetValue(hash1, out midPoint))
            {
                if (midPoint.idNext == HashType.MaxValue && midPoint.idPrev != hash0)
                {
                    midPoint.idNext = hash0;
                }
                else if (midPoint.idPrev == HashType.MaxValue && midPoint.idNext != hash0)
                {
                    midPoint.idPrev = hash0;
                }

                midPoints[hash1] = midPoint;
            }
            else
            {
                midPoints.Add(hash1, new MidPoint { id = hash1, vertexId = id1, idPrev = hash0, idNext = HashType.MaxValue/*, position = v1*/});
            }

            MidPointsCount = midPoints.Count;

#if PROFILING
            MeasureIt.End("AddTriangle");
#endif
        }

        public bool FindContours()
        {
            if (midPoints.Count == 0)
            {
                return false;
            }

#if DEBUG_CONTOUR
            MeshUtils.Log("MidPoint: " + midPoints.Count);

            foreach (var midPoint in midPoints)
            {
                if (midPoint.Value.idNext == HashType.MaxValue || midPoint.Value.idPrev == HashType.MaxValue)
                {
                    MeshUtils.Log(midPoint.Value.id + " " + midPoint.Value.idNext + " " + midPoint.Value.idPrev);

                    var sphere = PrimitivesPro.GameObjects.Sphere.Create(0.1f, 10, 0, 0, NormalsType.Vertex,
                                                                         PivotPosition.Center);
                    sphere.transform.position = midPoint.Value.position;
                    sphere.renderer.sharedMaterial.color = Color.red;
                }
            }
#endif

            var midContour = new Dictionary<HashType, int>(midPoints.Count);

            var loopsMax = 10000;

            // find contour
            var pStart = midPoints.GetFirstValue();
            midContour.Add(pStart.id, pStart.vertexId);
            midPoints.Remove(pStart.id);
            var nextP = pStart.idNext;

            while (midPoints.Count > 0)
            {
                if (nextP == HashType.MaxValue)
                {
                    return false;
                }

                MidPoint p;
                if (!midPoints.TryGetValue(nextP, out p))
                {
//                    throw new Exception("Contour failed");
                    return false;
                }

                // add new point on contour
                midContour.Add(p.id, p.vertexId);
                midPoints.Remove(p.id);

                if (midContour.ContainsKey(p.idNext))
                {
                    if (midContour.ContainsKey(p.idPrev))
                    {
                        // closing the loop!
                        contour.Add(new Dictionary<int, int>(midContour));
                        midContour.Clear();

                        if (midPoints.Count == 0)
                        {
                            break;
                        }

                        pStart = midPoints.GetFirstValue();
                        midContour.Add(pStart.id, pStart.vertexId);
                        midPoints.Remove(pStart.id);
                        nextP = pStart.idNext;
                        continue;
                    }

                    nextP = p.idPrev;
                }
                else
                {
                    nextP = p.idNext;
                }

                loopsMax--;
                if (loopsMax == 0)
                {
                    MeshUtils.Assert(false, "ForeverLoop!");
//                    throw new Exception("Contour failed");
                    return false;
                }
            }

            return true;
        }
    }
}
