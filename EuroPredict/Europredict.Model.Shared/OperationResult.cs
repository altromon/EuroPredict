using System;
using System.Collections;
using System.Collections.Generic;

namespace EuroPredict.Model.Shared
{
    public class OperationResult
    {
        public enum OperationStatus
        {
            OK,
            WARNING,
            ERROR,
            EXCEPTION
        }

        public OperationStatus Status { get; set; }
        public IList<string> Messages { get; set; }

        public OperationResult()
        {
            Status = OperationStatus.OK;
            Messages = new List<string>();
        }
    }
}
