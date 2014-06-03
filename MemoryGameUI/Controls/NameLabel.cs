namespace MemoryGameUI.Controls
{
    using System.Drawing;
    using System.Windows.Forms;

    public sealed class NameLabel : Label
    {
        #region Constructors and Destructors

        public NameLabel()
        {
            Size = new Size(100, 20);
            AutoSize = true;
            Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Margin = new Padding(0, 10, 5, 10);
        }

        #endregion
    }
}