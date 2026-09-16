using System;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Core.Extensions;

namespace ConcreteStair
{
    public class ConcreteStairBuilder
    {
        // Public properties populated by the dialog/plugin
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public double StairWidth { get; set; }
        public double StairHeight { get; set; }
        public double Landing { get; set; }
        public double Run { get; set; }
        public double FloorThickness { get; set; }
        public double TreadThickness { get; set; }
        public string Alignment { get; set; }
        public double Rise { get; set; }
        public double NumberOfTreads { get; set; }
        public double FirstRiser { get; set; }
        public double StairWidthProfile { get; set; }

        public ConcreteStairBuilder(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public void BuildStair()
        {
            // 1. Calculate the 2D horizontal direction of the stair based on user's picked points
            Vector runDirection = new Point(StartPoint.X, StartPoint.Y, 0).GetDirectionTo(new Point(EndPoint.X, EndPoint.Y, 0));
            runDirection.Normalize();

            // 2. Create the main solid concrete beam
            Beam stairBody = CreateMainStairBody(runDirection);

            // 3. Cut out the jagged treads from the top
            CutTreads(stairBody, runDirection);

            // 4. Cut out the soffit/landing from the bottom
            CutSoffit(stairBody, runDirection);

            new Model().CommitChanges();
        }

        private Beam CreateMainStairBody(Vector runDirection)
        {
            Beam stair = new Beam();
            stair.StartPoint = StartPoint;

            // If the user provided a Run, use it to calculate the length, otherwise use the picked endpoint natively.
            if (Run <= 0)
            {
                stair.EndPoint = EndPoint;
            }
            else
            {
                stair.EndPoint = StartPoint.MoveTowards(runDirection, Run + Landing);
            }

            stair.Material.MaterialString = "Concrete_Undefined";
            stair.Profile.ProfileString = $"{StairHeight}X{StairWidth}";
            stair.Class = "4";
            stair.Position.Depth = Position.DepthEnum.FRONT;

            // Handle UI alignment dropdown
            switch (Alignment)
            {
                case "Left": stair.Position.Plane = Position.PlaneEnum.LEFT; break;
                case "Right": stair.Position.Plane = Position.PlaneEnum.RIGHT; break;
                default: stair.Position.Plane = Position.PlaneEnum.MIDDLE; break;
            }

            stair.Insert();
            return stair;
        }

        private void CutTreads(Beam stairBody, Vector runDirection)
        {
            double actualFirstRiser = FirstRiser <= 0 ? 7d.ToMm() : FirstRiser;
            double treadRunLength = Run / NumberOfTreads;
            double treadRiseHeight = (Rise - actualFirstRiser) / NumberOfTreads;

            List<ContourPoint> treadProfilePoints = new List<ContourPoint>();
            
            // Start traversing from the bottom of the first riser
            Point currentProfilePoint = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + actualFirstRiser);
            treadProfilePoints.Add(new ContourPoint(currentProfilePoint, null));

            // Generate the zigzag tread steps programmatically
            for (int i = 0; i < NumberOfTreads; i++)
            {
                // Step horizontally to the nosing
                currentProfilePoint = currentProfilePoint.MoveTowards(runDirection, treadRunLength);
                treadProfilePoints.Add(new ContourPoint(currentProfilePoint, null));

                // Step vertically to the next inner corner
                currentProfilePoint = new Point(currentProfilePoint.X, currentProfilePoint.Y, currentProfilePoint.Z + treadRiseHeight);
                treadProfilePoints.Add(new ContourPoint(currentProfilePoint, null));
            }
            
            // Close the profile back to the top-start position
            Point closingPoint = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + Rise);
            treadProfilePoints.Add(new ContourPoint(closingPoint, null));

            // Create the cutting plate and apply the BooleanCut
            ContourPlate treadCutout = new ContourPlate()
            {
                Profile = new Profile { ProfileString = $"PL{StairWidthProfile * 2}" },
                Material = new Material { MaterialString = "Concrete_Undefined" },
                Class = BooleanPart.BooleanOperativeClassName,
                Position = new Position { Depth = Position.DepthEnum.MIDDLE }
            };

            foreach (ContourPoint pt in treadProfilePoints)
            {
                treadCutout.AddContourPoint(pt);
            }

            treadCutout.Insert();

            BooleanPart booleanCut = new BooleanPart()
            {
                Father = stairBody,
                Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT
            };
            booleanCut.SetOperativePart(treadCutout);
            booleanCut.Insert();
            
