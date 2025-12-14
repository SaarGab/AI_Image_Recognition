using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDImage = System.Drawing.Image;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // ================= CONFIG =================
        private const string UNSPLASH_ACCESS_KEY = "INPUT YOUR UNSPLASH KEY HERE";
        private const float MATCH_THRESHOLD = 0.30f;

        // ================= STATE =================
        private readonly string modelPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clip_vision.onnx");

        private InferenceSession clipSession;
        private Dictionary<string, float[]> localEmbeddings = new();
        private List<string> onlineUrls = new();
        private int onlineIndex = 0;
        private bool paused = false;

        // ================= INIT =================
        public Form1()
        {
            InitializeComponent();
            LoadClipModel();
        }

        private void LoadClipModel()
        {
            if (!File.Exists(modelPath))
            {
                MessageBox.Show("Missing clip_vision.onnx");
                Close();
                return;
            }
            clipSession = new InferenceSession(modelPath);
        }

        // ================= UI =================
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            txtFolderPath.Text = dlg.SelectedPath;
            localEmbeddings.Clear();

            foreach (var file in Directory.GetFiles(dlg.SelectedPath)
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg")))
            {
                try
                {
                    localEmbeddings[file] = EncodeImage(file);
                }
                catch { }
            }

            lblStatus.Text = $"Loaded {localEmbeddings.Count} local images";
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (localEmbeddings.Count == 0)
            {
                MessageBox.Show("Choose image folder first");
                return;
            }

            onlineUrls = await FetchUnsplash(txtSearch.Text);
            onlineIndex = 0;
            paused = false;

            lblStatus.Text = "Searching...";
            _ = Task.Run(SearchLoop);
        }

        private void btnNextArrow_Click(object sender, EventArgs e)
        {
            paused = false;
            _ = Task.Run(SearchLoop);
        }

        // ================= SEARCH =================
        private async Task SearchLoop()
        {
            using var http = new HttpClient();

            for (; onlineIndex < onlineUrls.Count; onlineIndex++)
            {
                if (paused) return;

                byte[] data = await http.GetByteArrayAsync(onlineUrls[onlineIndex]);
                string temp = Path.GetTempFileName();
                await File.WriteAllBytesAsync(temp, data);

                float[] remoteVec;
                try { remoteVec = EncodeImage(temp); }
                catch { continue; }
                finally { File.Delete(temp); }

                var best = localEmbeddings
                    .Select(k => new
                    {
                        Path = k.Key,
                        Score = Cosine(remoteVec, k.Value)
                    })
                    .OrderByDescending(x => x.Score)
                    .First();

                if (best.Score >= MATCH_THRESHOLD)
                {
                    paused = true;
                    ShowLocalImage(best.Path, best.Score);
                    onlineIndex++;
                    return;
                }
            }

            Invoke(() => lblStatus.Text = "No more matches");
        }

        private void ShowLocalImage(string path, float score)
        {
            Invoke(() =>
            {
                pictureMatchPreview.Image?.Dispose();
                pictureMatchPreview.Image = SDImage.FromFile(path);
                lblStatus.Text = $"Match score: {score:F2}";
            });
        }

        // ================= CLIP =================
        private float[] EncodeImage(string path)
        {
            using var img = SixLabors.ImageSharp.Image.Load<Rgb24>(path);
            img.Mutate(x => x.Resize(224, 224));

            var tensor = new DenseTensor<float>(new[] { 1, 3, 224, 224 });

            img.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < 224; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    for (int x = 0; x < 224; x++)
                    {
                        tensor[0, 0, y, x] = row[x].R / 255f;
                        tensor[0, 1, y, x] = row[x].G / 255f;
                        tensor[0, 2, y, x] = row[x].B / 255f;
                    }
                }
            });

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("pixel_values", tensor)
            };

            using var results = clipSession.Run(inputs);
            return results.First().AsEnumerable<float>().ToArray();
        }

        private float Cosine(float[] a, float[] b)
        {
            float dot = 0, ma = 0, mb = 0;
            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                ma += a[i] * a[i];
                mb += b[i] * b[i];
            }
            return dot / (float)(Math.Sqrt(ma) * Math.Sqrt(mb));
        }

        // ================= UNSPLASH =================
        private async Task<List<string>> FetchUnsplash(string query)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Client-ID {UNSPLASH_ACCESS_KEY}");

            string url =
                $"https://api.unsplash.com/search/photos?query={Uri.EscapeDataString(query)}&per_page=30";

            string json = await http.GetStringAsync(url);
            var root = JObject.Parse(json);

            return root["results"]
                .Select(r => r["urls"]["regular"].ToString())
                .ToList();
        }
    }
}
