/* 

Filename:   LocalRecyclers	
Author:     Aaron Pidd
Date:       09 May 2022
Version:    1.0
Notes:      internal class Recycler
            public ToString() method
            implemented CompareTo() method
            ToCSVString Method
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalRecyclers
{
    /// <summary>
    /// Recycler class
    /// Public properties
    /// string Name { get; set; }
    /// string Address { get; set; }
    /// string Phone { get; set; }
    /// string Website { get; set; }
    /// string Recycles { get; set; }
    /// </summary>
    internal class Recycler : IComparable<Recycler>
    {
        // public properties
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Recycles { get; set; }

        // public parameterised constructor method
        /// <summary>
        /// public parameterised constructor method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="website"></param>
        /// <param name="recycles"></param>
        public Recycler (string name, string address, string phone, string website, string recycles)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Website = website;
            Recycles = recycles;    
        } // END recycler constructor

        // public ToString() method
        // use to display in the application
        /// <summary>
        /// ToString Method
        /// string recyclerString = Name + "\t" + Address + "\t" + Phone + "\t" + Website + "\t" + Recycles;
        /// </summary>
        /// <returns> recyclerString </returns>
        public override string ToString()
        {
            string recyclerString = Name + "\t" + Address + "\t" + Phone + "\t" + Website + "\t" + Recycles;
            return recyclerString;
        } // END ToString() method

        // implemented CompareTo() method
        // used to sort by last name
        /// <summary>
        /// Implemented CompareTo() method
        /// Used to sort by last name
        /// return Name.CompareTo(other.Name);
        /// 0 == strings are the same
        /// 1 == Name > other.Name
        /// -1 == Name < other.Name
        /// </summary>
        /// <param name="other"></param>
        /// <returns> Name.CompareTo(other.Name) </returns>
        public int CompareTo(Recycler other)
        {
            return Name.CompareTo(other.Name);
            // 0 == strings are the same
            // 1 == Name > other.Name
            //-1 == Name < other.Name

        } // END CompareTo Method

        // returns a CSV string version of the enrolment instance data
        // used to write to the external file for saving
        /// <summary>
        /// ToCSVString Method
        /// return Name + "," + Address + "," + Phone + "," + Website + "," + Recycles;
        /// </summary>
        /// <returns> Name, Address, Phone, Website, Recycles </returns>
        public string ToCSVString()
        {
            return Name + "," + Address + "," + Phone + "," + Website + "," + Recycles;
        } // END ToCSVString() method

    } // END class Recycler

} // END namespace LocalRecyclers
