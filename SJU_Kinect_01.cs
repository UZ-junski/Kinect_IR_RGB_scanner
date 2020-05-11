using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using Coding4Fun.Kinect;
using Coding4Fun.Kinect.WinForm;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Color = System.Drawing.Color;
using System.Threading;

namespace Kinect_01
{
	public partial class SJU_Kinect_01 : Form
	{
		private KinectSensor kinectSensor;
		//private int cnt;
		private bool canGetNext;
		private bool canGetColorNext;
		private static object _nextColorLocker;
		private static object _nextLocker;
		private int[][] distance;

		public SJU_Kinect_01()
		{
			InitializeComponent();
			//cnt = 0;
			_nextLocker = new object();
			canGetNext = false;
			_nextColorLocker = new object();
			canGetColorNext = false;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			//cnt = 0;
			canGetNext = false;
			if (btnConnect.Text == "Connect")
			{
				if (KinectSensor.KinectSensors.Count > 0)
				{
					kinectSensor = KinectSensor.KinectSensors[0];
					KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;

					btnConnect.Text = "Disconnect";
					lblId.Text = kinectSensor.DeviceConnectionId;
					//kinectSensor.ColorStream.Enable(ColorImageFormat.RawBayerResolution1280x960Fps12);
					//kinectSensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);

					
					//kinectSensor.ColorFrameReady += KinectSensor_ColorFrameReady;

					kinectSensor.DepthStream.Enable();
					kinectSensor.DepthFrameReady += KinectSensor_DepthFrameReady;

					kinectSensor.ColorStream.Enable();
					kinectSensor.ColorFrameReady += KinectSensor_ColorFrameReady_Final;

					kinectSensor.Start();
				}
			}
			else
			{
				if (kinectSensor != null && kinectSensor.IsRunning)
				{
					kinectSensor.Stop();
					btnConnect.Text = "Connect";
					pictureBox2 = null;
				}
			}
		}

		private void KinectSensor_ColorFrameReady_Final(object sender, ColorImageFrameReadyEventArgs e)
		{
			lock (_nextColorLocker)
			{
				if (!canGetColorNext)
					return;
			}
			using (var frame = e.OpenColorImageFrame())
			{
				if (frame != null)
				{
					//pbxResult.Image = CreateBitmapFromSensor(frame);
					//pictureBox1.Image = CreateBitmapFromSensor2(frame);
					pictureBox2.Image = CreateBitmapFromSensor(frame);
					//kinectSensor.Stop();
					lock (_nextColorLocker)
					{
						canGetColorNext = false;
					}
				}
			}
		}

		private void KinectSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
		{
			lock (_nextLocker)
			{
				if (!canGetNext)
					return;
			}
			using (var frame = e.OpenDepthImageFrame())
			{
				if (frame != null)
				{
					//pbxResult.Image = CreateBitmapFromSensor(frame);
					//pictureBox1.Image = CreateBitmapFromSensor2(frame);
					pictureBox2.Image = CreateBitmapFromSensor3(frame);
					//kinectSensor.Stop();
					lock (_nextLocker)
					{
						canGetNext = false;
					}
				}
			}
		}

		private void KinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
		{
			using (var frame = e.OpenColorImageFrame())
			{
				if (frame != null)
				{
					pictureBox2.Image = CreateBitmapFromSensor(frame);
				}
			}
		}

		private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			lblStatus.Text = kinectSensor.Status.ToString();


		}

		private Bitmap CreateBitmapFromSensor(ColorImageFrame frame)
		{
			var pixelData = new byte[frame.PixelDataLength];
			frame.CopyPixelDataTo(pixelData);
			return pixelData.ToBitmap(frame.Width, frame.Height);

		}

		private Bitmap CreateBitmapFromSensor(DepthImageFrame frame)
		{
			int width = frame.Width;
			int height = frame.Height;
			var pixelData = frame.ToDepthArray();

			//var pixelData = new short[frame.PixelDataLength];
			//frame.CopyPixelDataTo(pixelData);
			var bmp = new Bitmap(frame.Width, frame.Height);

			for (int y = 0; y < height; y++)
			{
				int basePositionY = y * width;
				for (int x = 0; x < width; x++)
				{
					int realOositionY = basePositionY + x;
					byte red = (byte)(pixelData[realOositionY] >> 8);
					byte green = 125;
					byte blue = (byte)(pixelData[realOositionY] & 255); ;
					bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
				}
			}
			return bmp;
		}

		private Bitmap CreateBitmapFromSensor2(DepthImageFrame frame)
		{
			var pixelData = new short[frame.PixelDataLength];
			frame.CopyPixelDataTo(pixelData);
			return pixelData.ToBitmap(frame.Width, frame.Height, 0, Color.White);
		}

