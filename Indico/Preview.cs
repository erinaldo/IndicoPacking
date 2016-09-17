using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndicoPacking
{
    public partial class Preview : Form
    {
        public string ImageURI 
        { 
            set
            {
                var request = WebRequest.Create(value);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    this.picPreview.Image = Bitmap.FromStream(stream);
                }
            }
        }

        public Preview()
        {
            InitializeComponent();
            this.picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void picPreview_Click(object sender, EventArgs e)
        {

        }
    }
}
