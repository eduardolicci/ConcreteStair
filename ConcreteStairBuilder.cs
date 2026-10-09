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
        public double NosingRadius { get; set; }
        public double Undercut { get; set; }

        public bool CreateLeftStringer { get; set; }
        public bool CreateRightStringer { get; set; }
        public double TopPerpendicularOffset { get; set; }
        public double BottomPerpendicularOffset { get; set; }
        public double BottomEndOffset { get; set; }
        public double TopEndOffset { get; set; }
        public double StringerThickness { get; set; }
        public double SlabThickness { get; set; }
        public double BottomLanding { get; set; }

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

            // 4. Cut out the sloped soffit from the bottom
            CutSoffit(stairBody, runDirection);

            // 5. Create Left/Right parametric stringers
            CreateStringers(runDirection);

            // Commit the structural changes
            new Model().CommitChanges();
        }

        private Beam CreateMainStairBody(Vector runDirection)
        {
            Beam stair = new Beam();
            double safeDrop = Math.Max(SlabThickness, 1000d.ToMm());
            double totalProfileHeight = StairHeight + safeDrop;

            Point p1 = StartPoint.MoveTowards(runDirection, -BottomLanding);
            p1.Z -= safeDrop;
            stair.StartPoint = p1;

            // If the user provided a Run, use it to calculate the length, otherwise use the picked endpoint natively.
            if (Run <= 0)
            {
                Point p2 = new Point(EndPoint.X, EndPoint.Y, EndPoint.Z);
                p2.Z -= safeDrop;
                stair.EndPoint = p2;
            }
            else
            {
                Point p2 = StartPoint.MoveTowards(runDirection, Run + Landing);
                p2.Z -= safeDrop;
                stair.EndPoint = p2;
            }

            stair.Material.MaterialString = "Concrete_Undefined";
            stair.Profile.ProfileString = totalProfileHeight.ToString(System.Globalization.CultureInfo.InvariantCulture) + "X" + StairWidth.ToString(System.Globalization.CultureInfo.InvariantCulture);
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
            
            // Create the chamfer object if a radius is provided
            Chamfer nosingChamfer = null;
            if (NosingRadius > 0)
            {
                nosingChamfer = new Chamfer(NosingRadius, 0, Chamfer.ChamferTypeEnum.CHAMFER_ROUNDING);
            }

            double overshoot = 10d.ToMm();

            // 1. Top Left Air (above and behind the start point)
            Point topLeftAir = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + Rise + overshoot).MoveTowards(runDirection, -BottomLanding - overshoot);
            treadProfilePoints.Add(new ContourPoint(topLeftAir, null));

            // 2. Bottom Left Air (below and behind the start point)
            // The cut must not go below Z = StartPoint.Z in the bottom landing area
            Point bottomLeftAir = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z).MoveTowards(runDirection, -BottomLanding - overshoot);
            treadProfilePoints.Add(new ContourPoint(bottomLeftAir, null));

            // 3. Bottom of Front Face (directly below the first nosing)
            Point bottomFrontFace = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z);
            if (Undercut > 0)
            {
                bottomFrontFace = bottomFrontFace.MoveTowards(runDirection, Undercut);
            }
            treadProfilePoints.Add(new ContourPoint(bottomFrontFace, null));

            // 4. First Nosing
            Point currentProfilePoint = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + actualFirstRiser);
            treadProfilePoints.Add(new ContourPoint(currentProfilePoint, nosingChamfer));

            // Generate the zigzag tread steps programmatically
            for (int i = 0; i < NumberOfTreads; i++)
            {
                // Step horizontally to the back of the tread (Inner corner of the stair)
                // We add Undercut to slant the riser backwards
                Point innerCorner = currentProfilePoint.MoveTowards(runDirection, treadRunLength + Undercut);
                treadProfilePoints.Add(new ContourPoint(innerCorner, null));

                // Step vertically to the top of the next riser (Outer corner / Nosing of the stair)
                // The next nosing geometry (run/rise) does not change
                currentProfilePoint = currentProfilePoint.MoveTowards(runDirection, treadRunLength);
                currentProfilePoint = new Point(currentProfilePoint.X, currentProfilePoint.Y, currentProfilePoint.Z + treadRiseHeight);
                treadProfilePoints.Add(new ContourPoint(currentProfilePoint, nosingChamfer));
            }
            
            // 5. Back of Landing (moves horizontally from the last nosing to cover the landing)
            Point backOfLanding = currentProfilePoint.MoveTowards(runDirection, Landing + overshoot);
            treadProfilePoints.Add(new ContourPoint(backOfLanding, null));

            // 6. Top Right Air (above the landing)
            Point topRightAir = new Point(backOfLanding.X, backOfLanding.Y, backOfLanding.Z + overshoot);
            treadProfilePoints.Add(new ContourPoint(topRightAir, null));

            // Create the cutting plate and apply the BooleanCut
            ContourPlate treadCutout = new ContourPlate()
            {
                Profile = new Profile { ProfileString = "PL" + (StairWidthProfile * 2).ToString() },
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
            Point c0 = new Point(StartPoint.X, StartPoint.Y, StartPoint.Z + actualFirstRiser - treadRiseHeight);
            Point s0 = new Point(c0.X, c0.Y, c0.Z - verticalDrop);

            // 3. Define the top inner corner (C_top) and the soffit point directly below it (S_top)
            Point cTop = c0.MoveTowards(runDirection, Run);
            cTop.Z = c0.Z + (Rise - actualFirstRiser);
            Point sTop = new Point(cTop.X, cTop.Y, cTop.Z - verticalDrop);

            // 4. Determine where the sloped soffit intersects the top horizontal landing soffit
            double topLandingSoffitZ = StartPoint.Z + Rise - FloorThickness;
            double tTop = (topLandingSoffitZ - s0.Z) / (sTop.Z - s0.Z);
            Point topIntersectionPt = s0.MoveTowards(runDirection, Run * tTop);
            topIntersectionPt.Z = topLandingSoffitZ;

            // 5. Determine where the sloped soffit intersects the bottom horizontal landing soffit
            double bottomLandingSoffitZ = StartPoint.Z - SlabThickness;
            double tBot = (bottomLandingSoffitZ - s0.Z) / (sTop.Z - s0.Z);
            Point botIntersectionPt = s0.MoveTowards(runDirection, Run * tBot);
            botIntersectionPt.Z = bottomLandingSoffitZ;

            // 6. Define the cut polygon (encompassing everything below the stair)
            ContourPlate soffitCutout = new ContourPlate()
            {
                Profile = new Profile { ProfileString = "PL" + (StairWidthProfile * 2).ToString() },
                Material = new Material { MaterialString = "Concrete_Undefined" },
                Class = BooleanPart.BooleanOperativeClassName,
                Position = new Position { Depth = Position.DepthEnum.MIDDLE }
            };

            double overshoot = 10d.ToMm();

            // Point 1: Top-back top landing soffit corner
            Point topBack = StartPoint.MoveTowards(runDirection, Run + Landing + overshoot);
            topBack.Z = topLandingSoffitZ;
            soffitCutout.AddContourPoint(new ContourPoint(topBack, null));

            // Point 2: The intersection of slope and top landing soffit
            soffitCutout.AddContourPoint(new ContourPoint(topIntersectionPt, null));

            // Point 3: The intersection of slope and bottom landing soffit
            soffitCutout.AddContourPoint(new ContourPoint(botIntersectionPt, null));

            // Point 4: Bottom-back of bottom landing (overshot backward)
            Point botBack = botIntersectionPt.MoveTowards(runDirection, -BottomLanding - Run * tBot - overshoot);
            botBack.Z = bottomLandingSoffitZ;
            soffitCutout.AddContourPoint(new ContourPoint(botBack, null));

            // Point 5: Bottom-front bounding box corner (deep underground, moved back)
            double safeDrop = Math.Max(SlabThickness, 1000d.ToMm());
            double deepZ = StartPoint.Z - safeDrop - 50d.ToMm();
            Point bottomDeepBack = new Point(botBack.X, botBack.Y, deepZ);
            soffitCutout.AddContourPoint(new ContourPoint(bottomDeepBack, null));

            // Point 6: Bottom-front bounding box corner (deep underground, moved forward)
            Point bottomDeepFront = new Point(topBack.X, topBack.Y, deepZ);
            soffitCutout.AddContourPoint(new ContourPoint(bottomDeepFront, null));

            soffitCutout.Insert();

            // Execute the Boolean Cut
            BooleanPart soffitBooleanCut = new BooleanPart()
            {
                Father = stairBody,
                Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT
            };
            soffitBooleanCut.SetOperativePart(soffitCutout);
            soffitBooleanCut.Insert();
            
            soffitCutout.Delete(); // Clean up the operative part
        }

        private void CreateStringers(Vector runDirection)
        {
            if (!CreateLeftStringer && !CreateRightStringer) return;

            double actualFirstRiser = FirstRiser <= 0 ? 7d.ToMm() : FirstRiser;
            double treadRunLength = Run / NumberOfTreads;

            // Nosing line vectors
            double n_dx = Run;
            double n_dz = Rise - actualFirstRiser;
            double n_len = Math.Sqrt(n_dx * n_dx + n_dz * n_dz);
            double nx = n_dx / n_len;
            double nz = n_dz / n_len;

            // Perpendicular UP vector
            double px = -nz;
            double pz = nx;
            double M = nz / nx;

            // Top edge line parameters (L_top)
            double top_px = px * TopPerpendicularOffset;
            double top_pz = actualFirstRiser + pz * TopPerpendicularOffset;
            double B_top = top_pz - M * top_px;

            // Bottom edge line parameters (L_bot)
            double bot_px = (treadRunLength + Undercut) - px * BottomPerpendicularOffset;
            double bot_pz = actualFirstRiser - pz * BottomPerpendicularOffset;
            double B_bot = bot_pz - M * bot_px;

            // Boundary points X & Z
            double X_front = -BottomLanding;
            double X_back = Run + Landing + TopEndOffset;
            double Z_bottom = BottomEndOffset;

            List<Point> profile2D = new List<Point>();

            // 1. Top-Front
            double z_top_front = M * X_front + B_top;
            profile2D.Add(new Point(X_front, 0, z_top_front));

            // 2. Bottom-Front
            double z_bot_front = M * X_front + B_bot;
            if (z_bot_front < Z_bottom)
            {
                profile2D.Add(new Point(X_front, 0, Z_bottom));
                double x_floor = (Z_bottom - B_bot) / M;
                profile2D.Add(new Point(x_floor, 0, Z_bottom));
            }
            else
            {
                profile2D.Add(new Point(X_front, 0, z_bot_front));
            }

            // 3. Bottom-Back
            double z_bot_back = M * X_back + B_bot;
            if (z_bot_back < Z_bottom)
            {
                profile2D.Add(new Point(X_back, 0, Z_bottom));
            }
            else
            {
                profile2D.Add(new Point(X_back, 0, z_bot_back));
            }

            // 4. Top-Back
            double z_top_back = M * X_back + B_top;
            profile2D.Add(new Point(X_back, 0, z_top_back));

            // Generate 3D lateral vectors
            Vector leftDir = new Vector(-runDirection.Y, runDirection.X, 0).GetNormal();
            Vector rightDir = new Vector(runDirection.Y, -runDirection.X, 0).GetNormal();

            double leftFaceOffset = 0;
            double rightFaceOffset = 0;
            switch (Alignment)
            {
                case "Left":
                    leftFaceOffset = StairWidth;
                    rightFaceOffset = 0;
                    break;
                case "Right":
                    leftFaceOffset = 0;
                    rightFaceOffset = StairWidth;
                    break;
                default: // Middle
                    leftFaceOffset = StairWidth / 2.0;
                    rightFaceOffset = StairWidth / 2.0;
                    break;
            }

            double thickness = StringerThickness <= 0 ? 200d.ToMm() : StringerThickness;

            if (CreateLeftStringer)
            {
                double centerOffset = leftFaceOffset + thickness / 2.0;
                InsertStringer(profile2D, runDirection, leftDir, centerOffset, thickness);
            }

            if (CreateRightStringer)
            {
                double centerOffset = rightFaceOffset + thickness / 2.0;
                InsertStringer(profile2D, runDirection, rightDir, centerOffset, thickness);
            }
        }

        private void InsertStringer(List<Point> profile2D, Vector runDir, Vector lateralDir, double lateralOffset, double thickness)
        {
            ContourPlate stringer = new ContourPlate();
            stringer.Profile.ProfileString = "PL" + thickness.ToString();
            stringer.Material.MaterialString = "Concrete_Undefined";
            stringer.Class = "8"; // Different color class for visibility
            stringer.Position.Depth = Position.DepthEnum.MIDDLE;

            foreach (var p2d in profile2D)
            {
                Point p3d = new Point(
                    StartPoint.X + runDir.X * p2d.X + lateralDir.X * lateralOffset,
                    StartPoint.Y + runDir.Y * p2d.X + lateralDir.Y * lateralOffset,
                    StartPoint.Z + p2d.Z
                );
                stringer.AddContourPoint(new ContourPoint(p3d, null));
            }

            stringer.Insert();
        }
    }
}
