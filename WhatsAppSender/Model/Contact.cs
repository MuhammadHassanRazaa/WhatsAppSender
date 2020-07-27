using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppSender.Model
{
    class Contact
    {
        public Contact(long number)
        {
            this.contactNumber = number;
        }
        private long contactNumber;

        public long ContactNumber
        {
            get { return contactNumber; }   // get method
            set { contactNumber = value; }  // set method
        }


    }
}
