using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 列表
{

    /// <summary> 自定义类</summary>
    public class Racer : IComparable<Racer>, IFormattable
    {
        public int Id
        {
            get;
            private set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Contry
        {
            set;
            get;
        }

        public int Wins
        {
            get;
            set;
        }

        public Racer(int id, string firstname, string lastname, string contry, int wins)
        {
            this.Id = id;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Contry = contry;
            this.Wins = wins;
        }

        public Racer(int id, string firstname, string lastname, string contry)
            : this(id, firstname, lastname, contry, 0)
        {

        }

        public override string ToString()
        {

            return string.Format("{0}{1}", FirstName, LastName);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "N";

            switch (format.ToUpper())
            {
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "W":
                    return Wins.ToString();
                case "C":
                    return Contry;
                default:
                    throw new FormatException(string.Format(formatProvider,
                        "Format {0} is not supported", format));

            }


        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }



        public int CompareTo(Racer other)
        {
            if (other == null) return -1;
            int compare=string.Compare(this.LastName,other.LastName);
            if (compare == 0)
                return string.Compare(this.FirstName, other.FirstName);
            return compare;
        }
    }
}
