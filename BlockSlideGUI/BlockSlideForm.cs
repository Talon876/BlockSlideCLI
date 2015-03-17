using System.Windows.Forms;

namespace BlockSlideGUI
{
    public partial class BlockSlideForm : Form
    {
        public BlockSlideForm()
        {
            InitializeComponent();
            levelPanel.ParentForm = this;
            levelPanel.TileSize = 32;
        }
    }
}
