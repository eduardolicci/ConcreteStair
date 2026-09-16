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


        #endregion
    }

    [Plugin("ConcreteStair")]
    [PluginUserInterface("ConcreteStair.MainForm")]
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
        string alignmentBox;
        double riseBox;
        double numberOfTreadsBox;
        double firstRiserBox;
        double stairWidthBox;


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
                Alignment = alignmentBox,
                Rise = riseBox,
                NumberOfTreads = numberOfTreadsBox,
                FirstRiser = firstRiserBox,
                StairWidthProfile = stairWidthBox
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
        }
        #endregion
    }
}
