using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoIndexing.Application;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;

namespace Features_Finder {
    public partial class Form1 : Form, IMainForm {
        private readonly IVideoIndexingEngine engine;

        public Form1(IVideoIndexingEngine engine) {
            InitializeComponent();

            this.engine = engine;
        }

        private async void ProcessBtn_Click(object sender, EventArgs e) {
            using FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK) {
                Concept concept = engine.BuildConceptFromDirectory(dialog.SelectedPath);

                textBox_concept.Text = concept.Id.ToString();
                textBox_video.Text = concept.Videos
                    .Select(video => video.ToString())
                    .Aggregate((one, other) => $"{one} {other}");
                textBox_frames.Text = concept.Videos.Count
                    .ToString();

                Invalidate();

                processBtn.Enabled = false;
                var result = await Task.Factory.StartNew(() => engine.FindSimilarVideos(GetFramingMode(), concept));
                processBtn.Enabled = true;

                SetGridViewData(result);
            }
        }

        private void SetGridViewData<T>(IEnumerable<T> data) {
            if (data != null) {
                searchResultGridView.DataSource = new BindingList<T>(data.ToList());
            }
        }

        private EnumFramingMode GetFramingMode() {
            return radioButton_1fps.Checked ? EnumFramingMode.OneFPS : radioButton_4fps.Checked ? EnumFramingMode.FourFPS : EnumFramingMode.OneFPS;
        }
    }
}
