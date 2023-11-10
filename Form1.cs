using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	enum Effects
	{
		Blur = 0,
		Blur2 = 1,
		Sharpen = 2,
		MotionBlur = 3
	}

	enum Algorythm
	{
		Primary,
		Bilinear
	}

	public partial class Form1 : Form
	{
		private Dictionary<Effects, string> effectsDict;

		private readonly Point[] originalPoints = new Point[3];
		private readonly Point[] newPoints = new Point[3];
		private byte pointIndex = 0;
		private readonly Graphics g1;
		private readonly Graphics g2;
		private Algorythm algorythm;

		public Form1()
		{
			InitializeComponent();
			g1 = pictureBox1.CreateGraphics();
			g2 = pictureBox2.CreateGraphics();
			InitializeDictionary();
			InitializeComboBox();

			// By default
			algorythm = Algorythm.Primary;
		}

		private void InitializeDictionary()
		{
			effectsDict = new Dictionary<Effects, string>
			{
				// Remember the order
				{ Effects.Blur, "Сильное размытие" },
				{ Effects.Blur2, "Размытие" },
				{ Effects.Sharpen, "Увеличение резкости" },
				{ Effects.MotionBlur, "Размытие в движении" }
			};
		}

		private void InitializeComboBox()
		{
			foreach (var effect in effectsDict.Values)
			{
				filtersBox.Items.Add(effect);
			}
		}

		private int[,] SelectConvolutionMatrix()
		{
			//Repeat the order
			switch (filtersBox.SelectedIndex)
			{
				case (int)Effects.Blur:
					return new int[,]
					{
						{1, 1, 1 },
						{1, 1, 1 },
						{1, 1, 1 }
					};
				case (int)Effects.Blur2:
					return new int[,]
					{
						{1, 2, 1 },
						{2, 4, 2 },
						{1, 2, 1 }
					};
				case (int)Effects.Sharpen:
					return new int[,]
					{
						{-1, -1, -1 },
						{-1, 9, -1 },
						{-1, -1, -1 }
					};
				case (int)Effects.MotionBlur:
					return new int[,]
					{
						{0, 0, 0 },
						{5, 5, 5 },
						{0, 0, 0 }
					};

				default:
					return new int[,]
					{
						{0, 1, 0 },
						{1, 0, -1 },
						{0, -1, 0 }
					};
			}
		}

		private double CalculateSumOfMatrixElements(int[,] A)
		{
			var S = 0;
			for (int i = 0; i < A.GetLength(0); i++)
				for (int j = 0; j < A.GetLength(1); j++)
					S += A[i, j];

			return S;
		}

		private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (pointIndex == 6) pointIndex = 0;
			// Get the coordinates of the clicked point
			Point clickedPoint = e.Location;

			if (pointIndex < 3)
			{
				originalPoints[pointIndex] = clickedPoint;

				g1.FillEllipse(Brushes.Red, clickedPoint.X - 4, clickedPoint.Y - 4, 8, 8);
				pointIndex++;
			}
			else MessageBox.Show("You already set 3 points");
		}

		private void PictureBox2_MouseClick(object sender, MouseEventArgs e)
		{
			// Get the coordinates of the clicked point
			Point clickedPoint = e.Location;

			if (pointIndex >=3 && pointIndex < 6)
			{
				newPoints[pointIndex % 3] = clickedPoint;

				g2.FillEllipse(Brushes.Red, clickedPoint.X - 5, clickedPoint.Y - 5, 10, 10);
				pointIndex++;
			}
			else if (pointIndex < 3)
			{
				MessageBox.Show("Before set 3 points on initial image!");
			}
			else
			{
				MessageBox.Show("You already set 3 points");

			}
		}

		private void SelectImageButton_Click(object sender, EventArgs e)
		{
			pointIndex = 0;

			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp;"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string imagePath = openFileDialog.FileName;
				try
				{
					// Load the image into the PictureBox
					pictureBox1.Image = new Bitmap(imagePath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error loading the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void TransformButton_Click(object sender, EventArgs e)
		{
			if (pictureBox1.Image == null)
			{
				MessageBox.Show("Before you need select an image");
				return;
			}

			var transformationMatrix = CalculateTransformationMatrix();

			Bitmap img1 = new Bitmap(pictureBox1.Image);
			Bitmap img2;    // Output image

			switch (algorythm)
			{
				case Algorythm.Primary:
					img2 = GetPrimaryTransformedImage(transformationMatrix, img1);
					break;
				case Algorythm.Bilinear:
					img2 = GetBilinearFilteringTransformedImage(transformationMatrix, img1);
					break;
				default:
					throw new Exception("Unexisting algorythm");
			}

			pictureBox2.Image = img2;
		}

		private static Bitmap GetPrimaryTransformedImage(Matrix<double> transformationMatrix, Bitmap img1)
		{
			var img2 = new Bitmap(img1.Width*2, img1.Height*2);

			var inversedTransformationMatrix = transformationMatrix.Inverse();
			for (int x = 0; x < img2.Width; x++)
			{
				for (int y = 0; y < img2.Height; y++)
				{
					// Apply the reverse transformation to coordinates (X', Y')
					Vector<double> outputCoordinates = Vector<double>.Build.DenseOfArray(new double[] { x, y, 1 });
					Vector<double> inputCoordinates = inversedTransformationMatrix * outputCoordinates;

					int inputX = (int)Math.Round(inputCoordinates[0]);
					int inputY = (int)Math.Round(inputCoordinates[1]);

					if (inputX >= 0 && inputX < img1.Width && inputY >= 0 && inputY < img1.Height)
					{
						// Get the source pixel and set it to resulting image
						Color inputPixelColor = img1.GetPixel(inputX, inputY);
						img2.SetPixel(x, y, inputPixelColor);
					}
					else
					{
						// Set black color, if pixel out of image
						img2.SetPixel(x, y, Color.Black);
					}
				}
			}

			return img2;
		}

		private static Bitmap GetBilinearFilteringTransformedImage(Matrix<double> transformationMatrix, Bitmap img1)
		{
			var img2 = new Bitmap(img1.Width*2, img1.Height*2);

			var inversedTransformationMatrix = transformationMatrix.Inverse();


			for (int x = 0; x < img2.Width; x++)
			{
				for (int y = 0; y < img2.Height; y++)
				{
					// Применяем обратное преобразование к координатам (X', Y')
					Vector<double> outputCoordinates = Vector<double>.Build.DenseOfArray(new double[] { x, y, 1 });
					Vector<double> inputCoordinates = inversedTransformationMatrix * outputCoordinates;

					var inputX = inputCoordinates[0];
					var inputY = inputCoordinates[1];

					// Calculating coordinates of 4 neighboring points
					int XUp, XDown;
					int YUp, YDown;
					XDown = (int)Math.Floor(inputX);
					XUp = (int)Math.Ceiling(inputX);
					YDown = (int)Math.Floor(inputY);
					YUp = (int)Math.Ceiling(inputY);

					if (XUp >= 0 && XUp < img1.Width && YUp >= 0 && YUp < img1.Height
						&& XDown >= 0 && XDown < img1.Width && YDown >= 0 && YDown < img1.Height)
					{
						// Calculating color of point
						Color color = GetPixelColor(XUp, XDown, YUp, YDown, inputX, inputY, img1);
						img2.SetPixel(x, y, color);
					}
					else
					{
						img2.SetPixel(x, y, Color.Black);
					}
				}
			}
			return img2;
		}

		// Get pixel in algorythm with bilinear filtering
		private static Color GetPixelColor(int xUp, int xDown, int yUp, int yDown, double x, double y, Bitmap img1)
		{
			Color c11 = img1.GetPixel(xDown, yUp);
			Color c12 = img1.GetPixel(xUp, yUp);
			Color c21 = img1.GetPixel(xDown, yDown);
			Color c22 = img1.GetPixel(xUp, yDown);

			int sumR, sumG, sumB;

			double w1 = (xUp - x);
			double w2 = (x - xDown);
			double w3 = (yUp - y);
			double w4 = (y - yDown);

			sumR = (int)((c21.R * w1 + c22.R * w2) * w3 + (c11.R * w1 + c12.R * w2) * w4);
			sumG = (int)((c21.G * w1 + c22.G * w2) * w3 + (c11.G * w1 + c12.G * w2) * w4);
			sumB = (int)((c21.B * w1 + c22.B * w2) * w3 + (c11.B * w1 + c12.B * w2) * w4);

			if (sumR > 255) sumR = 255;
			if (sumG > 255) sumG = 255;
			if (sumB > 255) sumB = 255;

			return Color.FromArgb(sumR, sumG, sumB);
		}

		private Matrix<double> CalculateTransformationMatrix()
		{
			// Creating matrix from source points
			var matrixA = Matrix<double>.Build.DenseOfArray(new double[,] {
			{ originalPoints[0].X, originalPoints[0].Y, 1 },
			{ originalPoints[1].X, originalPoints[1].Y, 1 },
			{ originalPoints[2].X, originalPoints[2].Y, 1 }
			});

			// Creating matrix from result points
			var matrixB = Matrix<double>.Build.DenseOfArray(new double[,] {
			{ newPoints[0].X, newPoints[0].Y, 1 },
			{ newPoints[1].X, newPoints[1].Y, 1 },
			{ newPoints[2].X, newPoints[2].Y, 1 }
			});

			// Solve system Ax = B 
			var x = matrixA.Solve(matrixB);

			var transformationMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
				{ x[0, 0], x[1, 0], x[2, 0] },
				{ x[0, 1], x[1, 1], x[2, 1] },
				{ x[0, 2], x[1, 2], x[2, 2] },
			});

			return transformationMatrix;
		}

		private void ApplyFilterButton_Click(object sender, EventArgs e)
		{
			// Convolution matrix
			var A = SelectConvolutionMatrix();

			var img1 = new Bitmap(pictureBox1.Image);
			Bitmap img2;
			double S = CalculateSumOfMatrixElements(A); // Sum of matrix elements

			img2 = GetFilteredImage(A, img1, S);

			pictureBox2.Image = img2;
		}

		private static Bitmap GetFilteredImage(int[,] A, Bitmap img1, double S)
		{
			int sumR, sumG, sumB;
			Color color;

			var img2 = new Bitmap(img1.Width, img1.Height);

			for (int x = 1; x < img2.Width - 1; x++)
			{
				for (int y = 1; y < img2.Height - 1; y++)
				{
					sumR = sumG = sumB = 0;
					for (int k1 = x - 1; k1 <= x + 1; k1++)
						for (int k2 = y - 1; k2 <= y + 1; k2++)
						{
							color = img1.GetPixel(k1, k2);

							sumR += color.R * A[k1 - x + 1, k2 - y + 1];
							sumB += color.B * A[k1 - x + 1, k2 - y + 1];
							sumG += color.G * A[k1 - x + 1, k2 - y + 1];
						}

					sumR = (int)Math.Round(sumR / S);
					if (sumR < 0) sumR = 0; if (sumR > 255) sumR = 255;

					sumG = (int)Math.Round(sumG / S);
					if (sumG < 0) sumG = 0; if (sumG > 255) sumG = 255;

					sumB = (int)Math.Round(sumB / S);
					if (sumB < 0) sumB = 0; if (sumB > 255) sumB = 255;

					Color newColor = Color.FromArgb(sumR, sumG, sumB);
					img2.SetPixel(x, y, newColor);
				}
			}

			return img2;
		}

		private void PrimaryAlgorythmButtom_CheckedChanged(object sender, EventArgs e)
		{
			algorythm = Algorythm.Primary;
		}

		private void BilinearAlgorythmButton_CheckedChanged(object sender, EventArgs e)
		{
			algorythm = Algorythm.Bilinear;
		}
	}
}