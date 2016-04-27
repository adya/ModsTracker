using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT.Utils
{
    static class TextBoxMessagesExtension
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
        /// <param name="textBox">Targeted text box.</param>
        /// <param name="text">Text of the message.</param>
        public static void SetError(this TextBox textBox, string text = null)
        {
            textBox.SetMessage(text);
            SetColor(textBox.GetBoundedLabel(), ErrorLabelColor);
            SetColor(textBox, ErrorBackColor);
        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies warning style.<para/>
        /// Empty or null messages will be hidden.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        /// <param name="text">Text of the message.</param>
        public static void SetWarning(this TextBox textBox, string text = null)
        {
            textBox.SetMessage(text);
            SetColor(textBox.GetBoundedLabel(), WarningLabelColor);
            SetColor(textBox, WarningBackColor);

        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies valid style.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="hideEmtpy">Flag indicating whether the bounded label should be hidden if message is empty. </param>
        public static void SetValidMessage(this TextBox textBox, string text = null, bool hideEmpty = true)
        {
            textBox.SetMessage(text, hideEmpty);
            SetColor(textBox.GetBoundedLabel(), ValidLabelColor);
            SetColor(textBox, ValidBackColor);
        }

        /// <summary>
        /// Sets message to the bounded label if it exists and applies clear style.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="hideEmtpy">Flag indicating whether the bounded label should be hidden if message is empty. </param>
        public static void SetMessage(this TextBox textBox, string text = null, bool hideEmtpy = true)
        {
            bool isEmpty = (text == null || text == "");
            Label boundedLabel = textBox.GetBoundedLabel();
            if (boundedLabel != null)
            {
                boundedLabel.Visible = !hideEmtpy || !isEmpty;
                if (boundedLabel.Visible) boundedLabel.Text = text;
            }

            SetColor(boundedLabel, ClearLabelColor);
            SetColor(textBox, ClearBackColor);
        }

        /// <summary>
        /// Clears bounded label if it exists and aplies clear style.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        public static void ClearMessage(this TextBox textBox) { textBox.SetMessage(null); }

        private static void SetColor(TextBox textBox, Color color) { if (textBox != null) textBox.BackColor = color; }
        private static void SetColor(Label label, Color color) { if (label != null) label.ForeColor = color; }
    }

    static class TextBoxBinderExtension
    {
        private static Dictionary<TextBox, Label> assignments = new Dictionary<TextBox, Label>();

        /// <summary>
        /// Bounds specified label with targeted text box.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        /// <param name="label">Label to be bounded.</param>
        public static void BindLabel(this TextBox textBox, Label label) { assignments.Set(textBox, label); }

        /// <summary>
        /// Removes binding for targeted text box.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        public static void UnbindLabel(this TextBox textBox) { assignments.Remove(textBox); }

        /// <summary>
        /// Gets bounded label of targeted text box.
        /// </summary>
        /// <param name="textBox">Targeted text box.</param>
        /// <returns></returns>
        public static Label GetBoundedLabel(this TextBox textBox)
        {
            Label label;
            assignments.TryGetValue(textBox, out label);
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
