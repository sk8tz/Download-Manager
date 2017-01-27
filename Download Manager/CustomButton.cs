using System.Drawing;
using System.Windows.Forms;

namespace Download_Manager
{
    public class CustomButton : Button
    {
        private bool active = true;
        
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;

                if (active)
                {
                    FlatAppearance.BorderColor = FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
                    FlatAppearance.MouseOverBackColor = CUSTOM_GRAYS[0];
                    ForeColor = Color.White;
                }
                else
                {
                    FlatAppearance.BorderColor = ForeColor = CUSTOM_GRAYS[1];
                    FlatAppearance.MouseDownBackColor = FlatAppearance.MouseOverBackColor = Color.Black;
                }
            }
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        private static Color[] CUSTOM_GRAYS = { Color.FromArgb(64, 64, 64), Color.FromArgb(128, 128, 128) };
        private static Font font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);

        public CustomButton()
        {
            Initialize();
        }

        private void Initialize()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Black;
            FlatAppearance.BorderColor = FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            FlatAppearance.MouseOverBackColor = CUSTOM_GRAYS[0];
            Font = font;
            ForeColor = Color.White;
            UseVisualStyleBackColor = false;
        }
    }
}