using System;
using System.Drawing;
using System.Windows.Forms;

namespace SMT.Utils
{
    class StateButton : Button
    {
        public StateButton() : base()
        {
            DisabledBackColor = EnabledBackColor = BackColor;
            DisabledForeColor = EnabledForeColor = ForeColor;
        }

        public Color DisabledBackColor { get; set; }
        public Color DisabledForeColor { get; set; }

        public Color EnabledBackColor { get; set; }
        public Color EnabledForeColor { get; set; }

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
