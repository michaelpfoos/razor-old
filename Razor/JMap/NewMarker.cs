﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assistant.JMap
{
    public partial class NewMarker : Form
    {
        public MapPanel mapPanel;

        public string name;
        public int x;
        public int y;
        public string extra;
        public CheckBox IsPublic;
        public string MarkerOwner;

        public NewMarker(MapPanel mapPan)
        {
            InitializeComponent();
            mapPanel = mapPan;
            IsPublic = this.IsPublicCheckbox;

            //this.Location = new Point(mapPanel.Left - (this.Width + 5), mapPanel.Top);

            if (mapPanel.AddingMarker)
                CreateMarker();

            if (mapPanel.EditingMarker)
                EditMarker(mapPanel.MarkerToEdit);

            //this.ActiveControl = newMarkerName;
        }

        private void CreateMarker()
        {
            PointF lastPos = mapPanel.mouseLastPosOnMap;
            x = Convert.ToInt32(Math.Floor(lastPos.X));
            y = Convert.ToInt32(Math.Floor(lastPos.Y));

            this.newMarkerX.Text = x.ToString();
            this.newMarkerY.Text = y.ToString();
            this.OwnerLabel.Text = mapPanel.FocusMobile.Name;

            mapPanel.MarkerToEdit = null;
        }

        private void EditMarker(JMapButton markerToEdit)
        {
            x = Convert.ToInt32(Math.Floor(markerToEdit.mapLoc.X));
            y = Convert.ToInt32(Math.Floor(markerToEdit.mapLoc.Y));

            this.newMarkerName.Text = markerToEdit.displayText;
            this.newMarkerX.Text = x.ToString();
            this.newMarkerY.Text = y.ToString();
            this.newMarkerExtra.Text = markerToEdit.extraText;
            this.IsPublic.Checked = markerToEdit.IsPublic;
            this.OwnerLabel.Text = markerToEdit.MarkerOwner;

            
        }

        private void NewMarkerPoint(object sender, EventArgs e)
        {
            if(mapPanel.AddingMarker)
            {
                mapPanel.AddMarker(new Point(x, y), IsPublic.Checked, mapPanel.FocusMobile.Name, this.newMarkerName.Text, this.newMarkerExtra.Text);
            }
            if(mapPanel.EditingMarker)
            {
                mapPanel.MarkerToEdit.mapLoc = new Point(x, y);
                mapPanel.MarkerToEdit.displayText = this.newMarkerName.Text;
                mapPanel.MarkerToEdit.extraText = this.newMarkerExtra.Text;
                mapPanel.MarkerToEdit.MarkerOwner = this.OwnerLabel.Text;
                mapPanel.MarkerToEdit.IsPublic = this.IsPublic.Checked;

                mapPanel.MarkerToEdit.LoadButton();
            }

            mapPanel.EditingMarker = false;
            mapPanel.AddingMarker = false;
            mapPanel.MarkerToEdit = null;

            mapPanel.UpdateAll();

            this.Dispose();

        }

        private void CancelPoint(object sender, EventArgs e)
        {
            mapPanel.EditingMarker = false;
            mapPanel.AddingMarker = false;
            mapPanel.MarkerToEdit = null;

            this.Dispose();
        }

        protected override void OnShown(EventArgs e)
        {
            this.Focus();
            this.BringToFront();
            this.newMarkerName.Focus();
            base.OnShown(e);
        }
    }
}
