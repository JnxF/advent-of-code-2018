using System;
using System.Collections.Generic;
using System.Text;

namespace Day4
{
    class Information
    {
        public DateTime Date { get; set; }
        public InformationType Type { get; set; }
        public int? GuardId { get; set; }
        public int Month { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }

        public override string ToString()
        {
            return $"Date = {Date}, Type = {Type} " + (GuardId == null ? "" : ", GuardId = " + GuardId);
        }
    }
}
