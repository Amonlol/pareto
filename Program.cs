using System.Text;

using Microsoft.VisualBasic.FileIO;

namespace Pareto
{
	internal class Program
	{
		public static List<Pareto> MyData;

		static void Main(string[] args)
		{
			Utils.GetData();

			for (int i = 0; i < MyData.Count - 1; i++)
			{
				bool next = false;
				string[] checki = MyData[i].values;

				for (int j = i + 1; j < MyData.Count; j++)
				{
					string[] checkj = MyData[j].values;
					int dominate = Utils.WhoIsDominating(checki, checkj);

					if (dominate == 2)
					{
						MyData[i].dominate = 0;
						next = true;
						continue;
					}
				}

				if (next)
				{
					continue;
				}
				else
				{
					MyData[i].dominate = 1;
				}
			}

			for (int i = 0; i < MyData.Count; i++)
			{
				StringBuilder sb = new StringBuilder();

				for (int j = 0; j < MyData[i].values.Length; j++)
				{
					sb.Append(MyData[i].values[j].ToString());
				}

				sb.Append(' ').Append(MyData[i].dominate);
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
		//Метод загрузки данных множества Парето в кэш
		public static void GetData()
		{
			Program.MyData = new List<Pareto>();
			Console.WriteLine("Введите путь к файлу data.csv:");

			using (TextFieldParser parser = new TextFieldParser(Console.ReadLine() + @"\data.csv"))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");

				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					Program.MyData.Add(new Pareto(fields));
				}
			}
		}

		/// <summary>
		/// Метод проверки доминации двух множеств
		/// </summary>
		/// <param name="a">Первое множество</param>
		/// <param name="b">Второе множество</param>
		/// <returns>
		/// 0 - данные множества равны или несовместимы, 
		/// 1 - первое множество доминирует над вторым, 
		/// 2 - второе множество доминирует над первым
		/// </returns>
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

			return 0;
		}
	}
}