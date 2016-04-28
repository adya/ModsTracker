using System;
using System.Drawing;
using System.Windows.Forms;

namespace SMT.Utils
{
    class StateButton : Button
    {

        public Color EnabledBackColor { get; set; }
        public Color EnabledForeColor { get; set; }
        public Color DisabledBackColor { get; set; }
        public Color DisabledForeColor { get; set; }

        public StateButton() : base()
        {
            DisabledBackColor = Color.LightGray;
            DisabledForeColor = Color.DimGray;
            EnabledBackColor = BackColor;
            EnabledForeColor = ForeColor;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            if (DesignMode)
            {
                if (Enabled) EnabledForeColor = ForeColor;
                else DisabledForeColor = ForeColor;
                Refresh();
            }
                
            base.OnForeColorChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (DesignMode)
            {
                if (Enabled) EnabledBackColor = BackColor;
                else DisabledBackColor = BackColor;
                Refresh();
            }
            base.OnBackColorChanged(e);
        }


        protected override void OnEnabledChanged(EventArgs e)
        {
            UpdateColors();
            base.OnEnabledChanged(e);
        }

        protected override void OnCreateControl()
        {
            UpdateColors();
            base.OnCreateControl();
        }

        private void UpdateColors()
        {
            BackColor = (Enabled ? EnabledBackColor : DisabledBackColor);
            ForeColor = (Enabled ? EnabledForeColor : DisabledForeColor);
        }
    }
}
