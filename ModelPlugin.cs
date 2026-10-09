using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Datatype;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using Tekla.Core.Extensions;
using System.IO;

namespace ConcreteStair

{
    public class PluginData
    {
        #region Fields
        [StructuresField("firstRiserBox")]
        public double firstRiserBox;

        [StructuresField("runBox")]
        public double runBox;

        [StructuresField("riseBox")]
        public double riseBox;

        [StructuresField("numberOfTreadsBox")]
        public double numberOfTreadsBox;

        [StructuresField("stairWidthBox")]
        public double stairWidthBox;

        [StructuresField("alignmentBox")]
        public string alignmentBox;

        [StructuresField("landingBox")]
        public double landingBox;

        [StructuresField("floorTickness")]
        public double floorTickness;

        [StructuresField("treadTickness")]
        public double treadTickness;

        [StructuresField("nosingRadiusBox")]
        public double nosingRadiusBox;

        [StructuresField("undercutBox")]
        public double undercutBox;

        [StructuresField("leftStringer")]
        public int createLeftStringerBox;

        [StructuresField("rightStringer")]
        public int createRightStringerBox;

        [StructuresField("topPerpOffsetBox")]
        public double topPerpOffsetBox;

        [StructuresField("bottomPerpOffsetBox")]
        public double bottomPerpOffsetBox;

        [StructuresField("bottomEndOffsetBox")]
        public double bottomEndOffsetBox;

        [StructuresField("topEndOffsetBox")]
        public double topEndOffsetBox;

        [StructuresField("stringerThick")]
        public double stringerThicknessBox;

        [StructuresField("slabTickness")]
        public double slabTickness;

        [StructuresField("slabLenght")]
        public double slabLenght;
        #endregion
    }

    [Plugin("ConcreteStair")]
    public class ConcreteStair : PluginBase
    {
        #region Fields
        private Model _Model;
        private PluginData _Data;

        double stairWidth;
        double stairHeight;
        double landingBox;
        double runBox;
        double floorTickness;
        double treadTickness;
        double nosingRadiusBox;
        double undercutBox;
        string alignmentBox;
        double riseBox;
        double numberOfTreadsBox;
        double firstRiserBox;
        double stairWidthBox;
        int createLeftStringerBox;
        int createRightStringerBox;
        double topPerpOffsetBox;
        double bottomPerpOffsetBox;
        double bottomEndOffsetBox;
        double topEndOffsetBox;
        double stringerThicknessBox;
        double slabTickness;
        double slabLenght;


        #endregion

        #region Properties
        private Model Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }

        private PluginData Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }
        #endregion

        #region Constructor
        public ConcreteStair(PluginData data)
        {
            Model = new Model();
            Data = data;
        }
        #endregion

        #region Overrides
        public override List<InputDefinition> DefineInput()
        {
            //
            // This is an example for selecting two points; change this to suit your needs.
            //
            List<InputDefinition> PointList = new List<InputDefinition>();
            Picker Picker = new Picker();
            ArrayList PickedPoints = Picker.PickPoints(Picker.PickPointEnum.PICK_TWO_POINTS, "Select insertion point and direction of stair");

            PointList.Add(new InputDefinition(PickedPoints));

            return PointList;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                GetValuesFromDialog();
                ArrayList Points = (ArrayList)Input[0].GetInput();
                Point StartPoint = Points[0] as Point;
                Point EndPoint = Points[1] as Point;

                CreateConcreteStair(StartPoint, EndPoint);
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.ToString());
            }

            return true;
        }
        #endregion

        #region Private methods
        private void CreateConcreteStair(Point startPoint, Point endPoint)
        {
            ConcreteStairBuilder builder = new ConcreteStairBuilder(startPoint, endPoint)
            {
                StairWidth = stairWidth,
                StairHeight = stairHeight,
                Landing = landingBox,
                Run = runBox,
                FloorThickness = floorTickness,
                TreadThickness = treadTickness,
                NosingRadius = nosingRadiusBox,
                Undercut = undercutBox,
                Alignment = alignmentBox,
                Rise = riseBox,
                NumberOfTreads = numberOfTreadsBox,
                FirstRiser = firstRiserBox,
                StairWidthProfile = stairWidthBox,
                CreateLeftStringer = createLeftStringerBox == 1,
                CreateRightStringer = createRightStringerBox == 1,
                TopPerpendicularOffset = topPerpOffsetBox,
                BottomPerpendicularOffset = bottomPerpOffsetBox,
                BottomEndOffset = bottomEndOffsetBox,
                TopEndOffset = topEndOffsetBox,
                StringerThickness = stringerThicknessBox,
                SlabThickness = slabTickness,
                BottomLanding = slabLenght
            };

            builder.BuildStair();
        }

        private void GetValuesFromDialog()
        {
            stairWidth = Data.stairWidthBox.ToMm();
            stairHeight = Data.riseBox.ToMm();
            landingBox = Data.landingBox.ToMm();
            runBox = Data.runBox.ToMm();
            floorTickness = Data.floorTickness.ToMm();
            alignmentBox = Data.alignmentBox;
            riseBox = Data.riseBox.ToMm();
            numberOfTreadsBox = Data.numberOfTreadsBox;
            firstRiserBox = Data.firstRiserBox.ToMm();
            stairWidthBox = Data.stairWidthBox.ToMm();
            treadTickness = Data.treadTickness.ToMm();
            nosingRadiusBox = Data.nosingRadiusBox.ToMm();
            undercutBox = Data.undercutBox.ToMm();
            createLeftStringerBox = Data.createLeftStringerBox;
            createRightStringerBox = Data.createRightStringerBox;
            topPerpOffsetBox = Data.topPerpOffsetBox.ToMm();
            bottomPerpOffsetBox = Data.bottomPerpOffsetBox.ToMm();
            bottomEndOffsetBox = Data.bottomEndOffsetBox.ToMm();
            topEndOffsetBox = Data.topEndOffsetBox.ToMm();
            stringerThicknessBox = Data.stringerThicknessBox.ToMm();
            slabTickness = Data.slabTickness.ToMm();
            slabLenght = Data.slabLenght.ToMm();
        }
        #endregion
    }
}
