using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot
{
    /// <summary>
    /// Object model that stores coordinates for buttons that are used to send message in Instagram 
    /// </summary>
    internal class SendMessageClass
    {
        public Tuple<uint,uint> MessagePage { get; set; }
        public Tuple<uint,uint> SearchUsers { get; set; } 
        public Tuple<uint,uint> FirstUser { get; set; }
        public Tuple<uint,uint> Next { get; set; }
    }
}
