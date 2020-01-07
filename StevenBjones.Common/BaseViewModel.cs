using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Common
{
	public class BaseViewModel : ObservableClass
	{
		private String _titel;

		public BaseViewModel()
		{
			Titel = "";
		}

		public String Titel
		{
			get { return _titel; }
			set
			{
				if (_titel != value)
				{
					_titel = value;
					OnPropertyChanged("Titel");
				}
			}
		}
	}
}
