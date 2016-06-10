using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT.Utils
{
    static class ControlMessagesExtension
    {
        public static Color ErrorBackColor { get; set; }
        public static Color ErrorLabelColor { get; set; }

        public static Color WarningBackColor { get; set; }
        public static Color WarningLabelColor { get; set; }

        public static Color ValidBackColor { get; set; }
        public static Color ValidLabelColor { get; set; }

        public static Color ClearBackColor { get; set; }
        public static Color ClearLabelColor { get; set; }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies error style. <para/>
        /// Empty or null messages will be hidden.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <param name="text">Text of the message.</param>
        public static void SetError(this Control control, string text = null)
        {
            control.SetMessage(text);
            SetColor(control.GetBoundedLabel(), ErrorLabelColor);
            SetColor(control, ErrorBackColor);
        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies warning style.<para/>
        /// Empty or null messages will be hidden.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <param name="text">Text of the message.</param>
        public static void SetWarning(this Control control, string text = null)
        {
            control.SetMessage(text);
            SetColor(control.GetBoundedLabel(), WarningLabelColor);
            SetColor(control, WarningBackColor);

        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies valid style.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="hideEmtpy">Flag indicating whether the bounded label should be hidden if message is empty. </param>
        public static void SetValidMessage(this Control control, string text = null, bool hideEmpty = true)
        {
            control.SetMessage(text, hideEmpty);
            SetColor(control.GetBoundedLabel(), ValidLabelColor);
            SetColor(control, ValidBackColor);
        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies clear style.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="hideEmtpy">Flag indicating whether the bounded label should be hidden if message is empty. </param>
        public static void SetMessage(this Control control, string text = null, bool hideEmtpy = true)
        {
            bool isEmpty = (text == null || text == "");
            Label boundedLabel = control.GetBoundedLabel();
            if (boundedLabel != null)
            {
                boundedLabel.Visible = !hideEmtpy || !isEmpty;
                if (boundedLabel.Visible) boundedLabel.Text = text;
            }

            SetColor(boundedLabel, ClearLabelColor);
            SetColor(control, ClearBackColor);
        }

        /// <summary>
        /// Clears bounded label if it exists and aplies clear style.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        public static void ClearMessage(this Control control) { control.SetMessage(null); }

        private static void SetColor(Control control, Color color) { if (control != null) control.BackColor = color; }
        private static void SetColor(Label label, Color color) { if (label != null) label.ForeColor = color; }
    }

    static class ControlBinderExtension
    {
        private static Dictionary<Control, Label> assignments = new Dictionary<Control, Label>();

        /// <summary>
        /// Bounds specified label with targeted control.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <param name="label">Label to be bounded.</param>
        public static void BindLabel(this Control control, Label label) { assignments.Set(control, label); }

        /// <summary>
        /// Removes binding for targeted control.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        public static void UnbindLabel(this Control control) { assignments.Remove(control); }

        /// <summary>
        /// Gets bounded label of targeted control.
        /// </summary>
        /// <param name="control">Targeted control.</param>
        /// <returns></returns>
        public static Label GetBoundedLabel(this Control control)
        {
            Label label;
            assignments.TryGetValue(control, out label);
            return label;
        }

        /// <summary>
        /// Removes invalid bindings.
        /// </summary>
        public static void RefreshBindings()
        {
            foreach (var tb in assignments.Keys)
                if (assignments[tb] == null) assignments.Remove(tb);
        }

    }    
}
