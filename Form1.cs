using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;

namespace keyboard_typer_v1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		public static Random rnd = new Random();
		private static List<string> wordsEn = new List<string>();
		private static List<string> wordsRu = new List<string>();
		private static Typer typer = new Typer();
		private void Form1_Load(object sender, EventArgs e)
		{
			wordsEn = Properties.Resources.english.Split('\n').ToList();
			wordsRu = Properties.Resources.russian.Split('\n').ToList();
			wordLabel.ForeColor = Color.Black;
			wordLabel.Text = "qwerty";
			wordLabel.Location = new Point((this.Size.Width - wordLabel.Width) / 2 - 6, 12);
		}

		private async void startbutton_Click(object sender, EventArgs e)
		{
			if (typer.keyboard.totalSamples > 0)
			{
				gamepanel.Visible = true;
				await type(typer.sequence(wordLabel.Text, currentLang == Language.Russian ? kbLanguage.Russian : kbLanguage.English));
			}
			else
			{
				MessageBox.Show("Please select the result file!", "Error");
			}
		}

		private void selectresultbutton_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Title = "Select result file to use...";
				dialog.Filter = "JSON file|*.json";
				dialog.InitialDirectory = Environment.CurrentDirectory;
				dialog.Multiselect = false;
				dialog.ShowDialog();
				string path = dialog.FileName;
				typer.keyboard = new Keyboard(path);
				if (typer.keyboard.totalSamples > 0)
					MessageBox.Show($"Now using keyboard with {typer.keyboard.totalSamples} samples.", "Read result");
				else
					MessageBox.Show("Please select the correct result file!", "Read result");
			}
		}

		private void settingsbutton_Click(object sender, EventArgs e)
		{
			gamepanel.Visible = false;
		}

		private void languagebutton_Click(object sender, EventArgs e)
		{
			switch (currentLang)
			{
				case Language.English:
					currentLang = Language.Russian;
					languagebutton.Text = "Ru";
					break;
				case Language.Russian:
					currentLang = Language.English;
					languagebutton.Text = "En";
					break;
			}
		}
		private enum Language
		{
			English,
			Russian,
		}
		private static Language currentLang = Language.English;
		private async void wordLabel_Click(object sender, EventArgs e)
		{
			switch (currentLang)
			{
				case Language.English:
					wordLabel.Text = wordsEn[rnd.Next(wordsEn.Count)].ToLower();
					break;
				case Language.Russian:
					wordLabel.Text = wordsRu[rnd.Next(wordsRu.Count)].ToLower();
					break;
				default:
					wordLabel.Text = wordsEn[rnd.Next(wordsEn.Count)].ToLower();
					break;
			}
			wordLabel.Location = new Point((this.Size.Width - wordLabel.Width) / 2 - 6, 12);

			inputBox.Text = String.Empty;
			await type(typer.sequence(wordLabel.Text, currentLang == Language.Russian ? kbLanguage.Russian : kbLanguage.English));
		}
		public async Task type(List<Tuple<char?, int, int>> sequence)
		{
			// 3-rd value, which is downtime, currently not used. In driver though it will be.
			if (gamepanel.Visible)
			{
				foreach (var tup in sequence)
				{
					await Task.Delay(tup.Item2);
					if (tup.Item1 == null)
					{
						if (inputBox.Text.Length > 0)
							inputBox.Text = inputBox.Text[0..^1];
					}
					else
						inputBox.Text += tup.Item1;
				}
			}
		}

		private static int score = 0;
		private static int hit = 0;
		private static int miss = 0;
		private static double precision = 0;
		private void inputBox_TextChanged(object sender, EventArgs e)
		{
			if (inputBox.Text.Length > 0)
				if (inputBox.Text.Length <= wordLabel.Text.Length)
				{
					if (wordLabel.Text[0..(inputBox.Text.Length)] == inputBox.Text)
					{ wordLabel.ForeColor = Color.Black; hit++; }
					else
					{ wordLabel.ForeColor = Color.Crimson; miss++; }
				}
				else
				{
					wordLabel.ForeColor = Color.Crimson;
				}
			else
			{ wordLabel.ForeColor = Color.Black; miss++; }

			if (inputBox.Text.ToLower() == wordLabel.Text)
			{
				wordLabel.ForeColor = Color.Black;
				score++;
				scorelabel.Text = $"Score: {score}";
				wordLabel_Click(new object(), new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1));
			}
			precision = (double)(hit) / (double)(hit + miss);
			precisionlabel.Text = $"Precision: {precision:F2}";
		}
	}
	public class Typer
	{
		public Keyboard keyboard { get; set; }
		public kbLanguage lang { get; set; }
		public kbSpeed speed { get; set; }
		public kbPrecision precision { get; set; }
		public bool caps { get; set; }
		public Typer(Keyboard keyboard, kbLanguage lang, kbSpeed speed, kbPrecision precision, bool caps)
		{
			this.keyboard = keyboard;
			this.lang = lang;
			this.speed = speed;
			this.precision = precision;
			this.caps = caps;
		}
		public Typer(Keyboard keyboard)
		{
			this.keyboard = keyboard;
			this.lang = kbLanguage.English;
			this.speed = kbSpeed.Regular;
			this.precision = kbPrecision.Regular;
			this.caps = false;
		}
		public Typer()
		{
			this.keyboard = new Keyboard();
			this.lang = kbLanguage.English;
			this.speed = kbSpeed.Regular;
			this.precision = kbPrecision.Regular;
			this.caps = false;
		}
		public void setLanguage(kbLanguage lang)
		{
			if (this.lang != lang)
				switchLanguage();
		}
		public void switchLanguage()
		{
			// pretend to click Shift + Alt in driver
			switch (lang)
			{
				case kbLanguage.English:
					lang = kbLanguage.Russian;
					break;
				case kbLanguage.Russian:
					lang = kbLanguage.English;
					break;
				default:
					lang = kbLanguage.English;
					break;
			}
		}
		public void setCaps(bool caps)
		{
			if (this.caps != caps)
				switchCaps();
		}
		public void switchCaps()
		{
			// pretend to release Shift or click CapsLock in driver
			caps = !caps;
		}
		public List<Tuple<char?, int, int>> sequence(string str, kbLanguage language)
		{
			// actual driver func will differ, using this as a showcase
			if (this.lang != language)
			{
				setLanguage(language);
			}
			List<Tuple<char?, int, int>> ret = new List<Tuple<char?, int, int>>();
			for (int i = 0; i < str.Length; i++)
			{
				int index = this.keyboard.keypads.FindIndex(zxc => zxc.chars.Contains(str[i]) && this.lang == kbLanguage.English ? zxc.chars.IndexOf(str[i]) < 2 : zxc.chars.IndexOf(str[i]) >= 2);
				if (index != -1)
				{
					if (this.keyboard.keypads[index].samples > 0)
					{
						if (Form1.rnd.NextDouble() < Math.Max(this.keyboard.keypads[index].precision, 0.1))
						{
							int charindex = 0;
							if (this.lang == kbLanguage.Russian) charindex += 2;
							if (this.caps) charindex += 1;
							char c = this.keyboard.keypads[index].chars[charindex];
							int r;
							if (i > 0)
							{
								if (this.keyboard.keypads[index].reactions.ContainsKey(str[i - 1]))
									r = this.keyboard.keypads[index].reactions[str[i - 1]];
								else
									r = this.keyboard.keypads[index].reaction;
							}
							else
							{
								r = this.keyboard.keypads[index].reaction;
							}
							int d = this.keyboard.keypads[index].downtime;
							ret.Add(new(c, r, d));
						}
						else // make a mistake
						{
							List<double> misses = this.keyboard.keypads[index].mistakes.Values.ToList();
							double mist = Form1.rnd.NextDouble();
							double missSum = 0;
							int missindex = -1;
							for (int j = 0; j < this.keyboard.keypads[index].mistakes.Count; j++)
							{
								double toadd = misses[j];
								if (missSum <= mist && mist <= missSum + toadd)
								{
									missindex = j; break;
								}
								missSum += toadd;
							}
							if (missindex != -1)
							{
								int charindex = 0;
								if (this.lang == kbLanguage.Russian) charindex += 2;
								if (this.caps) charindex += 1;
								char c = this.keyboard.keypads[missindex].chars[charindex];
								int r;
								if (i > 0)
								{
									if (this.keyboard.keypads[missindex].reactions.ContainsKey(str[i - 1]))
										r = this.keyboard.keypads[missindex].reactions[str[i - 1]];
									else
										r = this.keyboard.keypads[missindex].reaction;
								}
								else
								{
									r = this.keyboard.keypads[missindex].reaction;
								}
								int d = this.keyboard.keypads[missindex].downtime;
								ret.Add(new(c, r, d));
							}
							else
							{
								ret.Add(new(str[i], this.keyboard.reaction, this.keyboard.downtime));
							}
							ret.Add(new(null, this.keyboard.reaction, this.keyboard.downtime));
							i--;
						}
					}
					else
					{
						if (Form1.rnd.NextDouble() < Math.Max(this.keyboard.precision, 0.1))
						{
							char c = str[i];
							int r = this.keyboard.reaction;
							int d = this.keyboard.downtime;
							ret.Add(new(c, r, d));
						}
						else // make a mistake
						{
							int rndindex = Form1.rnd.Next(this.keyboard.keypads.Count);
							int charindex = 0;
							if (this.lang == kbLanguage.Russian) charindex += 2;
							if (this.caps) charindex += 1;
							char c = this.keyboard.keypads[rndindex].chars[charindex];
							int r;
							if (i > 0)
							{
								if (this.keyboard.keypads[rndindex].reactions.ContainsKey(str[i - 1]))
									r = this.keyboard.keypads[rndindex].reactions[str[i - 1]];
								else
									r = this.keyboard.keypads[rndindex].reaction;
							}
							else
							{
								r = this.keyboard.keypads[rndindex].reaction;
							}
							int d = this.keyboard.keypads[rndindex].downtime;
							ret.Add(new(c, r, d));
							ret.Add(new(null, this.keyboard.reaction, this.keyboard.downtime));
							i--;
						}
					}
				}
				else
				{
					MessageBox.Show($"Could not find char: {str[i]}");
				}
			}
			return ret;
		}
	}
	public enum kbLanguage
	{
		English,
		Russian
	}
	public enum kbSpeed
	{
		Slow,
		Regular,
		Fast,
		Insane
	}
	public enum kbPrecision
	{
		Low,
		Regular,
		High,
		Ultra
	}
	public class Keyboard
	{
		public int totalSamples { get; set; }
		public double precision { get; set; }
		public int reaction { get; set; }
		public int downtime { get; set; }
		public List<KeyPad> keypads { get; set; }
		public Keyboard(List<KeyPad> keypads)
		{
			this.totalSamples = keypads.Select(zxc => zxc.samples).Sum();
			if (this.totalSamples > 0)
			{
				this.precision = keypads.Select(zxc => zxc.precision * zxc.samples).Sum() / this.totalSamples;
				this.reaction = keypads.Select(zxc => zxc.reaction * zxc.samples).Sum() / this.totalSamples;
				this.downtime = keypads.Select(zxc => zxc.downtime * zxc.samples).Sum() / this.totalSamples;
			}
			else
			{
				this.precision = 0;
				this.reaction = 0;
				this.downtime = 0;
			}
			this.keypads = new List<KeyPad>(keypads);
		}
		public Keyboard(string json, bool isFile = true)
		{
			Keyboard? result;
			if (isFile)
				result = JsonSerializer.Deserialize<Keyboard>(File.ReadAllText(json, Encoding.UTF8), new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
			else
				result = JsonSerializer.Deserialize<Keyboard>(json, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
			if (result == null)
			{
				this.totalSamples = 0;
				this.precision = 0;
				this.reaction = 0;
				this.downtime = 0;
				this.keypads = new List<KeyPad>();
			}
			else
			{
				this.totalSamples = result.keypads.Select(zxc => zxc.samples).Sum();
				if (this.totalSamples > 0)
				{
					this.precision = result.keypads.Select(zxc => zxc.precision * zxc.samples).Sum() / this.totalSamples;
					this.reaction = result.keypads.Select(zxc => zxc.reaction * zxc.samples).Sum() / this.totalSamples;
					this.downtime = result.keypads.Select(zxc => zxc.downtime * zxc.samples).Sum() / this.totalSamples;
				}
				else
				{
					this.precision = 0;
					this.reaction = 0;
					this.downtime = 0;
				}
				this.keypads = result.keypads;
			}
		}
		public Keyboard()
		{
			this.totalSamples = 0;
			this.precision = 0;
			this.reaction = 0;
			this.downtime = 0;
			this.keypads = new List<KeyPad>();
		}
		public string toJson()
		{
			return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
		}
		public Keyboard copy()
		{
			return new Keyboard(this.keypads);
		}
	}
	public class KeyPad
	{
		public char origin { get; }
		public List<char> chars { get; }
		public int value { get; }
		public int samples { get; set; }
		public double precision { get; set; }
		public int reaction { get; set; }
		public int downtime { get; set; }
		public Dictionary<char, int> reactions { get; set; }
		public Dictionary<char, double> mistakes { get; set; }
		public KeyPad(char origin, List<char> chars, int value, int samples, double precision, int reaction, int downtime, Dictionary<char, int> reactions, Dictionary<char, double> mistakes)
		{
			this.origin = origin;
			this.chars = chars;
			this.value = value;
			this.samples = samples;
			this.precision = precision;
			this.reaction = reaction;
			this.downtime = downtime;
			this.reactions = reactions;
			this.mistakes = mistakes;
		}
	}
}