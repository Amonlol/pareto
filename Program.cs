using System.Text;

using Microsoft.VisualBasic.FileIO;

namespace Pareto
{
	internal class Program
	{
		const string PATH = @"C:\Users\Admin\Desktop\Институт\Судаков\pareto\data.csv";
		static List<Pareto> MyData;

		static void Main(string[] args)
		{
			MyData = new List<Pareto>();

			using (TextFieldParser parser = new TextFieldParser(PATH))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				while (!parser.EndOfData)
				{
					//Process row
					string[] fields = parser.ReadFields();
					MyData.Add(new Pareto(fields));
				}
			}

			for (int i = 0; i < MyData.Count - 1; i++)
			{
				string[] checki = MyData[i].values;

				for (int j = i + 1; j < MyData.Count; j++)
				{
					string[] checkj = MyData[j].values;
					int dominate = Utils.WhoIsDominating(checki, checkj);

					if (dominate == 1)
					{
						MyData[i].dominate = 1;
					}
					else if (dominate == 2)
					{
						MyData[i].dominate = 0;
					}
				}
			}

			for (int i = 0; i < MyData.Count; i++)
			{
				StringBuilder sb = new StringBuilder();
				//Console.WriteLine(String.Format(MyData[i].values.ToString(), MyData[i].dominate));
				for (int j = 0; j < MyData[i].values.Length; j++)
				{
					sb.Append(MyData[i].values[j].ToString());
				}
				sb.Append(" ").Append(MyData[i].dominate);
				Console.WriteLine(sb.ToString());
			}
		}
	}

	public class Pareto
	{
		public string[] values;
		public int dominate;

		public Pareto(string[] values)
		{
			this.values = values;
		}
	}

	public static class Utils
	{
		public static int WhoIsDominating(string[] a, string[] b)
		{
			int check = 0;
			for (int i = 0; i < a.Length; i++)
			{
				int t = check;
				check += (Convert.ToInt32(a[i]) - Convert.ToInt32(b[i]));

				if (Math.Abs(check) <= Math.Abs(t))
				{
					return 0;
				}
			}

			if (check > 0)
			{
				return 1;
			}
			else if (check < 0)
			{
				return 2;
			}
			else
			{
				return 0;
			}
		}
	}
}