		private Bitmap CreateBitmapFromSensor3(DepthImageFrame frame)
		{
			int minDepth = frame.MinDepth;
			int maxDepth = frame.MaxDepth;
			int colorsNumer = maxDepth - minDepth + 10;
			Color[] colors = new Color[colorsNumer];
			for (int colorCnt = 0; colorCnt < colorsNumer; colorCnt++)
			{
				colors[colorCnt] = MapRainbowColor(colorCnt, 0, colorsNumer);
			}
			int width = frame.Width;
			int height = frame.Height;
			var pixelData2 = new DepthImagePixel[frame.PixelDataLength];
			int[][] localDistance = new int[height][];
			frame.CopyDepthImagePixelDataTo(pixelData2);
			var bmp = new Bitmap(frame.Width, frame.Height);
			for (int y = 0; y < height; y++)
			{
				int basePositionY = y * width;
				localDistance[y] = new int[width];
				for (int x = 0; x < width; x++)
				{
					int realOositionY = basePositionY + x;
					if (pixelData2[realOositionY].IsKnownDepth)
					{
						//byte red = (byte)(pixelData2[realOositionY].Depth >> 8);
						//byte green = 125;
						//byte blue = (byte)(pixelData2[realOositionY].Depth & 255); ;
						//bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
						int depthLocal = pixelData2[realOositionY].Depth;
						localDistance[y][x] = depthLocal;

						if (depthLocal > maxDepth)
						{
							depthLocal = maxDepth;
						}
						else if (depthLocal < minDepth)
						{
							depthLocal = minDepth;
						}
						bmp.SetPixel(x, y, colors[depthLocal - minDepth]);
					}
				}
			}
			distance = localDistance;
			return bmp;
		}


		public Bitmap BitmapFromSource(BitmapSource bitmapsource)
		{
			Bitmap bitmap;
			using (var outStream = new MemoryStream())
			{
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapsource));
				enc.Save(outStream);
				bitmap = new Bitmap(outStream);
			}
			return bitmap;
		}

		private const int MaxDepthDistance = 4000;
		private const int MinDepthDistance = 850;
		private const int MaxDepthDistanceOffset = 3150;

		public BitmapSource SliceDepthImage(DepthImageFrame image, int min = 20, int max = 1000)
		{
			int width = image.Width;
			int height = image.Height;

			//var depthFrame = image.Image.Bits;
			short[] rawDepthData = new short[image.PixelDataLength];
			image.CopyPixelDataTo(rawDepthData);

			var pixels = new byte[height * width * 4];

			const int BlueIndex = 0;
			const int GreenIndex = 1;
			const int RedIndex = 2;

			for (int depthIndex = 0, colorIndex = 0;
				depthIndex < rawDepthData.Length && colorIndex < pixels.Length;
				depthIndex++, colorIndex += 4)
			{

				// Calculate the distance represented by the two depth bytes
				int depth = rawDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

				// Map the distance to an intesity that can be represented in RGB
				var intensity = CalculateIntensityFromDistance(depth);

				if (depth > min && depth < max)
				{
					// Apply the intensity to the color channels
					pixels[colorIndex + BlueIndex] = intensity; //blue
					pixels[colorIndex + GreenIndex] = intensity; //green
					pixels[colorIndex + RedIndex] = intensity; //red                    
				}
			}

			return BitmapSource.Create(width, height, 1, 1, PixelFormats.Bgr32, null, pixels, width * 4);
		}

		public byte CalculateIntensityFromDistance(int distance)
		{
			//  will map a distance value to a 0 - 255 range
			// for the purposes of applying the resulting value
			// to RGB pixels.
			int newMax = distance - MinDepthDistance;
			if (newMax > 0)
				return (byte)(255 - (255 * newMax
				/ (MaxDepthDistanceOffset)));
			else
				return (byte)255;
		}

		private void btnGetNext_Click(object sender, EventArgs e)
		{
			lock (_nextLocker)
			{
				canGetNext = true;
			}
		}

		// Map a value to a rainbow color.
		private Color MapRainbowColor(
			float value, float red_value, float blue_value)
		{
			// Convert into a value between 0 and 1023.
			int int_value = (int)(1023 * (value - red_value) /
				(blue_value - red_value));

			// Map different color bands.
			if (int_value < 256)
			{
				// Red to yellow. (255, 0, 0) to (255, 255, 0).
				return Color.FromArgb(255, int_value, 0);
			}
			else if (int_value < 512)
			{
				// Yellow to green. (255, 255, 0) to (0, 255, 0).
				int_value -= 256;
				return Color.FromArgb(255 - int_value, 255, 0);
			}
			else if (int_value < 768)
			{
				// Green to aqua. (0, 255, 0) to (0, 255, 255).
				int_value -= 512;
				return Color.FromArgb(0, 255, int_value);
			}
			else
			{
				// Aqua to blue. (0, 255, 255) to (0, 0, 255).
				int_value -= 768;
				return Color.FromArgb(0, 255 - int_value, 255);
			}
		}

		private void btnSaveDistances_Click(object sender, EventArgs e)
		{
			if (distance != null)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Title = "Save measurements to .csv file";
				saveFileDialog1.DefaultExt = "csv";
				saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
				saveFileDialog1.RestoreDirectory = true;
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					StringBuilder resultToSave = new StringBuilder();
					foreach (var y in distance)
					{
						foreach (var x in y)
						{
							resultToSave.Append($"{x},");
						}
						resultToSave.Remove(resultToSave.Length - 1, 1);
						resultToSave.AppendLine();
					}

					using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
					{
						sw.Write(resultToSave);
					}
				}
			}
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			if (pictureBox2.Image != null)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Title = "Save bitmap to .bmp file";
				saveFileDialog1.DefaultExt = "bmp";
				saveFileDialog1.Filter = "Bitmap files (*.bmp)|*.bmp";
				saveFileDialog1.RestoreDirectory = true;
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);					
				}
			}
		}

		private void btnGetColorNext_Click_1(object sender, EventArgs e)
		{
			lock (_nextColorLocker)
			{
				canGetColorNext = true;
			}
		}
	}
}