            treadCutout.Delete(); // Clean up the operative part
        }

        private void CutSoffit(Beam stairBody, Vector runDirection)
        {
            double actualFirstRiser = FirstRiser <= 0 ? 7d.ToMm() : FirstRiser;
            double treadRiseHeight = (Rise - actualFirstRiser) / NumberOfTreads;
            double treadRunLength = Run / NumberOfTreads;

            // 1. Calculate vertical drop needed to achieve the required perpendicular TreadThickness (throat thickness)
            // Pythagoras: length of one tread slope
            double slopeLength = Math.Sqrt(treadRunLength * treadRunLength + treadRiseHeight * treadRiseHeight);
            // cos(theta) = adjacent / hypotenuse
            double cosTheta = treadRunLength / slopeLength;
            // vertical drop = thickness / cos(theta)
            double verticalDrop = TreadThickness / cosTheta;

            // 2. Define the theoretical starting intercept of the inner corner line at the front face (C0)
            // The first nosing is at Z + actualFirstRiser. The inner corner line is lower by one treadRiseHeight.
            Point c0 = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + actualFirstRiser - treadRiseHeight);
            Point s0 = new Point(c0.X, c0.Y, c0.Z - verticalDrop);

            // 3. Define the top inner corner (C_top) and the soffit point directly below it (S_top)
            Point cTop = c0.MoveTowards(runDirection, Run);
            cTop.Z = c0.Z + (Rise - actualFirstRiser);
            Point sTop = new Point(cTop.X, cTop.Y, cTop.Z - verticalDrop);

            // 4. Determine where the sloped soffit intersects the horizontal landing soffit
            double landingSoffitZ = StartPoint.Z + Rise - FloorThickness;
            
            // Parametric line equation: Z(t) = s0.Z + t * (sTop.Z - s0.Z)
            // Solve for t when Z(t) = landingSoffitZ
            double t = (landingSoffitZ - s0.Z) / (sTop.Z - s0.Z);
            
            // Find the intersection point
            Point intersectionPt = s0.MoveTowards(runDirection, Run * t);
            intersectionPt.Z = landingSoffitZ;

            // 5. Define the cut polygon (encompassing everything below the stair)
            ContourPlate soffitCutout = new ContourPlate()
            {
                Profile = new Profile { ProfileString = $"PL{StairWidthProfile * 2}" },
                Material = new Material { MaterialString = "Concrete_Undefined" },
                Class = BooleanPart.BooleanOperativeClassName,
                Position = new Position { Depth = Position.DepthEnum.MIDDLE }
            };

            // To avoid microscopic face rendering bugs in Tekla, we overshoot the cut boundary 
            // backwards along the slope by 500mm so it completely slices through the front face.
            double overshoot = 500d.ToMm();
            Point s0_overshoot = s0.MoveTowards(runDirection, -overshoot);
            double slopeZ = (sTop.Z - s0.Z) / Run;
            s0_overshoot.Z = s0.Z - (overshoot * slopeZ);

            // Point 1: The intersection of slope and landing soffit
            soffitCutout.AddContourPoint(new ContourPoint(intersectionPt, null));

            // Point 2: The start of the sloped soffit (overshot)
            soffitCutout.AddContourPoint(new ContourPoint(s0_overshoot, null));

            // Point 3: Bottom-front bounding box corner (deep underground)
            double deepZ = Math.Min(s0_overshoot.Z, StartPoint.Z - StairHeight) - 1000d.ToMm();
            Point bottomFront = new Point(s0_overshoot.X, s0_overshoot.Y, deepZ);
            soffitCutout.AddContourPoint(new ContourPoint(bottomFront, null));

            // Point 4: Bottom-back bounding box corner (overshot past the landing)
            Point bottomBack = StartPoint.MoveTowards(runDirection, Run + Landing + overshoot);
            bottomBack.Z = deepZ;
            soffitCutout.AddContourPoint(new ContourPoint(bottomBack, null));

            // Point 5: Top-back landing soffit corner
            Point topBack = new Point(bottomBack.X, bottomBack.Y, landingSoffitZ);
            soffitCutout.AddContourPoint(new ContourPoint(topBack, null));

            soffitCutout.Insert();

            // Execute the Boolean Cut
            BooleanPart soffitBooleanCut = new BooleanPart()
            {
                Father = stairBody,
                Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT
            };
            soffitBooleanCut.SetOperativePart(soffitCutout);
            soffitBooleanCut.Insert();
            
            soffitCutout.Delete();
        }
    }
}

