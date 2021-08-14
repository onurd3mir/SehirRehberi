using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithExeption:LogDetail
    {
        public string ExeptionMessage { get; set; }
        public string Date { get; set; }
    }
}
